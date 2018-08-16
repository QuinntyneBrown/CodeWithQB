using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace CodeWithQB.Infrastructure
{
    public class QueuedHostedService : BackgroundService
    {
        private readonly IBackgroundTaskQueue _taskQueue;

        public QueuedHostedService(IBackgroundTaskQueue taskQueue)
            => _taskQueue = taskQueue;
        
        protected async override Task ExecuteAsync(
            CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var workItem = await _taskQueue.DequeueAsync(cancellationToken);
                
                await workItem(cancellationToken);
            }
        }
    }
}
