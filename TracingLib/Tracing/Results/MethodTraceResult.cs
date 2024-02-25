namespace TracingLib.Tracing.Results;

public record struct MethodTraceResult(string MethodName, string ClassName, TimeSpan ElapsedTime, IReadOnlyList<MethodTraceResult> InnerMethodTraceResults);
