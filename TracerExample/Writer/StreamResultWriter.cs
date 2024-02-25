using TracingLib.Serialization;
using TracingLib.Tracing.Results;
namespace TracerExample.Writer;

public sealed class StreamResultWriter<TSerializer> : IResultWriter 
    where TSerializer : ITraceSerializer, new()
{
    private readonly TSerializer _traceSerializer = new();
    private readonly Stream _stream;
    
    public StreamResultWriter(Stream stream)
    {
        if (!stream.CanWrite)
            throw new ArgumentException("Stream is not writable", nameof(stream));
        
        _stream = stream;
    }

    public void Write(TraceResult traceResult) => 
        _traceSerializer.Serialize(traceResult, _stream);

    public async Task WriteAsync(TraceResult traceResult, CancellationToken cancellationToken = default) => 
        await _traceSerializer.SerializeAsync(traceResult, _stream, cancellationToken);
    
    public void Dispose() => _stream.Dispose();
    public async ValueTask DisposeAsync() => await _stream.DisposeAsync();
}