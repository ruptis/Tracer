using TracingLib.Tracing.Results;
namespace TracingLib.Tracing;

public interface ITracer
{
    void StartTrace();
    
    void StopTrace();
    
    TraceResult GetTraceResult();
}