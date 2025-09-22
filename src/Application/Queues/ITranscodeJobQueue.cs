using Mediaspot.Domain.Transcoding;

namespace Mediaspot.Application.Workers;

public interface ITranscodeJobQueue
{
    ValueTask EnqueueAsync(TranscodeJob task);
    ValueTask<TranscodeJob> DequeueAsync(CancellationToken cancellationToken);
}
