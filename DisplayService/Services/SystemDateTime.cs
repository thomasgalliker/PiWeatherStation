using System;
using DisplayService.Services;

namespace InvoiceScanner.Api.Services.System
{
    public class SystemDateTime : IDateTime
    {
        DateTime IDateTime.Now => DateTime.Now;

        public DateTime UtcNow => DateTime.UtcNow;
    }
}