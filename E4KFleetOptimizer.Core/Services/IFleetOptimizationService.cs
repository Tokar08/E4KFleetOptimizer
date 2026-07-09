using E4KFleetOptimizer.Core.Models;
using System.Collections.Generic;

namespace E4KFleetOptimizer.Core.Services;

public interface IFleetOptimizationService
{
    OptimizationResult CalculateBestPath(int budget, List<int> currentShips, int maxIslandSlots);
    OptimizationResult CalculateCostForTarget(int targetPointsDelta, List<int> currentShips, int maxIslandSlots);
}