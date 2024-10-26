﻿using Application.DTOs;
using Application.Queries;
using BuildingBlocks.Exception;
using Contractor;
using MediatR;
using SharedIdentity;
using Tender;

namespace Services;

public class GetContractorQueryHandler(ITenderRepository tenderRepository, IContractorRepository contractorRepository) : IRequestHandler<GetContractorQuery, ContractorDto>
{
    public async Task<ContractorDto> Handle(GetContractorQuery request, CancellationToken cancellationToken)
    {
        var contractor = await contractorRepository.GetByIdAsync(new ContractorId(request.ContractorId));

        if (contractor == null) throw new LogicException("Contractor Not Find");

        var tenders = await tenderRepository.GetByContractor(contractor.Id);

        var tendersDto = tenders.Select(t => TenderDto.CreateInstance(t, [contractor]));

        return ContractorDto.CreateInstance(contractor, tendersDto);
    }
}