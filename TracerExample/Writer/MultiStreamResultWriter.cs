using TracingLib.Serialization;
using TracingLib.Tracing.Results;
namespace TracerExample.Writer;

public sealed class MultiStreamResultWriter<TSerializer> : IResultWriter 
    where TSerializer : ITraceSerializer, new()
{
    private readonly TSerializer _traceSerializer = new();
    private readonly Stream[] _streams;
    
    public MultiStreamResultWriter(params Stream[] streams)
    {
        if (streams.Any(stream => !stream.CanWrite))
            throw new ArgumentException("One of the streams is not writable", nameof(streams));
        
        _streams = streams;
    }
    
    public void Write(TraceResult traceResult)
    {
        foreach (Stream stream in _streams)
            _traceSerializer.Serialize(traceResult, stream);
    }
    
    public async Task WriteAsync(TraceResult traceResult, CancellationToken cancellationToken = default)
    {
        await Task.WhenAll(_streams.Select(stream => _traceSerializer.SerializeAsync(traceResult, stream, cancellationToken)));
    }
    
    public void Dispose()
    {
        foreach (Stream stream in _streams)
            stream.Dispose();
    }
    
    public async ValueTask DisposeAsync()
    {
        foreach (Stream stream in _streams)
            await stream.DisposeAsync();
    }
}
