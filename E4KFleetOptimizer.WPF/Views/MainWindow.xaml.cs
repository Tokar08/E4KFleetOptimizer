using E4KFleetOptimizer.WPF.ViewModels;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;


namespace E4KFleetOptimizer.WPF;

public partial class MainWindow : Window
{
    private static readonly Regex _numericRegex = new Regex("[^0-9]+", RegexOptions.Compiled);
    public MainWindow(MainViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }

    private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
    {
        e.Handled = _numericRegex.IsMatch(e.Text);
    }

    private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
    {
        Process.Start(new ProcessStartInfo
        {
            FileName = e.Uri.AbsoluteUri,
            UseShellExecute = true
        });
        e.Handled = true;
    }

    private async void CopyEmail_Click(object sender, RoutedEventArgs e)
    {
        Clipboard.SetText("ivan.tokar.git@gmail.com");


        if (sender is Button btn && btn.ToolTip is ToolTip toolTip) 
        {
            toolTip.PlacementTarget = btn;
            toolTip.Content = "Скопировано!";
            toolTip.IsOpen = true;

            await Task.Delay(1500);

            toolTip.IsOpen = false;
            toolTip.Content = "Скопировать почту";
        }
    }
}