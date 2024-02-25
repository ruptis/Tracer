using System.Text.Json.Serialization;
namespace TracingLib.Serialization.Json.Dto;

[Serializable]
internal record struct TraceResultDto
{
    [JsonPropertyName("threads")]
    public IReadOnlyList<ThreadTraceResultDto> ThreadTraceResults { get; init; }
}
