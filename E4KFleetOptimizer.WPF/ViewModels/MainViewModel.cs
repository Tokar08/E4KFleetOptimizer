using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using E4KFleetOptimizer.Core.Data;
using E4KFleetOptimizer.Core.Models;
using E4KFleetOptimizer.Core.Services;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace E4KFleetOptimizer.WPF.ViewModels;

public partial class MainViewModel : ObservableValidator
{
    private readonly IFleetOptimizationService _optimizationService;
    private readonly IThemeService _themeService;
    private readonly IShipReferenceProvider _shipReferenceProvider;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Range(0, int.MaxValue, ErrorMessage = "Бюджет не может быть отрицательным!")]
    private int _budget;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Range(1, int.MaxValue, ErrorMessage = "Цель по очкам должна быть больше нуля!")]
    private int _targetPoints = 100;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Range(1, int.MaxValue, ErrorMessage = "Нужен хотя бы 1 доступный слот!")]
    private int _maxIslandSlots;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ActiveResult))]
    private int _selectedTabIndex;

    [ObservableProperty]
    private int _selectedLevel = 1;

    [ObservableProperty]
    private int _shipsToAddCount = 1;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ActiveResult))]
    private OptimizationResult? _budgetResult;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ActiveResult))]
    private OptimizationResult? _targetResult;

    [ObservableProperty]
    private bool _isDarkTheme = true;

    [ObservableProperty]
    private string _errorMessage = "";

    [ObservableProperty]
    private IEnumerable<ShipLevelReference> _shipReferences;

    public OptimizationResult? ActiveResult => SelectedTabIndex == 0 ? BudgetResult : TargetResult;

    public ObservableCollection<int> AvailableLevels { get; } = new(Enumerable.Range(1, 10));

    public ObservableCollection<ShipGroup> CurrentFleet { get; } = new();

    public MainViewModel(IFleetOptimizationService optimizationService, IThemeService themeService, IShipReferenceProvider shipReferenceProvider)
    {
        _optimizationService = optimizationService;
        _themeService = themeService;
        _shipReferenceProvider = shipReferenceProvider;
        LoadShipReferences();
    }

    private List<int> GetCurrentShipsList()
    {
        return CurrentFleet
            .SelectMany(g => Enumerable.Repeat(g.Level, g.Count))
            .ToList();
    }

    [RelayCommand]
    private void CalculateByBudget()
    {
        ClearErrors();
        ValidateProperty(Budget, nameof(Budget));
        ValidateProperty(MaxIslandSlots, nameof(MaxIslandSlots));

        if (GetErrors(nameof(Budget)).Any() || GetErrors(nameof(MaxIslandSlots)).Any())
            return;

        ErrorMessage = string.Empty;
        BudgetResult = null;

        try
        {
            BudgetResult = _optimizationService.CalculateBestPath(Budget, GetCurrentShipsList(), MaxIslandSlots);

            if (BudgetResult == null)
                ErrorMessage = "Сбой: сервис оптимизации не смог сгенерировать результат!";
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Внутренняя ошибка расчета: {ex.Message}!";
        }
    }

    [RelayCommand]
    private void CalculateByTarget()
    {
        ClearErrors();
        ValidateProperty(TargetPoints, nameof(TargetPoints));
        ValidateProperty(MaxIslandSlots, nameof(MaxIslandSlots));

        if (GetErrors(nameof(TargetPoints)).Any() || GetErrors(nameof(MaxIslandSlots)).Any())
            return;

        ErrorMessage = string.Empty;
        TargetResult = null;

        try
        {
            TargetResult = _optimizationService.CalculateCostForTarget(TargetPoints, GetCurrentShipsList(), MaxIslandSlots);

            if (TargetResult == null)
                ErrorMessage = "Сбой: сервис оптимизации не смог сгенерировать результат!";
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Внутренняя ошибка расчета: {ex.Message}!";
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

    partial void OnIsDarkThemeChanged(bool value) => _themeService.SetTheme(value);

    private void LoadShipReferences()
    {
        try
        {
            ShipReferences = _shipReferenceProvider.LoadReferences();
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Ошибка загрузки справочника кораблей: {ex.Message}";
        }
    }

}