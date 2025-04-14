using Raylib_cs;
using VisualNovelEngine.Engine.Game.Component;
using VisualNovelEngine.Engine.Editor.Component.Command;
using VisualNovelEngine.Engine.Editor.Interface;

namespace VisualNovelEngine.Engine.Editor.Component
{
    /// <summary>
    /// Represents the types which the mini window can have.
    /// </summary>
    public enum MiniWindowType
    {
        Vertical,
        Horizontal
    }
    /// <summary>
    /// A mini window is a small window that can be used to display a number of informations and interactable components.
    /// </summary>
    public class MiniWindow : IWindow
    {
        /// <summary>
        /// The type of the mini window.
        /// </summary>
        internal MiniWindowType Type { get; set; }
        /// <summary>
        /// Represents the editor.
        /// </summary>
        private Editor Editor { get; set; }
        /// <summary>
        /// The x position of the mini window.
        /// </summary>
        internal int XPosition { get; set; }
        /// <summary>
        /// The y position of the mini window.
        /// </summary>
        internal int YPosition { get; set; }
        /// <summary>
        /// The width of the mini window.
        /// </summary>
        internal int Width { get; set; }
        /// <summary>
        /// The height of the mini window.
        /// </summary>
        internal int Height { get; set; }
        /// <summary>
        /// The border width of the mini window.
        /// </summary>
        internal int BorderWidth { get; set; }
        /// <summary>
        /// The color of the mini window.
        /// </summary>
        internal Color Color { get; set; }
        /// <summary>
        /// The border color of the mini window.
        /// </summary>
        internal Color BorderColor { get; set; }
        /// <summary>
        /// Whether the mini window is being hovered over.
        /// </summary>
        internal bool IsHover { get; set; } = false;
        /// <summary>
        /// Whether the mini window is active.
        /// </summary>
        internal bool HasVariableComponent { get; set; }
        /// <summary>
        /// Whether the mini window has a scene component.
        /// </summary>
        internal bool HasSceneComponent { get; set; }
        /// <summary>
        /// The list of variable components.
        /// </summary>
        internal List<IComponent> VariableComponentList { get; set; } = [];
        /// <summary>
        /// The list of button components.
        /// </summary>
        internal List<Button> ButtonComponentList { get; set; } = [];
        /// <summary>
        /// The list of components.
        /// </summary>
        internal List<IComponent>? ComponentList { get; set; }
        /// <summary>
        /// The scrollbar of the mini window.
        /// </summary>
        internal Scrollbar Scrollbar { get; set; }
        /// <summary>
        /// The close button of the mini window.
        /// </summary>
        internal Button? CloseButton { get; set; }
        /// <summary>
        /// Creates a new mini window.
        /// </summary>
        /// <param name="editor"></param>
        /// <param name="closeButton"></param>
        /// <param name="hasVariableComponent"></param>
        /// <param name="hasScene"></param>
        /// <param name="xPosition"></param>
        /// <param name="yPosition"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="borderWidth"></param>
        /// <param name="color"></param>
        /// <param name="borderColor"></param>
        /// <param name="miniWindowType"></param>
        /// <param name="buttons"></param>
        public MiniWindow(Editor editor, bool closeButton, bool hasVariableComponent, bool hasScene, int xPosition, int yPosition, int width, int height, int borderWidth, Color color, Color borderColor, MiniWindowType miniWindowType, Button[] buttons)
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
                case MiniWindowType.Vertical:
                    Scrollbar = new Scrollbar(Editor, XPosition + Width, closeButton ? YPosition + Editor.SmallButtonHeight : YPosition, closeButton ? Width - Editor.SmallButtonHeight : Width, Editor.SmallButtonWidth, Scrollbar.ScrollbarType.Vertical, false, [.. ButtonComponentList]);
                    break;
                case MiniWindowType.Horizontal:
                    Scrollbar = new Scrollbar(Editor, XPosition, YPosition + Height - Editor.SmallButtonHeight, Editor.SmallButtonHeight, Width, Scrollbar.ScrollbarType.Horizontal, false, [.. ButtonComponentList]);
                    break;
            }
        }
        /// <summary>
        /// Creates a new mini window.
        /// </summary>
        /// <param name="editor"></param>
        /// <param name="closeButton"></param>
        /// <param name="hasVariable"></param>
        /// <param name="hasScene"></param>
        /// <param name="xPosition"></param>
        /// <param name="yPosition"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="borderWidth"></param>
        /// <param name="color"></param>
        /// <param name="borderColor"></param>
        /// <param name="miniWindowType"></param>
        /// <param name="components"></param>
        public MiniWindow(Editor editor, bool closeButton, bool hasVariable, bool hasScene, int xPosition, int yPosition, int width, int height, int borderWidth, Color color, Color borderColor, MiniWindowType miniWindowType, IComponent[] components)
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
                case MiniWindowType.Vertical:
                    Scrollbar = new Scrollbar(Editor, XPosition + Width - Editor.SmallButtonWidth, closeButton ? YPosition + Editor.SmallButtonHeight : YPosition, Height - Editor.SmallButtonHeight, Editor.SmallButtonWidth, Scrollbar.ScrollbarType.Vertical, false, [.. ComponentList]);
                    break;
                case MiniWindowType.Horizontal:
                    Scrollbar = new Scrollbar(Editor, XPosition, YPosition + Height - Editor.SmallButtonHeight, Editor.SmallButtonHeight, Width, Scrollbar.ScrollbarType.Horizontal, false, [.. components]);
                    break;
            }
        }
        /// <summary>
        /// Fetch the active scene attributes.
        /// </summary>
        internal void FetchActiveSceneAttributes()
        {
            //Fetch the active scene name
            TextField sceneNameTextField = ComponentList[2] as TextField;
            sceneNameTextField.Text = Editor.ActiveScene.Name;
            //Fetch and set activeComponents
            Dropdown sceneBackgroundDropDown = ComponentList[3] as Dropdown;
            sceneBackgroundDropDown.Button.Text = Editor.ActiveScene.BackgroundOption.ToString();
            switch (Editor.ActiveScene.BackgroundOption)
            {
                case VisualNovelEngine.Engine.Game.Component.Scene.BackgroundOption.SolidColor:
                    Label sceneBackgroundColorLabel = new(XPosition, YPosition, "Background Color");
                    TextField sceneBackgroundColor = new(Editor, 0, 0, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, $"{Editor.ActiveScene.BackgroundColor.Value.R}, {Editor.ActiveScene.BackgroundColor.Value.G}, {Editor.ActiveScene.BackgroundColor.Value.B}", Raylib.GetFontDefault(), false);
                    ComponentList.Insert(ComponentList.Count - 1, sceneBackgroundColorLabel);
                    ComponentList.Insert(ComponentList.Count - 1, sceneBackgroundColor);
                    Scrollbar.Components.Insert(Scrollbar.Components.Count - 1, sceneBackgroundColorLabel);
                    Scrollbar.Components.Insert(Scrollbar.Components.Count - 1, sceneBackgroundColor);
                    break;
                case VisualNovelEngine.Engine.Game.Component.Scene.BackgroundOption.Image:
                    Label sceneBackgroundImageLabel = new(XPosition, YPosition, "Background Image");
                    TextField sceneBackgroundImage = new(Editor, 0, 0, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, Editor.ActiveScene.BackgroundImage.ToString(), Raylib.GetFontDefault(), false);
                    ComponentList.Insert(ComponentList.Count - 1, sceneBackgroundImageLabel);
                    ComponentList.Insert(ComponentList.Count - 1, sceneBackgroundImage);
                    Scrollbar.Components.Insert(Scrollbar.Components.Count - 1, sceneBackgroundImageLabel);
                    Scrollbar.Components.Insert(Scrollbar.Components.Count - 1, sceneBackgroundImage);
                    break;
                case VisualNovelEngine.Engine.Game.Component.Scene.BackgroundOption.GradientHorizontal:
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
                case VisualNovelEngine.Engine.Game.Component.Scene.BackgroundOption.GradientVertical:
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
        /// <summary>
        /// Fetch the active scene attributes.
        /// </summary>
        internal void FetchVariables()
        {
            for (int i = 0; i < Editor.GameVariables.Count; i++)
            {
                TextField VariableNameField = new(Editor, 0, 0, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, Editor.GameVariables[i].Name, Raylib.GetFontDefault(), false);
                TextField VariableValueField = new(Editor, 0, 0, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, Editor.GameVariables[i].Value, Raylib.GetFontDefault(), false);
                Dropdown VariableDropDown = new(Editor, 0, 0, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, Dropdown.FilterType.VariableType);
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
        /// <summary>
        /// Fetch the active scene attributes.
        /// </summary>
        internal void UpdateComponentPosition()
        {
            switch (ComponentList is null)
            {
                case true:
                    if (ButtonComponentList.Count < 1) return;
                    switch (Type)
                    {
                        case MiniWindowType.Vertical:
                            for (int i = 0; i < ButtonComponentList.Count; i++)
                            {
                                ButtonComponentList[i].XPosition = XPosition;
                                ButtonComponentList[i].YPosition = YPosition + (i * Editor.SmallButtonHeight);
                            }
                            break;
                        case MiniWindowType.Horizontal:
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
                        case MiniWindowType.Vertical:
                            for (int i = 0; i < ComponentList.Count; i++)
                            {
                                ComponentList[i].XPosition = XPosition;
                                ComponentList[i].YPosition = YPosition + ((i + 1) * Editor.ComponentHeight);
                            }
                            break;
                        case MiniWindowType.Horizontal:
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
        /// <summary>
        /// Render the mini window.
        /// </summary>
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
                    case MiniWindowType.Vertical:
                        if ((YPosition + ComponentList.Count * Editor.ComponentHeight) > Raylib.GetScreenHeight() / 2 + Height / 2) Scrollbar.Render();
                        for (int i = 0; i < ComponentList.Count; i++) if (ComponentList[i].YPosition < Raylib.GetScreenHeight() / 2 + Height / 2 && ComponentList[i].YPosition > YPosition) ComponentList[i].Render();
                        break;
                    case MiniWindowType.Horizontal:
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
                case MiniWindowType.Vertical:
                    if (ButtonComponentList.Count * Editor.SmallButtonHeight > Height)
                        Scrollbar.Render();
                    break;
                case MiniWindowType.Horizontal:
                    if (ButtonComponentList.Count * Editor.ButtonWidth >= Width)
                        Scrollbar.Render();
                    break;
            }
        }
    }
}