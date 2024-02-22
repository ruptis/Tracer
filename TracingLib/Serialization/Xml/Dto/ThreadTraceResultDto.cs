using System.Xml.Serialization;
namespace TracingLib.Serialization.Xml.Dto;

[Serializable]
[XmlType("thread")]
public record struct ThreadTraceResultDto
{
    [XmlAttribute("id")]
    public int ThreadId { get; init; }
    [XmlAttribute("time")]
    public TimeSpan ElapsedTime { get; init; }
    [XmlElement("method")]
    public List<MethodTraceResultDto> MethodTraceResults { get; init; }
}
