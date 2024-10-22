using System.Runtime.CompilerServices;
using System.Text.Json;
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
        //
        Font defaultFont = Raylib.GetFontDefault();
        defaultFont.BaseSize = 64;
        //defaultFont.GlyphPadding = 5;
        //
        //Game activeGame = new();

        var tt = TextBox.CreateNewTextBox(50,
        defaultFont,
        defaultTextboxPosition[0],
        defaultTextboxPosition[1],
        textBoxSize[0],
        textBoxSize[1], false, "Réb", ["Lingan guli guli guláj", " Ki, mint veti ágyát, úgy alussza álmát!"]);
        Raylib.SetWindowSize(800, 800);

        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Yellow);
            //activeGame.UpdateScene();
            tt.WriteToScreen();
            Raylib.EndDrawing();
        }
        Raylib.CloseWindow();
    }
}