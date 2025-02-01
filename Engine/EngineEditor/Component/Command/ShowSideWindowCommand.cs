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
        private Button ToolButton;
        private string ButtonName { get; set; }
        Button[] Buttons { get; set; }
        public ShowSideWindowCommand(Editor editor, string buttonName, Button[] buttons)
        {
            Editor = editor;
            ButtonName = buttonName;
            Buttons = buttons;
        }

        public void Execute()
        {
            if (ToolButton == null)
            {
                ToolButton = Editor.Toolbar.ComponentList.Select(x => x as Button).FirstOrDefault(x => x.Text == ButtonName);
            }
            if (Editor.MiniWindow.Contains(SideWindow)) return;
            SideWindow = new MiniWindow(
                Editor,
                ToolButton.XPosition,
                ToolButton.YPosition + Editor.ComponentHeight,
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