﻿using System.Runtime.Serialization;

namespace MyHome.Core.Models.EnergySupplier;

public enum PriceLevel
{
    [EnumMember(Value = "NORMAL")]
    Normal,
    [EnumMember(Value = "CHEAP")]
    Cheap,
    [EnumMember(Value = "VERY_CHEAP")]
    VeryCheap,
    [EnumMember(Value = "EXPENSIVE")]
    Expensive,
    [EnumMember(Value = "VERY_EXPENSIVE")]
    VeryExpensive,
    [EnumMember(Value = "EXTREME")]
    Extreme
}