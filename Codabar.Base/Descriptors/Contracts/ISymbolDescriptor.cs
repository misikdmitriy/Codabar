namespace Codabar.Base.Descriptors.Contracts
{
    public interface ISymbolDescriptor
    {
        char Symbol { get; }
        byte Bars { get; }
        byte Spaces { get; }
    }
}
