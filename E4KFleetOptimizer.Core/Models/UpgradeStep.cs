namespace E4KFleetOptimizer.Core.Models;

public class UpgradeStep
{
    public int FromLevel { get; init; }
    public int ToLevel { get; init; }
    public int ShipsCount { get; set; }
    public int TotalCost { get; set; }
    public int TotalPointsGained { get; set; }
}