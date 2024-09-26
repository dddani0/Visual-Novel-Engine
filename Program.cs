using System.Text.Json;
using Raylib_cs;

class Program
{
    public static void Main()
    {
        // JsonDocument windowInitials = ;
        Raylib.InitWindow(800, 480, "Visual Novel Engine");

        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.White);

            Raylib.DrawText("Hello, world!", 12, 12, 20, Color.Black);

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}