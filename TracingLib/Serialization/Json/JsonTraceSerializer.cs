using System.Text.Json;
using TracingLib.Tracing;
namespace TracingLib.Serialization.Json;

public class JsonTraceSerializer : ITraceSerializer
{
    private static readonly JsonSerializerOptions Options = new()
    {
        WriteIndented = true
    };
    
    public string Serialize(TraceResult traceResult) => 
        JsonSerializer.Serialize(traceResult.ToDto(), Options);
}
