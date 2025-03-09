using Raylib_cs;
using VisualNovelEngine.Engine.EngineEditor.Interface;

namespace VisualNovelEngine.Engine.EngineEditor.Component.Command
{
    public class ShowMiniWindowComand : ICommand
    {
        Editor Editor { get; set; }
        MiniWindow MiniWindow { get; set; }
        internal IComponent[]? Components { get; set; }
        internal Button[]? Buttons { get; set; }
        internal bool HasVariable { get; set; }

        public ShowMiniWindowComand(Editor editor, bool hasVariable, Button[] buttons, MiniWindow.miniWindowType type)
        {
            Editor = editor;
            Buttons = buttons;
            HasVariable = hasVariable;
            MiniWindow = new MiniWindow(Editor, true, HasVariable, Raylib.GetScreenWidth() / 2 - Editor.MiniWindowWidth / 2, Raylib.GetScreenHeight() / 2 - Editor.MiniWindowHeight / 2, Editor.MiniWindowWidth, Editor.MiniWindowHeight, Editor.MiniWindowBorderWidth, Editor.BaseColor, Editor.BorderColor, type, buttons);
        }

        public ShowMiniWindowComand(Editor editor, bool hasVariable, IComponent[] components, MiniWindow.miniWindowType type)
        {
            Editor = editor;
            Components = components;
            HasVariable = hasVariable;
            MiniWindow = new MiniWindow(Editor, true, HasVariable, Raylib.GetScreenWidth() / 2 - Editor.MiniWindowWidth / 2, Raylib.GetScreenHeight() / 2 - Editor.MiniWindowHeight / 2, Editor.MiniWindowWidth, Editor.MiniWindowHeight, Editor.MiniWindowBorderWidth, Editor.BaseColor, Editor.BorderColor, type, components);
        }

        public void Execute()
        {
            if (Editor.MiniWindow.Contains(MiniWindow)) return;
            Editor.MiniWindow.Add(MiniWindow);
        }
    }
}