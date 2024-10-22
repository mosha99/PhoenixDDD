using Application.DTOs;
using MediatR;

namespace Application.Queries;

public record GetTenderQuery(long TenderId) : IRequest<TenderDto>;