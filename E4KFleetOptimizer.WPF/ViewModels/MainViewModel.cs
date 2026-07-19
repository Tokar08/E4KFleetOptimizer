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
        ErrorMessage = string.Empty;
        CalculationResult = null;
        if (Budget < 0)
        {
            ErrorMessage = "Бюджет не может быть отрицательным.";
            return;
        }

        if (MaxIslandSlots <= 0)
        {
            ErrorMessage = "Для постройки флота нужен хотя бы 1 доступный слот на острове.";
            return;
        }

        try
        {
            var currentShips = CurrentFleet
             .SelectMany(g => Enumerable.Repeat(g.Level, g.Count))
             .ToList();

            var result = _optimizationService.CalculateBestPath(Budget, currentShips, MaxIslandSlots);

            if (result != null)
            {
                CalculationResult = result;
            }
            else
            {
                ErrorMessage = "Сбой: сервис оптимизации не смог сгенерировать результат.";
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Внутренняя ошибка расчета: {ex.Message}";
        }
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


    [RelayCommand]
    private void AddToFleet()
    {
        var existingGroup = CurrentFleet.FirstOrDefault(g => g.Level == SelectedLevel);

        if (existingGroup != null)
        {
            var newCount = existingGroup.Count + ShipsToAddCount;
            CurrentFleet.Remove(existingGroup);
            CurrentFleet.Add(new ShipGroup { Level = SelectedLevel, Count = newCount });
        }
        else
        {
            CurrentFleet.Add(new ShipGroup { Level = SelectedLevel, Count = ShipsToAddCount });
        }

        ShipsToAddCount = 1;
    }


    [RelayCommand]
    private void RemoveFromFleet(ShipGroup group) 
    {
        if (group != null) 
        {
            CurrentFleet.Remove(group);
        }
    }
}