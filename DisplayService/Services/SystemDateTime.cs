using System;

namespace DisplayService.Services
{
    public class SystemDateTime : IDateTime
    {
        public DateTime Now => DateTime.Now;

        public DateTime UtcNow => DateTime.UtcNow;
    }
}