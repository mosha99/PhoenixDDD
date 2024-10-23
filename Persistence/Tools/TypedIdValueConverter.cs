using BuildingBlocks;
using BuildingBlocks.Exception;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Persistence.Tools;

public class TypedIdValueConverter<TTypedIdValue>()
    : ValueConverter<TTypedIdValue, long>(id => id.Id, value => Create(value))
    where TTypedIdValue : IdentityBase, IIdentityCreator
{
    private static TTypedIdValue Create(long id) => TTypedIdValue.CreateInstance(id) as TTypedIdValue ?? throw new LogicException();
}