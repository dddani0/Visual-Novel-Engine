using Raylib_cs;
using VisualNovelEngine.Engine.EngineEditor.Interface;

namespace VisualNovelEngine.Engine.EngineEditor.Component
{
    /// <summary>
    /// The inspector window allows the user to inspect and change the properties of a dinamic component.
    /// </summary>
    public class InspectorWindow : IWindow
    {
        private Editor Editor { get; set; }
        internal int XPosition { get; set; }
        internal int YPosition { get; set; }
        internal int Width { get; set; }
        internal int Height { get; set; }
        internal int BorderWidth { get; set; }
        internal Color Color { get; set; }
        internal Color BorderColor { get; set; }
        internal bool Active { get; set; }
        internal Component? ActiveComponent { get; set; } = null;
        internal Button CloseButton { get; set; }
        internal List<IComponent> ComponentList { get; set; } = [];
        public InspectorWindow(Editor editor, int xPosition, int yPosition, int width, int height, int borderWidth, int enabledRowComponentCount, Color color, Color borderColor, IComponent[] components)
        {
            Editor = editor;
            XPosition = xPosition;
            YPosition = yPosition;
            BorderWidth = borderWidth;
            Color = color;
            BorderColor = borderColor;
            CloseButton = new Button(Editor, XPosition + Width, YPosition, "X", Editor.ComponentWidth, 20, Editor.ComponentBorderWidth, Editor.BaseColor, Editor.BorderColor, Editor.HoverColor, null, Button.ButtonType.Trigger);
            if (components is null) return;
            for (int i = 0; i < components.Length; i++) ComponentList.Add(components[i]);
            UpdateComponentPosition(width, height, enabledRowComponentCount);
        }

        private void UpdateComponentPosition(int width, int height, int enabledRowComponentCount)
        {
            //If the component list is empty, return.
            if (ComponentList.Count < 1) return;
            //If the component list is not empty, update the position of each component.
            int rowcount = 0;
            for (int i = 0; i < ComponentList.Count; i++)
            {
                if (i % enabledRowComponentCount == 0) rowcount++;
                //If the component is a button, update the position of the button.
                switch (ComponentList[i])
                {
                    case Button button:
                        button.XPosition = rowcount > 1 ? ComponentList[i % enabledRowComponentCount].XPosition : XPosition + (i * (Editor.ComponentWidth));
                        button.YPosition = YPosition + ((rowcount - 1) * Editor.ComponentHeight);
                        continue;
                    case Component component:
                        component.XPosition = rowcount > 1 ? ComponentList[i % enabledRowComponentCount].XPosition : XPosition + (i * (Editor.ComponentWidth));
                        component.YPosition = YPosition + ((rowcount - 1) * Editor.ComponentHeight);
                        continue;
                    default:
                        break;
                }
            }
            if (ComponentList.Count >= enabledRowComponentCount)
            {
                Width = XPosition + (enabledRowComponentCount * Editor.ComponentWidth);
                Height = YPosition + (Editor.ComponentHeight * rowcount);
            }
            else
            {
                Width = XPosition + (ComponentList.Count * Editor.ComponentWidth);
                Height = YPosition + (Editor.ComponentHeight * rowcount);
            }
        }
        public void Show()
        {
            if (Active is false) return;
            Raylib.DrawRectangle(XPosition, YPosition, Width, Height, Color);
            Raylib.DrawRectangleLines(XPosition, YPosition, Width, Height, BorderColor);
            //for loop render each tool
            if (ComponentList.Count < 1) return;
            for (int i = 0; i < ComponentList.Count; i++) ComponentList[i].Render();
        }
    }
}