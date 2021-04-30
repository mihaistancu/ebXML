﻿using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Xml;

namespace ebXML.DotNetExtensions
{
    public class ExtendedSignedXml : SignedXml
    {
        static ExtendedSignedXml()
        {
            CryptoConfig.AddAlgorithm(typeof(AttachmentContentSignatureTransform), AttachmentContentSignatureTransform.AlgorithmName);
        }

        public ExtendedSignedXml(XmlDocument xml) : base(xml)
        {
            SafeCanonicalizationMethods.Add(AttachmentContentSignatureTransform.AlgorithmName);
        }
        
        public void AddExternalReference(Reference reference)
        {
            var existing = GetReferenceByUri(SignedInfo, reference.Uri);

            if (existing != null)
            {
                reference.DigestValue = existing.DigestValue;
                SignedInfo.References.Remove(existing);
            }

            SignedInfo.AddReference(reference);
        }

        private Reference GetReferenceByUri(SignedInfo signedInfo, string uri)
        {
            return signedInfo.References.Cast<Reference>().FirstOrDefault(reference => reference.Uri == uri);
        }

        public override XmlElement GetIdElement(XmlDocument doc, string id)
        {
            return GetReferencesById(doc, id)
                .Concat(GetDataObjectsById(id))
                .Single() as XmlElement;
        }

        private IEnumerable<XmlNode> GetReferencesById(XmlDocument doc, string id)
        {
            var namespaceManager = new XmlNamespaceManager(doc.NameTable);
            namespaceManager.AddNamespace("wsu", Namespaces.WebServiceSecurityUtility);
            var xpath = $@"//*[@wsu:Id=""{id}""]";
            return doc.SelectNodes(xpath, namespaceManager).Cast<XmlNode>();
        }

        private IEnumerable<XmlNode> GetDataObjectsById(string id)
        {
            var xpath = $@"//*[@Id=""{id}""]";
            return Signature.ObjectList.Cast<DataObject>()
                .SelectMany(d => 
                    d.Data.Cast<XmlNode>()
                        .SelectMany(x=>x.SelectNodes(xpath).Cast<XmlNode>())
                    );
        }
    }
}
