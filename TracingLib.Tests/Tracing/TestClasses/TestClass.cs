namespace TracingLib.Tests.Tracing.TestClasses;

public class TestClass(Action startTraceAction, Action stopTraceAction)
{
    private readonly TestClassWithRecursiveMethod _recursiveMethodClass = new(startTraceAction, stopTraceAction);

    public const int SleepTime = 1000;
    public const int RecursiveDepth = 3;

    public void Method()
    {
        startTraceAction();
        Thread.Sleep(SleepTime);
        stopTraceAction();
    }

    public void MethodWithInnerMethod()
    {
        startTraceAction();
        Method();
        stopTraceAction();
    }

    public void MethodWithInnerRecursiveMethod()
    {
        startTraceAction();
        _recursiveMethodClass.RecursiveMethod(RecursiveDepth);
        stopTraceAction();
    }
}