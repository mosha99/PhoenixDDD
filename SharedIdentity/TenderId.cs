using BuildingBlocks;

namespace SharedIdentity;

public class TenderId(long id) : IdentityBase(id), IIdentityCreator
{
    public static IdentityBase CreateInstance(long id)
        => new TenderId(id);
}