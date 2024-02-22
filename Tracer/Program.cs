// See https://aka.ms/new-console-template for more information

using TracingLib.Serialization.Json;
using TracingLib.Serialization.Xml;
using TracingLib.Tracing;
var tracer = new Tracer();
var foo = new Foo(tracer);
foo.MyMethod();

var traceResult = tracer.GetTraceResult();
JsonTraceSerializer jsonTraceSerializer = new();
var serializedTraceResult = jsonTraceSerializer.Serialize(traceResult);
Console.WriteLine(serializedTraceResult);

XmlTraceSerializer xmlTraceSerializer = new();
serializedTraceResult = xmlTraceSerializer.Serialize(traceResult);
Console.WriteLine(serializedTraceResult);

public class Foo
{
    private Bar _bar;
    private ITracer _tracer;

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

public class Bar
{
    private ITracer _tracer;

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

    public void RecursiveMethod(int depth)
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
