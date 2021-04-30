﻿using System;
using System.Xml.Serialization;

namespace ebXML
{
    public class Body
    {
        [XmlAttribute(Namespace = Namespaces.WebServiceSecurityUtility)]
        public string Id { get; set; }
    }

    public class Service
    {
        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }

        [XmlText]
        public string Value { get; set; }
    }

    public class CollaborationInfo
    {
        public string Action { get; set; }
        public string ConversationId { get; set; }
        public Service Service { get; set; }
    }

    public class PartyId
    {
        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }

        [XmlText]
        public string Value { get; set; }
    }

    public class Party
    {
        public string Role { get; set; }
        public PartyId PartyId { get; set; }
    }

    public class PartyInfo
    {
        public Party To { get; set; }
        public Party From { get; set; }
    }

    public class MessageInfo
    {
        public DateTime Timestamp { get; set; }
        public string MessageId { get; set; }
    }

    public class UserMessage
    {
        public MessageInfo MessageInfo { get; set; }
        public CollaborationInfo CollaborationInfo { get; set; }
        public PartyInfo PartyInfo { get; set; }
    }

    public class Messaging
    {
        public UserMessage UserMessage { get; set; }

        [XmlAttribute(Namespace = Namespaces.WebServiceSecurityUtility)]
        public string Id { get; set; }
    }

    public class Header
    {
        [XmlElement(Namespace = Namespaces.ElectronicBusinessMessagingService)]
        public Messaging Messaging { get; set; }
    }

    [XmlRoot(Namespace = Namespaces.SoapEnvelope)]
    public class Envelope
    {
        public Header Header { get; set; }
        public Body Body { get; set; }
    }

    public static class Namespaces
    {
        public const string SoapEnvelope = "http://www.w3.org/2003/05/soap-envelope";
        public const string WebServiceSecurityUtility = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd";
        public const string ElectronicBusinessMessagingService = "http://docs.oasis-open.org/ebxml-msg/ebms/v3.0/ns/core/200704/";
    }
}
