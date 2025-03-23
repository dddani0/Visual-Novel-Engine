using VisualNovelEngine.Engine.Editor.Interface;

namespace VisualNovelEngine.Engine.Editor.Component.Command
{
    /// <summary>
    /// Inserts an extra field to the inspector window
    /// </summary>
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
                    TextField newTextField = new(Editor, InspectorWindow.XPosition, InspectorWindow.YPosition, txtfield.Width, txtfield.Height, txtfield.BorderWidth, txtfield.Text, txtfield.Font, txtfield.IsStatic);
                    InspectorWindow.ComponentList.Insert(InspectorWindow.ComponentList.Count - (IndexOffset - 1), newTextField);
                    InspectorWindow.Scrollbar.AddComponent(newTextField, IndexOffset);
                    break;
                case Button button:
                    Button newButton = new(Editor, InspectorWindow.XPosition, InspectorWindow.YPosition, button.Text,
                    true, button.Width, button.Height, button.BorderWidth, button.Color, button.BorderColor, button.HoverColor, button.Command, button.Type);
                    InspectorWindow.ComponentList.Insert(InspectorWindow.ComponentList.Count - (IndexOffset - 1), newButton);
                    InspectorWindow.Scrollbar.AddComponent(newButton, IndexOffset);
                    break;
                case DropDown dropDown:
                    DropDown newDropDown = new(Editor, InspectorWindow.XPosition, InspectorWindow.YPosition, dropDown.Width, dropDown.Height, dropDown.BorderWidth, dropDown.Filter);
                    InspectorWindow.ComponentList.Insert(InspectorWindow.ComponentList.Count - (IndexOffset - 1), newDropDown);
                    InspectorWindow.Scrollbar.AddComponent(newDropDown, IndexOffset);
                    break;
            }
            InspectorWindow.UpdateComponentPosition(InspectorWindow.EnabledRowComponentCount);
        }
    }
}