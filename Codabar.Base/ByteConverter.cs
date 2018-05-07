namespace Codabar.Base
{
    public class ByteConverter
    {
        public int GetBit(int number, int num)
        {
            return (number >> num) & 1;
        }
    }
}
