﻿using System.Text.Json.Serialization;

namespace BuildingBlocks;

public abstract class IdentityBase(long id)
{
    [JsonPropertyName("Id")]
    public long Id { get; private set; } = id;

    public static string GetSequenceBase<TId>()
        => GetSequenceBase(typeof(TId));
    public static string GetSequenceBase(Type idType)
        => $"{idType.Name}_Seq";

    public static implicit operator long(IdentityBase? identityBase) => identityBase?.Id ?? 0;
}