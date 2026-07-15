using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using E4KFleetOptimizer.Core.Models; 
using E4KFleetOptimizer.Core.Services;

namespace E4KFleetOptimizer.WPF.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly IFleetOptimizationService _optimizationService;

    [ObservableProperty]
    private int _budget;

    [ObservableProperty]
    private int _maxIslandSlots;

    [ObservableProperty]
    private OptimizationResult? _calculationResult;

    [ObservableProperty]
    private List<int> _currentShips = new();

    public MainViewModel(IFleetOptimizationService optimizationService)
    {
        _optimizationService = optimizationService;
    }

    [RelayCommand]
    private void CalculateOptimization()
    {
        if (Budget <= 0 || MaxIslandSlots <= 0)
        {
            return;
        }

        CalculationResult = _optimizationService.CalculateBestPath(Budget, CurrentShips, MaxIslandSlots);
    }
}