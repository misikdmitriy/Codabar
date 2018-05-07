using System.IO;
using CommandLine;
using SixLabors.ImageSharp.Formats.Bmp;

namespace Codabar.Base
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<CommandLineArgs>(args)
                .WithParsed(Run);
        }

        public static void Run(CommandLineArgs args)
        {
            var builder = new StringRepresentationBuilder();
            var bits = builder.ToCodabar($"a{args.Text}b");

            var @params = new CodabarParams(10, 125);

            using (var image = new BitmapConverter().Convert(bits, @params))
            using (var file = File.OpenWrite(args.OutputPath))
            {
                image.Save(file, new BmpEncoder());
            }
        }
    }
}
