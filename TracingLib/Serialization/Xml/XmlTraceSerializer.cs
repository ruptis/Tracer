using System.Xml.Serialization;
using TracingLib.Serialization.Xml.Dto;
using TracingLib.Tracing;
namespace TracingLib.Serialization.Xml;

public class XmlTraceSerializer : ITraceSerializer
{
    private readonly XmlSerializer _serializer = new(typeof(TraceResultDto), [typeof(ThreadTraceResultDto), typeof(MethodTraceResultDto)]);

    public string Serialize(TraceResult traceResult)
    {
        using var stream = new StringWriter();
        _serializer.Serialize(stream, traceResult.ToDto());
        return stream.ToString();
    }
}
