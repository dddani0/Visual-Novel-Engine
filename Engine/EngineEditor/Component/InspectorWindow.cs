using Namespace;
using Raylib_cs;
using TemplateGame.Component;
using TemplateGame.Component.Action;
using TemplateGame.Component.Action.TimelineDependent;
using TemplateGame.Interface;
using VisualNovelEngine.Engine.EngineEditor.Component.Command;
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
        internal int EnabledRowComponentCount { get; set; }
        internal Color Color { get; set; }
        internal Color BorderColor { get; set; }
        internal bool Active { get; set; } = false;
        internal Component? ActiveComponent { get; set; } = null;
        internal IEvent? ActiveEvent { get; set; } = null;
        internal Button CloseButton { get; set; }
        internal List<IComponent> ComponentList { get; set; } = [];
        public InspectorWindow(Editor editor, int xPosition, int yPosition, int enabledRowComponentCount)
        {
            Editor = editor;
            XPosition = xPosition;
            YPosition = yPosition;
            Width = Editor.InspectorWindowWidth;
            Height = Editor.InspectorWindowHeight;
            BorderWidth = Editor.InspectorWindowBorderWidth;
            Color = Editor.BaseColor;
            BorderColor = Editor.BorderColor;
            EnabledRowComponentCount = enabledRowComponentCount;
            CloseButton = new Button(Editor, XPosition + Editor.InspectorWindowWidth - Editor.SmallButtonWidth, YPosition, "X", Editor.SmallButtonWidth, Editor.SmallButtonHeight, Editor.SmallButtonBorderWidth, Editor.CloseButtonBaseColor, Editor.CloseButtonBorderColor, Editor.CloseButtonHoverColor, new CloseInspectorCommand(Editor, this), Button.ButtonType.Trigger);
        }

        internal void SetActiveComponent(Component component)
        {
            if (component.RenderingObject is Sprite sprite)
            {
                ComponentList.Add(new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, component.ID.ToString(), Raylib.GetFontDefault(), true));
                ComponentList.Add(new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, sprite.Name, Raylib.GetFontDefault(), false));
            }
            ActiveComponent = component;
            UpdateComponentPosition(Width, Height, EnabledRowComponentCount);
        }
        internal void SetActiveComponent(IEvent eventData)
        {
            //DANI ezt dolgozd ki!
            ActiveEvent = eventData;
            switch (eventData)
            {
                case CreateMenuAction createMenuAction:
                    ComponentList.Add(new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, "Create Menu Action", Raylib.GetFontDefault(), false));
                    break;
                case LoadSceneAction loadSceneAction:
                    ComponentList.Add(new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, "Load Scene Action", Raylib.GetFontDefault(), false));
                    break;
                case NativeLoadSceneAction nativeLoadSceneAction:
                    ComponentList.Add(new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, "Native Load Scene Action", Raylib.GetFontDefault(), false));
                    break;
                case AddSpriteAction addSpriteAction:
                    ComponentList.Add(new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, "Add Sprite Action", Raylib.GetFontDefault(), false));
                    break;
                case ChangeSpriteAction changeSpriteAction:
                    ComponentList.Add(new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, "Change Sprite Action", Raylib.GetFontDefault(), false));
                    break;
                case CreateVariableAction createVariableAction:
                    ComponentList.Add(new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, "Create Variable Action", Raylib.GetFontDefault(), false));
                    break;
                case DecrementVariableAction decrementVariableAction:
                    ComponentList.Add(new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, "Decrement Variable Action", Raylib.GetFontDefault(), false));
                    break;
                case EmptyAction:
                    ComponentList.Add(new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, "Empty Action", Raylib.GetFontDefault(), false));
                    break;
            }
            UpdateComponentPosition(Width, Height, EnabledRowComponentCount);
        }

        internal void DropActiveComponent()
        {
            ComponentList.Clear();
            ActiveComponent = null;
            Active = false;
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
                ComponentList[i].XPosition = rowcount > 1 ? ComponentList[i % enabledRowComponentCount].XPosition : XPosition + (i * (Editor.ComponentWidth));
                ComponentList[i].YPosition = YPosition + ((rowcount - 1) * Editor.ComponentHeight);
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
            CloseButton.Render();
            //for loop render each tool
            if (ComponentList.Count < 1) return;
            for (int i = 0; i < ComponentList.Count; i++) ComponentList[i].Render();
        }
    }
}