using System;
using System.Drawing;
using System.IO;
using DisplayService.Internals;
using Waveshare;
using Waveshare.Devices;
using Waveshare.Interfaces;

namespace DisplayService.Services
{
    public class WaveShareDisplay : IDisplay
    {
        private readonly IEPaperDisplay ePaperDisplay;
        private readonly SyncHelper syncHelper = new SyncHelper();

        private bool disposed;

        public WaveShareDisplay(string displayType)
        {
            Console.WriteLine($"WaveShareDisplay creating for display type {displayType}");

            var displayTypeEnum = (EPaperDisplayType)Enum.Parse(typeof(EPaperDisplayType), displayType);
            this.ePaperDisplay = EPaperDisplay.Create(displayTypeEnum);
            Console.WriteLine($"WaveShareDisplay created for display type {this.ePaperDisplay}");
        }

        public int Width => this.ePaperDisplay.Width;

        public int Height => this.ePaperDisplay.Height;

        public void Clear()
        {
            Console.WriteLine($"WaveShareDisplay: Clear");
            this.ePaperDisplay.PowerOn();
            this.ePaperDisplay.Clear();
            this.ePaperDisplay.PowerOff();
        }

        public void DisplayImage(Stream bitmapStream)
        {
            this.DisplayImage(new Bitmap(bitmapStream));
        }

        private void DisplayImage(Bitmap bitmap)
        {
            this.syncHelper.RunOnce(() =>
            {
                Console.WriteLine($"WaveShareDisplay: DisplayImage");
                this.ePaperDisplay.PowerOn();
                this.ePaperDisplay.DisplayImage(bitmap);
                this.ePaperDisplay.PowerOff();
            });
        }

        ///private void Renderer_ScreenChanged(object sender, EventArgs e)
        ///{
        ///    if (this.ePaperDisplay == null)
        ///    {
        ///        this.lastUpdated = DateTime.UtcNow;
        ///    }
        ///    else
        ///    {
        ///        var args = (ScreenChangedEventArgs)e;
        ///        var lockSuccess = false;
        ///        try
        ///        {
        ///            Monitor.TryEnter(this.updatelock, this.updateLockTimeout, ref lockSuccess);
        ///            if (!lockSuccess)
        ///            {
        ///                throw new TimeoutException("A wait for update lock timed out.");
        ///            }
///
        ///            if (!this.updating)
        ///            {
        ///                if (args.Delay)
        ///                {
        ///                    this.delayed = true;
        ///                }
        ///                else
        ///                {
        ///                    var x2 = args.Width + args.X - 1;
        ///                    var y2 = args.Height + args.Y - 1;
        ///                    this.sectionX1 = Math.Min(args.X, this.sectionX1);
        ///                    this.sectionY1 = Math.Min(args.Y, this.sectionY1);
        ///                    this.sectionX2 = Math.Max(x2, this.sectionX2);
        ///                    this.sectionY2 = Math.Max(y2, this.sectionY2);
        ///                    this.updating = true;
        ///                    //_updateTimer.Interval = 5000;
        ///                }
        ///            }
        ///        }
        ///        catch (Exception ex)
        ///        {
        ///            Console.WriteLine("An exception occurred setting update timer. " + ex.Message);
        ///        }
        ///        finally
        ///        {
        ///            if (lockSuccess)
        ///            {
        ///                Monitor.Exit(this.updatelock);
        ///            }
        ///        }
        ///    }
        ///}

        // private void UpdateScreen(object source, TimerElapsedEventArgs e)
        // {
        //     if (this.delayed || this.updating)
        //     {
        //         var lockSuccess = false;
        //         try
        //         {
        //             Monitor.TryEnter(this.ePaperDisplay, this.displayLockTimeout, ref lockSuccess);
        //             if (!lockSuccess)
        //             {
        //                 throw new TimeoutException("A wait for display lock timed out.");
        //             }
        //
        //             Stream memStream = null;
        //             try
        //             {
        //                 lockSuccess = false;
        //                 Monitor.TryEnter(this.updatelock, this.updateLockTimeout, ref lockSuccess);
        //                 if (!lockSuccess)
        //                 {
        //                     throw new TimeoutException("A wait for update lock timed out.");
        //                 }
        //
        //                 //memStream = _renderer.GetScreen();
        //                 this.updating = false;
        //                 this.delayed = false;
        //             }
        //             catch (Exception ex)
        //             {
        //                 Console.WriteLine("An exception occurred trying to get screen to update. " + ex.Message);
        //             }
        //             finally
        //             {
        //                 if (lockSuccess)
        //                 {
        //                     Monitor.Exit(this.updatelock);
        //                 }
        //             }
        //
        //             // Implement partial update and factor in isportrait ** TODO **
        //             this.ePaperDisplay.PowerOn();
        //             this.ePaperDisplay.DisplayImage(new(memStream));
        //             this.ePaperDisplay.PowerOff();
        //             memStream.Close();
        //             memStream.Dispose();
        //             this.lastUpdated = DateTime.UtcNow;
        //         }
        //         catch (Exception ex)
        //         {
        //             Console.WriteLine("An exception occurred trying to update display. " + ex.Message);
        //         }
        //         finally
        //         {
        //             if (lockSuccess)
        //             {
        //                 Monitor.Exit(this.ePaperDisplay);
        //             }
        //         }
        //     }
        // }

        // private void RefreshScreen(object source, TimerElapsedEventArgs e)
        // {
        //     // Screen flushing.  Cycle three times:
        //     //   Color: black, white, white, black, white, white
        //     //   Monochrome: black, white
        //     var lockSuccess = false;
        //     try
        //     {
        //         Monitor.TryEnter(this.ePaperDisplay, this.displayLockTimeout, ref lockSuccess);
        //         if (!lockSuccess)
        //         {
        //             throw new TimeoutException("A wait for display lock timed out.");
        //         }
        //
        //         this.ePaperDisplay.PowerOn();
        //         for (var i = 0; i < 6; i++)
        //         {
        //             Console.WriteLine("Flushing display");
        //             this.ePaperDisplay.ClearBlack();
        //             this.ePaperDisplay.Clear();
        //         }
        //
        //         this.ePaperDisplay.PowerOff();
        //         Console.WriteLine("Finished flushing display");
        //         //_renderer.Refresh();
        //     }
        //     catch (Exception ex)
        //     {
        //         Console.WriteLine("An exception occurred flushing display. " + ex.Message);
        //     }
        //     finally
        //     {
        //         if (lockSuccess)
        //         {
        //             Monitor.Exit(this.ePaperDisplay);
        //         }
        //     }
        // }

        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                if (this.ePaperDisplay != null)
                {
                    this.ePaperDisplay.Sleep();
                    this.ePaperDisplay.Dispose();
                }
            }

            this.disposed = true;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }


        ~WaveShareDisplay() => this.Dispose(false);
    }
}
