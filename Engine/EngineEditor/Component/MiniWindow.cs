using Raylib_cs;
using TemplateGame.Component;
using VisualNovelEngine.Engine.EngineEditor.Component.Command;
using VisualNovelEngine.Engine.EngineEditor.Interface;

namespace VisualNovelEngine.Engine.EngineEditor.Component
{
    /// <summary>
    /// A mini window is a window that is only shown if the player presses the right click.
    /// </summary>
    public class MiniWindow : IWindow
    {
        public enum miniWindowType
        {
            Vertical,
            Horizontal
        }
        internal miniWindowType Type { get; set; }
        private Editor Editor { get; set; }
        internal int XPosition { get; set; }
        internal int YPosition { get; set; }
        internal int Width { get; set; }
        internal int Height { get; set; }
        internal int BorderWidth { get; set; }
        internal Color Color { get; set; }
        internal Color BorderColor { get; set; }
        internal bool IsHover { get; set; } = false;
        internal bool HasVariableComponent { get; set; }
        internal bool HasSceneComponent { get; set; }
        internal List<IComponent> VariableComponentList { get; set; } = [];
        internal List<Button> ButtonComponentList { get; set; } = [];
        internal List<IComponent>? ComponentList { get; set; }
        internal Scrollbar Scrollbar { get; set; }
        internal Button? CloseButton { get; set; }

        public MiniWindow(Editor editor, bool closeButton, bool hasVariableComponent, bool hasScene, int xPosition, int yPosition, int width, int height, int borderWidth, Color color, Color borderColor, miniWindowType miniWindowType, Button[] buttons)
        {
            Editor = editor;
            Type = miniWindowType;
            XPosition = xPosition;
            YPosition = yPosition;
            Width = width;
            Height = height;
            BorderWidth = borderWidth;
            Color = color;
            BorderColor = borderColor;
            ButtonComponentList.AddRange(buttons);
            HasVariableComponent = hasVariableComponent;
            HasSceneComponent = hasScene;
            if (closeButton) CloseButton = new Button(Editor, XPosition + Width, YPosition, "X", true, Editor.SmallButtonWidth, Editor.SmallButtonHeight, Editor.SmallButtonBorderWidth, Editor.CloseButtonBaseColor, Editor.CloseButtonBorderColor, Editor.CloseButtonHoverColor, new CloseMiniWindowCommand(Editor, this), Button.ButtonType.Trigger);
            UpdateComponentPosition();
            switch (Type)
            {
                case miniWindowType.Vertical:
                    Scrollbar = new Scrollbar(Editor, XPosition + Width, closeButton ? YPosition + Editor.SmallButtonHeight : YPosition, closeButton ? Width - Editor.SmallButtonHeight : Width, Editor.SmallButtonWidth, Scrollbar.ScrollbarType.Vertical, false, [.. ButtonComponentList]);
                    break;
                case miniWindowType.Horizontal:
                    Scrollbar = new Scrollbar(Editor, XPosition, YPosition + Height - Editor.SmallButtonHeight, Editor.SmallButtonHeight, Width, Scrollbar.ScrollbarType.Horizontal, false, [.. ButtonComponentList]);
                    break;
            }
        }

