using E4KFleetOptimizer.WPF.Data;
using E4KFleetOptimizer.WPF.ViewModels;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;


namespace E4KFleetOptimizer.WPF;

public partial class MainWindow : Window
{
    public MainWindow(MainViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }

    private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
    {
        Regex regex = new Regex("[^0-9]+");
        e.Handled = regex.IsMatch(e.Text);
    }
}