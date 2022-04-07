using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DisplayService.Services.Scheduling
{
    public interface ICronJob
    {
        Task RunJob(CancellationToken cancellationToken);
    }
}
