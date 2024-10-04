using System.Text.Json;
using EngineComponents;
using Raylib_cs;

class Program
{
    public static void Main()
    {
        Raylib.SetConfigFlags(ConfigFlags.ResizableWindow);
        int[] boxSize = [50, 225];
        int[] defaultTextboxPosition = [560, 700];
        Raylib.InitWindow(1680, 900, "Visual Novel Engine");
        var testTextBox = TextBox.createNewTextBox(10, defaultTextboxPosition[0], defaultTextboxPosition[1], boxSize[0], boxSize[1], ["Aint that a bitch?"]);
        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Beige);
            Raylib.DrawText(Raylib.GetScreenWidth().ToString(), 0, 0, 20, Color.Black);
            testTextBox.WriteToScreen();
            Raylib.EndDrawing();
        }
        Raylib.CloseWindow();
    }
}