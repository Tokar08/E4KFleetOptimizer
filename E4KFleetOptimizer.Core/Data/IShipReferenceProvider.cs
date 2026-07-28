using E4KFleetOptimizer.Core.Models;

namespace E4KFleetOptimizer.Core.Data;

public interface IShipReferenceProvider
{
    IEnumerable<ShipLevelReference> LoadReferences();
}
