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
        /// <summary>
        /// Constructor for toolbar associated purposes
        /// </summary>
        /// <param name="editor"></param>
        /// <param name="buttonName"></param>
        /// <param name="buttons"></param>
        public ShowSideWindowCommand(Editor editor, string buttonName, Button[] buttons)
        {
            Editor = editor;
            DependentButtonName = buttonName;
            Buttons = buttons;
        }

        public ShowSideWindowCommand(Editor editor, Button dependentButton, Button[] buttons)
        {
            Editor = editor;
            DependentButton = dependentButton;
            Buttons = buttons;
        }

        public void Execute()
        {
            if (DependentButton == null)
            {
                DependentButton = Editor.Toolbar.ComponentList.Select(x => x as Button).FirstOrDefault(x => x.Text == DependentButtonName);
            }
            if (Editor.MiniWindow.Contains(SideWindow))
            {
                if (DependentButton.Selected is false)
                {
                    Editor.MiniWindow.Remove(SideWindow);
                }
                return;
            }
            SideWindow = new MiniWindow(
                Editor,
                DependentButton.XPosition,
                DependentButton.YPosition + Editor.ComponentHeight,
                Editor.ComponentWidth,
                Editor.ComponentWidth,
                Editor.ComponentBorderWidth,
                Editor.BaseColor,
                Editor.BorderColor,
                MiniWindow.miniWindowType.Vertical
                , [.. Buttons]
            );
            Editor.MiniWindow.Add(SideWindow);
        }
    }
}