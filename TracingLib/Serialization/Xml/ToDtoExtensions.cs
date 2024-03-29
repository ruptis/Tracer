﻿using TracingLib.Serialization.Xml.Dto;
using TracingLib.Tracing.Results;
namespace TracingLib.Serialization.Xml;

internal static class ToDtoExtensions
{
    public static TraceResultDto ToDto(this TraceResult traceResult) => new()
    {
        ThreadTraceResults = traceResult.ThreadTraceResults.Select(ToDto).ToList()
    };

    private static ThreadTraceResultDto ToDto(ThreadTraceResult threadTraceResult) => new()
    {
        ThreadId = threadTraceResult.ThreadId,
        ElapsedTime = threadTraceResult.ElapsedTime,
        MethodTraceResults = threadTraceResult.MethodTraceResults.Select(ToDto).ToList()
    };

    private static MethodTraceResultDto ToDto(MethodTraceResult methodTraceResult) => new()
    {
        MethodName = methodTraceResult.MethodName,
        ClassName = methodTraceResult.ClassName,
        ElapsedTime = methodTraceResult.ElapsedTime,
        InnerMethodTraceResults = methodTraceResult.InnerMethodTraceResults.Select(ToDto).ToList()
    };
}
