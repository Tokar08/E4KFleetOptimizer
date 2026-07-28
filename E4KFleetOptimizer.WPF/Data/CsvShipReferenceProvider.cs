using E4KFleetOptimizer.Core.Data;
using E4KFleetOptimizer.Core.Models;
using System.Globalization;
using System.IO;

namespace E4KFleetOptimizer.WPF.Data;

public class CsvShipReferenceProvider : IShipReferenceProvider
{
    public IEnumerable<ShipLevelReference> LoadReferences()
    {
        var references = new List<ShipLevelReference>();
        string basePath = AppContext.BaseDirectory;
        string filePath = Path.Combine(basePath, "Resources", "ShipDataTable.csv");

        if (!File.Exists(filePath))
        {
            return references;
        }

        var lines = File.ReadAllLines(filePath);
        var culture = CultureInfo.GetCultureInfo("ru-RU");

        for (int i = 1; i < lines.Length; i++)
        {
            var line = lines[i];

            if (string.IsNullOrWhiteSpace(line)) 
                continue;

            var parts = line.Split(';');
            static string CleanNumber(string input) => input.Replace(" ", "").Replace("\u00A0", "");

            try
            {
                var step = new ShipLevelReference
                {
                    Level = int.Parse(parts[1]),
                    UpgradeCost = int.Parse(CleanNumber(parts[2])),
                    PointsDelta = int.Parse(CleanNumber(parts[3])),
                    PricePerPoint = double.Parse(parts[4], culture)
                };

                references.Add(step);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка парсинга строки {i}: {ex.Message}");
            }
        }

        return references;
    }
}
