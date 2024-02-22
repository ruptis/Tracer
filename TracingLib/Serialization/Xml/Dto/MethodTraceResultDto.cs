using System.Xml.Serialization;
namespace TracingLib.Serialization.Xml.Dto;

[Serializable]
[XmlType("method")]
public record struct MethodTraceResultDto
{
    [XmlAttribute("name")]
    public string MethodName { get; init; }
    [XmlAttribute("class")]
    public string ClassName { get; init; }
    [XmlAttribute("time")]
    public TimeSpan ElapsedTime { get; init; }
    [XmlElement("method")]
    public List<MethodTraceResultDto> InnerMethodTraceResults { get; init; }
}
