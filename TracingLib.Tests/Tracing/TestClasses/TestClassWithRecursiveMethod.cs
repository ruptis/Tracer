namespace TracingLib.Tests.Tracing.TestClasses;

public class TestClassWithRecursiveMethod(Action startTraceAction, Action stopTraceAction)
{
    public void RecursiveMethod(int depth)
    {
        startTraceAction();
        if (depth == 0)
        {
            stopTraceAction();
            return;
        }
        RecursiveMethod(depth - 1);
        stopTraceAction();
    }
}
