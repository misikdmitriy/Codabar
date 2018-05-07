using CommandLine;

namespace Codabar.Base
{
    public class CommandLineArgs
    {
        [Option('o', "Outpub BMP", Required = true, HelpText = "Output file")]
        public string OutputPath { get; set; }

        [Option('t', "Input", Required = true, HelpText = "Input text")]
        public string Text { get; set; }

        [Option('s', "Start symbol", Required = false, HelpText = "Start symbol for codabar", Default = 'a')]
        public char? StartSymbol { get; set; }

        [Option('e', "End symbol", Required = false, HelpText = "End symbol for codabar", Default = 'b')]
        public char? EndSymbol { get; set; }
    }
}
