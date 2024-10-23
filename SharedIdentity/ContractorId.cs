using BuildingBlocks;

namespace SharedIdentity;

public class ContractorId(long id) : IdentityBase(id), IIdentityCreator
{

    public static IdentityBase CreateInstance(long id)
        => new ContractorId(id);
}