
using System;
using System.Timers;

namespace DisplayService.Services
{
    public class TimerService : ITimerService
    {
        private readonly Timer timer;

        private DateTime dueTime;
        private bool disposed = false;

        public event EventHandler<TimerElapsedEventArgs> Elapsed;
        //{
        //    add { timer.Elapsed += value; }
        //    remove { timer.Elapsed -= value; }
        //}

        public TimerService()
        {
            this.timer = new Timer();
            this.timer.Elapsed += this.ElapsedInternal;

            this.AutoReset = true;
            this.TargetTime = default;
            this.TargetMillisecond = 0;
            this.ToleranceMillisecond = 0;
        }

        public TimeSpan TargetTime { get; set; }

        public int TargetMillisecond { get; set; }

        public int ToleranceMillisecond { get; set; }

        public bool AutoReset
        {
            get => this.timer.AutoReset;
            set => this.timer.AutoReset = value;
        }

        public bool Enabled
        {
            get
            {
                return this.timer.Enabled;
            }
            set
            {
                if (value)
                {
                    //SetInterval();
                }

                this.timer.Enabled = value;
            }
        }

        public TimeSpan Interval
        {
            get => TimeSpan.FromMilliseconds(this.timer.Interval);
            set
            {
                this.timer.Interval = value.TotalMilliseconds;
                if (value == TimeSpan.Zero)
                {
                    this.Stop();
                }
            }
        }

        public double TimeLeft => (this.dueTime - DateTime.Now).TotalMilliseconds;

        public void Start()
        {
            //SetInterval();
            this.timer.Start();
        }

        public void Stop()
        {
            this.Enabled = false;
            this.timer.Stop();
        }

        private void SetInterval()
        {
            var now = DateTime.Now;
            int next;
            // Calculate how long to wait for the next interval, shooting for the target
            // millisecond mark but not less than tolerance millisecond due to display update time.
            if (this.TargetTime == default)
            {
                var targetminute = 0;
                var targetsecond = this.TargetMillisecond;
                if (this.TargetMillisecond > 59999)
                {
                    targetminute = this.TargetMillisecond / 60000;
                    targetsecond = this.TargetMillisecond % 60000;
                }

                next = targetsecond - (now.Second * 1000 + now.Millisecond);
                if (next <= this.ToleranceMillisecond)
                {
                    next += 60000;
                }

                if (targetminute > 0)
                {
                    next += (targetminute - (now.AddMilliseconds(next).Minute % targetminute)) * 60000;
                }
            }

            // Calculate how long to wait for the next interval, shooting for the reset time
            // but not less than tolerance millisecond due to display update time.
            else
            {
                next = (int)(this.TargetTime - now.TimeOfDay).TotalMilliseconds;
                if (next <= this.ToleranceMillisecond)
                {
                    next += 24 * 60 * 60000;
                }
            }

            // Update the interval to prevent clock drift
            //Interval = next;
            this.dueTime = now.AddMilliseconds(next);
        }

        private void ElapsedInternal(object sender, System.Timers.ElapsedEventArgs e)
        {
            this.Elapsed?.Invoke(this, new TimerElapsedEventArgs(e.SignalTime));
        }

        protected void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                this.timer.Elapsed -= this.ElapsedInternal;
                this.timer.Dispose();
            }

            this.disposed = true;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~TimerService() => this.Dispose(false);
    }
}
