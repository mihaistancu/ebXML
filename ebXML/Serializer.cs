﻿using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace ebXML
{
    public class Serializer
    {
        public static void Serialize<T>(T data, Stream output)
        {
            var serializer = new XmlSerializer(typeof(T));

            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add("s", Namespaces.SoapEnvelope);
            namespaces.Add("wsu", Namespaces.WebServiceSecurityUtility);
            namespaces.Add("eb", Namespaces.ElectronicBusinessMessagingService);

            var settings = new XmlWriterSettings
            {
                Indent = true
            };

            using (var writer = XmlWriter.Create(output, settings))
            {
                serializer.Serialize(writer, data, namespaces);
            }
        }
    }
}
