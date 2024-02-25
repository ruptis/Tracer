using System.Text.Json;
using TracingLib.Tracing.Results;
namespace TracingLib.Serialization.Json;

public class JsonTraceSerializer : ITraceSerializer
{
    private static readonly JsonSerializerOptions Options = new()
    {
        WriteIndented = true
    };

    public void Serialize(TraceResult traceResult, Stream stream) => 
        JsonSerializer.Serialize(stream, traceResult.ToDto(), Options);
    
    public async Task SerializeAsync(TraceResult traceResult, Stream stream, CancellationToken cancellationToken = default) =>
        await JsonSerializer.SerializeAsync(stream, traceResult.ToDto(), Options, cancellationToken);
}
