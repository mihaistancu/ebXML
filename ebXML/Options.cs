using CommandLine;

namespace ebXML
{
    public class Options
    {
        [Option(Default = false)]
        public bool Create { get; set; }

        [Option(Default = false)]
        public bool Sign { get; set; }

        [Option]
        public string Certificate { get; set; }

        [Option]
        public string Filename { get; set; }

        [Option]
        public string Sed { get; set; }

        [Option]
        public string Sender { get; set; }
        
        [Option]
        public string SenderRole { get; set; }
        
        [Option]
        public string Receiver { get; set; }
        
        [Option]
        public string ReceiverRole { get; set; }

        [Option]
        public string UseCase { get; set; }
    }
}
