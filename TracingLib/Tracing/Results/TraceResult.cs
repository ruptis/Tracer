namespace TracingLib.Tracing.Results;

public record struct TraceResult(IReadOnlyList<ThreadTraceResult> ThreadTraceResults);