        public MiniWindow(Editor editor, bool closeButton, bool hasVariable, bool hasScene, int xPosition, int yPosition, int width, int height, int borderWidth, Color color, Color borderColor, miniWindowType miniWindowType, IComponent[] components)
        {
            Editor = editor;
            Type = miniWindowType;
            XPosition = xPosition;
            YPosition = yPosition;
            Width = width;
            Height = height;
            BorderWidth = borderWidth;
            Color = color;
            BorderColor = borderColor;
            ComponentList = [];
            ComponentList.AddRange(components);
            HasVariableComponent = hasVariable;
            HasSceneComponent = hasScene;
            if (closeButton) CloseButton = new Button(Editor, XPosition + Width - Editor.SmallButtonWidth, YPosition, "X", true, Editor.SmallButtonWidth, Editor.SmallButtonHeight, Editor.SmallButtonBorderWidth, Editor.CloseButtonBaseColor, Editor.CloseButtonBorderColor, Editor.CloseButtonHoverColor, new CloseMiniWindowCommand(Editor, this), Button.ButtonType.Trigger);
            UpdateComponentPosition();
            switch (Type)
            {
                case miniWindowType.Vertical:
                    Scrollbar = new Scrollbar(Editor, XPosition + Width - Editor.SmallButtonWidth, closeButton ? YPosition + Editor.SmallButtonHeight : YPosition, Height - Editor.SmallButtonHeight, Editor.SmallButtonWidth, Scrollbar.ScrollbarType.Vertical, false, [.. ComponentList]);
                    break;
                case miniWindowType.Horizontal:
                    Scrollbar = new Scrollbar(Editor, XPosition, YPosition + Height - Editor.SmallButtonHeight, Editor.SmallButtonHeight, Width, Scrollbar.ScrollbarType.Horizontal, false, [.. components]);
                    break;
            }
        }
        internal void FetchActiveSceneAttributes()
        {
            //Fetch the active scene name
            TextField sceneNameTextField = ComponentList[2] as TextField;
            sceneNameTextField.Text = Editor.ActiveScene.Name;
            //Fetch and set activeComponents
            DropDown sceneBackgroundDropDown = ComponentList[3] as DropDown;
            sceneBackgroundDropDown.Button.Text = Editor.ActiveScene.BackgroundOption.ToString();
            switch (Editor.ActiveScene.BackgroundOption)
            {
                case TemplateGame.Component.Scene.BackgroundOption.SolidColor:
                    Label sceneBackgroundColorLabel = new(XPosition, YPosition, "Background Color");
                    TextField sceneBackgroundColor = new(Editor, 0, 0, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, $"{Editor.ActiveScene.BackgroundColor.Value.R}, {Editor.ActiveScene.BackgroundColor.Value.G}, {Editor.ActiveScene.BackgroundColor.Value.B}", Raylib.GetFontDefault(), false);
                    ComponentList.Insert(ComponentList.Count - 1, sceneBackgroundColorLabel);
                    ComponentList.Insert(ComponentList.Count - 1, sceneBackgroundColor);
                    Scrollbar.Components.Insert(Scrollbar.Components.Count - 1, sceneBackgroundColorLabel);
                    Scrollbar.Components.Insert(Scrollbar.Components.Count - 1, sceneBackgroundColor);
                    break;
                case TemplateGame.Component.Scene.BackgroundOption.Image:
                    Label sceneBackgroundImageLabel = new(XPosition, YPosition, "Background Image");
                    TextField sceneBackgroundImage = new(Editor, 0, 0, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, Editor.ActiveScene.BackgroundImage.ToString(), Raylib.GetFontDefault(), false);
                    ComponentList.Insert(ComponentList.Count - 1, sceneBackgroundImageLabel);
                    ComponentList.Insert(ComponentList.Count - 1, sceneBackgroundImage);
                    Scrollbar.Components.Insert(Scrollbar.Components.Count - 1, sceneBackgroundImageLabel);
                    Scrollbar.Components.Insert(Scrollbar.Components.Count - 1, sceneBackgroundImage);
                    break;
                case TemplateGame.Component.Scene.BackgroundOption.GradientHorizontal:
                    Label sceneBackgroundGradientHorizontalLabel = new(XPosition, YPosition, "Background Gradient Horizontal");
                    TextField sceneBackgroundGradientHorizontalColor1 = new(Editor, 0, 0, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, $"{Editor.ActiveScene.BackgroundGradientColor[0].R}, {Editor.ActiveScene.BackgroundGradientColor[0].G}, {Editor.ActiveScene.BackgroundGradientColor[0].B}", Raylib.GetFontDefault(), false);
                    TextField sceneBackgroundGradientHorizontalColor2 = new(Editor, 0, 0, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, $"{Editor.ActiveScene.BackgroundGradientColor[1].R}, {Editor.ActiveScene.BackgroundGradientColor[1].G}, {Editor.ActiveScene.BackgroundGradientColor[1].B}", Raylib.GetFontDefault(), false);
                    ComponentList.Insert(ComponentList.Count - 1, sceneBackgroundGradientHorizontalLabel);
                    ComponentList.Insert(ComponentList.Count - 1, sceneBackgroundGradientHorizontalColor1);
                    ComponentList.Insert(ComponentList.Count - 1, sceneBackgroundGradientHorizontalColor2);
                    Scrollbar.Components.Insert(Scrollbar.Components.Count - 1, sceneBackgroundGradientHorizontalLabel);
                    Scrollbar.Components.Insert(Scrollbar.Components.Count - 1, sceneBackgroundGradientHorizontalColor1);
                    Scrollbar.Components.Insert(Scrollbar.Components.Count - 1, sceneBackgroundGradientHorizontalColor2);
                    break;
                case TemplateGame.Component.Scene.BackgroundOption.GradientVertical:
                    Label sceneBackgroundGradientVerticalLabel = new(XPosition, YPosition, "Background Gradient Vertical");
                    TextField sceneBackgroundGradientVerticalColor1 = new(Editor, 0, 0, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, $"{Editor.ActiveScene.BackgroundGradientColor[0].R}, {Editor.ActiveScene.BackgroundGradientColor[0].G}, {Editor.ActiveScene.BackgroundGradientColor[0].B}", Raylib.GetFontDefault(), false);
                    TextField sceneBackgroundGradientVerticalColor2 = new(Editor, 0, 0, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, $"{Editor.ActiveScene.BackgroundGradientColor[1].R}, {Editor.ActiveScene.BackgroundGradientColor[1].G}, {Editor.ActiveScene.BackgroundGradientColor[1].B}", Raylib.GetFontDefault(), false);
                    ComponentList.Insert(ComponentList.Count - 1, sceneBackgroundGradientVerticalLabel);
                    ComponentList.Insert(ComponentList.Count - 1, sceneBackgroundGradientVerticalColor1);
                    ComponentList.Insert(ComponentList.Count - 1, sceneBackgroundGradientVerticalColor2);
                    Scrollbar.Components.Insert(Scrollbar.Components.Count - 1, sceneBackgroundGradientVerticalLabel);
                    Scrollbar.Components.Insert(Scrollbar.Components.Count - 1, sceneBackgroundGradientVerticalColor1);
                    Scrollbar.Components.Insert(Scrollbar.Components.Count - 1, sceneBackgroundGradientVerticalColor2);
                    break;
            }
            UpdateComponentPosition();
        }
        internal void FetchVariables()
        {
            for (int i = 0; i < Editor.GameVariables.Count; i++)
            {
                TextField VariableNameField = new(Editor, 0, 0, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, Editor.GameVariables[i].Name, Raylib.GetFontDefault(), false);
                TextField VariableValueField = new(Editor, 0, 0, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, Editor.GameVariables[i].Value, Raylib.GetFontDefault(), false);
                DropDown VariableDropDown = new(Editor, 0, 0, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, DropDown.FilterType.VariableType);
                VariableDropDown.Button.VariableType = Editor.GameVariables[i].Type;
                VariableDropDown.Button.Text = Editor.GameVariables[i].Type.ToString();
                //Add button options for all types of variables
                foreach (VariableType variableType in Enum.GetValues(typeof(VariableType)))
                {
                    Button button = new(Editor, VariableDropDown, variableType.ToString(), Editor.BaseColor, Editor.BorderColor, Editor.HoverColor, null);
                    button.VariableType = variableType;
                    VariableDropDown.ButtonList.Add(button);
                }
                //Only add to the list if the variable is not already in the list
                if (ComponentList.FindIndex(x => x is TextField textField && textField.Text == VariableNameField.Text) == -1)
                {
                    ComponentList.AddRange([VariableNameField, VariableValueField, VariableDropDown]);
                    VariableComponentList.AddRange([VariableNameField, VariableValueField, VariableDropDown]);
                    Scrollbar.AddComponents([VariableNameField, VariableValueField, VariableDropDown]);
                    UpdateComponentPosition();
                }
            }
        }
        internal void UpdateComponentPosition()
        {
            switch (ComponentList is null)
            {
                case true:
                    if (ButtonComponentList.Count < 1) return;
                    switch (Type)
                    {
                        case miniWindowType.Vertical:
                            for (int i = 0; i < ButtonComponentList.Count; i++)
                            {
                                ButtonComponentList[i].XPosition = XPosition;
                                ButtonComponentList[i].YPosition = YPosition + (i * Editor.SmallButtonHeight);
                            }
                            break;
                        case miniWindowType.Horizontal:
                            for (int i = 0; i < ButtonComponentList.Count; i++)
                            {
                                ButtonComponentList[i].XPosition = XPosition + (i * Editor.ButtonWidth);
                                ButtonComponentList[i].YPosition = YPosition;
                            }
                            break;
                    }
                    break;
                case false:
                    if (ComponentList.Count < 1) return;
                    switch (Type)
                    {
                        case miniWindowType.Vertical:
                            for (int i = 0; i < ComponentList.Count; i++)
                            {
                                ComponentList[i].XPosition = XPosition;
                                ComponentList[i].YPosition = YPosition + ((i + 1) * Editor.ComponentHeight);
                            }
                            break;
                        case miniWindowType.Horizontal:
                            for (int i = 0; i < ComponentList.Count; i++)
                            {
                                ComponentList[i].XPosition = XPosition + (i * Editor.ButtonWidth);
                                ComponentList[i].YPosition = YPosition;
                            }
                            break;
                    }
                    break;
            }
        }
        public void Show()
        {
            IsHover = Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), new Rectangle(XPosition, YPosition, Width, Height));
            Raylib.DrawRectangle(XPosition, YPosition, Width, Height, Color);
            Raylib.DrawRectangleLines(XPosition, YPosition, Width, Height, BorderColor);
            CloseButton?.Render();
            if (ComponentList is not null)
            {
                switch (Type)
                {
                    case miniWindowType.Vertical:
                        if (YPosition + ComponentList.Count * Editor.ComponentHeight > Raylib.GetScreenHeight() / 2 + Height / 2) Scrollbar.Render();
                        for (int i = 0; i < ComponentList.Count; i++) if (ComponentList[i].YPosition < Raylib.GetScreenHeight() / 2 + Height / 2 && ComponentList[i].YPosition > YPosition) ComponentList[i].Render();
                        break;
                    case miniWindowType.Horizontal:
                        if (ComponentList.Count * Editor.ComponentWidth > Raylib.GetScreenWidth() / 2 + Width / 2) Scrollbar.Render();
                        for (int i = 0; i < ComponentList.Count; i++) ComponentList[i].Render();
                        break;
                }
                return;
            }
            if (ButtonComponentList == null) return;
            for (int i = 0; i < ButtonComponentList.Count; i++)
            {
                ButtonComponentList[i].Render();
            }
            switch (Type)
            {
                case miniWindowType.Vertical:
                    if (ButtonComponentList.Count * Editor.SmallButtonHeight > Height)
                        Scrollbar.Render();
                    break;
                case miniWindowType.Horizontal:
                    if (ButtonComponentList.Count * Editor.ButtonWidth >= Width)
                        Scrollbar.Render();
                    break;
            }
        }
    }
}