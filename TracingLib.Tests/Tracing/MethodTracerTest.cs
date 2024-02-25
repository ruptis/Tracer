using TracingLib.Tracing;
using TracingLib.Tracing.Results;
namespace TracingLib.Tests.Tracing;

[TestFixture]
[TestOf(typeof(MethodTracer))]
public class MethodTracerTest
{
    private const int SleepTime = 1000;

    [Test]
    public void TestSingleMethod()
    {
        MethodTracer methodTracer = new(nameof(TestSingleMethod), nameof(MethodTracerTest));
        methodTracer.StartTrace();
        Thread.Sleep(SleepTime);
        methodTracer.StopTrace();

        MethodTraceResult traceResult = methodTracer.GetTraceResult();
        Assert.Multiple(() =>
        {
            Assert.That(traceResult.MethodName, Is.EqualTo(nameof(TestSingleMethod)));
            Assert.That(traceResult.ClassName, Is.EqualTo(nameof(MethodTracerTest)));
            Assert.That(traceResult.InnerMethodTraceResults, Is.Empty);
            Assert.That(traceResult.ElapsedTime, Is.GreaterThanOrEqualTo(TimeSpan.FromMilliseconds(SleepTime)));
        });
    }
    
    [Test]
    public void TestMethodWithInnerMethod()
    {
        MethodTracer methodTracer = new(nameof(TestMethodWithInnerMethod), nameof(MethodTracerTest));
        MethodTracer innerMethodTracer = new(nameof(TestSingleMethod), nameof(MethodTracer));
        methodTracer.StartTrace();
        Thread.Sleep(SleepTime);
        methodTracer.AddInnerMethod(innerMethodTracer);
        innerMethodTracer.StartTrace();
        Thread.Sleep(SleepTime);
        innerMethodTracer.StopTrace();
        methodTracer.StopTrace();

        MethodTraceResult traceResult = methodTracer.GetTraceResult();
        Assert.Multiple(() =>
        {
            Assert.That(traceResult.MethodName, Is.EqualTo(nameof(TestMethodWithInnerMethod)));
            Assert.That(traceResult.ClassName, Is.EqualTo(nameof(MethodTracerTest)));
            Assert.That(traceResult.InnerMethodTraceResults, Has.Count.EqualTo(1));
            Assert.That(traceResult.InnerMethodTraceResults[0].MethodName, Is.EqualTo(nameof(TestSingleMethod)));
            Assert.That(traceResult.InnerMethodTraceResults[0].ClassName, Is.EqualTo(nameof(MethodTracer)));
            Assert.That(traceResult.InnerMethodTraceResults[0].InnerMethodTraceResults, Is.Empty);
            Assert.That(traceResult.InnerMethodTraceResults[0].ElapsedTime, Is.GreaterThanOrEqualTo(TimeSpan.FromMilliseconds(SleepTime)));
            Assert.That(traceResult.ElapsedTime, Is.GreaterThanOrEqualTo(TimeSpan.FromMilliseconds(SleepTime)));
        });
    }
}
