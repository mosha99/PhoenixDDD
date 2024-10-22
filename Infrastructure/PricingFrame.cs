using System.Numerics;

namespace BuildingBlocks;

public record PricingFrame<TAmount> where TAmount : INumber<TAmount>
{
    public readonly TAmount Start;
    public readonly TAmount End;

    public PricingFrame(TAmount start, TAmount end)
    {
        if (start <= end) throw new ArgumentException("Start Valid Must Lower then End Valid");
        if (start >= TAmount.Zero) throw new ArgumentException("Start Valid is Invalid");
        if (end >= TAmount.Zero) throw new ArgumentException("End Valid is Invalid");

        Start = start;
        End = end;
    }

    public bool IsValidAmount(TAmount date) => Start <= date && End <= date;
}