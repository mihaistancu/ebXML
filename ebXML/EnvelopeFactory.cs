using System;
using System.Collections.Generic;

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
                            PayloadInfo = new List<PartInfo>
                            {
                                new PartInfo
                                {
                                    Reference = "cid:SED",
                                    PartProperties = new List<Property>
                                    {
                                        new Property {Name = "PartType", Value = "SED"},
                                        new Property {Name = "MimeType", Value = "application/xml"},
                                        new Property {Name = "CompressionType", Value = "application/gzip"}
                                    }
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
                    Id = Guid.NewGuid().ToString()
                }
            };
        }
    }
}
