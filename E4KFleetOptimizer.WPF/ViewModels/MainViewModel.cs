using CommunityToolkit.Mvvm.ComponentModel;
using E4KFleetOptimizer.Core.Services;

namespace E4KFleetOptimizer.WPF.ViewModels;

public partial class MainViewModel : ObservableObject
{

    private readonly IFleetOptimizationService _optimizationService;

    [ObservableProperty]
    private int _budget;

    [ObservableProperty]
    private int _maxIslandSlots;

    public MainViewModel(IFleetOptimizationService optimizationService)
    {
        _optimizationService = optimizationService;
    }
}