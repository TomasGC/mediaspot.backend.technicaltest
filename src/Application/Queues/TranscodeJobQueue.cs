using Mediaspot.Domain.Transcoding;
using System.Threading.Channels;

namespace Mediaspot.Application.Workers;

public class TranscodeJobQueue : ITranscodeJobQueue
{
    private readonly Channel<TranscodeJob> _channel;

    public TranscodeJobQueue()
    {
        _channel = Channel.CreateUnbounded<TranscodeJob>();
    }

    public async ValueTask EnqueueAsync(TranscodeJob task)
        => await _channel.Writer.WriteAsync(task);

    public async ValueTask<TranscodeJob> DequeueAsync(CancellationToken cancellationToken)
        => await _channel.Reader.ReadAsync(cancellationToken);
}
