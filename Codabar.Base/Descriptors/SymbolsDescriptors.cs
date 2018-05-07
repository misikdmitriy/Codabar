using System.Linq;
using System.Reflection;
using Codabar.Base.Descriptors.Contracts;

namespace Codabar.Base.Descriptors
{
    /// <summary>
    /// use http://www.barcodeman.com/info/codabar.php
    /// </summary>
    public class SymbolDescriptor : ISymbolDescriptor
    {
        public static readonly ISymbolDescriptor ZeroDescriptor = new SymbolDescriptor('0', 0b0001, 0b001);
        public static readonly ISymbolDescriptor OneDescriptor = new SymbolDescriptor('1', 0b0010, 0b001);
        public static readonly ISymbolDescriptor TwoDescriptor = new SymbolDescriptor('2', 0b0001, 0b010);
        public static readonly ISymbolDescriptor ThreeDescriptor = new SymbolDescriptor('3', 0b1000, 0b100);
        public static readonly ISymbolDescriptor FourDescriptor = new SymbolDescriptor('4', 0b0100, 0b001);
        public static readonly ISymbolDescriptor FiveDescriptor = new SymbolDescriptor('5', 0b1000, 0b001);
        public static readonly ISymbolDescriptor SixDescriptor = new SymbolDescriptor('6', 0b0001, 0b100);
        public static readonly ISymbolDescriptor SevenDescriptor = new SymbolDescriptor('7', 0b0010, 0b100);
        public static readonly ISymbolDescriptor EightDescriptor = new SymbolDescriptor('8', 0b0100, 0b100);
        public static readonly ISymbolDescriptor NineDescriptor = new SymbolDescriptor('9', 0b1000, 0b010);
        public static readonly ISymbolDescriptor MinusDescriptor = new SymbolDescriptor('-', 0b0010, 0b010);
        public static readonly ISymbolDescriptor DollarDescriptor = new SymbolDescriptor('$', 0b0100, 0b010);
        public static readonly ISymbolDescriptor ColonDescriptor = new SymbolDescriptor(':', 0b1011, 0b000);
        public static readonly ISymbolDescriptor SlashDescriptor = new SymbolDescriptor('/', 0b1101, 0b000);
        public static readonly ISymbolDescriptor DotDescriptor = new SymbolDescriptor('.', 0b1110, 0b000);
        public static readonly ISymbolDescriptor PlusDescriptor = new SymbolDescriptor('+', 0b0111, 0b000);
        public static readonly ISymbolDescriptor ADescriptor = new SymbolDescriptor('a', 0b0100, 0b011);
        public static readonly ISymbolDescriptor BDescriptor = new SymbolDescriptor('b', 0b0001, 0b110);
        public static readonly ISymbolDescriptor CDescriptor = new SymbolDescriptor('c', 0b0001, 0b011);
        public static readonly ISymbolDescriptor DDescriptor = new SymbolDescriptor('d', 0b0010, 0b011);
        public static readonly ISymbolDescriptor TDescriptor = new SymbolDescriptor('t', 0b0100, 0b011);
        public static readonly ISymbolDescriptor NDescriptor = new SymbolDescriptor('n', 0b0001, 0b110);
        public static readonly ISymbolDescriptor AsterixDescriptor = new SymbolDescriptor('*', 0b0001, 0b011);
        public static readonly ISymbolDescriptor EDescriptor = new SymbolDescriptor('e', 0b0010, 0b011);

        public static char[] AllowedSymbols;

        public char Symbol { get; }
        public byte Bars { get; }
        public byte Spaces { get; }

        static SymbolDescriptor()
        {
            AllowedSymbols = typeof(SymbolDescriptor)
                .GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(t => typeof(ISymbolDescriptor).IsAssignableFrom(t.FieldType))
                .Select(x => ((ISymbolDescriptor)x.GetValue(null)).Symbol)
                .ToArray();
        }

        public SymbolDescriptor(char symbol, byte bars, byte spaces)
        {
            Bars = bars;
            Spaces = spaces;
            Symbol = symbol;
        }
    }
}
