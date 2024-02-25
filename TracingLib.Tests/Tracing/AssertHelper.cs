using TracingLib.Tracing.Results;
namespace TracingLib.Tests.Tracing;

public static class AssertHelper
{
    public static void RecursiveAssert(IReadOnlyList<MethodTraceResult> innerMethodTraceResults, int recursiveDepth, string recursiveMethodName, string className)
    {
        Assert.That(innerMethodTraceResults, Has.Count.EqualTo(1));
        Assert.Multiple(() =>
        {
            Assert.That(innerMethodTraceResults[0].MethodName, Is.EqualTo(recursiveMethodName));
            Assert.That(innerMethodTraceResults[0].ClassName, Is.EqualTo(className));
            if (recursiveDepth == 0)
            {
                Assert.That(innerMethodTraceResults[0].InnerMethodTraceResults, Is.Empty);
            }
            else
            {
                RecursiveAssert(innerMethodTraceResults[0].InnerMethodTraceResults, recursiveDepth - 1, recursiveMethodName, className);
            }
        });
    }
}
