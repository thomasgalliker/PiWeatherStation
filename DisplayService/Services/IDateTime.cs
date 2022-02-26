using System;

namespace DisplayService.Services
{
    public interface IDateTime
    {
        DateTime Now { get; }

        DateTime UtcNow { get; }
    }
}