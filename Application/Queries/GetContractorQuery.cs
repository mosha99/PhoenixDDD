using Application.DTOs;
using MediatR;

namespace Application.Queries;

public record GetContractorQuery(long ContractorId) : IRequest<ContractorDto>;