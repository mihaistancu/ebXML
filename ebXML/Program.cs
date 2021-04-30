using System;
using System.IO;
using CommandLine;

namespace ebXML
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args).WithParsed(o =>
            {
                if (o.Create)
                {
                    var factory = new EnvelopeFactory();
                    var envelope = factory.Create();
                    var filename = $"Envelope-{DateTime.Now:HH-mm-ss}";
                    Serializer.Serialize(envelope, File.Create(filename));
                }
            });
        }
    }
}
