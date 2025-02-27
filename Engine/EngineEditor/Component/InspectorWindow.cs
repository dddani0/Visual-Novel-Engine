using System.Linq;
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
        internal bool IsOverflow { get; set; } = false;
        internal Scrollbar Scrollbar { get; set; }
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
            CloseButton = new Button(Editor, XPosition + Editor.InspectorWindowWidth - Editor.SmallButtonWidth, YPosition, "X", true, Editor.SmallButtonWidth, Editor.SmallButtonHeight, Editor.SmallButtonBorderWidth, Editor.CloseButtonBaseColor, Editor.CloseButtonBorderColor, Editor.CloseButtonHoverColor, new CloseInspectorCommand(Editor, this), Button.ButtonType.Trigger);
            Scrollbar = new Scrollbar(Editor, XPosition + Width - Editor.SmallButtonWidth, YPosition + editor.SmallButtonHeight, Height - Editor.SmallButtonWidth, Editor.SmallButtonWidth, Scrollbar.ScrollbarType.Vertical, false, [.. ComponentList]);
        }

        internal void SetActiveComponent(Component component)
        {
            Scrollbar.DropComponents();
            //Dinamic Component Editor name and field
            ComponentList.Add(new Label(Editor, XPosition, YPosition + BorderWidth, "Name"));
            ComponentList.Add(
                new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, component.Name, Raylib.GetFontDefault(), false));
            //Static ID field value
            ComponentList.Add(new Label(Editor, XPosition, YPosition, "ID:"));
            ComponentList.Add(
                new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, component.ID.ToString(), Raylib.GetFontDefault(), true));
            switch (component.RenderingObject)
            {
                case Sprite sprite:
                    //Dinamic Path name
                    ComponentList.Add(new Label(Editor, XPosition, YPosition, "Path:"));
                    ComponentList.Add(
                        new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, sprite.Name, Raylib.GetFontDefault(), false));
                    //Dinamic Color rgb value
                    ComponentList.Add(new Label(Editor, XPosition, YPosition, "Color:"));
                    ComponentList.Add(
                        new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, sprite.Color.ToString(), Raylib.GetFontDefault(), false));
                    break;
                case TemplateGame.Component.Button button:
                    //Dinamic Button Name
                    ComponentList.Add(new Label(Editor, XPosition, YPosition, "Button Text:"));
                    ComponentList.Add(
                        new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, button.Text, Raylib.GetFontDefault(), true));
                    //Dinamic button color rgb value
                    ComponentList.Add(new Label(Editor, XPosition, YPosition, "Color:"));
                    ComponentList.Add(new Label(Editor, XPosition, YPosition, "Red:"));
                    ComponentList.Add(
                        new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, button.Color.R.ToString(), Raylib.GetFontDefault(), true));
                    ComponentList.Add(new Label(Editor, XPosition, YPosition, "Green:"));
                    ComponentList.Add(
                        new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, button.Color.G.ToString(), Raylib.GetFontDefault(), true));
                    ComponentList.Add(new Label(Editor, XPosition, YPosition, "Blue:"));
                    ComponentList.Add(
                        new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, button.Color.B.ToString(), Raylib.GetFontDefault(), true));
                    //Dinamic Button Border Color rgb value
                    ComponentList.Add(new Label(Editor, XPosition, YPosition, "Border Color:"));
                    ComponentList.Add(new Label(Editor, XPosition, YPosition, "Red:"));
                    ComponentList.Add(
                        new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, button.BorderColor.R.ToString(), Raylib.GetFontDefault(), true));
                    ComponentList.Add(new Label(Editor, XPosition, YPosition, "Green:"));
                    ComponentList.Add(
                             new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, button.BorderColor.G.ToString(), Raylib.GetFontDefault(), true));
                    ComponentList.Add(new Label(Editor, XPosition, YPosition, "Blue:"));
                    ComponentList.Add(
                        new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, button.BorderColor.B.ToString(), Raylib.GetFontDefault(), true));
                    //Dinamic button Hover Color rgb value
                    ComponentList.Add(new Label(Editor, XPosition, YPosition, "Hover color:"));
                    ComponentList.Add(new Label(Editor, XPosition, YPosition, "Red:"));
                    ComponentList.Add(
                        new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, button.HoverColor.R.ToString(), Raylib.GetFontDefault(), true));
                    ComponentList.Add(new Label(Editor, XPosition, YPosition, "Green:"));
                    ComponentList.Add(
                             new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, button.HoverColor.G.ToString(), Raylib.GetFontDefault(), true));
                    ComponentList.Add(new Label(Editor, XPosition, YPosition, "Blue:"));
                    ComponentList.Add(
                        new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, button.HoverColor.B.ToString(), Raylib.GetFontDefault(), true));
                    break;
                case TemplateGame.Component.TextField textField:
                    //Dinamic Text
                    ComponentList.Add(new Label(Editor, XPosition, YPosition, "Text:"));
                    ComponentList.Add(
                        new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, textField.Text, Raylib.GetFontDefault(), true));
                    //Dinamic Wordwrap Toggle 
                    ComponentList.Add(
                        new ToggleButton(Editor, XPosition, YPosition, Editor.SmallButtonWidth, Editor.SmallButtonWidth, "Wordwrap:"));
                    break;
                case TextBox textBox:
                    //Dinamic Wordwrap Toggle 
                    ComponentList.Add(
                        new ToggleButton(Editor, XPosition, YPosition, Editor.SmallButtonWidth, Editor.SmallButtonWidth, "Wordwrap:"));
                    //Dinamic Title
                    ComponentList.Add(new Label(Editor, XPosition, YPosition, "Textbox title:"));
                    ComponentList.Add(
                        new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, textBox.TextBoxTitle, Raylib.GetFontDefault(), true));
                    //Dinamic text content
                    ComponentList.Add(
                new Label(Editor, XPosition, YPosition, "Text content:")
                );
                    for (int i = 0; i < textBox.Content.Count; i++)
                    {
                        ComponentList.Add(
                            new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, textBox.Content[i], Raylib.GetFontDefault(), true));
                    }
                    ComponentList.Add(
                        new Button(Editor, XPosition, YPosition, "Add text", true, Editor.ButtonWidth, Editor.ButtonHeight, Editor.ButtonBorderWidth, Editor.BaseColor, Editor.BorderColor, Editor.HoverColor, new InsertExtraFieldToInspectorCommand(Editor, this, new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, "New text field", Raylib.GetFontDefault(), false), 1), Button.ButtonType.Trigger));
                    break;
                case Menu menu:
                    //Dinamic menu color
                    ComponentList.Add(new Label(Editor, XPosition, YPosition, "Color:"));
                    ComponentList.Add(new Label(Editor, XPosition, YPosition, "Red:"));
                    ComponentList.Add(
                        new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, menu.MenuColor.R.ToString(), Raylib.GetFontDefault(), true));
                    ComponentList.Add(new Label(Editor, XPosition, YPosition, "Green:"));
                    ComponentList.Add(
                             new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, menu.MenuColor.G.ToString(), Raylib.GetFontDefault(), true));
                    ComponentList.Add(new Label(Editor, XPosition, YPosition, "Blue:"));
                    ComponentList.Add(
                        new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, menu.MenuColor.B.ToString(), Raylib.GetFontDefault(), true));
                    //Dinamic menu border color
                    ComponentList.Add(new Label(Editor, XPosition, YPosition, "Border color:"));
                    ComponentList.Add(new Label(Editor, XPosition, YPosition, "Red:"));
                    ComponentList.Add(
                        new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, menu.MenuBorderColor.R.ToString(), Raylib.GetFontDefault(), true));
                    ComponentList.Add(new Label(Editor, XPosition, YPosition, "Green:"));
                    ComponentList.Add(
                             new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, menu.MenuBorderColor.G.ToString(), Raylib.GetFontDefault(), true));
                    ComponentList.Add(new Label(Editor, XPosition, YPosition, "Blue:"));
                    ComponentList.Add(
                        new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, menu.MenuBorderColor.B.ToString(), Raylib.GetFontDefault(), true));
                    //Add a dropdown for every block
                    ComponentList.Add(new Label(Editor, XPosition, YPosition, "List of blocks:"));
                    for (int i = 0; i < menu.BlockList.Count; i++)
                    {
                        DropDown blockDropDown = new(Editor, XPosition, YPosition, Editor.ButtonWidth, Editor.ButtonHeight, Editor.ComponentBorderWidth, DropDown.FilterType.Block);
                        ComponentList.Add(blockDropDown);
                    }
                    //Dinamic add block insert button
                    ComponentList.Add(
                        new Button(Editor, XPosition, YPosition, "Add button", true, Editor.ButtonWidth, Editor.ButtonHeight, Editor.ButtonBorderWidth, Editor.BaseColor, Editor.BorderColor, Editor.HoverColor, new InsertExtraFieldToInspectorCommand(Editor, this, new DropDown(Editor, 0, 0, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, DropDown.FilterType.Block), 2), Button.ButtonType.Trigger));
                    break;
            }
            ActiveComponent = component;
            Scrollbar.AddComponents([.. ComponentList]);
            UpdateComponentPosition(EnabledRowComponentCount);
        }
        internal void SetActiveComponent(IEvent eventData)
        {
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
            UpdateComponentPosition(EnabledRowComponentCount);
        }

        internal void DropActiveComponent()
        {
            ComponentList.Clear();
            ActiveComponent = null;
            Active = false;
        }

        internal void UpdateComponentPosition(int enabledRowComponentCount)
        {
            //If the component list is empty, return.
            if (ComponentList.Count < 1) return;
            //If the component list is not empty, update the position of each component.
            int rowcount = 0;
            for (int i = 0; i < ComponentList.Count; i++)
            {
                if (i % enabledRowComponentCount == 0) rowcount++;
                //If the component is a button, update the position of the button.
                ComponentList[i].XPosition = rowcount > 1 ? ComponentList[i % enabledRowComponentCount].XPosition : XPosition + (i * Editor.ComponentWidth);
                ComponentList[i].YPosition = YPosition + ((rowcount - 1) * Editor.ComponentHeight);
            }
        }

        public void Show()
        {
            if (Active is false) return;
            Raylib.DrawRectangle(XPosition, YPosition, Width, Height, Color);
            Raylib.DrawRectangleLines(XPosition, YPosition, Width, Height, BorderColor);
            CloseButton.Render();
            if (ComponentList.Count * Editor.ComponentHeight > Height) Scrollbar.Render();
            if (ComponentList.Count < 1) return;
            for (int i = 0; i < ComponentList.Count; i++)
            {
                if (ComponentList[i].YPosition <= YPosition + Height && ComponentList[i].YPosition >= YPosition + BorderWidth) ComponentList[i].Render();
            }
        }
    }
}