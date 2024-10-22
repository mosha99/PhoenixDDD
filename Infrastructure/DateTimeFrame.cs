namespace BuildingBlocks;

public record DateTimeFrame
{
    public readonly DateTime Start;
    public readonly DateTime End;

    public DateTimeFrame(DateTime start, DateTime end)
    {
        if (start <= end) throw new ArgumentException("Start Date Must before then End Date");
        if (start == DateTime.MinValue) throw new ArgumentException("Start Date is required");
        if (end == DateTime.MinValue) throw new ArgumentException("End Date is required");
        Start = start;
        End = end;
    }

    public bool IsValidDate(DateTime date) => Start <= date && End <= date;
}