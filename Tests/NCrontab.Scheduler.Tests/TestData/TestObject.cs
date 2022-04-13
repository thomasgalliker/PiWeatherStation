using System.Threading.Tasks;

namespace NCrontab.Scheduler.Tests.TestData
{
    public class TestObject
    {
        public bool Modified => this.ModifiedCount > 0;

        public int ModifiedCount { get; private set; }

        public string CronExpression { get; set; }

        public void DoWork()
        {
            this.ModifiedCount++;
        }

        public Task DoWorkAsync()
        {
            this.ModifiedCount++;

            return Task.CompletedTask;
        }
    }
}
