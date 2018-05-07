using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Codabar.Base
{
    public class BitmapConverter
    {
        public Image<Rgba32> Convert(string bits, CodabarParams codabarParams)
        {
            var image = new Image<Rgba32>(codabarParams.LineWidth * bits.Length, 
                codabarParams.LineHeight);

            for (var i = 0; i < bits.Length; ++i)
            {
                var color = bits[i] == '1'
                    ? Rgba32.Black
                    : Rgba32.White;

                for (var j = i * codabarParams.LineWidth; j < (i + 1) * codabarParams.LineWidth; j++)
                {
                    for (var k = 0; k < codabarParams.LineHeight; k++)
                    {
                        image[j, k] = color;
                    }
                }
            }

            return image;
        }
    }
}
