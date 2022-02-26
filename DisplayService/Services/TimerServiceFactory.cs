namespace DisplayService.Services
{
    public class TimerServiceFactory : ITimerServiceFactory
    {
        public ITimerService Create()
        {
            return new TimerService();
        }
    }
}