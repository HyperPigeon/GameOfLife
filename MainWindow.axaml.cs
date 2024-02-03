using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using Avalonia.Media;
using Avalonia.Controls.Shapes;
using System;

namespace GameOfLife;

public partial class MainWindow : Window {
    private readonly GameOfLifeLogic gameLogic;
    private readonly DispatcherTimer gameTimer;
    private readonly Canvas gameCanvas;
    private readonly int Seed; 
    private const int CellSize = 10; // Size of each cell in pixels

    public MainWindow(int seed){
        InitializeComponent();
        Seed = seed; 
        gameCanvas = this.FindControl<Canvas>("GameCanvas");
        if (gameCanvas == null){
            throw new InvalidOperationException("GameCanvas not found.");
        }

        int canvasWidth = 800;
        int canvasHeight = 600;
        gameLogic = new GameOfLifeLogic(canvasWidth/CellSize, canvasHeight/CellSize, Seed);
        gameLogic.Initialize(); // Initialize the game with random states

        // Setup the game timer
        gameTimer = new DispatcherTimer{
            // Update every 100ms
            Interval = TimeSpan.FromMilliseconds(100) 
        };
        gameTimer.Tick += GameTimerTick;
        gameTimer.Start();
    }

    private void InitializeComponent(){
        AvaloniaXamlLoader.Load(this);
    }

    private void GameTimerTick(object? sender, EventArgs e){
        gameLogic.NextGeneration();
        DrawGame();
    }

    private void DrawGame(){
        gameCanvas.Children.Clear();

        for (int x = 0; x < gameLogic.Width; x++){
            for (int y = 0; y < gameLogic.Height; y++){
                if (gameLogic.IsCellAlive(x, y)){
                    var rect = new Rectangle{
                        Fill = Brushes.White,
                        Width = CellSize,
                        Height = CellSize,
                        [Canvas.LeftProperty] = x * CellSize,
                        [Canvas.TopProperty] = y * CellSize
                    };
                    gameCanvas.Children.Add(rect);
                }
            }
        }
    }
}