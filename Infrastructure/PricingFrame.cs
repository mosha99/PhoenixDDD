using System.Numerics;
using BuildingBlocks.Exception;

namespace BuildingBlocks;

public record PricingFrame<TAmount> where TAmount : INumber<TAmount>
{
    public TAmount Start { get; private set; }
    public TAmount End { get; private set; }

    public PricingFrame(TAmount start, TAmount end)
    {
        if (start >= end) throw new LogicException("Start Amount Must Lower then End Amount");
        if (start <= TAmount.Zero) throw new LogicException("Start Amount is Invalid");
        if (end <= TAmount.Zero) throw new LogicException("End Amount is Invalid");

        Start = start;
        End = end;
    }

    public bool IsValidAmount(TAmount date) => Start <= date && End >= date;
}