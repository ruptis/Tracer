namespace TracingLib.Tracing.Results;

public record struct ThreadTraceResult(int ThreadId, TimeSpan ElapsedTime, IReadOnlyList<MethodTraceResult> MethodTraceResults);
