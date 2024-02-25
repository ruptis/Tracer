using System.Xml.Serialization;
using TracingLib.Serialization.Xml.Dto;
using TracingLib.Tracing.Results;
namespace TracingLib.Serialization.Xml;

public class XmlTraceSerializer : ITraceSerializer
{
    private readonly XmlSerializer _serializer = new(typeof(TraceResultDto), [typeof(ThreadTraceResultDto), typeof(MethodTraceResultDto)]);

    public void Serialize(TraceResult traceResult, Stream stream) => 
        _serializer.Serialize(stream, traceResult.ToDto());
    
    public Task SerializeAsync(TraceResult traceResult, Stream stream, CancellationToken cancellationToken = default)
    {
        _serializer.Serialize(stream, traceResult.ToDto());
        return Task.CompletedTask;
    }
}
