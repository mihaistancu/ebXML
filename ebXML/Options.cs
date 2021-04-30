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
    }
}
