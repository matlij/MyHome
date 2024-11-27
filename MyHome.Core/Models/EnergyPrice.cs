﻿using Tibber.Sdk;

namespace MyHome.Core.Models;

public enum RelativePriceLevel
{
    Normal,
    VeryLow,
    Low,
    High,
    VeryHigh,
}

public record EnergyPrice
{
    public DateTime Time { get; init; }
    public decimal Price { get; init; }
    public PriceLevel PriceLevel { get; init; }
    public RelativePriceLevel RelativePriceLevel { get; init; } = RelativePriceLevel.Normal;
}