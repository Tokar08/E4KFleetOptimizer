using E4KFleetOptimizer.WPF.Data;
using System.Windows;


namespace E4KFleetOptimizer.WPF;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        try
        {
            var provider = new JsonShipDataProvider();
            var data = provider.GetLevelData().ToList();


            string basePath = AppContext.BaseDirectory;
            string filePath = System.IO.Path.Combine(basePath, "Resources", "ships.json");

            MessageBox.Show(
                $"Файл успешно считан: \n\nВесь путь:\n{filePath}\n\nЗагружено уровней кораблей: {data.Count}",
                "Успех",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                $"Произошла ошибка при чтении:\n{ex.Message}",
                "Ошибка",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }
}