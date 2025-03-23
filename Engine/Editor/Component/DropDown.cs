using Raylib_cs;
using VisualNovelEngine.Engine.Game.Component;
using VisualNovelEngine.Engine.Editor.Component.Command;
using VisualNovelEngine.Engine.Editor.Interface;

namespace VisualNovelEngine.Engine.Editor.Component
{
    public class DropDown : IComponent
    {
        /// <summary>
        /// Dropdown object filter
        /// </summary>
        public enum FilterType
        {
            None,
            Sprite,
            Menu,
            Button,
            TextBox,
            Block,
            DropBox,
            Slider,
            InputField,
            StaticInputField,
            Toggle,
            NoBlock,
            TextBoxPosition,
            SceneBackground,
            VariableType,
            SliderToggleInputField,
            TimelineIndependentAction,
            TimelineDependentAction
        }
        /// <summary>
        /// Dropdown object filter
        /// </summary>
        internal FilterType Filter { get; set; }
        /// <summary>
        /// Represents the editor.
        /// </summary>
        private Editor Editor { get; set; }
        /// <summary>
        /// The x position of the dropdown.
        /// </summary>
        public int XPosition { get; set; }
        /// <summary>
        /// The y position of the dropdown.
        /// </summary>
        public int YPosition { get; set; }
        /// <summary>
        /// The width of the dropdown.
        /// </summary>
        internal int Width { get; set; }
        /// <summary>
        /// The height of the dropdown.
        /// </summary>
        internal int Height { get; set; }
        /// <summary>
        /// The border width of the dropdown.
        /// </summary>
        internal int BorderWidth { get; set; }
        /// <summary>
        /// The text of the dropdown.
        /// </summary>
        internal string? Text { get; set; }
        /// <summary>
        /// Represents if the dropdown is active.
        /// </summary>
        internal bool IsHover { get; set; }
        /// <summary>
        /// Represents if the dropdown is selected.
        /// </summary>
        internal bool IsSelected { get; set; } = false;
        /// <summary>
        /// Represents the selected button of the dropdown.
        /// </summary>
        internal Button Button { get; set; }
        /// <summary>
        /// Represents the list of buttons in the dropdown.
        /// </summary>
        internal List<Button> ButtonList { get; set; } = [];
        /// <summary>
        /// Represents the list of buttons with the filter applied in the dropdown.
        /// </summary>
        internal List<Button> FilteredButtonList { get; set; } = [];
        /// <summary>
        /// Creates a new dropdown object.
        /// </summary>
        /// <param name="editor"></param>
        /// <param name="xPosition"></param>
        /// <param name="yPosition"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="borderWidth"></param>
        /// <param name="filter"></param>
        public DropDown(Editor editor, int xPosition, int yPosition, int width, int height, int borderWidth, FilterType filter)
        {
            Editor = editor;
            XPosition = xPosition;
            YPosition = yPosition;
            Width = width;
            Height = height;
            BorderWidth = borderWidth;
            Button = new Button(Editor, XPosition, YPosition, "Select", true, Editor.ButtonWidth, Editor.ButtonHeight, Editor.ButtonBorderWidth, Editor.BaseColor, Editor.BorderColor, Editor.HoverColor, new OpenDropDownCommand(Editor, this), Button.ButtonType.Hold);
            Filter = filter;
            UpdateComponentList();
        }
        /// <summary>
        /// Updates the list of components in the dropdown.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        internal void UpdateComponentList()
        {
            //drops the existing list
            ButtonList.Clear();
            FilteredButtonList.Clear();
            //update whole list
            if (Editor.ActiveScene == null) return;
            ButtonList.AddRange([.. Editor.ActiveScene.ComponentList.Cast<Component>().Select(component => new Button(Editor, this, $"ID:{component.ID}, Name:{component.Name}", Editor.BaseColor, Editor.BorderColor, Editor.HoverColor, component))]);
            switch (Filter)
            {
                case FilterType.None:
                    FilteredButtonList = ButtonList;
                    break;
                case FilterType.Sprite:
                case FilterType.Menu:
                case FilterType.Button:
                case FilterType.TextBox:
                case FilterType.Block:
                case FilterType.DropBox:
                case FilterType.Slider:
                case FilterType.InputField:
                case FilterType.Toggle:
                    foreach (Button item in ButtonList)
                    {
                        var component = item.Component as Component;
                        FilterType filter = component.RenderingObject switch
                        {
                            Sprite => FilterType.Sprite,
                            Menu => FilterType.Menu,
                            VisualNovelEngine.Engine.Game.Component.Button => FilterType.Button,
                            TextBox => FilterType.TextBox,
                            Block => FilterType.Block,
                            DropBox => FilterType.DropBox,
                            Slider => FilterType.Slider,
                            InputField => FilterType.InputField,
                            Toggle => FilterType.Toggle,
                            _ => throw new NotImplementedException()
                        };
                        if (filter == Filter) FilteredButtonList.Add(item);
                    }
                    break;
                case FilterType.SliderToggleInputField:
                    foreach (Button item in ButtonList)
                    {
                        var component = item.Component as Component;
                        FilterType filter = component.RenderingObject switch
                        {
                            Sprite => FilterType.Sprite,
                            Menu => FilterType.Menu,
                            VisualNovelEngine.Engine.Game.Component.Button => FilterType.Button,
                            TextBox => FilterType.TextBox,
                            Block => FilterType.Block,
                            DropBox => FilterType.DropBox,
                            Slider => FilterType.Slider,
                            InputField => FilterType.InputField,
                            Toggle => FilterType.Toggle,
                            _ => throw new NotImplementedException()
                        };
                        if (filter == FilterType.Slider || filter == FilterType.Toggle || filter == FilterType.InputField)
                            FilteredButtonList.Add(item);
                    }
                    break;
                case FilterType.NoBlock:
                    foreach (Button item in ButtonList)
                    {
                        var component = item.Component as Component;
                        if (component.RenderingObject is Block is false) FilteredButtonList.Add(item);
                    }
                    break;
                case FilterType.TextBoxPosition:
                    Button textBoxDefaultPositionType = new(Editor, this, TextBox.PositionType.defaultPosition.ToString(), Editor.BaseColor, Editor.BorderColor, Editor.HoverColor, null)
                    {
                        PositionType = TextBox.PositionType.defaultPosition
                    };
                    Button textBoxupperPositionPositionType = new(Editor, this, TextBox.PositionType.upperPosition.ToString(), Editor.BaseColor, Editor.BorderColor, Editor.HoverColor, null)
                    {
                        PositionType = TextBox.PositionType.upperPosition
                    };
                    FilteredButtonList = [textBoxDefaultPositionType, textBoxupperPositionPositionType];
                    break;
                case FilterType.SceneBackground:
                    Button sceneBackgroundDefaultBackgroundType = new(Editor, this, VisualNovelEngine.Engine.Game.Component.Scene.BackgroundOption.SolidColor.ToString(), Editor.BaseColor, Editor.BorderColor, Editor.HoverColor, null)
                    {
                        SceneBackgroundOption = VisualNovelEngine.Engine.Game.Component.Scene.BackgroundOption.SolidColor
                    };
                    Button sceneBackgroundGradientVerticalBackgroundType = new(Editor, this, VisualNovelEngine.Engine.Game.Component.Scene.BackgroundOption.GradientVertical.ToString(), Editor.BaseColor, Editor.BorderColor, Editor.HoverColor, null)
                    {
                        SceneBackgroundOption = VisualNovelEngine.Engine.Game.Component.Scene.BackgroundOption.GradientVertical
                    };
                    Button sceneBackgroundGradientHorizontalBackgroundType = new(Editor, this, VisualNovelEngine.Engine.Game.Component.Scene.BackgroundOption.GradientHorizontal.ToString(), Editor.BaseColor, Editor.BorderColor, Editor.HoverColor, null)
                    {
                        SceneBackgroundOption = VisualNovelEngine.Engine.Game.Component.Scene.BackgroundOption.GradientHorizontal
                    };
                    Button sceneBackgroundImageBackgroundType = new(Editor, this, VisualNovelEngine.Engine.Game.Component.Scene.BackgroundOption.Image.ToString(), Editor.BaseColor, Editor.BorderColor, Editor.HoverColor, null)
                    {
                        SceneBackgroundOption = VisualNovelEngine.Engine.Game.Component.Scene.BackgroundOption.Image
                    };
                    FilteredButtonList = [sceneBackgroundDefaultBackgroundType, sceneBackgroundGradientVerticalBackgroundType, sceneBackgroundGradientHorizontalBackgroundType, sceneBackgroundImageBackgroundType];
                    break;
                case FilterType.VariableType:
                    Button variableTypeStringType = new(Editor, this, VariableType.String.ToString(), Editor.BaseColor, Editor.BorderColor, Editor.HoverColor, null)
                    {
                        VariableType = VariableType.String
                    };
                    Button variableTypeIntType = new(Editor, this, VariableType.Int.ToString(), Editor.BaseColor, Editor.BorderColor, Editor.HoverColor, null)
                    {
                        VariableType = VariableType.Int
                    };
                    Button variableTypeFloatType = new(Editor, this, VariableType.Float.ToString(), Editor.BaseColor, Editor.BorderColor, Editor.HoverColor, null)
                    {
                        VariableType = VariableType.Float
                    };
                    Button variableTypeBoolType = new(Editor, this, VariableType.Boolean.ToString(), Editor.BaseColor, Editor.BorderColor, Editor.HoverColor, null)
                    {
                        VariableType = VariableType.Boolean
                    };
                    FilteredButtonList = [variableTypeStringType, variableTypeIntType, variableTypeFloatType, variableTypeBoolType];
                    break;
                case FilterType.TimelineIndependentAction:
                    //Options for timeline independent events
                    Button SetVariableValueActionButton = new(Editor, this, "Set variable action", Editor.BaseColor, Editor.BorderColor, Editor.HoverColor, null)
                    {
                        Action = new VisualNovelEngine.Engine.Game.Component.Action.TimelineIndependent.SetVariableValueAction(Editor.Game, "", null, 1)
                    };
                    Button SwitchStaticMenuActionButton = new(Editor, this, "Switch menu action", Editor.BaseColor, Editor.BorderColor, Editor.HoverColor, null)
                    {
                        Action = new VisualNovelEngine.Engine.Game.Component.Action.TimelineIndependent.SwitchStaticMenuAction(Editor.Game, null, 0, 0)
                    };
                    Button createMenuActionButton = new(Editor, this, "Create menu action", Editor.BaseColor, Editor.BorderColor, Editor.HoverColor, null)
                    {
                        Action = new VisualNovelEngine.Engine.Game.Component.Action.CreateMenuAction(Editor.Game, null, null)
                    };
                    Button loadSceneActionButton = new(Editor, this, "Load scene action", Editor.BaseColor, Editor.BorderColor, Editor.HoverColor, null)
                    {
                        Action = new VisualNovelEngine.Engine.Game.Component.Action.LoadSceneAction(Editor.Game, 0, "variable name")
                    };
                    Button nativeLoadSceneActionButton = new(Editor, this, "Native load scene action", Editor.BaseColor, Editor.BorderColor, Editor.HoverColor, null)
                    {
                        Action = new VisualNovelEngine.Engine.Game.Component.Action.NativeLoadSceneAction(Editor.Game, 0)
                    };
                    FilteredButtonList = [SetVariableValueActionButton, SwitchStaticMenuActionButton, createMenuActionButton, loadSceneActionButton, nativeLoadSceneActionButton];
                    break;
                case FilterType.TimelineDependentAction:
                    //Options for timeline dependent events
                    Button addSpriteActionButton = new(Editor, this, "Add sprite action", Editor.BaseColor, Editor.BorderColor, Editor.HoverColor, null)
                    {
                        Action = new VisualNovelEngine.Engine.Game.Component.Action.TimelineDependent.AddSpriteAction(new Sprite("Empty path"), Editor.Game)
                    };
                    Button changeSpriteAction = new(Editor, this, "Change sprite action", Editor.BaseColor, Editor.BorderColor, Editor.HoverColor, null)
                    {
                        Action = new VisualNovelEngine.Engine.Game.Component.Action.TimelineDependent.ChangeSpriteAction(null, "Change sprite path", Editor.Game)
                    };
                    Button decrementVariableActionButton = new(Editor, this, "Decrement variable action", Editor.BaseColor, Editor.BorderColor, Editor.HoverColor, null)
                    {
                        Action = new VisualNovelEngine.Engine.Game.Component.Action.TimelineDependent.DecrementVariableAction(Editor.Game, "variable name 1", "decremen variable name")
                    };
                    Button EmptyActionButton = new(Editor, this, "Empty action", Editor.BaseColor, Editor.BorderColor, Editor.HoverColor, null)
                    {
                        Action = new VisualNovelEngine.Engine.Game.Component.Action.TimelineDependent.EmptyAction(Editor.Game)
                    };
                    Button incrementVariableAction = new(Editor, this, "Increment variable action", Editor.BaseColor, Editor.BorderColor, Editor.HoverColor, null)
                    {
                        Action = new VisualNovelEngine.Engine.Game.Component.Action.TimelineDependent.IncrementVariableAction(Editor.Game, "variable name 1", "increment variable name")
                    };
                    Button removeSpriteAction = new(Editor, this, "Remove sprite action", Editor.BaseColor, Editor.BorderColor, Editor.HoverColor, null)
                    {
                        Action = new VisualNovelEngine.Engine.Game.Component.Action.TimelineDependent.RemoveSpriteAction(null, Editor.Game)
                    };
                    Button setBoolVariableAction = new(Editor, this, "Set bool variable action", Editor.BaseColor, Editor.BorderColor, Editor.HoverColor, null)
                    {
                        Action = new VisualNovelEngine.Engine.Game.Component.Action.TimelineDependent.SetBoolVariableAction(Editor.Game, "variable name", true)
                    };
                    Button setVariableFalseAction = new(Editor, this, "Set variable false action", Editor.BaseColor, Editor.BorderColor, Editor.HoverColor, null)
                    {
                        Action = new VisualNovelEngine.Engine.Game.Component.Action.TimelineDependent.SetVariableFalseAction(Editor.Game, "variable name")
                    };
                    Button setVariableTrueAction = new(Editor, this, "Set variable true action", Editor.BaseColor, Editor.BorderColor, Editor.HoverColor, null)
                    {
                        Action = new VisualNovelEngine.Engine.Game.Component.Action.TimelineDependent.SetVariableTrueAction(Editor.Game, "variable name")
                    };
                    Button createTextBoxButton = new(Editor, this, "Create textbox action", Editor.BaseColor, Editor.BorderColor, Editor.HoverColor, null)
                    {
                        Action = new VisualNovelEngine.Engine.Game.Component.Action.TimelineDependent.TextBoxCreateAction(null)
                    };
                    Button tintSpriteActionButton = new(Editor, this, "Tint sprite action", Editor.BaseColor, Editor.BorderColor, Editor.HoverColor, null)
                    {
                        Action = new VisualNovelEngine.Engine.Game.Component.Action.TimelineDependent.TintSpriteAction(null, Color.Black, Editor.Game)
                    };
                    Button toggleVariableActionButton = new(Editor, this, "Toggle variable action", Editor.BaseColor, Editor.BorderColor, Editor.HoverColor, null)
                    {
                        Action = new VisualNovelEngine.Engine.Game.Component.Action.TimelineDependent.ToggleVariableAction(Editor.Game, "variable name")
                    };
                    FilteredButtonList = [addSpriteActionButton, changeSpriteAction, decrementVariableActionButton, EmptyActionButton, incrementVariableAction, removeSpriteAction, setBoolVariableAction, setVariableFalseAction, setVariableTrueAction, createTextBoxButton, tintSpriteActionButton, toggleVariableActionButton];
                    break;
            }
            UpdateComponentPosition();
        }
        /// <summary>
        /// Updates the position of the components in the dropdown.
        /// </summary>
        internal void UpdateComponentPosition()
        {
            for (int i = 0; i < FilteredButtonList.Count; i++)
            {
                FilteredButtonList[i].Width = Editor.SideButtonWidth;
                FilteredButtonList[i].Height = Editor.SideButtonHeight;
                FilteredButtonList[i].BorderWidth = Editor.SideButtonBorderWidth;
                FilteredButtonList[i].XPosition = XPosition;
                FilteredButtonList[i].YPosition = YPosition + (i * Editor.SideButtonHeight);
            }
        }
        /// <summary>
        /// Updates the dropdown.
        /// </summary>
        public void Update()
        {
            Button.XPosition = XPosition;
            Button.YPosition = YPosition;
            IsSelected = Button.Selected;
        }
        /// <summary>
        /// Renders the dropdown.
        /// </summary>
        public void Render()
        {
            Update();
            Button.Render();
        }
    }
}