using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Xml;
using ebXML.DotNetExtensions;

namespace ebXML
{
    public class Signer
    {
        private readonly X509Certificate2 certificate;

        public Signer(X509Certificate2 certificate)
        {
            this.certificate = certificate;
        }

        public void Sign(XmlDocument xml, Stream sed)
        {
            var security = new Security
            {
                BinarySecurityToken = new BinarySecurityToken
                {
                    Id = Guid.NewGuid().ToString(),
                    EncodingType = Namespaces.Base64Binary,
                    ValueType = Namespaces.X509TokenProfile,
                    Value = certificate.GetRawCertData()
                }
            };

            var securityXml = Serializer.Serialize(security);

            var signedXml = new ExtendedSignedXml(xml)
            {
                SigningKey = certificate.GetRSAPrivateKey()
            };

            var namespaces = new XmlNamespaceManager(xml.NameTable);
            namespaces.AddNamespace("s", Namespaces.SoapEnvelope);
            namespaces.AddNamespace("eb", Namespaces.ElectronicBusinessMessagingService);
            namespaces.AddNamespace("wsu", Namespaces.WebServiceSecurityUtility);
            var messaging = xml.SelectSingleNode("/s:Envelope/s:Header/eb:Messaging", namespaces);
            var body = xml.SelectSingleNode("/s:Envelope/s:Body", namespaces);

            var messagingReference = new Reference
            {
                Uri = "#" + messaging.Attributes["wsu:Id"].Value,
                DigestMethod = SignedXml.XmlDsigSHA256Url
            };
            messagingReference.AddTransform(new XmlDsigExcC14NTransform());
            signedXml.AddReference(messagingReference);

            var bodyReference = new Reference
            {
                Uri = "#" + body.Attributes["wsu:Id"].Value,
                DigestMethod = SignedXml.XmlDsigSHA256Url
            };
            bodyReference.AddTransform(new XmlDsigExcC14NTransform());
            signedXml.AddReference(bodyReference);

            var sedReference = new Reference(new NonCloseableStream(sed))
            {
                Uri = "cid:DefaultSED",
                DigestMethod = SignedXml.XmlDsigSHA256Url
            };
            sedReference.AddTransform(new AttachmentContentSignatureTransform());
            signedXml.AddExternalReference(sedReference);
            
            var keyInfo = new KeyInfo();
            keyInfo.AddClause(new SecurityTokenReference(security.BinarySecurityToken.Id));
            signedXml.KeyInfo = keyInfo;
            signedXml.SignedInfo.CanonicalizationMethod = SignedXml.XmlDsigExcC14NTransformUrl;
            signedXml.SignedInfo.SignatureMethod = SignedXml.XmlDsigRSASHA256Url;
            signedXml.ComputeSignature();

            var signature = signedXml.GetXml();
            Insert(signature, securityXml.DocumentElement);
            var header = xml.SelectSingleNode("/s:Envelope/s:Header", namespaces);
            Insert(securityXml, header);
        }

        private static void Insert(XmlNode source, XmlNode destination)
        {
            using (var writer = destination.CreateNavigator().AppendChild())
            {
                source.WriteTo(writer);
            }
        }
    }
}
