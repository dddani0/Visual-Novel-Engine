using TemplateGame.Component;
using EngineEditor.Component;
using Raylib_cs;

class Program
{
    public static void Main()
    {
        Raylib.InitWindow(800, 800, "Falastini - Editor"); //placeholder data

        //Game game = new();
        Editor editor = new();

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