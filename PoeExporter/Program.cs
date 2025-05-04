using System.Text.Json;

var data = File.ReadAllText("schema.min.json");
var json = JsonDocument.Parse(data);

var options = new JsonWriterOptions
{
    Indented = true,
};

using var fs = File.Create("schema.json");
// using var stream = new MemoryStream();
// using var writer = new Utf8JsonWriter(stream, options);
using var writer = new Utf8JsonWriter(fs, options);
json.WriteTo(writer);
// writer.Flush();
// var str = Encoding.UTF8.GetString(stream.ToArray());
// Console.WriteLine(str);
