namespace BuildingBlocks;

public interface IIdentityCreator
{
    static abstract IdentityBase CreateInstance(long id);
}