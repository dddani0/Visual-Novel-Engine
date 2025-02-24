using VisualNovelEngine.Engine.EngineEditor.Interface;

namespace VisualNovelEngine.Engine.EngineEditor.Component.Command
{
    public class InsertExtraFieldToInspectorCommand : ICommand
    {
        private readonly Editor Editor;
        private readonly InspectorWindow InspectorWindow;
        private readonly IComponent Component;
        private int IndexOffset { get; set; }
        public InsertExtraFieldToInspectorCommand(Editor editor, InspectorWindow inspectorWindow, IComponent component, int indexOffset)
        {
            Editor = editor;
            InspectorWindow = inspectorWindow;
            IndexOffset = indexOffset;
            Component = component;
        }

        public void Execute()
        {
            switch (Component)
            {
                case TextField txtfield:
                    InspectorWindow.ComponentList.Insert(InspectorWindow.ComponentList.Count - 1 - IndexOffset, new TextField(Editor, txtfield.XPosition, txtfield.YPosition, txtfield.Width, txtfield.Height, txtfield.BorderWidth, txtfield.Text, txtfield.Font, txtfield.IsStatic));
                    break;
                case Button button:
                    InspectorWindow.ComponentList.Insert(InspectorWindow.ComponentList.Count - 1 - IndexOffset, new Button(Editor, button.XPosition, button.YPosition, button.Text, button.Width, button.Height, button.BorderWidth, button.Color, button.BorderColor, button.HoverColor, button.Command, button.Type));
                    break;
            }
            InspectorWindow.UpdateComponentPosition(InspectorWindow.EnabledRowComponentCount);
        }
    }
}