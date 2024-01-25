using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

class SnakeGame
{
    static int width = 20;
    static int height = 10;
    static int score = 0;
    static bool gameOver = false;
    static char[,] matrix = new char[width, height];
    static List<int> snakeX = new List<int>();
    static List<int> snakeY = new List<int>();
    static Random random = new Random();
    static int foodX;
    static int foodY;

    static void Main()
    {
        Console.Title = "Snake Game";
        Console.CursorVisible = false;
        InitializeGame();

        while (!gameOver)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey();
                ChangeDirection(key);
            }

            Move();
            CheckCollision();
            UpdateGame();
            PrintGame();
            Thread.Sleep(100);
            Console.Clear();
        }

        Console.SetCursorPosition(width / 2 - 5, height / 2);
        Console.WriteLine($"Game Over! Your score: {score}");
    }

    static void InitializeGame()
    {
        snakeX.Clear();
        snakeY.Clear();
        snakeX.Add(0);
        snakeY.Add(0);
        matrix = new char[width, height];
        score = 0;
        gameOver = false;
        GenerateFood();
    }

    static void GenerateFood()
    {
        foodX = random.Next(width);
        foodY = random.Next(height);
        matrix[foodX, foodY] = '*';
    }

    static void PrintGame()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Console.Write(matrix[x, y]);
            }
            Console.WriteLine();
        }
        Console.WriteLine($"Score: {score}");
    }

    static void UpdateGame()
    {
        matrix = new char[width, height];
        matrix[foodX, foodY] = '*';

        for (int i = 0; i < snakeX.Count; i++)
        {
            matrix[snakeX[i], snakeY[i]] = 'o';
        }

        matrix[snakeX.First(), snakeY.First()] = 'O';
    }

    static void CheckCollision()
    {
        int headX = snakeX.First();
        int headY = snakeY.First();

        if (snakeX.Contains(headX) && snakeY.Contains(headY) && (snakeX.IndexOf(headX) != 0 || snakeY.IndexOf(headY) != 0))
        {
            gameOver = true;
        }

        if (headX == foodX && headY == foodY)
        {
            score++;
            GenerateFood();
        }
    }

    static void ChangeDirection(ConsoleKeyInfo key)
    {
        int dx = 0;
        int dy = 0;

        switch (key.Key)
        {
            case ConsoleKey.UpArrow:
                dy = -1;
                break;
            case ConsoleKey.DownArrow:
                dy = 1;
                break;
            case ConsoleKey.LeftArrow:
                dx = -1;
                break;
            case ConsoleKey.RightArrow:
                dx = 1;
                break;
        }

        int newHeadX = snakeX.First() + dx;
        int newHeadY = snakeY.First() + dy;

        if (newHeadX < 0) newHeadX = width - 1;
        if (newHeadX >= width) newHeadX = 0;
        if (newHeadY < 0) newHeadY = height - 1;
        if (newHeadY >= height) newHeadY = 0;

        snakeX.Insert(0, newHeadX);
        snakeY.Insert(0, newHeadY);

        if (newHeadX == foodX && newHeadY == foodY)
        {
            score++;
            GenerateFood();
        }
        else
        {
            snakeX.RemoveAt(snakeX.Count - 1);
            snakeY.RemoveAt(snakeY.Count - 1);
        }
    }


    static void Move()
    {
        // No need for this method in the current implementation
        // MoveSnake();
    }
}
