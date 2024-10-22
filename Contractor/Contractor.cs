using BuildingBlocks;
using SharedIdentity;

namespace Contractor;

public class Contractor(string name, string phoneNumber) : AggregateBase<ContractorId>
{
    public string Name { get; set; } = name;
    public string PhoneNumber { get; set; } = phoneNumber;
}