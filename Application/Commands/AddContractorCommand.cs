using MediatR;

namespace Application.Commands;

public record AddContractorCommand(string Name, string PhoneNumber) : IRequest;