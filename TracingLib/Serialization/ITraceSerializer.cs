using TracingLib.Tracing;
namespace TracingLib.Serialization;

public interface ITraceSerializer
{
    string Serialize(TraceResult traceResult);
}
