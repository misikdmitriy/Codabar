using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;

namespace Codabar.Base
{
    public class CodabarCreator
    {
        public Image<Rgba32> Run(AlgorithmArgs args)
        {
            var builder = new StringRepresentationBuilder();
            var bits = builder.ToCodabar($"{args.StartSymbol}{args.Text}{args.EndSymbol}");

            var @params = new CodabarParams(args.LineWidth, args.LineHeight);

            return new BitmapConverter().Convert(bits, @params);
        }
    }
}
