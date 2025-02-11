using System.Numerics;
using VisualNovelEngine.Engine.EngineEditor.Interface;
using Raylib_cs;
using System.ComponentModel;

namespace VisualNovelEngine.Engine.EngineEditor.Component.Command
{
    public class ShowSideWindowCommand : ICommand
    {
        private MiniWindow? SideWindow { get; set; } = null;
        private readonly Editor Editor;
        internal Button DependentButton;
        internal string DependentButtonName { get; set; }
        internal Button[] Buttons { get; set; }
        public ShowSideWindowCommand(Editor editor, string buttonName, Button[] buttons)
        {
            Editor = editor;
            DependentButtonName = buttonName;
            Buttons = buttons;
        }

        public void Execute()
        {
            DependentButton ??= Editor.Toolbar.ComponentList.Select(x => x as Button).FirstOrDefault(x => x.Text == DependentButtonName);
            if (Editor.MiniWindow.Contains(SideWindow)) return;
            SideWindow = new MiniWindow(
                Editor,
                DependentButton.XPosition,
                DependentButton.YPosition + Editor.ComponentHeight,
                Editor.ComponentWidth,
                Editor.ComponentWidth,
                Editor.ComponentBorderWidth,
                Editor.BaseColor,
                Editor.BorderColor,
                [.. Buttons]
            );
            Editor.MiniWindow.Add(SideWindow);

        }
    }
}