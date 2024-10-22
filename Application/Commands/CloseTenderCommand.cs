using MediatR;

namespace Application.Commands;

public record CloseTenderCommand(long TenderId) : IRequest;