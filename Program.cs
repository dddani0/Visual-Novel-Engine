using System.Text.Json;
using EngineComponents;
using Raylib_cs;

class Program
{
    public static void Main()
    {
        Font defaultFont = Raylib.GetFontDefault();

        int[] windowSize = [800, 800];
        Raylib.InitWindow(windowSize[0], windowSize[1], "Visual Novel Engine");

        int[] textBoxSize = [Convert.ToInt32(Raylib.GetScreenWidth() / 1.6f), Convert.ToInt32(Raylib.GetScreenWidth() / 5.3f)];
        const int textBoxYOffset = 5;
        int[] defaultTextboxPosition = [Raylib.GetScreenWidth() / 2 - textBoxSize[0] / 2, Raylib.GetScreenHeight() - textBoxSize[1] - textBoxYOffset];

        defaultFont.BaseSize = 32;
        defaultFont.GlyphPadding = 5;
        var d = defaultFont.Texture;
        var testTextBox = TextBox.CreateNewTextBox(10,
            defaultFont,
            defaultTextboxPosition[0], defaultTextboxPosition[1],
            textBoxSize[0], textBoxSize[1],
            ["12345678901234567890r4dddddddddddddddddddwrfwe1234",
            "DDDD"]);


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