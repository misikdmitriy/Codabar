﻿using CommandLine;

namespace Codabar.Base
{
    public class AlgorithmArgs
    {
        [Option('o', "output", Required = true, HelpText = "Output file")]
        public string OutputPath { get; set; }

        [Option('i', "input", Required = true, HelpText = "Input text")]
        public string Text { get; set; }

        [Option('s', "start", Required = false, HelpText = "Start symbol for codabar", Default = 'a')]
        public char StartSymbol { get; set; }

        [Option('e', "end", Required = false, HelpText = "End symbol for codabar", Default = 'b')]
        public char EndSymbol { get; set; }

        [Option('w', "width", Required = false, HelpText = "Line width", Default = 5)]
        public int LineWidth { get; set; }

        [Option('h', "height", Required = false, HelpText = "Line height", Default = 150)]
        public int LineHeight { get; set; }
    }
}
