using System.Collections.Generic;

namespace E4KFleetOptimizer.Core.Models;

public class OptimizationResult
{
    public List<UpgradeStep> Steps { get; init; } = [];
    public int TotalSpent { get; init; }
    public int RemainingBudget { get; init; }
    public int TotalPointsGained { get; init; }

    public List<int> FinalFleet { get; init; } = [];
}