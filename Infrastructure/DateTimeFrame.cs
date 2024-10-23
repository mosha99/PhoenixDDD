using BuildingBlocks.Exception;

namespace BuildingBlocks;

public record DateTimeFrame
{
    public DateTime Start { get; private set; }
    public DateTime End { get; private set; }
    private DateTimeFrame() { }
    public DateTimeFrame(DateTime start, DateTime end)
    {
        if (start >= end) throw new LogicException("Start Date Must before then End Date");
        if (start == DateTime.MinValue) throw new LogicException("Start Date is required");
        if (end == DateTime.MinValue) throw new LogicException("End Date is required");
        Start = start;
        End = end;
    }

    public bool IsValidDate(DateTime date) => Start <= date && End >= date;
}