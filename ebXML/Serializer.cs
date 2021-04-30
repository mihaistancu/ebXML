using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace ebXML
{
    public class Serializer
    {
        public static XmlDocument Serialize<T>(T item)
        {
            var xml = new XmlDocument {PreserveWhitespace = true};
            
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add("s", Namespaces.SoapEnvelope);
            namespaces.Add("wsu", Namespaces.WebServiceSecurityUtility);
            namespaces.Add("eb", Namespaces.ElectronicBusinessMessagingService);

            var serializer = new XmlSerializer(item.GetType());

            using(var xmlWriter = xml.CreateNavigator().AppendChild())
            {
                serializer.Serialize(xmlWriter, item, namespaces);
            }
            return xml;
        }
    }
}
