using Mediaspot.Application.TranscodeJobs.Commands.CompleteTranscodeJob;
using Mediaspot.Application.TranscodeJobs.Commands.FailTranscodeJob;
using Mediaspot.Application.TranscodeJobs.Commands.StartTranscodeJob;
using Mediaspot.Application.Workers;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Mediaspot.Worker;

public class TranscoderBackgroundService(
    IServiceProvider serviceProvider,
    TranscodeJobQueue queue) : BackgroundService
{
    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await ProcessTaskQueueAsync(stoppingToken);
    }

    private async Task ProcessTaskQueueAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var sender = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<ISender>();
            var job = await queue.DequeueAsync(stoppingToken);

            try
            {
                await sender.Send(new StartTranscodeJobCommand(job.Id), stoppingToken);

                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);

                await sender.Send(new CompleteTranscodeJobCommand(job.Id), stoppingToken);
            }
            catch (OperationCanceledException)
            {
                // Prevent throwing if stoppingToken was signaled
            }
            catch (Exception ex)
            {
                await sender.Send(new FailTranscodeJobCommand(job.Id), stoppingToken);
            }
        }
    }
}
