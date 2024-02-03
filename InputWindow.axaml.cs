using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace GameOfLife;

 public partial class InputWindow : Window {
    public int Seed { get; private set; } = new Random().Next(int.MaxValue);


    public InputWindow() {
        InitializeComponent();
    }

    private void InitializeComponent() {
        AvaloniaXamlLoader.Load(this);
    }

    private void SubmitButton_Click(object? sender, RoutedEventArgs e) {
        
        if (int.TryParse(this.FindControl<TextBox>("SeedInput").Text, out int seed)){
            Seed = seed;
        }

        var mainWindow = new MainWindow(Seed);
        mainWindow.Show();
        Close();
    }
}