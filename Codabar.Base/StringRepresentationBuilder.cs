using System;
using System.Linq;
using System.Reflection;
using System.Text;
using Codabar.Base.Descriptors;
using Codabar.Base.Descriptors.Contracts;

namespace Codabar.Base
{
    public class StringRepresentationBuilder
    {
        public string ToCodabar(string input)
        {
            var allAllowed = input.All(s => SymbolDescriptor.AllowedSymbols.Contains(s));
            if (!allAllowed)
            {
                throw new ArgumentException(input);
            }

            var sb = new StringBuilder();
            foreach (var symbol in input)
            {
                sb.Append(ToCodabar(symbol));
            }
            

            return sb.Remove(sb.Length - 1, 1).ToString();
        }

        private string ToCodabar(char symbol)
        {
            var sb = new StringBuilder();
            var i = 3;
            var byteConverter = new ByteConverter();
            var descriptor = typeof(SymbolDescriptor)
                .GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(t => typeof(ISymbolDescriptor).IsAssignableFrom(t.FieldType))
                .Select(x => ((ISymbolDescriptor) x.GetValue(null)))
                .First(x => x.Symbol.Equals(symbol));

            while (i >= 0)
            {
                sb.Append(byteConverter.GetBit(descriptor.Bars, i) == 1
                    ? "11"
                    : "1");

                if (i > 0)
                {
                    sb.Append(byteConverter.GetBit(descriptor.Spaces, i - 1) == 1
                        ? "00"
                        : "0");
                }

                --i;
            }

            sb.Append("0");

            return sb.ToString();
        }
    }
}
