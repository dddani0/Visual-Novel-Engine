using TemplateGame.Component;
using Raylib_cs;
using VisualNovelEngine.Engine.EngineEditor.Component;

class Program
{
    public static void Main()
    {
        Raylib.InitWindow(800, 800, "Falastini - Editor"); //placeholder data

        //Game game = new();
        Editor editor = new();
        Raylib.BeginMode2D(editor.Camera);

        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            //game.UpdateScene();
            editor.Update();
            Raylib.EndDrawing();
        }
        Raylib.CloseWindow();
    }
}