using System.Runtime.CompilerServices;
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
            defaultFont, Color.Red, Color.Brown,
            defaultTextboxPosition[0], defaultTextboxPosition[1],
            textBoxSize[0], textBoxSize[1], true,
            ["Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam eget varius odio. Sed malesuada arcu vitae justo sagittis, id finibus nulla mollis. Quisque in luctus leo. Nam fringilla dui metus, nec tristique ante dapibus a. Nam velit odio, sagittis vel leo a, commodo faucibus nunc. Mauris pulvinar, mi non dictum ornare, lectus tellus tempor metus, non condimentum nibh sapien vitae tellus. Etiam sit amet ante enim. Phasellus id lacus est. Cras dapibus urna quis consectetur dapibus."]);


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