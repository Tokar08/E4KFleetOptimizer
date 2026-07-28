using System.ComponentModel;

namespace E4KFleetOptimizer.Core.Models;

public class ShipLevelReference
{
    [DisplayName("Уровень")]
    public int Level { get; init; }

    [DisplayName("Стоимость улучшения (Аквамарин)")]
    public int UpgradeCost { get; init; }

    [DisplayName("Прирост очков (Дельта)")]
    public int PointsDelta { get; init; }

    [DisplayName("Цена за 1 очко")]
    public double PricePerPoint { get; init; }
}