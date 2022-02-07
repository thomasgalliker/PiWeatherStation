
using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Timers;
using DisplayService.Settings;
using Waveshare;
using Waveshare.Devices;
using Waveshare.Interfaces;

namespace DisplayService.Services
{
    public class WaveShareDisplay : IDisplay
    {

        private static readonly TimerService _updateTimer;
        private static readonly TimerService _refreshTimer;

        private readonly IEPaperDisplay ePaperDisplay;
        private readonly int _displayLockTimeout = 60000;
        private readonly object _updatelock = new();
        private readonly int _updateLockTimeout = 60000;

        private int _sectionX1 = int.MaxValue;
        private int _sectionY1 = int.MaxValue;
        private int _sectionX2 = int.MinValue;
        private int _sectionY2 = int.MinValue;
        private DateTime _lastUpdated;
        private bool _updating = false;
        private bool _delayed = false;
        private bool _disposed = false;

        public WaveShareDisplay(string displayType)
        {
            Console.WriteLine($"WaveShareDisplay creating for display type {displayType}");

            var displayTypeEnum = (EPaperDisplayType)Enum.Parse(typeof(EPaperDisplayType), displayType);
            this.ePaperDisplay = EPaperDisplay.Create(displayTypeEnum);
            Console.WriteLine($"WaveShareDisplay created for display type {this.ePaperDisplay}");
        }

        public DateTime LastUpdated => this._lastUpdated;

        public int Width => this.ePaperDisplay.Width;

        public int Height => this.ePaperDisplay.Height;

        public void Clear()
        {
            this.ePaperDisplay.PowerOn();
            this.ePaperDisplay.Clear();
            this.ePaperDisplay.PowerOff();
        }

        public void DisplayImage(Stream bitmapStream)
        {
            this.DisplayImage(new Bitmap(bitmapStream));
        }

        public void DisplayImage(Bitmap bitmap)
        {
            this.ePaperDisplay.PowerOn();
            this.ePaperDisplay.DisplayImage(bitmap);
            this.ePaperDisplay.PowerOff();
        }

        private void Create(IRenderService renderer, RenderSettings settings)
        {
            //if (_display != null)
            //{
            //    settings.Resize(_display.Width, _display.Height);
            //    _display.Clear();
            //    _display.PowerOff();
            //}

            //_renderer = renderer;
            //renderer.ScreenChanged += Renderer_ScreenChanged;
            //_updateTimer = new ()
            //{
            //    TargetMillisecond = 300000,
            //    ToleranceMillisecond = 5000,
            //    Enabled = true
            //};
            //_updateTimer.Elapsed += UpdateScreen;
            //_lastUpdated = new (1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            //if (_display != null && _refreshTime != default)
            //{
            //    _refreshTimer = new ()
            //    {
            //        TargetTime = _refreshTime,
            //        ToleranceMillisecond = 180000,
            //        Enabled = true
            //    };
            //    _refreshTimer.Elapsed += RefreshScreen;
            //}
        }

        private void Renderer_ScreenChanged(object sender, EventArgs e)
        {
            if (this.ePaperDisplay == null)
            {
                this._lastUpdated = DateTime.UtcNow;
            }
            else
            {
                var args = (ScreenChangedEventArgs)e;
                var lockSuccess = false;
                try
                {
                    Monitor.TryEnter(this._updatelock, this._updateLockTimeout, ref lockSuccess);
                    if (!lockSuccess)
                    {
                        throw new TimeoutException("A wait for update lock timed out.");
                    }

                    if (!this._updating)
                    {
                        if (args.Delay)
                        {
                            this._delayed = true;
                        }
                        else
                        {
                            var x2 = args.Width + args.X - 1;
                            var y2 = args.Height + args.Y - 1;
                            this._sectionX1 = Math.Min(args.X, this._sectionX1);
                            this._sectionY1 = Math.Min(args.Y, this._sectionY1);
                            this._sectionX2 = Math.Max(x2, this._sectionX2);
                            this._sectionY2 = Math.Max(y2, this._sectionY2);
                            this._updating = true;
                            //_updateTimer.Interval = 5000;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An exception occurred setting update timer. " + ex.Message);
                }
                finally
                {
                    if (lockSuccess)
                    {
                        Monitor.Exit(this._updatelock);
                    }
                }
            }
        }

        private void UpdateScreen(object source, ElapsedEventArgs e)
        {
            if (this._delayed || this._updating)
            {
                var lockSuccess = false;
                try
                {
                    Monitor.TryEnter(this.ePaperDisplay, this._displayLockTimeout, ref lockSuccess);
                    if (!lockSuccess)
                    {
                        throw new TimeoutException("A wait for display lock timed out.");
                    }

                    Stream memStream = null;
                    try
                    {
                        lockSuccess = false;
                        Monitor.TryEnter(this._updatelock, this._updateLockTimeout, ref lockSuccess);
                        if (!lockSuccess)
                        {
                            throw new TimeoutException("A wait for update lock timed out.");
                        }

                        //memStream = _renderer.GetScreen();
                        this._updating = false;
                        this._delayed = false;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("An exception occurred trying to get screen to update. " + ex.Message);
                    }
                    finally
                    {
                        if (lockSuccess)
                        {
                            Monitor.Exit(this._updatelock);
                        }
                    }

                    // Implement partial update and factor in isportrait ** TODO **
                    this.ePaperDisplay.PowerOn();
                    this.ePaperDisplay.DisplayImage(new(memStream));
                    this.ePaperDisplay.PowerOff();
                    memStream.Close();
                    memStream.Dispose();
                    this._lastUpdated = DateTime.UtcNow;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An exception occurred trying to update display. " + ex.Message);
                }
                finally
                {
                    if (lockSuccess)
                    {
                        Monitor.Exit(this.ePaperDisplay);
                    }
                }
            }
        }

        private void RefreshScreen(object source, ElapsedEventArgs e)
        {
            // Screen flushing.  Cycle three times:
            //   Color: black, white, white, black, white, white
            //   Monochrome: black, white
            var lockSuccess = false;
            try
            {
                Monitor.TryEnter(this.ePaperDisplay, this._displayLockTimeout, ref lockSuccess);
                if (!lockSuccess)
                {
                    throw new TimeoutException("A wait for display lock timed out.");
                }

                this.ePaperDisplay.PowerOn();
                for (var i = 0; i < 6; i++)
                {
                    Console.WriteLine("Flushing display");
                    this.ePaperDisplay.ClearBlack();
                    this.ePaperDisplay.Clear();
                }

                this.ePaperDisplay.PowerOff();
                Console.WriteLine("Finished flushing display");
                //_renderer.Refresh();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An exception occurred flushing display. " + ex.Message);
            }
            finally
            {
                if (lockSuccess)
                {
                    Monitor.Exit(this.ePaperDisplay);
                }
            }
        }


        protected virtual void Dispose(bool disposing)
        {
            if (this._disposed)
            {
                return;
            }

            if (disposing)
            {
                if (this.ePaperDisplay != null)
                {
                    if (_updateTimer != null)
                    {
                        _updateTimer.Elapsed -= this.UpdateScreen;
                        _updateTimer.Enabled = false;
                        _updateTimer.Dispose();
                    }

                    if (_refreshTimer != null)
                    {
                        _refreshTimer.Elapsed -= this.RefreshScreen;
                        _refreshTimer.Enabled = false;
                        _refreshTimer.Dispose();
                    }

                    this.ePaperDisplay.Sleep();
                    this.ePaperDisplay.Dispose();
                }
            }

            this._disposed = true;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }


        ~WaveShareDisplay() => this.Dispose(false);
    }
}
