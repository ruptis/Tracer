using TracingLib.Tracing;
namespace TracerExample.SampleClasses;

internal class Foo
{
    private readonly Bar _bar;
    private readonly ITracer _tracer;

    internal Foo(ITracer tracer)
    {
        _tracer = tracer;
        _bar = new Bar(_tracer);
    }

    public void MyMethod()
    {
        _tracer.StartTrace();

        _bar.InnerMethod();

        _bar.MethodWithoutInnerMethods();

        Task.Run(() => _bar.InnerMethod()).Wait();

        _tracer.StopTrace();
    }
}