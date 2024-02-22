using System.Collections.Concurrent;
using System.Diagnostics;
using System.Reflection;
namespace TracingLib.Tracing;

public sealed class Tracer : ITracer
{
    private readonly ConcurrentDictionary<int, ThreadTracer> _threadTracers = new();

    public void StartTrace()
    {
        var threadId = Environment.CurrentManagedThreadId;
        ThreadTracer threadTracer = _threadTracers.GetOrAdd(threadId, _ => new ThreadTracer(threadId));

        MethodBase? method = new StackFrame(1).GetMethod();
        if (method == null)
            throw new InvalidOperationException("Method is not found");
        
        threadTracer.StartTrace(method);
    }

    public void StopTrace()
    {
        var threadId = Environment.CurrentManagedThreadId;
        if (!_threadTracers.TryGetValue(threadId, out ThreadTracer threadTracer))
            throw new InvalidOperationException("Trace is not started");
        
        threadTracer.StopTrace();
    }

    public TraceResult GetTraceResult()
    {
        var threadTraceResults = _threadTracers.Values
            .Select(threadTracer => threadTracer.GetTraceResult())
            .ToList();
        return new TraceResult(threadTraceResults);
    }
}