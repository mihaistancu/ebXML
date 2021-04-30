using System;
using System.IO;
using System.Security.Cryptography.Xml;
using System.Xml;

namespace ebXML.DotNetExtensions
{
    public class AttachmentContentSignatureTransform: Transform
    {
        public const string AlgorithmName = "http://docs.oasis-open.org/wss/oasis-wss-SwAProfile-1.1#Attachment-Content-Signature-Transform";
        private Stream stream;

        public AttachmentContentSignatureTransform()
        {
            Algorithm = AlgorithmName;
        }

        public override void LoadInnerXml(XmlNodeList nodeList)
        {
        }

        protected override XmlNodeList GetInnerXml()
        {
            return null;
        }

        public override void LoadInput(object obj)
        {
            stream = obj as Stream;
        }

        public override object GetOutput()
        {
            return stream;
        }

        public override object GetOutput(Type type)
        {
            return stream;
        }

        public override Type[] InputTypes => new [] {typeof(Stream)};
        public override Type[] OutputTypes => new[] {typeof(Stream)};
    }
}
