using TemplateGame.Component;
using Raylib_cs;

class Program
{
    public static void Main()
    {
        Raylib.InitWindow(800, 800, "placeholder game"); //placeholder data

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