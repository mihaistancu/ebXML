using System;

namespace ebXML
{
    
    public class Body
    {
        public string Id { get; set; }
    }

    public class Service
    {
        public string Type { get; set; }
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
        public string Type { get; set; }
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
        public string Id { get; set; }
    }

    public class Header
    {
        public Messaging Messaging { get; set; }
    }

    public class Envelope
    {
        public Header Header { get; set; }
        public Body Body { get; set; }
    }
}
