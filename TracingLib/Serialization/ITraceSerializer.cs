using TracingLib.Tracing.Results;
namespace TracingLib.Serialization;

public interface ITraceSerializer
{
    void Serialize(TraceResult traceResult, Stream stream);
    Task SerializeAsync(TraceResult traceResult, Stream stream, CancellationToken cancellationToken = default);
}
