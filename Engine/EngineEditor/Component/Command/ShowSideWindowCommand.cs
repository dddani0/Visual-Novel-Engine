
using System.Numerics;
using EngineEditor.Interface;
using Raylib_cs;

namespace EngineEditor.Component.Command
{
    public class ShowSideWindowCommand : ICommand
    {
        private readonly Group Group;
        private readonly Editor Editor;
        private readonly Button ToolButton;

        public ShowSideWindowCommand(Editor editor, Button button, Vector2 offset, ITool[] commands)
        {
            Editor = editor;
            ToolButton = button;
            Group = new Group((int)(button.XPosition + offset.X), (int)(button.YPosition + offset.Y), 70, 70, 5, Color.Red, Color.Black, Color.Gray, GroupType.SolidColor, 1, commands)
            {
                IsActive = false
            };
            Group.SelectButtonDependency(ToolButton);
        }

        public void Execute()
        {
            if (Editor.ComponentGroupList.Contains(Group)) return;
            Editor.ComponentGroupList.Add(Group);
        }
    }
}