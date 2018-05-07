namespace Codabar.Base
{
    public class CodabarParams
    {
        public int LineHeight { get; }
        public int LineWidth { get; }

        public CodabarParams(int lineWidth, int lineHeight)
        {
            LineHeight = lineHeight;
            LineWidth = lineWidth;
        }
    }
}
