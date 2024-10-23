using Application.Commands;
using Contractor;
using MediatR;

namespace Services;

public class AddContractorCommandHandler(IContractorRepository contractorRepository) : IRequestHandler<AddContractorCommand>
{
    public async Task Handle(AddContractorCommand request, CancellationToken cancellationToken)
    {
        var tender = Contractor.Contractor.CreateInstance(request.Name,request.PhoneNumber);

        await contractorRepository.AddContractor(tender);
    }
}