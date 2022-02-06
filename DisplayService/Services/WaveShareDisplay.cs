
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

        private static TimerService _updateTimer;
        private static TimerService _refreshTimer;

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
            var displayTypeEnum = (EPaperDisplayType)Enum.Parse(typeof(EPaperDisplayType), displayType);
            ePaperDisplay = EPaperDisplay.Create(displayTypeEnum);
        }

        public DateTime LastUpdated => _lastUpdated;

        public int Width => this.ePaperDisplay.Width;

        public int Height => this.ePaperDisplay.Height;

        public void Clear()
        {
            ePaperDisplay.PowerOn();
            ePaperDisplay.Clear();
            ePaperDisplay.PowerOff();
        }

        public void DisplayImage(Stream bitmapStream)
        {
            DisplayImage(new Bitmap(bitmapStream));
        }

        public void DisplayImage(Bitmap bitmap)
        {
            ePaperDisplay.PowerOn();
            ePaperDisplay.DisplayImage(bitmap);
            ePaperDisplay.PowerOff();
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
            if (ePaperDisplay == null)
            {
                _lastUpdated = DateTime.UtcNow;
            }
            else
            {
                ScreenChangedEventArgs args = (ScreenChangedEventArgs)e;
                bool lockSuccess = false;
                try
                {
                    Monitor.TryEnter(_updatelock, _updateLockTimeout, ref lockSuccess);
                    if (!lockSuccess)
                    {
                        throw new TimeoutException("A wait for update lock timed out.");
                    }

                    if (!_updating)
                    {
                        if (args.Delay)
                        {
                            _delayed = true;
                        }
                        else
                        {
                            int x2 = args.Width + args.X - 1;
                            int y2 = args.Height + args.Y - 1;
                            _sectionX1 = Math.Min(args.X, _sectionX1);
                            _sectionY1 = Math.Min(args.Y, _sectionY1);
                            _sectionX2 = Math.Max(x2, _sectionX2);
                            _sectionY2 = Math.Max(y2, _sectionY2);
                            _updating = true;
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
                        Monitor.Exit(_updatelock);
                    }
                }
            }
        }

        private void UpdateScreen(object source, ElapsedEventArgs e)
        {
            if (_delayed || _updating)
            {
                bool lockSuccess = false;
                try
                {
                    Monitor.TryEnter(ePaperDisplay, _displayLockTimeout, ref lockSuccess);
                    if (!lockSuccess)
                    {
                        throw new TimeoutException("A wait for display lock timed out.");
                    }

                    Stream memStream = null;
                    try
                    {
                        lockSuccess = false;
                        Monitor.TryEnter(_updatelock, _updateLockTimeout, ref lockSuccess);
                        if (!lockSuccess)
                        {
                            throw new TimeoutException("A wait for update lock timed out.");
                        }

                        //memStream = _renderer.GetScreen();
                        _updating = false;
                        _delayed = false;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("An exception occurred trying to get screen to update. " + ex.Message);
                    }
                    finally
                    {
                        if (lockSuccess)
                        {
                            Monitor.Exit(_updatelock);
                        }
                    }

                    // Implement partial update and factor in isportrait ** TODO **
                    ePaperDisplay.PowerOn();
                    ePaperDisplay.DisplayImage(new(memStream));
                    ePaperDisplay.PowerOff();
                    memStream.Close();
                    memStream.Dispose();
                    _lastUpdated = DateTime.UtcNow;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An exception occurred trying to update display. " + ex.Message);
                }
                finally
                {
                    if (lockSuccess)
                    {
                        Monitor.Exit(ePaperDisplay);
                    }
                }
            }
        }

        private void RefreshScreen(object source, ElapsedEventArgs e)
        {
            // Screen flushing.  Cycle three times:
            //   Color: black, white, white, black, white, white
            //   Monochrome: black, white
            bool lockSuccess = false;
            try
            {
                Monitor.TryEnter(ePaperDisplay, _displayLockTimeout, ref lockSuccess);
                if (!lockSuccess)
                {
                    throw new TimeoutException("A wait for display lock timed out.");
                }

                ePaperDisplay.PowerOn();
                for (int i = 0; i < 6; i++)
                {
                    Console.WriteLine("Flushing display");
                    ePaperDisplay.ClearBlack();
                    ePaperDisplay.Clear();
                }

                ePaperDisplay.PowerOff();
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
                    Monitor.Exit(ePaperDisplay);
                }
            }
        }


        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                if (ePaperDisplay != null)
                {
                    if (_updateTimer != null)
                    {
                        _updateTimer.Elapsed -= UpdateScreen;
                        _updateTimer.Enabled = false;
                        _updateTimer.Dispose();
                    }

                    if (_refreshTimer != null)
                    {
                        _refreshTimer.Elapsed -= RefreshScreen;
                        _refreshTimer.Enabled = false;
                        _refreshTimer.Dispose();
                    }

                    ePaperDisplay.Sleep();
                    ePaperDisplay.Dispose();
                }
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        ~WaveShareDisplay() => Dispose(false);
    }
}
