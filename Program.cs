using System.Text.Json;
using EngineComponents;
using Raylib_cs;

class Program
{
    public static void Main()
    {

        Raylib.InitWindow(1680, 900, "Visual Novel Engine");
        var testTextBox = TextBox.createNewTextBox(20, 1, 1, "lorem ipsum salameet? Lorem kibasz");

        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Beige);
            testTextBox.WriteToScreen();
            Raylib.EndDrawing();
        }
        Raylib.CloseWindow();
    }
}