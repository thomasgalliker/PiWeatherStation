namespace DisplayService.Services
{
    /// <summary>
    /// DateTime-based file randomizer implementation.
    /// </summary>
    public class DateTimeFileNameRandomizer : FileNameRandomizer
    {
        private const string DateTimePattern = "yyyy-MM-dd-HH-mm-ss-fff";

        private readonly IDateTime dateTime;

        public DateTimeFileNameRandomizer(IDateTime dateTime)
        {
            this.dateTime = dateTime;
        }

        public override string GetRandomness()
        {
            return $"-{this.dateTime.UtcNow.ToString(DateTimePattern)}";
        }
    }
}