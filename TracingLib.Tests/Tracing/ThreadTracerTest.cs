using System.Diagnostics;
using TracingLib.Tests.Tracing.TestClasses;
using TracingLib.Tracing;
using TracingLib.Tracing.Results;
namespace TracingLib.Tests.Tracing;

[TestFixture]
[TestOf(typeof(ThreadTracer))]
public class ThreadTracerTest
{
    private ThreadTracer _threadTracer;
    private TestClass _testClass = null!;
    
    [SetUp]
    public void SetUp()
    {
        _threadTracer = new ThreadTracer(1);
        _testClass = new TestClass(() => _threadTracer.StartTrace(new StackFrame(1).GetMethod()!), _threadTracer.StopTrace);
    }
    
    [Test]
    public void TestSingleMethod()
    {
        _testClass.Method();
        ThreadTraceResult traceResult = _threadTracer.GetTraceResult();
        Assert.That(traceResult.MethodTraceResults, Has.Count.EqualTo(1));
        Assert.That(traceResult.MethodTraceResults[0].MethodName, Is.EqualTo(nameof(TestClass.Method)));
        Assert.Multiple(() =>
        {
            Assert.That(traceResult.MethodTraceResults[0].ClassName, Is.EqualTo(nameof(TestClass)));
            Assert.That(traceResult.MethodTraceResults[0].InnerMethodTraceResults, Is.Empty);
            Assert.That(traceResult.MethodTraceResults[0].ElapsedTime, Is.GreaterThanOrEqualTo(TimeSpan.FromMilliseconds(TestClass.SleepTime)));
        });
    }
    
    [Test]
    public void TestMethodWithInnerMethod()
    {
        _testClass.MethodWithInnerMethod();
        ThreadTraceResult traceResult = _threadTracer.GetTraceResult();
        Assert.That(traceResult.MethodTraceResults, Has.Count.EqualTo(1));
        Assert.Multiple(() =>
        {
            Assert.That(traceResult.MethodTraceResults[0].MethodName, Is.EqualTo(nameof(TestClass.MethodWithInnerMethod)));
            Assert.That(traceResult.MethodTraceResults[0].ClassName, Is.EqualTo(nameof(TestClass)));
            Assert.That(traceResult.MethodTraceResults[0].InnerMethodTraceResults, Has.Count.EqualTo(1));
            Assert.That(traceResult.MethodTraceResults[0].InnerMethodTraceResults[0].MethodName, Is.EqualTo(nameof(TestClass.Method)));
            Assert.That(traceResult.MethodTraceResults[0].InnerMethodTraceResults[0].ClassName, Is.EqualTo(nameof(TestClass)));
            Assert.That(traceResult.MethodTraceResults[0].InnerMethodTraceResults[0].InnerMethodTraceResults, Is.Empty);
            Assert.That(traceResult.MethodTraceResults[0].InnerMethodTraceResults[0].ElapsedTime, Is.GreaterThanOrEqualTo(TimeSpan.FromMilliseconds(TestClass.SleepTime)));
            Assert.That(traceResult.MethodTraceResults[0].ElapsedTime, Is.GreaterThanOrEqualTo(TimeSpan.FromMilliseconds(TestClass.SleepTime)));
        });
    }
    
    [Test]
    public void TestMethodWithInnerRecursiveMethod()
    {
        _testClass.MethodWithInnerRecursiveMethod();
        ThreadTraceResult traceResult = _threadTracer.GetTraceResult();
        Assert.That(traceResult.MethodTraceResults, Has.Count.EqualTo(1));
        Assert.Multiple(() =>
        {
            Assert.That(traceResult.MethodTraceResults[0].MethodName, Is.EqualTo(nameof(TestClass.MethodWithInnerRecursiveMethod)));
            Assert.That(traceResult.MethodTraceResults[0].ClassName, Is.EqualTo(nameof(TestClass)));
            AssertHelper.RecursiveAssert(traceResult.MethodTraceResults[0].InnerMethodTraceResults, TestClass.RecursiveDepth, 
                nameof(TestClassWithRecursiveMethod.RecursiveMethod), nameof(TestClassWithRecursiveMethod));
        });
    }
    
    [Test]
    public void TestNotStartedTrace()
    {
        Assert.Throws<InvalidOperationException>(() => _threadTracer.StopTrace());
    }
}
