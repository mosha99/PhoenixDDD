using MediatR;

namespace Application.Commands;

public record CancelTenderCommand(long TenderId) : IRequest;