using TracerExample.SampleClasses;
using TracerExample.Writer;
using TracingLib.Serialization.Json;
using TracingLib.Serialization.Xml;
using TracingLib.Tracing;
using TracingLib.Tracing.Results;

var tracer = new Tracer();
var foo = new Foo(tracer);
foo.MyMethod();

TraceResult traceResult = tracer.GetTraceResult();

await using MultiStreamResultWriter<JsonTraceSerializer> jsonResultWriter = new(File.Create("trace.json"), Console.OpenStandardOutput());
await jsonResultWriter.WriteAsync(traceResult);

await using MultiStreamResultWriter<XmlTraceSerializer> xmlResultWriter = new(File.Create("trace.xml"), Console.OpenStandardOutput());
await xmlResultWriter.WriteAsync(traceResult);