using E4KFleetOptimizer.Core.Services;

namespace E4KFleetOptimizer.WPF.ViewModels;

public class MainViewModel
{
    private readonly IFleetOptimizationService _optimizationService;

    public MainViewModel(IFleetOptimizationService optimizationService)
    {
        _optimizationService = optimizationService;
    }
}