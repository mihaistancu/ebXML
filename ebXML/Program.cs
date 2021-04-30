using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using CommandLine;

namespace ebXML
{
    class Program
    {
        private static string Filename { get; set; }

        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args).WithParsed(o =>
            {
                Filename = o.Filename ?? $"Envelope-{DateTime.Now:HH-mm-ss}";

                if (o.Create)
                {
                    var factory = new EnvelopeFactory();
                    var envelope = factory.Create();
                    var xml = Serializer.Serialize(envelope);
                    xml.Save(Filename);
                }

                if (o.Sign)
                {
                    var xml = new XmlDocument();
                    xml.Load(Filename);
                    var certificate = new X509Certificate2(o.Certificate);
                    var signer = new Signer(certificate);
                    signer.Sign(xml, File.OpenRead(o.Sed));
                    xml.Save(Filename);
                }
            });
        }
    }
}
