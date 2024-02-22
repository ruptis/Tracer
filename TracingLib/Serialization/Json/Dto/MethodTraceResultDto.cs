using System.Text.Json.Serialization;
namespace TracingLib.Serialization.Json.Dto;

[Serializable]
internal record struct MethodTraceResultDto
{
    [JsonPropertyName("name")]
    public string MethodName { get; init; }
    [JsonPropertyName("class")]
    public string ClassName { get; init; }
    [JsonPropertyName("time")]
    public TimeSpan ElapsedTime { get; init; }
    [JsonPropertyName("methods")]
    public IReadOnlyList<MethodTraceResultDto> InnerMethodTraceResults { get; init; }
}
