namespace E4KFleetOptimizer.Core.Models;

public record ShipLevelData(
    int Level,
    int UpgradeCost,
    int PointsDelta)
{
    public double CostPerPoint => PointsDelta == 0 ? 0 : (double)UpgradeCost / PointsDelta;
}