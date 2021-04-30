using System;

namespace ebXML
{
    public class EnvelopeFactory
    {
        public Envelope Create()
        {
            return new Envelope
            {
                Header = new Header
                {
                    Messaging = new Messaging
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserMessage = new UserMessage
                        {
                            MessageInfo = new MessageInfo
                            {
                                Timestamp = DateTime.Now,
                                MessageId = Guid.NewGuid() + "@" + Environment.MachineName
                            },
                            PartyInfo = new PartyInfo
                            {
                                From = new Party
                                {
                                    PartyId = new PartyId
                                    {
                                        Type = "urn:eu:europa:ec:dgempl:eessi:ir",
                                        Value = string.Empty
                                    },
                                    Role = "urn:eu:europa:ec:dgempl:eessi:ir:institution"
                                },
                                To = new Party
                                {
                                    PartyId = new PartyId
                                    {
                                        Type = "urn:eu:europa:ec:dgempl:eessi:ir",
                                        Value = string.Empty
                                    },
                                    Role = "urn:eu:europa:ec:dgempl:eessi:ir:institution"
                                }
                            },
                            CollaborationInfo = new CollaborationInfo
                            {
                                Service = new Service
                                {
                                    Type = "urn:eu:europa:ec:dgempl:eessi",
                                    Value = string.Empty
                                },
                                Action = "Send",
                                ConversationId = Guid.NewGuid().ToString()
                            },
                            
                        }
                    }
                },
                Body = new Body
                {
                    Id = "body-id"
                }
            };
        }
    }
}
