using System.Linq;
using Namespace;
using Raylib_cs;
using TemplateGame.Component;
using TemplateGame.Component.Action;
using TemplateGame.Component.Action.TimelineDependent;
using TemplateGame.Component.Action.TimelineIndependent;
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
        internal IAction? ActiveAction { get; set; } = null;
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
            ComponentList.Add(new Label(XPosition, YPosition + BorderWidth, "Name"));
            ComponentList.Add(
                new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, component.Name, Raylib.GetFontDefault(), false));
            //Static ID field value
            ComponentList.Add(new Label(XPosition, YPosition, "ID:"));
            ComponentList.Add(
                new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, component.ID.ToString(), Raylib.GetFontDefault(), true));
            switch (component.RenderingObject)
            {
                case Sprite sprite:
                    //Dinamic Path name
                    ComponentList.Add(new Label(XPosition, YPosition, "Path:"));
                    ComponentList.Add(
                        new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, sprite.Name, Raylib.GetFontDefault(), false));
                    //Dinamic Color rgb value
                    ComponentList.Add(new Label(XPosition, YPosition, "Color:"));
                    ComponentList.Add(
                        new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, $"{sprite.Color.R}, {sprite.Color.G}, {sprite.Color.B}", Raylib.GetFontDefault(), false));
                    break;
                case TemplateGame.Component.Button button:
                    //Dinamic Button Name
                    ComponentList.Add(new Label(XPosition, YPosition, "Button Text:"));
                    ComponentList.Add(
                        new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, button.Text, Raylib.GetFontDefault(), true));
                    //Dinamic button color rgb value
                    ComponentList.Add(new Label(XPosition, YPosition, "Color:"));
                    ComponentList.Add(
                        new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, $"{button.Color.R}, {button.Color.G}, {button.Color.B}", Raylib.GetFontDefault(), true));
                    //Dinamic Button Border Color rgb value
                    ComponentList.Add(new Label(XPosition, YPosition, "Border Color:"));
                    ComponentList.Add(
                        new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, $"{button.BorderColor.R}, {button.BorderColor.G}, {button.BorderColor.B}", Raylib.GetFontDefault(), true));
                    //Dinamic button Hover Color rgb value
                    ComponentList.Add(new Label(XPosition, YPosition, "Hover color:"));
                    ComponentList.Add(
                        new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, $"{button.HoverColor.R}, {button.HoverColor.G}, {button.HoverColor.B}", Raylib.GetFontDefault(), true));
                    break;
                case TemplateGame.Component.TextField textField:
                    //Dinamic Text
                    ComponentList.Add(new Label(XPosition, YPosition, "Text:"));
                    ComponentList.Add(
                        new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, textField.Text, Raylib.GetFontDefault(), false));
                    //Textfield color
                    ComponentList.Add(new Label(XPosition, YPosition, "Color:"));
                    ComponentList.Add(
                        new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, $"{textField.Color.R}, {textField.Color.G}, {textField.Color.B}", Raylib.GetFontDefault(), false));
                    //Textfield border color
                    ComponentList.Add(new Label(XPosition, YPosition, "Border color:"));
                    ComponentList.Add(
                        new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, $"{textField.BorderColor.R}, {textField.BorderColor.G}, {textField.BorderColor.B}", Raylib.GetFontDefault(), false));
                    //Dinamic Wordwrap Toggle 
                    ComponentList.Add(
                        new ToggleButton(Editor, XPosition, YPosition, Editor.SmallButtonWidth, Editor.SmallButtonWidth, "Wordwrap:", textField.WordWrap));
                    break;
                case TextBox textBox:
                    //Dinamic Title
                    ComponentList.Add(new Label(XPosition, YPosition, "title:"));
                    ComponentList.Add(
                        new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, textBox.Title, Raylib.GetFontDefault(), true));
                    //Text content
                    ComponentList.Add(new Label(XPosition, YPosition, "Text content:"));
                    for (int i = 0; i < textBox.Content.Count; i++)
                    {
                        ComponentList.Add(
                            new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, textBox.Content[i], Raylib.GetFontDefault(), true));
                    }
                    ComponentList.Add(
                        new Button(Editor, XPosition, YPosition, "Add text", true, Editor.ButtonWidth, Editor.ButtonHeight, Editor.ButtonBorderWidth, Editor.BaseColor, Editor.BorderColor, Editor.HoverColor, new InsertExtraFieldToInspectorCommand(Editor, this, new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, "New text field", Raylib.GetFontDefault(), false), 14), Button.ButtonType.Trigger));
                    //Horizontal text margin
                    ComponentList.Add(new Label(XPosition, YPosition, "Horizontal margin:"));
                    ComponentList.Add(
                        new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, textBox.HorizontalTextMargin.ToString(), Raylib.GetFontDefault(), true));
                    //Vertical text margin
                    ComponentList.Add(new Label(XPosition, YPosition, "Vertical margin:"));
                    ComponentList.Add(
                        new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, textBox.VerticalTextMargin.ToString(), Raylib.GetFontDefault(), true));
                    //Characters per second
                    ComponentList.Add(new Label(XPosition, YPosition, "Characters per second:"));
                    ComponentList.Add(
                        new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, textBox.CPSTextSpeed.ToString(), Raylib.GetFontDefault(), true));
                    //Color
                    ComponentList.Add(new Label(XPosition, YPosition, "Color:"));
                    ComponentList.Add(
                        new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, $"{textBox.Color.R}, {textBox.Color.G}, {textBox.Color.B}", Raylib.GetFontDefault(), true));
                    //Border color
                    ComponentList.Add(new Label(XPosition, YPosition, "Border color:"));
                    ComponentList.Add(
                        new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, $"{textBox.BorderColor.R}, {textBox.BorderColor.G}, {textBox.BorderColor.B}", Raylib.GetFontDefault(), true));
                    //Wordwrap
                    ComponentList.Add(
                        new ToggleButton(Editor, XPosition, YPosition, Editor.SmallButtonWidth, Editor.SmallButtonWidth, "Wordwrap:", textBox.WordWrap));
                    //Textbox position
                    ComponentList.Add(new Label(XPosition, YPosition, "Position"));
                    ComponentList.Add(
                        new DropDown(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, DropDown.FilterType.TextBoxPosition));
                    break;
                case Menu menu:
                    //Position
                    ComponentList.Add(new Label(XPosition, YPosition, "Position"));
                    ComponentList.Add(new Label(XPosition, YPosition, "X axis:"));
                    ComponentList.Add(
                        new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, menu.XPosition.ToString(), Raylib.GetFontDefault(), true));
                    ComponentList.Add(new Label(XPosition, YPosition, "Y axis:"));
                    ComponentList.Add(
                        new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, menu.YPosition.ToString(), Raylib.GetFontDefault(), true));
                    //Width
                    ComponentList.Add(new Label(XPosition, YPosition, "Width:"));
                    ComponentList.Add(
                        new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, menu.Width.ToString(), Raylib.GetFontDefault(), true));
                    //Height
                    ComponentList.Add(new Label(XPosition, YPosition, "Height:"));
                    ComponentList.Add(
                        new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, menu.Height.ToString(), Raylib.GetFontDefault(), true));
                    ComponentList.Add(
                        new ToggleButton(Editor, XPosition, YPosition, Editor.SmallButtonWidth, Editor.SmallButtonWidth, "Fullscreen:", menu.IsFullScreen));
                    //Dinamic menu color
                    ComponentList.Add(new Label(XPosition, YPosition, "Color:"));
                    ComponentList.Add(
                        new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, $"{menu.Color.R}, {menu.Color.G}, {menu.Color.B}", Raylib.GetFontDefault(), true));
                    //Dinamic menu border color
                    ComponentList.Add(new Label(XPosition, YPosition, "Border color:"));
                    ComponentList.Add(
                        new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, $"{menu.BorderColor.R}, {menu.BorderColor.G}, {menu.BorderColor.B}", Raylib.GetFontDefault(), true));
                    //Add a dropdown for every block
                    ComponentList.Add(new Label(XPosition, YPosition, "List of blocks:"));
                    for (int i = 0; i < menu.BlockList.Count; i++)
                    {
                        DropDown blockDropDown = new(Editor, XPosition, YPosition, Editor.ButtonWidth, Editor.ButtonHeight, Editor.ComponentBorderWidth, DropDown.FilterType.Block);
                        ComponentList.Add(blockDropDown);
                    }
                    //Dinamic add block insert button
                    ComponentList.Add(
                        new Button(Editor, XPosition, YPosition, "Add button", true, Editor.ButtonWidth, Editor.ButtonHeight, Editor.ButtonBorderWidth, Editor.BaseColor, Editor.BorderColor, Editor.HoverColor, new InsertExtraFieldToInspectorCommand(Editor, this, new DropDown(Editor, 0, 0, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, DropDown.FilterType.Block), 2), Button.ButtonType.Trigger));
                    break;
                case Block block:
                    //X Position
                    ComponentList.Add(new Label(XPosition, YPosition, "Position"));
                    ComponentList.Add(new Label(XPosition, YPosition, "X axis:"));
                    ComponentList.Add(
                       new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, block.XPosition.ToString(), Raylib.GetFontDefault(), true));
                    //Y Position
                    ComponentList.Add(new Label(XPosition, YPosition, "Y axis:"));
                    ComponentList.Add(
                        new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, block.YPosition.ToString(), Raylib.GetFontDefault(), true));
                    //Component
                    ComponentList.Add(new Label(XPosition, YPosition, "Component:"));
                    var blockComponentDropDown = new DropDown(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, DropDown.FilterType.NoBlock);
                    if (block.Component == null)
                    {
                        ComponentList.Add(blockComponentDropDown);
                    }
                    else
                    {
                        foreach (Component item in Editor.ActiveScene.ComponentList)
                        {
                            if (block.Component == item.RenderingObject)
                            {
                                blockComponentDropDown.Button.Component = item;
                                blockComponentDropDown.Button.Text = item.Name;
                            }
                        }
                        ComponentList.Add(blockComponentDropDown);
                    }
                    break;
                case DropBox dropBox:
                    //X Position
                    ComponentList.Add(new Label(XPosition, YPosition, "Position"));
                    ComponentList.Add(new Label(XPosition, YPosition, "X axis:"));
                    ComponentList.Add(
                       new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, dropBox.XPosition.ToString(), Raylib.GetFontDefault(), false));
                    //Y Position
                    ComponentList.Add(new Label(XPosition, YPosition, "Y axis:"));
                    ComponentList.Add(
                        new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, dropBox.YPosition.ToString(), Raylib.GetFontDefault(), false));
                    //Width
                    ComponentList.Add(new Label(XPosition, YPosition, "Width:"));
                    ComponentList.Add(
                        new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, dropBox.Width.ToString(), Raylib.GetFontDefault(), false));
                    //Height
                    ComponentList.Add(new Label(XPosition, YPosition, "Height:"));
                    ComponentList.Add(
                        new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, dropBox.Height.ToString(), Raylib.GetFontDefault(), false));
                    //Color
                    ComponentList.Add(new Label(XPosition, YPosition, "Color:"));
                    ComponentList.Add(
                        new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, $"{dropBox.Color.R}, {dropBox.Color.G}, {dropBox.Color.B}", Raylib.GetFontDefault(), false));
                    //Border color
                    ComponentList.Add(new Label(XPosition, YPosition, "Border color:"));
                    ComponentList.Add(
                        new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, $"{dropBox.BorderColor.R}, {dropBox.BorderColor.G}, {dropBox.BorderColor.B}", Raylib.GetFontDefault(), false));
                    //Options
                    ComponentList.Add(new Label(XPosition, YPosition, "Options:"));
                    for (int i = 0; i < dropBox.Options.Count; i++)
                    {
                        ComponentList.Add(new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, dropBox.Options[i].Text, Raylib.GetFontDefault(), false));
                    }
                    break;
                case Slider slider:
                    //X Position
                    ComponentList.Add(new Label(XPosition, YPosition, "Position"));
                    ComponentList.Add(new Label(XPosition, YPosition, "X axis:"));
                    ComponentList.Add(
                       new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, slider.XPosition.ToString(), Raylib.GetFontDefault(), true));
                    //Y Position
                    ComponentList.Add(new Label(XPosition, YPosition, "Y axis:"));
                    ComponentList.Add(
                        new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, slider.YPosition.ToString(), Raylib.GetFontDefault(), true));
                    //Width
                    ComponentList.Add(new Label(XPosition, YPosition, "Width:"));
                    ComponentList.Add(
                        new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, slider.Width.ToString(), Raylib.GetFontDefault(), false));
                    //Height
                    ComponentList.Add(new Label(XPosition, YPosition, "Height:"));
                    ComponentList.Add(
                        new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, slider.Height.ToString(), Raylib.GetFontDefault(), false));
                    //Color
                    ComponentList.Add(new Label(XPosition, YPosition, "Color:"));
                    ComponentList.Add(
                        new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, $"{slider.Color.R}, {slider.Color.G}, {slider.Color.B}", Raylib.GetFontDefault(), false));
                    //Border color
                    ComponentList.Add(new Label(XPosition, YPosition, "Border color:"));
                    ComponentList.Add(
                        new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, $"{slider.BorderColor.R}, {slider.BorderColor.G}, {slider.BorderColor.B}", Raylib.GetFontDefault(), false));
                    //Slider drag color
                    ComponentList.Add(new Label(XPosition, YPosition, "Slider drag color:"));
                    ComponentList.Add(
                        new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, $"{slider.DragColor.R}, {slider.DragColor.G}, {slider.DragColor.B}", Raylib.GetFontDefault(), false));
                    //Slider drag radius
                    ComponentList.Add(new Label(XPosition, YPosition, "Slider drag radius:"));
                    ComponentList.Add(
                        new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, slider.SliderDragRadius.ToString(), Raylib.GetFontDefault(), false));
                    //Slider value
                    ComponentList.Add(new Label(XPosition, YPosition, "Value:"));
                    ComponentList.Add(
                        new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, slider.Value.ToString(), Raylib.GetFontDefault(), false));
                    //Action
                    ComponentList.Add(new Label(XPosition, YPosition, "Action:"));
                    break;
                case Toggle toggle:
                    //X Position
                    ComponentList.Add(new Label(XPosition, YPosition, "Position"));
                    ComponentList.Add(new Label(XPosition, YPosition, "X axis:"));
                    ComponentList.Add(
                       new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, toggle.XPosition.ToString(), Raylib.GetFontDefault(), false));
                    //Y Position
                    ComponentList.Add(new Label(XPosition, YPosition, "Y axis:"));
                    ComponentList.Add(
                        new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, toggle.YPosition.ToString(), Raylib.GetFontDefault(), false));
                    //Size
                    ComponentList.Add(new Label(XPosition, YPosition, "Size:"));
                    ComponentList.Add(
                        new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, toggle.BoxSize.ToString(), Raylib.GetFontDefault(), false));
                    //Color
                    ComponentList.Add(new Label(XPosition, YPosition, "Color:"));
                    ComponentList.Add(
                        new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, $"{toggle.Color.R}, {toggle.Color.G}, {toggle.Color.B}", Raylib.GetFontDefault(), false));
                    //Border color
                    ComponentList.Add(new Label(XPosition, YPosition, "Border color:"));
                    ComponentList.Add(
                        new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, $"{toggle.BorderColor.R}, {toggle.BorderColor.G}, {toggle.BorderColor.B}", Raylib.GetFontDefault(), false));
                    //Toggle color
                    ComponentList.Add(new Label(XPosition, YPosition, "Toggled color:"));
                    ComponentList.Add(
                        new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, $"{toggle.ToggledColor.R}, {toggle.ToggledColor.G}, {toggle.ToggledColor.B}", Raylib.GetFontDefault(), false));
                    //Toggle state
                    ComponentList.Add(new Label(XPosition, YPosition, "State:"));
                    ComponentList.Add(
                        new ToggleButton(Editor, XPosition, YPosition, Editor.SmallButtonWidth, Editor.SmallButtonWidth, "Toggled:", toggle.IsToggled));
                    //Toggle text
                    ComponentList.Add(new Label(XPosition, YPosition, "Text:"));
                    ComponentList.Add(
                        new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, toggle.Text, Raylib.GetFontDefault(), false));
                    //Action
                    ComponentList.Add(new Label(XPosition, YPosition, "Action:"));
                    //Action here
                    break;
                case InputField inputField:
                    //X Position
                    ComponentList.Add(new Label(XPosition, YPosition, "Position"));
                    ComponentList.Add(new Label(XPosition, YPosition, "X axis:"));
                    ComponentList.Add(
                       new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, inputField.XPosition.ToString(), Raylib.GetFontDefault(), false));
                    //Y Position
                    ComponentList.Add(new Label(XPosition, YPosition, "Y axis:"));
                    ComponentList.Add(
                        new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, inputField.YPosition.ToString(), Raylib.GetFontDefault(), false));
                    //Button Y offset
                    ComponentList.Add(new Label(XPosition, YPosition, "Button Y offset:"));
                    ComponentList.Add(
                        new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, inputField.ButtonYOffset.ToString(), Raylib.GetFontDefault(), false));
                    //Width
                    ComponentList.Add(new Label(XPosition, YPosition, "Width:"));
                    ComponentList.Add(
                        new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, inputField.Width.ToString(), Raylib.GetFontDefault(), false));
                    //Height
                    ComponentList.Add(new Label(XPosition, YPosition, "Height:"));
                    ComponentList.Add(
                        new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, inputField.Height.ToString(), Raylib.GetFontDefault(), false));
                    //Text
                    ComponentList.Add(new Label(XPosition, YPosition, "Text:"));
                    ComponentList.Add(
                        new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, inputField.Text, Raylib.GetFontDefault(), false));
                    //Placeholder
                    ComponentList.Add(new Label(XPosition, YPosition, "Placeholder:"));
                    ComponentList.Add(
                        new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, inputField.Placeholder, Raylib.GetFontDefault(), false));
                    //Color
                    ComponentList.Add(new Label(XPosition, YPosition, "Color:"));
                    ComponentList.Add(
                        new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, $"{inputField.Color.R}, {inputField.Color.G}, {inputField.Color.B}", Raylib.GetFontDefault(), false));
                    //Border color
                    ComponentList.Add(new Label(XPosition, YPosition, "Border color:"));
                    ComponentList.Add(
                        new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, $"{inputField.BorderColor.R}, {inputField.BorderColor.G}, {inputField.BorderColor.B}", Raylib.GetFontDefault(), false));
                    //Hover color
                    ComponentList.Add(new Label(XPosition, YPosition, "Hover color:"));
                    ComponentList.Add(
                        new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, $"{inputField.HoverColor.R}, {inputField.HoverColor.G}, {inputField.HoverColor.B}", Raylib.GetFontDefault(), false));
                    //Action
                    break;
            }
            ActiveComponent = component;
            Scrollbar.AddComponents([.. ComponentList]);
            UpdateComponentPosition(EnabledRowComponentCount);
        }
        internal void SetActiveComponent(IAction eventData)
        {
            Scrollbar.DropComponents();
            switch (eventData)
            {
                case CreateMenuAction createMenuAction:
                    ComponentList.Add(new Label(XPosition, YPosition + BorderWidth, "Name:"));
                    ComponentList.Add(new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, "Create Menu", Raylib.GetFontDefault(), true));
                    ComponentList.Add(new Label(XPosition, YPosition + BorderWidth, "Description:"));
                    ComponentList.Add(new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, "Display a menu.", Raylib.GetFontDefault(), true));
                    ComponentList.Add(new Label(XPosition, YPosition + BorderWidth, "Menu:"));
                    DropDown menuDropDown = new DropDown(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, DropDown.FilterType.Menu);
                    if (menuDropDown.FilteredButtonList.Count > 0)
                    {
                        foreach (Button item in menuDropDown.FilteredButtonList)
                        {
                            if (createMenuAction.Menu == ((Component)item.Component).RenderingObject)
                            {
                                menuDropDown.Button.Component = item.Component;
                                menuDropDown.Button.Text = ((Component)item.Component).Name;
                            }
                        }
                    }
                    ComponentList.Add(menuDropDown);
                    break;
                case LoadSceneAction loadSceneAction:
                    ComponentList.Add(new Label(XPosition, YPosition + BorderWidth, "Name:"));
                    ComponentList.Add(new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, "Load Scene", Raylib.GetFontDefault(), true));
                    ComponentList.Add(new Label(XPosition, YPosition + BorderWidth, "Description:"));
                    ComponentList.Add(new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, "Loads a scene, if the selected variable's condition is true.", Raylib.GetFontDefault(), true));
                    ComponentList.Add(new Label(XPosition, YPosition + BorderWidth, "Trigger variable's name:"));
                    ComponentList.Add(new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, loadSceneAction.TriggerVariableName, Raylib.GetFontDefault(), false));
                    ComponentList.Add(new Label(XPosition, YPosition + BorderWidth, "Loading scene's ID:"));
                    ComponentList.Add(new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, loadSceneAction.sceneID.ToString(), Raylib.GetFontDefault(), false));
                    break;
                case NativeLoadSceneAction nativeLoadSceneAction:
                    ComponentList.Add(new Label(XPosition, YPosition + BorderWidth, "Name:"));
                    ComponentList.Add(new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, "Load Scene", Raylib.GetFontDefault(), true));
                    ComponentList.Add(new Label(XPosition, YPosition + BorderWidth, "Description:"));
                    ComponentList.Add(new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, "Loads a scene instantly.", Raylib.GetFontDefault(), true));
                    ComponentList.Add(new Label(XPosition, YPosition + BorderWidth, "Loading scene's ID:"));
                    ComponentList.Add(new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, nativeLoadSceneAction.sceneID.ToString(), Raylib.GetFontDefault(), false));
                    break;
                case AddSpriteAction addSpriteAction:
                    ComponentList.Add(new Label(XPosition, YPosition + BorderWidth, "Name:"));
                    ComponentList.Add(new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, "Add Sprite", Raylib.GetFontDefault(), true));
                    ComponentList.Add(new Label(XPosition, YPosition + BorderWidth, "Description:"));
                    ComponentList.Add(new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, "Display a sprite.", Raylib.GetFontDefault(), true));
                    ComponentList.Add(new Label(XPosition, YPosition + BorderWidth, "Sprite:"));
                    DropDown addSpriteActionDropDown = new DropDown(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, DropDown.FilterType.Sprite);
                    if (addSpriteActionDropDown.FilteredButtonList.Count > 0)
                    {
                        foreach (Button item in addSpriteActionDropDown.FilteredButtonList)
                        {
                            if (addSpriteAction.sprite == ((Component)item.Component).RenderingObject)
                            {
                                addSpriteActionDropDown.Button.Component = item.Component;
                                addSpriteActionDropDown.Button.Text = ((Component)item.Component).Name;
                            }
                        }
                    }
                    ComponentList.Add(addSpriteActionDropDown);
                    break;
                case ChangeSpriteAction changeSpriteAction:
                    ComponentList.Add(new Label(XPosition, YPosition + BorderWidth, "Name:"));
                    ComponentList.Add(new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, "Change Sprite", Raylib.GetFontDefault(), true));
                    ComponentList.Add(new Label(XPosition, YPosition + BorderWidth, "Description:"));
                    ComponentList.Add(new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, "Swap active sprite to a new one.", Raylib.GetFontDefault(), true));
                    ComponentList.Add(new Label(XPosition, YPosition + BorderWidth, "Active Sprite:"));
                    DropDown changeSpriteDropDown = new(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, DropDown.FilterType.Sprite);
                    if (changeSpriteDropDown.FilteredButtonList.Count > 0)
                    {
                        foreach (Button item in changeSpriteDropDown.FilteredButtonList)
                        {
                            if (changeSpriteAction.sprite == ((Component)item.Component).RenderingObject)
                            {
                                changeSpriteDropDown.Button.Component = item.Component;
                                changeSpriteDropDown.Button.Text = ((Component)item.Component).Name;
                            }
                        }
                    }
                    ComponentList.Add(changeSpriteDropDown);
                    ComponentList.Add(new Label(XPosition, YPosition + BorderWidth, "New Sprite:"));
                    ComponentList.Add(new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, changeSpriteAction.replacementSprite.Path, Raylib.GetFontDefault(), false));
                    break;
                case RemoveSpriteAction removeSpriteAction:
                    ComponentList.Add(new Label(XPosition, YPosition + BorderWidth, "Name:"));
                    ComponentList.Add(new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, "Remove Sprite", Raylib.GetFontDefault(), true));
                    ComponentList.Add(new Label(XPosition, YPosition + BorderWidth, "Description:"));
                    ComponentList.Add(new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, "Removes a sprite from the scene.", Raylib.GetFontDefault(), true));
                    ComponentList.Add(new Label(XPosition, YPosition + BorderWidth, "Sprite:"));
                    DropDown removeSpriteDropDown = new DropDown(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, DropDown.FilterType.Sprite);
                    if (removeSpriteDropDown.FilteredButtonList.Count > 0)
                    {
                        foreach (Button item in removeSpriteDropDown.FilteredButtonList)
                        {
                            if (removeSpriteAction.sprite == ((Component)item.Component).RenderingObject)
                            {
                                removeSpriteDropDown.Button.Component = item.Component;
                                removeSpriteDropDown.Button.Text = ((Component)item.Component).Name;
                            }
                        }
                    }
                    ComponentList.Add(removeSpriteDropDown);

                    break;
                case DecrementVariableAction decrementVariableAction:
                    ComponentList.Add(new Label(XPosition, YPosition + BorderWidth, "Decrement Variable Action"));
                    ComponentList.Add(new Label(XPosition, YPosition + BorderWidth, "Name:"));
                    ComponentList.Add(new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, "Decrement Variable", Raylib.GetFontDefault(), true));
                    ComponentList.Add(new Label(XPosition, YPosition + BorderWidth, "Description:"));
                    ComponentList.Add(new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, "Decreases the variable by a value. Only supports Integer and float variables", Raylib.GetFontDefault(), true));
                    ComponentList.Add(new Label(XPosition, YPosition + BorderWidth, "Variable name:"));
                    ComponentList.Add(new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, decrementVariableAction.VariableName, Raylib.GetFontDefault(), false));
                    ComponentList.Add(new Label(XPosition, YPosition + BorderWidth, "Value:"));
                    ComponentList.Add(new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, decrementVariableAction.DecrementIntegerValue.ToString(), Raylib.GetFontDefault(), false));
                    break;
                case IncrementVariableAction incrementVariableAction:
                    ComponentList.Add(new Label(XPosition, YPosition + BorderWidth, "Increment Variable Action"));
                    ComponentList.Add(new Label(XPosition, YPosition + BorderWidth, "Name:"));
                    ComponentList.Add(new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, "Increment Variable", Raylib.GetFontDefault(), true));
                    ComponentList.Add(new Label(XPosition, YPosition + BorderWidth, "Description:"));
                    ComponentList.Add(new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, "Increases the variable by a value. Only supports Integer and float variables", Raylib.GetFontDefault(), true));
                    ComponentList.Add(new Label(XPosition, YPosition + BorderWidth, "Variable:"));
                    ComponentList.Add(new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, incrementVariableAction.VariableName, Raylib.GetFontDefault(), false));
                    ComponentList.Add(new Label(XPosition, YPosition + BorderWidth, "Increment variable Name:"));
                    ComponentList.Add(new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, incrementVariableAction.IncrementVariableName, Raylib.GetFontDefault(), false));
                    break;
                case SetBoolVariableAction setBoolVariableAction:
                    ComponentList.Add(new Label(XPosition, YPosition + BorderWidth, "Set Bool Variable Action"));
                    ComponentList.Add(new Label(XPosition, YPosition + BorderWidth, "Name:"));
                    ComponentList.Add(new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, "Set Bool Variable", Raylib.GetFontDefault(), true));
                    ComponentList.Add(new Label(XPosition, YPosition + BorderWidth, "Description:"));
                    ComponentList.Add(new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, "Sets a boolean variable to a value.", Raylib.GetFontDefault(), true));
                    ComponentList.Add(new Label(XPosition, YPosition + BorderWidth, "Variable:"));
                    ComponentList.Add(new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, setBoolVariableAction.VariableName, Raylib.GetFontDefault(), false));
                    ComponentList.Add(new ToggleButton(Editor, XPosition, YPosition, Editor.SmallButtonWidth, Editor.ComponentBorderWidth, "Value", setBoolVariableAction.Value));
                    break;
                case SetVariableFalseAction setVariableFalseAction:
                    ComponentList.Add(new Label(XPosition, YPosition + BorderWidth, "Set Variable False Action"));
                    ComponentList.Add(new Label(XPosition, YPosition + BorderWidth, "Name:"));
                    ComponentList.Add(new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, "Set Variable False", Raylib.GetFontDefault(), true));
                    ComponentList.Add(new Label(XPosition, YPosition + BorderWidth, "Description:"));
                    ComponentList.Add(new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, "Sets a boolean variable to false.", Raylib.GetFontDefault(), true));
                    ComponentList.Add(new Label(XPosition, YPosition + BorderWidth, "Variable:"));
                    ComponentList.Add(new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, setVariableFalseAction.VariableName, Raylib.GetFontDefault(), false));
                    break;
                case SetVariableTrueAction setVariableTrueAction:
                    ComponentList.Add(new Label(XPosition, YPosition + BorderWidth, "Set Variable True Action"));
                    ComponentList.Add(new Label(XPosition, YPosition + BorderWidth, "Name:"));
                    ComponentList.Add(new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, "Set Variable True", Raylib.GetFontDefault(), true));
                    ComponentList.Add(new Label(XPosition, YPosition + BorderWidth, "Description:"));
                    ComponentList.Add(new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, "Sets a boolean variable to true.", Raylib.GetFontDefault(), true));
                    ComponentList.Add(new Label(XPosition, YPosition + BorderWidth, "Variable:"));
                    ComponentList.Add(new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, setVariableTrueAction.VariableName, Raylib.GetFontDefault(), false));
                    break;
                case TextBoxCreateAction textBoxCreateAction:
                    ComponentList.Add(new Label(XPosition, YPosition + BorderWidth, "Name:"));
                    ComponentList.Add(new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, "Create TextBox", Raylib.GetFontDefault(), true));
                    ComponentList.Add(new Label(XPosition, YPosition + BorderWidth, "Description:"));
                    ComponentList.Add(new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, "Loads an instance of a textbox to the screen.", Raylib.GetFontDefault(), true));
                    ComponentList.Add(new Label(XPosition, YPosition + BorderWidth, "TextBox:"));
                    DropDown textBoxDropDown = new DropDown(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, DropDown.FilterType.TextBox);
                    if (textBoxDropDown.FilteredButtonList.Count > 0)
                    {
                        foreach (Button item in textBoxDropDown.FilteredButtonList)
                        {
                            if (textBoxCreateAction.TextBox == ((Component)item.Component).RenderingObject)
                            {
                                textBoxDropDown.Button.Component = item.Component;
                                textBoxDropDown.Button.Text = item.Text;
                            }
                        }
                    }
                    ComponentList.Add(textBoxDropDown);
                    break;
                case TintSpriteAction tintSpriteAction:
                    ComponentList.Add(new Label(XPosition, YPosition + BorderWidth, "Name:"));
                    ComponentList.Add(new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, "Tint Sprite", Raylib.GetFontDefault(), false));
                    ComponentList.Add(new Label(XPosition, YPosition + BorderWidth, "Description:"));
                    ComponentList.Add(new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, "Tints a sprite to a color.", Raylib.GetFontDefault(), false));
                    ComponentList.Add(new Label(XPosition, YPosition + BorderWidth, "Sprite:"));
                    DropDown tintSpriteDropDown = new DropDown(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, DropDown.FilterType.Sprite);
                    if (tintSpriteDropDown.FilteredButtonList.Count > 0)
                    {
                        foreach (Button item in tintSpriteDropDown.FilteredButtonList)
                        {
                            if (tintSpriteAction.sprite == ((Component)item.Component).RenderingObject)
                            {
                                tintSpriteDropDown.Button.Component = item.Component;
                                tintSpriteDropDown.Button.Text = ((Component)item.Component).Name;
                            }
                        }
                    }
                    ComponentList.Add(tintSpriteDropDown);
                    ComponentList.Add(new Label(XPosition, YPosition + BorderWidth, "Color:"));
                    ComponentList.Add(
                        new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, $"{tintSpriteAction.color.R}, {tintSpriteAction.color.G}, {tintSpriteAction.color.B}", Raylib.GetFontDefault(), false));
                    break;
                case ToggleVariableAction toggleVariableAction:
                    ComponentList.Add(new Label(XPosition, YPosition + BorderWidth, "Name:"));
                    ComponentList.Add(new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, "Variable name", Raylib.GetFontDefault(), false));
                    ComponentList.Add(new Label(XPosition, YPosition + BorderWidth, "Description:"));
                    ComponentList.Add(new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, "Toggles a boolean variable to it's opposing binary value.", Raylib.GetFontDefault(), true));
                    ComponentList.Add(new Label(XPosition, YPosition + BorderWidth, "Variable:"));
                    ComponentList.Add(new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, toggleVariableAction.VariableName, Raylib.GetFontDefault(), false));
                    break;
                case EmptyAction:
                    ComponentList.Add(new Label(XPosition, YPosition + BorderWidth, "Name:"));
                    ComponentList.Add(new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, "Empty Action", Raylib.GetFontDefault(), true));
                    ComponentList.Add(new Label(XPosition, YPosition + BorderWidth, "Description:"));
                    ComponentList.Add(new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, "An empty action does nothing.", Raylib.GetFontDefault(), true));
                    break;
                case SetVariableValueAction setVariableValueAction:
                    ComponentList.Add(new Label(XPosition, YPosition + BorderWidth, "Name:"));
                    ComponentList.Add(new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, "Set Variable Value", Raylib.GetFontDefault(), true));
                    ComponentList.Add(new Label(XPosition, YPosition + BorderWidth, "Description:"));
                    ComponentList.Add(new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, "Sets a rendering object to a string variable.", Raylib.GetFontDefault(), true));
                    ComponentList.Add(new Label(XPosition, YPosition + BorderWidth, "Variable name:"));
                    ComponentList.Add(new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, setVariableValueAction.VariableName, Raylib.GetFontDefault(), false));
                    ComponentList.Add(new Label(XPosition, YPosition + BorderWidth, "Set component:"));
                    DropDown setVariableValueDropDown = new DropDown(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, DropDown.FilterType.SliderToggleInputField);
                    if (setVariableValueDropDown.FilteredButtonList.Count > 0)
                    {
                        foreach (Button item in setVariableValueDropDown.FilteredButtonList)
                        {
                            if (setVariableValueDropDown.Button.Component == item.Component)
                            {
                                setVariableValueDropDown.Button.Component = item.Component;
                                setVariableValueDropDown.Button.Text = item.Text;
                            }
                        }
                    }
                    ComponentList.Add(setVariableValueDropDown);
                    break;
                case SwitchStaticMenuAction switchStaticMenuAction:
                    ComponentList.Add(new Label(XPosition, YPosition + BorderWidth, "Name:"));
                    ComponentList.Add(new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, "Switch Static Menu", Raylib.GetFontDefault(), true));
                    ComponentList.Add(new Label(XPosition, YPosition + BorderWidth, "Description:"));
                    ComponentList.Add(new TextField(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, "Switches the active static menu to another.", Raylib.GetFontDefault(), true));
                    ComponentList.Add(new Label(XPosition, YPosition + BorderWidth, "Menu:"));
                    DropDown switchStaticMenuOldDropDown = new DropDown(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, DropDown.FilterType.Menu);
                    if (switchStaticMenuOldDropDown.FilteredButtonList.Count > 0)
                    {
                        foreach (Button item in switchStaticMenuOldDropDown.FilteredButtonList)
                        {
                            if (switchStaticMenuAction.DisablingMenu == ((Component)item.Component).RenderingObject)
                            {
                                switchStaticMenuOldDropDown.Button.Component = item.Component;
                                switchStaticMenuOldDropDown.Button.Text = ((Component)item.Component).Name;
                            }
                        }
                    }
                    ComponentList.Add(switchStaticMenuOldDropDown);
                    ComponentList.Add(new Label(XPosition, YPosition + BorderWidth, "New Menu:"));
                    DropDown switchStaticMenuNewDopDown = new DropDown(Editor, XPosition, YPosition, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, DropDown.FilterType.Menu);
                    if (switchStaticMenuNewDopDown.FilteredButtonList.Count > 0)
                    {
                        foreach (Button item in switchStaticMenuNewDopDown.FilteredButtonList)
                        {
                            if (switchStaticMenuAction.EnablingMenu == ((Component)item.Component).RenderingObject)
                            {
                                switchStaticMenuNewDopDown.Button.Component = item.Component;
                                switchStaticMenuNewDopDown.Button.Text = ((Component)item.Component).Name;
                            }
                        }
                    }
                    ComponentList.Add(switchStaticMenuNewDopDown);
                    break;
            }
            ActiveAction = eventData;
            Scrollbar.AddComponents([.. ComponentList]);
            UpdateComponentPosition(EnabledRowComponentCount);
        }
        internal void DropActiveComponent()
        {
            ComponentList.Clear();
            ActiveComponent = null;
            ActiveAction = null;
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
                ComponentList[i].XPosition = rowcount > 1 ? ComponentList[i % enabledRowComponentCount].XPosition : XPosition + (Width / 2 - Editor.ComponentWidth / 2);
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
                if (ComponentList[i].YPosition <= YPosition + Height && ComponentList[i].YPosition >= YPosition - Editor.ComponentWidth) ComponentList[i].Render();
            }
        }
    }
}