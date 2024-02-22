using System.Diagnostics;
namespace TracingLib.Tracing;

internal readonly record struct MethodTracer(string MethodName, string ClassName)
{
    private readonly Stopwatch _stopwatch = new();
    private readonly List<MethodTracer> _innerMethodTracers = [];

    public void StartTrace() =>
        _stopwatch.Start();

    public void StopTrace() =>
        _stopwatch.Stop();

    public void AddInnerMethod(MethodTracer methodTracer) =>
        _innerMethodTracers.Add(methodTracer);

    public MethodTraceResult GetTraceResult()
    {
        var innerMethodTraceResults = _innerMethodTracers.Select(methodTracer => methodTracer.GetTraceResult()).ToList();
        return new MethodTraceResult(
            MethodName,
            ClassName,
            _stopwatch.Elapsed,
            innerMethodTraceResults
        );
    }
}
