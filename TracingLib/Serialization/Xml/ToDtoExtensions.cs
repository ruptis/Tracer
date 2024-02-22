using TracingLib.Serialization.Xml.Dto;
using TracingLib.Tracing;
namespace TracingLib.Serialization.Xml;

internal static class ToDtoExtensions
{
    public static TraceResultDto ToDto(this TraceResult traceResult)
    {
        return new TraceResultDto
        {
            ThreadTraceResults = traceResult.ThreadTraceResults.Select(ToDto).ToList()
        };
    }
    
    private static ThreadTraceResultDto ToDto(ThreadTraceResult threadTraceResult)
    {
        return new ThreadTraceResultDto
        {
            ThreadId = threadTraceResult.ThreadId,
            ElapsedTime = threadTraceResult.ElapsedTime,
            MethodTraceResults = threadTraceResult.MethodTraceResults.Select(ToDto).ToList()
        };
    }
    
    private static MethodTraceResultDto ToDto(MethodTraceResult methodTraceResult)
    {
        return new MethodTraceResultDto
        {
            MethodName = methodTraceResult.MethodName,
            ClassName = methodTraceResult.ClassName,
            ElapsedTime = methodTraceResult.ElapsedTime,
            InnerMethodTraceResults = methodTraceResult.InnerMethodTraceResults.Select(ToDto).ToList()
        };
    }
}
