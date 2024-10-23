using BuildingBlocks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Persistence.Tools;

public class TypedIdValueComparer<TTypedIdValue>()
    : ValueComparer<TTypedIdValue>((left, right) => left != null && right != null && left.Id.Equals(right.Id), o => o.Id.GetHashCode())
    where TTypedIdValue : IdentityBase, IIdentityCreator;