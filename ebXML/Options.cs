using CommandLine;

namespace ebXML
{
    public class Options
    {
        [Option(Default = false)]
        public bool Create { get; set; }
    }
}
