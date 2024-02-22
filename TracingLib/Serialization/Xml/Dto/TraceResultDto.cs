using System.Xml.Serialization;
namespace TracingLib.Serialization.Xml.Dto;

[Serializable]
[XmlType("root")]
public record struct TraceResultDto
{
    [XmlElement("thread")]
    public List<ThreadTraceResultDto> ThreadTraceResults { get; init; }
}