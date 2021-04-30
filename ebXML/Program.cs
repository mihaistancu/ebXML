namespace ebXML
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new EnvelopeFactory();
            var envelope = factory.Create();
        }
    }
}
