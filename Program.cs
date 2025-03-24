using VisualNovelEngine.Engine.Game.Component;
using Raylib_cs;
using VisualNovelEngine.Engine.Editor.Component;
using VisualNovelEngine.Engine.Component;

class Program
{
    internal static VisualNovelEngine.Engine.Component.Engine Engine { get; set; }
    public static void Main()
    {
        Engine = new();
        //
        Engine.Process();
    }
}