using BuildingBlocks;
using Contractor.Exception;
using SharedIdentity;

namespace Contractor;

public class Contractor : AggregateBase<ContractorId>
{
    public static Contractor CreateInstance(string name, string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length < 3) throw new InvalidContractorNameException();
        if (string.IsNullOrWhiteSpace(phoneNumber) || phoneNumber.Length != 11) throw new InvalidContractorPhoneNumberException();
        return new Contractor()
        {
            Name = name,
            PhoneNumber = phoneNumber
        };
    }

    public string Name { get; private set; } = null!;
    public string PhoneNumber { get; private set; } = null!;
}