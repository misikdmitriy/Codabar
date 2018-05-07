using System.IO;
using CommandLine;
using SixLabors.ImageSharp.Formats.Png;

namespace Codabar.Base
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<AlgorithmArgs>(args)
                .WithParsed(algorithmArgs =>
                {
                    using (var image = new CodabarCreator().Run(algorithmArgs))
                    using (var file = File.OpenWrite(algorithmArgs.OutputPath))
                    {
                        image.Save(file, new PngEncoder());
                    }
                });
        }
    }
}
