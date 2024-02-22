using System.Text.Json.Serialization;
namespace TracingLib.Serialization.Json.Dto;

[Serializable]
internal record struct ThreadTraceResultDto
{
    [JsonPropertyName("id")]
    public int ThreadId { get; init; }
    [JsonPropertyName("time")]
    public TimeSpan ElapsedTime { get; init; }
    [JsonPropertyName("methods")]
    public IReadOnlyList<MethodTraceResultDto> MethodTraceResults { get; init; }
}
