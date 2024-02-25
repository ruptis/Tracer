using TracingLib.Tracing;
namespace TracerExample.SampleClasses;

internal class Bar
{
    private readonly ITracer _tracer;

    internal Bar(ITracer tracer)
    {
        _tracer = tracer;
    }

    public void InnerMethod()
    {
        _tracer.StartTrace();

        Thread.Sleep(1000);

        RecursiveMethod(3);

        MethodWithoutInnerMethods();

        _tracer.StopTrace();
    }

    private void RecursiveMethod(int depth)
    {
        _tracer.StartTrace();

        if (depth == 0)
        {
            _tracer.StopTrace();
            return;
        }

        RecursiveMethod(depth - 1);

        _tracer.StopTrace();
    }

    public void MethodWithoutInnerMethods()
    {
        _tracer.StartTrace();

        Thread.Sleep(1000);

        _tracer.StopTrace();
    }
}