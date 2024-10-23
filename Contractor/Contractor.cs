using BuildingBlocks;
using BuildingBlocks.Exception;
using SharedIdentity;

namespace Contractor;

public class Contractor : AggregateBase<ContractorId>
{
    public static Contractor CreateInstance(string name, string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length < 3) throw new LogicException("Name Is Required And Must Grater Then 2 Character");
        if (string.IsNullOrWhiteSpace(phoneNumber) || phoneNumber.Length != 11) throw new LogicException("PhoneNumber Is Required And Must Then 11 Character");
        return new Contractor()
        {
            Name = name,
            PhoneNumber = phoneNumber
        };
    }

    public string Name { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
}