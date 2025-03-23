using TemplateGame.Component;
using Raylib_cs;
using VisualNovelEngine.Engine.Editor.Component;

class Program
{
    internal static bool Exit { get; set; } = false;
    public static void Main()
    {
        Raylib.InitWindow(800, 800, "Vizuális Novella Motor");

        //Game game = new();
        Editor editor = new();

        while (Exit is false)
        {
            Raylib.BeginDrawing();
            //game.UpdateScene();
            editor.Update();
            Raylib.EndDrawing();
        }
        Raylib.CloseWindow();
    }
}