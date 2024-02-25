using TracingLib.Tests.Tracing.TestClasses;
using TracingLib.Tracing;
using TracingLib.Tracing.Results;
namespace TracingLib.Tests.Tracing;

[TestFixture]
[TestOf(typeof(Tracer))]
public class TracerTest
{
    private const int ThreadsCount = 3;

    private Tracer _tracer = null!;
    private TestClass _testClass = null!;

    [SetUp]
    public void SetUp()
    {
        _tracer = new Tracer();
        _testClass = new TestClass(_tracer.StartTrace, _tracer.StopTrace);
    }

    [Test]
    public void TestSingleMethod()
    {
        _testClass.Method();
        TraceResult traceResult = _tracer.GetTraceResult();
        Assert.That(traceResult.ThreadTraceResults, Has.Count.EqualTo(1));
        Assert.That(traceResult.ThreadTraceResults[0].MethodTraceResults, Has.Count.EqualTo(1));
        Assert.Multiple(() =>
        {
            Assert.That(traceResult.ThreadTraceResults[0].MethodTraceResults[0].MethodName, Is.EqualTo(nameof(TestClass.Method)));
            Assert.That(traceResult.ThreadTraceResults[0].MethodTraceResults[0].ClassName, Is.EqualTo(nameof(TestClass)));
            Assert.That(traceResult.ThreadTraceResults[0].MethodTraceResults[0].InnerMethodTraceResults, Is.Empty);
            Assert.That(traceResult.ThreadTraceResults[0].MethodTraceResults[0].ElapsedTime, Is.GreaterThanOrEqualTo(TimeSpan.FromMilliseconds(TestClass.SleepTime)));
        });
    }

    [Test]
    public void TestMethodWithInnerMethod()
    {
        _testClass.MethodWithInnerMethod();
        TraceResult traceResult = _tracer.GetTraceResult();
        Assert.That(traceResult.ThreadTraceResults, Has.Count.EqualTo(1));
        Assert.That(traceResult.ThreadTraceResults[0].MethodTraceResults, Has.Count.EqualTo(1));
        Assert.Multiple(() =>
        {
            Assert.That(traceResult.ThreadTraceResults[0].MethodTraceResults[0].MethodName, Is.EqualTo(nameof(TestClass.MethodWithInnerMethod)));
            Assert.That(traceResult.ThreadTraceResults[0].MethodTraceResults[0].ClassName, Is.EqualTo(nameof(TestClass)));
            Assert.That(traceResult.ThreadTraceResults[0].MethodTraceResults[0].InnerMethodTraceResults, Has.Count.EqualTo(1));
            Assert.That(traceResult.ThreadTraceResults[0].MethodTraceResults[0].InnerMethodTraceResults[0].MethodName, Is.EqualTo(nameof(TestClass.Method)));
            Assert.That(traceResult.ThreadTraceResults[0].MethodTraceResults[0].InnerMethodTraceResults[0].ClassName, Is.EqualTo(nameof(TestClass)));
            Assert.That(traceResult.ThreadTraceResults[0].MethodTraceResults[0].InnerMethodTraceResults[0].InnerMethodTraceResults, Is.Empty);
            Assert.That(traceResult.ThreadTraceResults[0].MethodTraceResults[0].InnerMethodTraceResults[0].ElapsedTime, Is.GreaterThanOrEqualTo(TimeSpan.FromMilliseconds(TestClass.SleepTime)));
            Assert.That(traceResult.ThreadTraceResults[0].MethodTraceResults[0].ElapsedTime, Is.GreaterThanOrEqualTo(TimeSpan.FromMilliseconds(TestClass.SleepTime)));
        });
    }

    [Test]
    public void TestMethodWithInnerRecursiveMethod()
    {
        _testClass.MethodWithInnerRecursiveMethod();
        TraceResult traceResult = _tracer.GetTraceResult();
        Assert.That(traceResult.ThreadTraceResults, Has.Count.EqualTo(1));
        Assert.That(traceResult.ThreadTraceResults[0].MethodTraceResults, Has.Count.EqualTo(1));
        Assert.Multiple(() =>
        {
            Assert.That(traceResult.ThreadTraceResults[0].MethodTraceResults[0].MethodName, Is.EqualTo(nameof(TestClass.MethodWithInnerRecursiveMethod)));
            Assert.That(traceResult.ThreadTraceResults[0].MethodTraceResults[0].ClassName, Is.EqualTo(nameof(TestClass)));
            AssertHelper.RecursiveAssert(traceResult.ThreadTraceResults[0].MethodTraceResults[0].InnerMethodTraceResults, TestClass.RecursiveDepth,
                nameof(TestClassWithRecursiveMethod.RecursiveMethod), nameof(TestClassWithRecursiveMethod));
        });
    }

    [Test]
    public void TestMultipleThreads()
    {
        List<Thread> threads = [];
        for (var i = 0; i < ThreadsCount; i++)
        {
            Thread thread = new(() => _testClass.Method());
            threads.Add(thread);
            thread.Start();
        }
        foreach (Thread thread in threads)
        {
            thread.Join();
        }

        TraceResult traceResult = _tracer.GetTraceResult();
        Assert.That(traceResult.ThreadTraceResults, Has.Count.EqualTo(ThreadsCount));
        foreach (ThreadTraceResult threadTraceResult in traceResult.ThreadTraceResults)
        {
            Assert.That(threadTraceResult.MethodTraceResults, Has.Count.EqualTo(1));
            Assert.Multiple(() =>
            {
                Assert.That(threadTraceResult.MethodTraceResults[0].MethodName, Is.EqualTo(nameof(TestClass.Method)));
                Assert.That(threadTraceResult.MethodTraceResults[0].ClassName, Is.EqualTo(nameof(TestClass)));
                Assert.That(threadTraceResult.MethodTraceResults[0].InnerMethodTraceResults, Is.Empty);
                Assert.That(threadTraceResult.MethodTraceResults[0].ElapsedTime, Is.GreaterThanOrEqualTo(TimeSpan.FromMilliseconds(TestClass.SleepTime)));
            });
        }
    }

    [Test]
    public void TestTraceNotStarted()
    {
        Assert.Throws<InvalidOperationException>(() => _tracer.StopTrace());
    }
}
