using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using E4KFleetOptimizer.Core.Data;
using E4KFleetOptimizer.Core.Models;

namespace E4KFleetOptimizer.WPF.Data;

public class CsvShipReferenceProvider : IShipReferenceProvider
{
    public IEnumerable<ShipLevelReference> LoadReferences()
    {
        var references = new List<ShipLevelReference>();
        var filePath = Path.Combine(AppContext.BaseDirectory, "Resources", "ShipDataTable.csv");

        if (!File.Exists(filePath))
            return references;

        var lines = File.ReadAllLines(filePath);
        var culture = new CultureInfo("ru-RU");

        foreach (var line in lines.Skip(1))
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            var parts = line.Split(';');

            if (parts.Length >= 5)
            {
                var levelStr = Regex.Replace(parts[1], @"[^\d]", "");
                var upgradeCostStr = Regex.Replace(parts[2], @"[^\d]", "");
                var pointsDeltaStr = Regex.Replace(parts[3], @"[^\d]", "");
                var pricePerPointStr = Regex.Replace(parts[4], @"[^\d,]", "");

                if (int.TryParse(levelStr, out int level) &&
                    int.TryParse(upgradeCostStr, out int upgradeCost) &&
                    int.TryParse(pointsDeltaStr, out int pointsDelta) &&
                    double.TryParse(pricePerPointStr, NumberStyles.Any, culture, out double pricePerPoint))
                {
                    references.Add(new ShipLevelReference
                    {
                        Level = level,
                        UpgradeCost = upgradeCost,
                        PointsDelta = pointsDelta,
                        PricePerPoint = pricePerPoint
                    });
                }
            }
        }

        return references;
    }
}