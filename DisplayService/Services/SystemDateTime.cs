using System;

namespace DisplayService.Services
{
    public class SystemDateTime : IDateTime
    {
        DateTime IDateTime.Now => DateTime.Now;

        public DateTime UtcNow => DateTime.UtcNow;
    }
}