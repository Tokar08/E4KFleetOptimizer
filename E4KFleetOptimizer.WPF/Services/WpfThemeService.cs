using System;
using System.Windows;
using E4KFleetOptimizer.Core.Services;

namespace E4KFleetOptimizer.WPF.Services;

public class WpfThemeService : IThemeService
{
    public void SetTheme(bool isDark)
    {
        string themeName = isDark ? "Dark" : "Light";

        var newTheme = new ResourceDictionary
        {
            Source = new Uri($"Themes/{themeName}.xaml", UriKind.Relative)
        };

        Application.Current.Resources.MergedDictionaries[0] = newTheme;
    }
}