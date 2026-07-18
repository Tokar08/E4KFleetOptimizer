using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using E4KFleetOptimizer.Core.Models; 
using E4KFleetOptimizer.Core.Services;
using System.Collections.ObjectModel;

namespace E4KFleetOptimizer.WPF.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly IFleetOptimizationService _optimizationService;

    [ObservableProperty]
    private int _budget;

    [ObservableProperty]
    private int _maxIslandSlots;

    [ObservableProperty]
    private int _selectedLevel = 1;

    [ObservableProperty]
    private int _shipsToAddCount = 1;

    [ObservableProperty]
    private OptimizationResult? _calculationResult;

    [ObservableProperty]
    private string _errorMessage = "";

    public ObservableCollection<int> AvailableLevels { get; } = new(Enumerable.Range(1, 10));

    public ObservableCollection<ShipGroup> CurrentFleet { get; } = new();

    public MainViewModel(IFleetOptimizationService optimizationService)
    {
        _optimizationService = optimizationService;
    }

    [RelayCommand]
    private void CalculateOptimization()
    {
    }

    [RelayCommand]
    private void ChangeShipsCount(string amountStr)
    {
        if (int.TryParse(amountStr, out int amount))
        {
            var newCount = ShipsToAddCount + amount;
            if (newCount >= 1)
            {
                ShipsToAddCount = newCount;
            }
        }
    }
}