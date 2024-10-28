using EngineComponents;
using Raylib_cs;

class Program
{
    public static void Main()
    {
        int[] windowSize = [800, 800];
        Raylib.InitWindow(windowSize[0], windowSize[1], "placeholder game");
        //
        int[] textBoxSize = [Convert.ToInt32(Raylib.GetScreenWidth() / 1.6f), Convert.ToInt32(Raylib.GetScreenWidth() / 5.3f)];
        const int textBoxYOffset = 5;
        int[] defaultTextboxPosition = [Raylib.GetScreenWidth() / 2 - textBoxSize[0] / 2, Raylib.GetScreenHeight() - textBoxSize[1] - textBoxYOffset];

        Game game = new();

        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            game.UpdateScene();
            Raylib.EndDrawing();
        }
        Raylib.CloseWindow();
    }
}