using System.Security.Cryptography.Xml;
using System.Xml;

namespace ebXML.DotNetExtensions
{
    public class SecurityTokenReference : KeyInfoClause
    {
        public const string Namespace = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd";
        private readonly string referenceId;

        public SecurityTokenReference(string referenceId)
        {
            this.referenceId = referenceId;
        }

        public override XmlElement GetXml()
        {
            var xmlDocument = new XmlDocument {PreserveWhitespace = true};
            var securityTokenReference = xmlDocument.CreateElement("SecurityTokenReference", Namespace);
            var reference = xmlDocument.CreateElement("Reference", Namespace);
            reference.SetAttribute("ValueType", Namespaces.X509TokenProfile);
            reference.SetAttribute("URI", "#" + referenceId);
            securityTokenReference.AppendChild(reference);
            return securityTokenReference;
        }

        public override void LoadXml(XmlElement element)
        {
        }
    }
}
