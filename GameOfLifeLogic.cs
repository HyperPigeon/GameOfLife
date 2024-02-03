using System;

namespace GameOfLife; 

public class GameOfLifeLogic(int width, int height, int seed){
    public int Width { get; private set; } = width;
    public int Height { get; private set; } = height;
    private bool[,] grid = new bool[width, height];
    private bool[,] nextGrid = new bool[width, height];
    private readonly int? Seed = seed;

    public void Initialize(){
        var random = Seed != null ? new Random((int)Seed) : new Random();
        for (int x = 0; x < Width; x++){
            for (int y = 0; y < Height; y++){
                grid[x, y] = random.Next(2) == 0;
            }
        }
    }

    public void NextGeneration(){
        for (int x = 0; x < Width; x++){
            for (int y = 0; y < Height; y++){
                int aliveNeighbors = CountLivingNeighbors(x, y);
                if (grid[x, y]){
                    // living cell with exactly 2 or 3 living neighbors stays alive
                    nextGrid[x, y] = aliveNeighbors == 2 || aliveNeighbors == 3;
                }
                else{
                    // Dead cell with three living neighbors becomes alive
                    nextGrid[x, y] = aliveNeighbors == 3;
                }
            }
        }
        // Swap to next grid
        (grid, nextGrid) = (nextGrid, grid);
    }

    private int CountLivingNeighbors(int x, int y){
        int count = 0;
        for (int i = -1; i <= 1; i++){
            for (int j = -1; j <= 1; j++){
                if (i == 0 && j == 0) continue; 
                int nx = (x + i + Width) % Width;
                int ny = (y + j + Height) % Height;
                if (grid[nx, ny]){
                    count++;
                }
            }
        }
        return count;
    }

    // Get current state of a cell
    public bool IsCellAlive(int x, int y){
        return grid[x, y];
    }
}