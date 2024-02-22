using System.Reflection;
namespace TracingLib.Tracing;

internal readonly record struct ThreadTracer(int ThreadId)
{
    private readonly Stack<MethodTracer> _callStack = new();
    private readonly List<MethodTracer> _methodTracers = [];

    public void StartTrace(MethodBase method)
    {
        var methodTracer = new MethodTracer(method.Name, method.DeclaringType?.Name ?? "");
        if (_callStack.TryPeek(out MethodTracer currentMethodTracer))
            currentMethodTracer.AddInnerMethod(methodTracer);

        _callStack.Push(methodTracer);

        methodTracer.StartTrace();
    }

    public void StopTrace()
    {
        if (!_callStack.TryPop(out MethodTracer methodTracer))
            throw new InvalidOperationException("Trace is not started");
        
        methodTracer.StopTrace();

        if (_callStack.Count == 0)
            _methodTracers.Add(methodTracer);
    }

    public ThreadTraceResult GetTraceResult()
    {
        var methodTraceResults = _methodTracers.Select(methodTracer => methodTracer.GetTraceResult()).ToList();
        return new ThreadTraceResult(
            ThreadId,
            methodTraceResults.Aggregate(TimeSpan.Zero, (sum, result) => sum + result.ElapsedTime),
            methodTraceResults
        );
    }
}
