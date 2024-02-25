using TracingLib.Tracing.Results;
namespace TracerExample.Writer;

public interface IResultWriter : IDisposable, IAsyncDisposable
{
    void Write(TraceResult traceResult);
    Task WriteAsync(TraceResult traceResult, CancellationToken cancellationToken = default);
}