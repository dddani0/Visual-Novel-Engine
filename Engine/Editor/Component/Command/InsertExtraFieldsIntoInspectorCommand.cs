using VisualNovelEngine.Engine.Editor.Interface;

namespace VisualNovelEngine.Engine.Editor.Component.Command
{
    /// <summary>
    /// Inserts extra fields into the inspector window at an index
    /// </summary>
    public class InsertExtraFieldsIntoInspectorCommand : ICommand
    {
        private readonly Editor Editor;
        private readonly InspectorWindow InspectorWindow;
        private readonly IComponent[] Components;
        private int IndexOffset { get; set; }
        public InsertExtraFieldsIntoInspectorCommand(Editor editor, InspectorWindow inspectorWindow, IComponent[] components, int indexOffset)
        {
            Editor = editor;
            InspectorWindow = inspectorWindow;
            IndexOffset = indexOffset;
            Components = components;
        }

        public void Execute()
        {
            foreach (var item in Components)
            {
                switch (item)
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
                    case Dropdown dropDown:
                        Dropdown newDropDown = new(Editor, InspectorWindow.XPosition, InspectorWindow.YPosition, dropDown.Width, dropDown.Height, dropDown.BorderWidth, dropDown.Filter);
                        InspectorWindow.ComponentList.Insert(InspectorWindow.ComponentList.Count - (IndexOffset - 1), newDropDown);
                        InspectorWindow.Scrollbar.AddComponent(newDropDown, IndexOffset);
                        break;
                }
            }
            InspectorWindow.UpdateComponentPosition(InspectorWindow.EnabledRowComponentCount);
        }
    }
}