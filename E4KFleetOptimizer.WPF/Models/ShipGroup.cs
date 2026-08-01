using CommunityToolkit.Mvvm.ComponentModel;

namespace E4KFleetOptimizer.WPF.ViewModels;

public partial class ShipGroup : ObservableObject
{
    [ObservableProperty]
    private int _level;

    [ObservableProperty]
    private int _count;
}