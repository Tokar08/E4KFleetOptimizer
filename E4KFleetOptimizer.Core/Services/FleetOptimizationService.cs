using E4KFleetOptimizer.Core.Data;
using E4KFleetOptimizer.Core.Models;

namespace E4KFleetOptimizer.Core.Services;

public class FleetOptimizationService : IFleetOptimizationService
{
    private readonly IShipDataProvider _dataProvider;

    public FleetOptimizationService(IShipDataProvider dataProvider)
    {
        _dataProvider = dataProvider;
    }

    public OptimizationResult CalculateBestPath(int budget, List<int> currentShips, int maxIslandSlots)
    {
        var referenceData = _dataProvider.GetLevelData().ToDictionary(x => x.Level);
        var fleet = InitializeFleet(currentShips, maxIslandSlots);

        int totalSpent = 0;
        int totalPointsGained = 0;
        var rawUpgrades = new List<(int From, int To, int Cost, int Points)>();

        while (budget > 0)
        {
            int bestShipIdx = FindBestUpgrade(fleet, referenceData);

            if (bestShipIdx == -1)
                break;

            int nextLvlToApply = fleet[bestShipIdx] + 1;
            var targetData = referenceData[nextLvlToApply];

            if (budget >= targetData.UpgradeCost)
            {
                budget -= targetData.UpgradeCost;
                totalSpent += targetData.UpgradeCost;
                totalPointsGained += targetData.PointsDelta;

                rawUpgrades.Add((fleet[bestShipIdx], nextLvlToApply, targetData.UpgradeCost, targetData.PointsDelta));
                fleet[bestShipIdx]++;
            }
            else
            {
                break;
            }
        }

        return new OptimizationResult
        {
            Steps = GroupUpgrades(rawUpgrades),
            TotalSpent = totalSpent,
            RemainingBudget = budget,
            TotalPointsGained = totalPointsGained,
            FinalFleet = fleet.Where(x => x > 0).ToList()
        };
    }

    public OptimizationResult CalculateCostForTarget(int targetPointsDelta, List<int> currentShips, int maxIslandSlots)
    {
        var referenceData = _dataProvider.GetLevelData().ToDictionary(x => x.Level);
        var fleet = InitializeFleet(currentShips, maxIslandSlots);

        int totalSpent = 0;
        int totalPointsGained = 0;
        var rawUpgrades = new List<(int From, int To, int Cost, int Points)>();

        while (totalPointsGained < targetPointsDelta)
        {
            int bestShipIdx = FindBestUpgrade(fleet, referenceData);

            if (bestShipIdx == -1)
                break;

            int nextLvlToApply = fleet[bestShipIdx] + 1;
            var targetData = referenceData[nextLvlToApply];

            totalSpent += targetData.UpgradeCost;
            totalPointsGained += targetData.PointsDelta;
            rawUpgrades.Add((fleet[bestShipIdx], nextLvlToApply, targetData.UpgradeCost, targetData.PointsDelta));
            fleet[bestShipIdx]++;
        }

        return new OptimizationResult
        {
            Steps = GroupUpgrades(rawUpgrades),
            TotalSpent = totalSpent,
            RemainingBudget = 0,
            TotalPointsGained = totalPointsGained,
            FinalFleet = fleet.Where(x => x > 0).ToList()
        };
    }

    private static List<int> InitializeFleet(List<int> currentShips, int maxIslandSlots)
    {
        int emptySlots = maxIslandSlots - currentShips.Count;
        return currentShips.Concat(new int[emptySlots]).ToList();
    }

    private static int FindBestUpgrade(List<int> fleet, Dictionary<int, ShipLevelData> referenceData)
    {
        var bestUpgrade = fleet
            .Select((level, index) => new { Level = level, Index = index })
            .Where(x => x.Level < 10)
            .Select(x => new
            {
                x.Index,
                HasData = referenceData.TryGetValue(x.Level + 1, out var data),
                Data = data
            })
            .Where(x => x.HasData)
            .OrderBy(x => x.Data.CostPerPoint)
            .ThenBy(x => x.Data.UpgradeCost)
            .FirstOrDefault();

        return bestUpgrade?.Index ?? -1;
    }

    private static List<UpgradeStep> GroupUpgrades(List<(int From, int To, int Cost, int Points)> rawUpgrades)
    {
        return rawUpgrades
            .GroupBy(u => new { u.From, u.To })
            .Select(g => new UpgradeStep
            {
                FromLevel = g.Key.From,
                ToLevel = g.Key.To,
                ShipsCount = g.Count(),
                TotalCost = g.Sum(x => x.Cost),
                TotalPointsGained = g.Sum(x => x.Points)
            })
            .OrderBy(s => s.FromLevel)
            .ToList();
    }
}