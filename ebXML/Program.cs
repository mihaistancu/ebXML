using System;
using System.IO;

namespace ebXML
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new EnvelopeFactory();
            var envelope = factory.Create();
            var filename = $"Envelope-{DateTime.Now:HH-mm-ss}";
            Serializer.Serialize(envelope, File.Create(filename));
        }
    }
}
