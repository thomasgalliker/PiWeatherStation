
using System;
using System.Timers;

namespace DisplayService.Services
{
    public class TimerService : ITimerService
    {
        private readonly Timer timer;

        private DateTime dueTime;
        private bool disposed = false;

        public event ElapsedEventHandler Elapsed;
        //{
        //    add { timer.Elapsed += value; }
        //    remove { timer.Elapsed -= value; }
        //}

        public TimerService()
        {
            this.timer = new Timer
            {
                AutoReset = true,
            };
            timer.Elapsed += ElapsedAction;

            TargetTime = default;
            TargetMillisecond = 0;
            ToleranceMillisecond = 0;
        }

        public TimeSpan TargetTime { get; set; }

        public int TargetMillisecond { get; set; }

        public int ToleranceMillisecond { get; set; }

        public bool Enabled
        {
            get
            {
                return timer.Enabled;
            }
            set
            {
                if (value)
                {
                    //SetInterval();
                }

                timer.Enabled = value;
            }
        }

        public TimeSpan Interval
        {
            get => TimeSpan.FromMilliseconds(timer.Interval);
            set
            {
                timer.Interval = value.TotalMilliseconds;
                if (value == TimeSpan.Zero)
                {
                    Stop();
                }
            }
        }

        public double TimeLeft
        {
            get
            {
                return (dueTime - DateTime.Now).TotalMilliseconds;
            }
        }

        public void Start()
        {
            //SetInterval();
            timer.Start();
        }

        public void Stop()
        {
            this.Enabled = false;
            timer.Stop();
        }

        private void SetInterval()
        {
            var now = DateTime.Now;
            int next;
            // Calculate how long to wait for the next interval, shooting for the target
            // millisecond mark but not less than tolerance millisecond due to display update time.
            if (TargetTime == default)
            {
                int targetminute = 0;
                int targetsecond = TargetMillisecond;
                if (TargetMillisecond > 59999)
                {
                    targetminute = TargetMillisecond / 60000;
                    targetsecond = TargetMillisecond % 60000;
                }

                next = targetsecond - (now.Second * 1000 + now.Millisecond);
                if (next <= ToleranceMillisecond)
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
                next = (int)(TargetTime - now.TimeOfDay).TotalMilliseconds;
                if (next <= ToleranceMillisecond)
                {
                    next += 24 * 60 * 60000;
                }
            }

            // Update the interval to prevent clock drift
            //Interval = next;
            dueTime = now.AddMilliseconds(next);
        }

        private void ElapsedAction(object sender, ElapsedEventArgs e)
        {
            this.Elapsed?.Invoke(this, e);

            if (timer.AutoReset) // TODO: Always!?
            {
                //SetInterval();
            }
        }

        protected void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                this.timer.Elapsed -= this.ElapsedAction;
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
