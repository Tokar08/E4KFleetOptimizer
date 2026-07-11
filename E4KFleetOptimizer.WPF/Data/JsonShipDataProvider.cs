using E4KFleetOptimizer.Core.Data;
using E4KFleetOptimizer.Core.Models;
using System.IO;
using System.Text.Json;


namespace E4KFleetOptimizer.WPF.Data;

public class JsonShipDataProvider : IShipDataProvider
{
    private static readonly JsonSerializerOptions _jsonOptions = new()
{
        PropertyNameCaseInsensitive = true
    };

    public IEnumerable<ShipLevelData> GetLevelData()
    {
        string basePath = AppContext.BaseDirectory;
        string filePath = Path.Combine(basePath, "Resources", "ships.json");

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"Ship level data file not found at path: {filePath}");
        }

        string json = File.ReadAllText(filePath);
        var data = JsonSerializer.Deserialize<List<ShipLevelData>>(json, _jsonOptions);
        return data ?? [];
    }
}
