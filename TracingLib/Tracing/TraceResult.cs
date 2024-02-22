namespace TracingLib.Tracing;

[Serializable]
public record struct TraceResult(IReadOnlyList<ThreadTraceResult> ThreadTraceResults);
[Serializable]
public record struct ThreadTraceResult(int ThreadId, TimeSpan ElapsedTime, IReadOnlyList<MethodTraceResult> MethodTraceResults);
[Serializable]
public record struct MethodTraceResult(string MethodName, string ClassName, TimeSpan ElapsedTime, IReadOnlyList<MethodTraceResult> InnerMethodTraceResults);
