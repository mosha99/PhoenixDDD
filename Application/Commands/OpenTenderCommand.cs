using MediatR;

namespace Application.Commands;

public record OpenTenderCommand : IRequest
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime StartTimeFrame { get; set; }
    public DateTime EndTimeFrame { get; set; }
    public long StartPricingFrame { get; set; }
    public long EndPricingFrame { get; set; }
}