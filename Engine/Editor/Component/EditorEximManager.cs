using System.Text.Json;
using Raylib_cs;
using ICommand = VisualNovelEngine.Engine.Editor.Interface.ICommand;
using VisualNovelEngine.Engine.Editor.Component.Command;
using VisualNovelEngine.Engine.Game.Component;
using static VisualNovelEngine.Engine.Editor.Component.Command.CreateComponentCommand;
using VisualNovelEngine.Engine.PortData;
using VisualNovelEngine.Engine.Game.Interface;
using System.Text.RegularExpressions;
using VisualNovelEngine.Engine.Game.Component.Action.TimelineDependent;
using VisualNovelEngine.Engine.Game.Component.Action;
using System.Collections.Concurrent;
using VisualNovelEngine.Engine.Game.Component.Action.TimelineIndependent;
using VisualNovelEngine.Engine.Editor.Interface;
using Engine.EngineEditor.Component.Command;
using EngineEditor.Component.Command;
using System.Collections.Generic;

namespace VisualNovelEngine.Engine.Editor.Component
{
    /// <summary>
    /// Imports editor data and preferences from the associated JSON files.
    //  Exports Game related data from the Editor to the associated JSON file.
    /// </summary>
    public class EditorEXIMManager
    {
        Editor Editor { get; set; }
        /// <summary>
        /// Represents the editor's imported preferences.
        /// </summary>
        internal EditorPreferencesImport EditorPreferencesImport { get; set; }
        /// <summary>
        /// Represents the editor's imported data.
        /// </summary>
        internal EditorExIm EditorExIm { get; set; }
        /// <summary>
        /// Represents the game's imported data.
        /// </summary>
        internal GameExim GameExim { get; set; }
        /// <summary>
        /// Represents the game's importer.
        /// </summary>
        internal GameEximManager GameEximManager { get; set; }
        /// <summary>
        /// Cache the block for static components.
        /// Because of the way the editor works, when loading static data, the block always comes first.
        /// </summary>
        internal Block BlockCache { get; set; }
        /// <summary>
        /// Represents the editor's imported data.
        /// </summary>
        /// <param name="editor"></param>
        /// <param name="editorConfigPath"></param>
        /// <param name="editorDataPath"></param>
        /// <exception cref="Exception"></exception>
        public EditorEXIMManager(Editor editor, string editorConfigPath, string editorDataPath)
        {
            Editor = editor;
            //Editor config
            string rawFile = File.ReadAllText(editorConfigPath) ?? throw new Exception("Editor preference file not found!");
            EditorPreferencesImport = JsonSerializer.Deserialize<EditorPreferencesImport>(rawFile) ?? throw new Exception("Editor preference file not found!");
            //Editor data
            rawFile = File.ReadAllText(editorDataPath) ?? throw new Exception("Editor data file not found!");
            EditorExIm = JsonSerializer.Deserialize<EditorExIm>(rawFile) ?? throw new Exception("Editor data file not found!");
            //Game importer
            GameEximManager = Editor.Game.GameImport;
            //Game data
            GameExim = GameEximManager.GameExim;
        }
        /// <summary>
        /// Fetches a color from an import object.
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static Color FetchColorFromImport(int[] color) => new()
        {
            R = (byte)color[0],
            G = (byte)color[1],
            B = (byte)color[2],
            A = (byte)color[3]
        };
        /// <summary>
        /// Fetches a gradient color from an import object.
        /// </summary>
        /// <param name="Color"></param>
        /// <returns></returns>
        public static Color[] FetchGradientColorFromImport(int[] Color)
        {
            Color color1 = FetchColorFromImport([.. Color.Take(4)]);
            Color color2 = FetchColorFromImport([.. Color.Skip(4)]);
            return [color1, color2];
        }
        /// <summary>
        /// Fetch a scene from an import object.
        /// </summary>
        /// <param name="sceneImport"></param>
        /// <returns></returns>
        public Scene FetchEditorSceneFromImport(SceneExIm sceneImport)
        {
            VisualNovelEngine.Engine.Game.Component.Scene.BackgroundOption backgroundOption = sceneImport.Background switch
            {
                "SolidColor" => Game.Component.Scene.BackgroundOption.SolidColor,
                "GradientVertical" => Game.Component.Scene.BackgroundOption.GradientVertical,
                "GradientHorizontal" => Game.Component.Scene.BackgroundOption.GradientHorizontal,
                "Image" => Game.Component.Scene.BackgroundOption.Image,
                _ => throw new Exception("Background type not found!")
            };
            Texture2D? image;
            Color[]? gradientColor;
            Color? solidColor;
            if (backgroundOption == Game.Component.Scene.BackgroundOption.Image)
            {
                image = Raylib.LoadTexture(sceneImport.ImageTexture);
                return new Scene(
                    Editor,
                    FetchTimelineFromImport(sceneImport.Timeline),
                    sceneImport.Name,
                    [.. sceneImport.Components.Select(FetchEditorComponentFromImport)],
                    [.. sceneImport.GroupList.Select(FetchGroupFromImport)]
                    )
                {
                    BackgroundOption = backgroundOption,
                    BackgroundImage = image
                };
            }
            else if (backgroundOption == Game.Component.Scene.BackgroundOption.GradientHorizontal || backgroundOption == Game.Component.Scene.BackgroundOption.GradientVertical)
            {
                gradientColor = FetchGradientColorFromImport(sceneImport.GradientColor);
                return new Scene(Editor,
                    FetchTimelineFromImport(sceneImport.Timeline),
                sceneImport.Name,
                [.. sceneImport.Components.Select(FetchEditorComponentFromImport)],
                [.. sceneImport.GroupList.Select(FetchGroupFromImport)])
                {
                    BackgroundOption = backgroundOption,
                    BackgroundGradientColor = gradientColor
                };
            }
            else
            {
                solidColor = FetchColorFromImport(sceneImport.SolidColor);
                return new Scene(
                    Editor,
                    FetchTimelineFromImport(sceneImport.Timeline),
                sceneImport.Name,
                [.. sceneImport.Components.Select(FetchEditorComponentFromImport)],
                [.. sceneImport.GroupList.Select(FetchGroupFromImport)])
                {
                    BackgroundOption = backgroundOption,
                    BackgroundColor = solidColor
                };
            }

        }
        /// <summary>
        /// Fetch a timeline from an import object.
        /// </summary>
        /// <param name="timelineImport"></param>
        /// <returns></returns>
        public Timeline FetchTimelineFromImport(TimelineExIm timelineImport)
        {
            List<IAction> actions = [];
            List<ISettingsAction> timelineIndependentActions = [];
            //
            for (int i = 0; i < timelineImport.Actions.Length; i++)
            {
                actions.Add(FetchActionFromImport(timelineImport.Actions[i]));
            }
            for (int i = 0; i < timelineImport.TimelineIndependentActions.Length; i++)
            {
                timelineIndependentActions.Add(FetchTimelineIndependentActionFromImport(timelineImport.TimelineIndependentActions[i]));
            }
            return new Timeline(
                Editor,
                timelineImport.XPosition,
                timelineImport.YPosition,
                [.. actions],
                [.. timelineIndependentActions]
                );
        }
        /// <summary>
        /// Fetch a variable from an import object.
        /// </summary>
        /// <param name="variableImport"></param>
        /// <returns></returns>
        public Variable FetchVariableFromImport(VariableExim variableImport)
        {
            return new Variable(variableImport.Name, variableImport.Value, (VariableType)variableImport.Type);
        }
        /// <summary>
        /// Fetch a block from an import object.
        /// </summary>
        /// <param name="blockImport"></param>
        /// <returns></returns>
        public Block FetchBlockFromImport(BlockExim blockImport)
        {
            return new Block(blockImport.XPosition, blockImport.YPosition, null, blockImport.ID);
        }
        /// <summary>
        /// Fetch a Timeline event from an import object.
        /// </summary>
        /// <param name="ActionImport"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public IAction FetchActionFromImport(ActionExim ActionImport)
        {
            return GameEximManager.FetchTimelineDependentActionFromImport(ActionImport);
        }
        /// <summary>
        /// Fetch a Timeline independent event from an import object.
        /// </summary>
        /// <param name="ActionImport"></param>
        /// <returns></returns>
        public ISettingsAction FetchTimelineIndependentActionFromImport(ActionExim ActionImport)
        {
            return GameEximManager.FetchTimelineIndependentActionFromImport(ActionImport);
        }
        /// <summary>
        /// Fetch the Editor related toolbar from an import object.
        /// </summary>
        /// <param name="toolBarImport"></param>
        /// <returns></returns>
        public Group FetchToolBarFromImport(GroupExIm toolBarImport)
        {
            return FetchGroupFromImport(toolBarImport);
        }
        /// <summary>
        /// Fetch an editor related group from an import object.
        /// </summary>
        /// <param name="groupImport"></param>
        /// <returns></returns>
        public Group FetchGroupFromImport(GroupExIm groupImport)
        {
            switch (groupImport.Static.ToLower() == "true")
            {
                case true:
                    return new Group(
                    Editor,
                    groupImport.XPosition,
                    groupImport.YPosition,
                    groupImport.Width,
                    groupImport.Height,
                    groupImport.BorderWidth,
                    FetchColorFromImport(EditorPreferencesImport.BaseColor),
                    FetchColorFromImport(EditorPreferencesImport.BorderColor),
                    FetchColorFromImport(EditorPreferencesImport.HoverColor),
                    GroupType.SolidColor,
                    groupImport.MaximumHorizontalComponentCount,
                    [.. groupImport.Buttons.Select(FetchEditorButtonFromImport)]
                )
                    {
                        IsStatic = true
                    };
                case false:
                    return new Group(
                    Editor,
                    groupImport.XPosition,
                    groupImport.YPosition,
                    groupImport.Width,
                    groupImport.Height,
                    groupImport.BorderWidth,
                    FetchColorFromImport(EditorPreferencesImport.BaseColor),
                    FetchColorFromImport(EditorPreferencesImport.BorderColor),
                    FetchColorFromImport(EditorPreferencesImport.HoverColor),
                    [.. groupImport.Components.Select(FetchEditorComponentFromImport)]
                )
                    {
                        IsStatic = false
                    };
            }
        }
        /// <summary>
        /// Fetch an editor related component from an import object.
        /// </summary>
        /// <param name="componentImport"></param>
        /// <returns></returns>
        public Component FetchEditorComponentFromImport(ComponentExIm componentImport)
        {
            return new Component(
                componentImport.ID,
                Editor,
                null,
                componentImport.Name,
                componentImport.XPosition,
                componentImport.YPosition,
                Editor.ComponentWidth,
                Editor.ComponentHeight,
                Editor.ComponentBorderWidth,
                Editor.BaseColor,
                Editor.BorderColor,
                Editor.HoverColor,
                Editor.HoverColor,
                FetchRenderingObjectFromImport(componentImport.RenderingObject)
            );
        }
        /// <summary>
        /// Fetch a label from an import object.
        /// </summary>
        /// <param name="labelImport"></param>
        /// <returns></returns>
        public Label FetchLabelFromImport(LabelExIm labelImport)
        {
            return new(labelImport.XPosition, labelImport.YPosition, labelImport.Text);
        }
        /// <summary>
        /// Fetch an editor realted textfield from an import object.
        /// </summary>
        /// <param name="textFieldImport"></param>
        /// <returns></returns>
        public TextField FetchTextFieldFromImport(EditorTextFieldExim textFieldImport)
        {
            if (textFieldImport.ActiveVariableType != null)
            {
                var txtField = new TextField(
                    Editor,
                    textFieldImport.XPosition,
                    textFieldImport.YPosition,
                    Editor.ComponentWidth,
                    Editor.ComponentHeight,
                    Editor.ComponentBorderWidth,
                    textFieldImport.Text,
                    Raylib.GetFontDefault(),
                    textFieldImport.Static == "true"
                )
                {
                    IsStatic = textFieldImport.Static == "true",
                };
                switch (textFieldImport.ActiveVariableType)
                {
                    case "GameName":
                        txtField.Text = Editor.ProjectName;
                        break;
                    case "Resolution":
                        txtField.Text = $"{Raylib.GetScreenWidth()}x{Raylib.GetScreenHeight()}";
                        break;
                }
                return txtField;
            }
            return new TextField(
                Editor,
                textFieldImport.XPosition,
                textFieldImport.YPosition,
                Editor.ComponentWidth,
                Editor.ComponentHeight,
                Editor.ComponentBorderWidth,
                textFieldImport.Text,
                Raylib.GetFontDefault(),
                textFieldImport.Static == "true"
            )
            {
                IsStatic = textFieldImport.Static == "true"
            };
        }
        /// <summary>
        /// Fetch an editor related toggle from an import object.
        /// </summary>
        /// <param name="toggleImport"></param>
        /// <returns></returns>
        public ToggleButton FetchToggleFromImport(EditorToggleExim toggleImport)
        {
            return new(
                Editor,
                toggleImport.XPosition,
                toggleImport.YPosition,
                Editor.SmallButtonWidth,
                Editor.ButtonBorderWidth,
                toggleImport.Text,
                toggleImport.Value == "true"
            );
        }
        /// <summary>
        /// Fetch an editor related dropdown from an import object.
        /// </summary>
        /// <param name="dropDownImport"></param>
        /// <returns></returns>
        public Dropdown FetchDropDownFromImport(DropDownExim dropDownImport)
        {
            return new(
                Editor,
                dropDownImport.XPosition,
                dropDownImport.YPosition,
                Editor.ComponentWidth,
                Editor.ComponentHeight,
                Editor.ComponentBorderWidth,
                (Dropdown.FilterType)dropDownImport.Filter
            )
            {
                ButtonList = [.. dropDownImport.Options.Select(FetchEditorButtonFromImport)]
            };
        }
        /// <summary>
        /// Fetch an editor related component from an import object.
        /// </summary>
        /// <param name="renderingComponentImport"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public IComponent FetchComponentFromImport(RenderingComponentExIm renderingComponentImport)
        {
            if (renderingComponentImport.Label != null)
            {
                return FetchLabelFromImport(renderingComponentImport.Label);
            }
            else if (renderingComponentImport.TextField != null)
            {
                return FetchTextFieldFromImport(renderingComponentImport.TextField);
            }
            else if (renderingComponentImport.Toggle != null)
            {
                return FetchToggleFromImport(renderingComponentImport.Toggle);
            }
            else if (renderingComponentImport.DropDown != null)
            {
                return FetchDropDownFromImport(renderingComponentImport.DropDown);
            }
            else if (renderingComponentImport.Button != null)
            {
                return FetchEditorButtonFromImport(renderingComponentImport.Button);
            }
            else
            {
                throw new Exception("Rendering component type not found!");
            }
        }
        /// <summary>
        /// Fetch an editor related rendering object from an import object.
        /// </summary>
        /// <param name="textBox"></param>
        /// <returns></returns>
        public TextBox FetchTextBoxFromImport(TextBoxExIm textBox)
        {
            return TextBox.CreateNewTextBox(
                Editor.Game,
                textBox.CharactersPerSecond.Value,
                Raylib.GetFontDefault(),
                FetchColorFromImport(textBox.Color),
                FetchColorFromImport(textBox.BorderColor),
                (TextBox.PositionType)textBox.PositionType,
                textBox.HorizontalTextMargin.Value,
                textBox.VerticalTextMargin.Value,
                textBox.WordWrap.Value,
                textBox.Title,
                [.. textBox.Content]
            );
        }
        /// <summary>
        /// Imports component's renderingobject data from an import object.
        /// Each object is attached to a component.
        /// </summary>
        /// <param name="renderingObjectImport"></param>
        /// <returns></returns>
        public IRenderingObject FetchRenderingObjectFromImport(RenderingObjectExIm renderingObjectImport)
        {
            if (renderingObjectImport.Sprite != null)
            {
                return new Sprite(renderingObjectImport.Sprite.Path);
            }
            else if (renderingObjectImport.Block != null)
            {
                BlockCache = GameEximManager.FetchBlockFromImport(renderingObjectImport.Block);
                return GameEximManager.FetchBlockFromImport(renderingObjectImport.Block);
            }
            else if (renderingObjectImport.StaticBlock != null)
            {
                BlockCache = GameEximManager.FetchStaticBlockFromImport(renderingObjectImport.StaticBlock);
                return GameEximManager.FetchStaticBlockFromImport(renderingObjectImport.StaticBlock);
            }
            else if (renderingObjectImport.Textbox != null)
            {
                return FetchTextBoxFromImport(renderingObjectImport.Textbox);
            }
            else if (renderingObjectImport.Button != null)
            {
                return GameEximManager.FetchButtonFromImport(renderingObjectImport.Button, BlockCache);
            }
            else if (renderingObjectImport.StaticButton != null)
            {
                //When working with separate components, the autonomous button does not have a reference to a block.
                //Either create a new method to fetch the connecting block, or feign a block.
                return GameEximManager.FetchStaticButtonFromImport(renderingObjectImport.StaticButton, BlockCache);
            }
            else if (renderingObjectImport.InputField != null)
            {
                return GameEximManager.FetchInputFieldFromImport(renderingObjectImport.InputField, BlockCache);
            }
            else if (renderingObjectImport.StaticInputField != null)
            {
                return GameEximManager.FetchInputFieldFromImport(renderingObjectImport.StaticInputField, BlockCache);
            }
            else if (renderingObjectImport.DropBox != null)
            {
                return GameEximManager.FetchDropBoxFromImport(renderingObjectImport.DropBox, BlockCache);
            }
            else if (renderingObjectImport.Menu != null)
            {
                return GameEximManager.FetchMenuFromImport(renderingObjectImport.Menu);
            }
            else if (renderingObjectImport.Slider != null)
            {
                return GameEximManager.FetchSliderFromImport(renderingObjectImport.Slider, BlockCache);
            }
            else if (renderingObjectImport.TextField != null)
            {
                return GameEximManager.FetchTextFieldFromImport(renderingObjectImport.TextField, BlockCache);
            }
            else if (renderingObjectImport.StaticInputField != null)
            {
                return GameEximManager.FetchInputFieldFromImport(renderingObjectImport.StaticInputField, BlockCache);
            }
            else if (renderingObjectImport.Toggle != null)
            {
                return GameEximManager.FetchToggleFromImport(renderingObjectImport.Toggle, BlockCache);
            }
            else
            {
                throw new Exception("Rendering object type not found!");
            }
        }
        /// <summary>
        /// Fetch an editor related button from an import object.
        /// </summary>
        /// <param name="buttonImport"></param>
        /// <returns></returns>
        public Button FetchEditorButtonFromImport(ButtonExIm buttonImport)
        {
            return new Button(Editor,
                buttonImport.XPosition,
                buttonImport.YPosition,
                buttonImport.Text,
                true,
                Editor.ComponentWidth,
                Editor.ComponentHeight,
                Editor.ComponentBorderWidth,
                FetchColorFromImport(EditorPreferencesImport.BaseColor),
                FetchColorFromImport(EditorPreferencesImport.BorderColor),
                FetchColorFromImport(EditorPreferencesImport.HoverColor),
                FetchCommandFromImport(buttonImport.Command),
                (Button.ButtonType)buttonImport.Type
            );
        }
        /// <summary>
        /// Fetch an editor related command from an import object.
        /// </summary>
        /// <param name="commandImport"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public ICommand FetchCommandFromImport(CommandExIm commandImport)
        {
            switch (commandImport.Type)
            {
                case "EmptyCommand":
                    return new EmptyCommand();
                case "CreateComponentCommand":
                    return new CreateComponentCommand(Editor, (RenderingObjectType)commandImport.RenderingObjectType);
                case "CreateVariableCommand":
                    if (commandImport.VariableValue == null) throw new Exception("Value not found!");
                    if (commandImport.VariableType == null) throw new Exception("Variable type not found!");
                    return new CreateVariableCommand(Editor, commandImport.VariableValue, (VariableType)commandImport.VariableType);
                case "DeleteSceneCommand":
                    return new DeleteSceneCommand(Editor);
                case "ShowSideWindowCommand":
                    if (commandImport.Buttons == null) throw new Exception("Buttons not found!");
                    if (commandImport.DependentButton == null) throw new Exception("Dependent button name not found!");
                    string parentButtonName = commandImport.DependentButton;
                    Button[] buttons = [.. commandImport.Buttons.Select(FetchEditorButtonFromImport)];
                    foreach (Button button in buttons)
                    {
                        button.Width = EditorPreferencesImport.SideButtonWidth;
                        button.Height = EditorPreferencesImport.SideButtonHeight;
                        button.BorderWidth = EditorPreferencesImport.SideButtonBorderWidth;
                    }
                    return new ShowSideWindowCommand(Editor, parentButtonName, buttons);
                case "ShowMiniWindowCommand":
                    if (commandImport.WindowComponents == null && commandImport.Buttons == null) throw new Exception("Window components and buttons are not found!");
                    if (commandImport.WindowComponents == null)
                    {
                        return new ShowMiniWindowComand(Editor, commandImport.HasVariable == "true", commandImport.HasSceneRelatedComponent == "true", [.. commandImport.Buttons.Select(FetchEditorButtonFromImport)], MiniWindowType.Vertical);
                    }
                    else
                    {
                        return new ShowMiniWindowComand(Editor, commandImport.HasVariable == "true", commandImport.HasSceneRelatedComponent == "true", [.. commandImport.WindowComponents.Select(FetchComponentFromImport)], MiniWindowType.Vertical);
                    }
                case "ShowInspectorCommand":
                    return new ShowInspectorCommand(Editor,
                        commandImport.EnabledRowComponentCount);
                case "SaveProjectDataCommand":
                    return new SaveProjectDataCommand(Editor);
                case "BuildProjectCommand":
                    return new BuildProjectCommand(Editor);
                case "ShowErrorCommand":
                    if (commandImport.ErrorMessage == null) throw new Exception("Error message not found!");
                    if (commandImport.WarningButtons == null) throw new Exception("Warning buttons not found!");
                    return new ShowErrorCommand(Editor, commandImport.ErrorMessage, [.. commandImport.WarningButtons.Select(FetchEditorButtonFromImport)]);
                case "DeleteAllComponentsCommand":
                    return new DeleteAllComponentsCommand(Editor);
                default:
                    throw new Exception("Command type not found!");
            }
        }
        /// <summary>
        /// Export color data suitable for saving or building.
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public int[] ExportColorData(Color color)
        {
            return [color.R, color.G, color.B, color.A];
        }
        /// <summary>
        /// Export gradient color data suitable for saving or building.
        /// </summary>
        /// <param name="gradientColor"></param>
        /// <returns></returns>
        public int[] ExportGradientColor(Color[] gradientColor)
        {
            return [.. gradientColor.SelectMany(ExportColorData)];
        }
        /// <summary>
        /// Export variable from an object.
        /// </summary>
        /// <param name="variable"></param>
        /// <returns></returns>
        public VariableExim ExportVariableData(Variable variable)
        {
            return new()
            {
                Name = variable.Name,
                Value = variable.Value,
                Type = (int)variable.Type
            };
        }
        /// <summary>
        /// Export command from an object.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public CommandExIm ExportCommandData(ICommand command)
        {
            switch (command)
            {
                case EmptyCommand emptyCommand:
                    return new() { Type = "EmptyCommand" };
                case CreateComponentCommand createComponentCommand:
                    return new()
                    {
                        Type = "CreateComponentCommand",
                        RenderingObjectType = (int)createComponentCommand.RenderableObjectType
                    };
                case DeleteSceneCommand:
                    return new()
                    {
                        Type = "DeleteSceneCommand"
                    };
                case ShowSideWindowCommand showSideWindowCommand:
                    return new()
                    {
                        Type = "ShowSideWindowCommand",
                        DependentButton = showSideWindowCommand.DependentButtonName,
                        Buttons = [.. showSideWindowCommand.Buttons.Select(ExportEditorButtonData)]
                    };
                case ShowMiniWindowComand showMiniWindowComand:
                    return new()
                    {
                        Type = "ShowMiniWindowCommand",
                        WindowComponents = [.. showMiniWindowComand.Components.Select(ExportEditorComponentFromImport)],
                        HasVariable = showMiniWindowComand.HasVariable.ToString(),
                        HasSceneRelatedComponent = showMiniWindowComand.HasScene.ToString()
                    };
                case ShowInspectorCommand showInspectorCommand:
                    return new()
                    {
                        Type = "ShowInspectorCommand",
                        EnabledRowComponentCount = showInspectorCommand.EnabledRowComponentCount,
                        XPosition = showInspectorCommand.XPosition,
                        YPosition = showInspectorCommand.YPosition
                    };
                case SaveProjectDataCommand saveProjectDataCommand:
                    return new()
                    {
                        Type = "SaveProjectDataCommand"
                    };
                case CreateVariableCommand createVariableCommand:
                    return new()
                    {
                        Type = "CreateVariableCommand",
                        VariableValue = createVariableCommand.Value,
                        VariableType = (int)createVariableCommand.Type
                    };
                case BuildProjectCommand buildProjectCommand:
                    return new()
                    {
                        Type = "BuildProjectCommand"
                    };
                case ShowErrorCommand showErrorCommand:
                    return new()
                    {
                        Type = "ShowErrorCommand",
                        ErrorMessage = showErrorCommand.ErrorMessage,
                        WarningButtons = [.. showErrorCommand.ErrorWindow.Buttons.Select(ExportEditorButtonData)]
                    };
                case DeleteAllComponentsCommand deleteAllComponentsCommand:
                    return new()
                    {
                        Type = "DeleteAllComponentsCommand"
                    };
                default:
                    throw new Exception("Command type not found!");
            }
        }
        /// <summary>
        /// Export a rendering object from an object.
        /// </summary>
        /// <param name="editorComponent"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public RenderingComponentExIm ExportEditorComponentFromImport(IComponent editorComponent)
        {
            switch (editorComponent)
            {
                case Label label:
                    return new() { Label = ExportLabelData(label) };
                case TextField textField:
                    return new() { TextField = ExportTextFieldData(textField) };
                case ToggleButton toggleButton:
                    return new() { Toggle = ExportToggleData(toggleButton) };
                case Dropdown dropDown:
                    return new() { DropDown = ExportDropDownData(dropDown) };
                case Button button:
                    return new() { Button = ExportEditorButtonData(button) };
                default:
                    throw new Exception("Rendering component type not found!");
            }
        }
        /// <summary>
        /// Fetch a label from an object.
        /// </summary>
        /// <param name="label"></param>
        /// <returns></returns>
        public LabelExIm ExportLabelData(Label label)
        {
            return new()
            {
                XPosition = label.XPosition,
                YPosition = label.YPosition,
                Text = label.Text
            };
        }
        /// <summary>
        /// Fetch a textfield from an object.
        /// </summary>
        /// <param name="textField"></param>
        /// <returns></returns>
        public EditorTextFieldExim ExportTextFieldData(TextField textField)
        {
            return new()
            {
                Text = textField.Text,
                XPosition = textField.XPosition,
                YPosition = textField.YPosition,
                Static = textField.IsStatic.ToString()
            };
        }
        /// <summary>
        /// Fetch an editor toggle from an object.
        /// </summary>
        /// <param name="toggleButton"></param>
        /// <returns></returns>
        public EditorToggleExim ExportToggleData(ToggleButton toggleButton)
        {
            return new()
            {
                XPosition = toggleButton.XPosition,
                YPosition = toggleButton.YPosition,
                Text = toggleButton.Text,
                Value = toggleButton.IsToggled.ToString()
            };
        }
        /// <summary>
        /// Fetch a dropdown from an object.
        /// </summary>
        /// <param name="dropDown"></param>
        /// <returns></returns>
        public DropDownExim ExportDropDownData(Dropdown dropDown)
        {
            return new()
            {
                XPosition = dropDown.XPosition,
                YPosition = dropDown.YPosition,
                Text = dropDown.Text,
                Filter = (int)dropDown.Filter,
                Options = [.. dropDown.ButtonList.Select(ExportEditorButtonData)]
            };
        }
        /// <summary>
        /// Fetch an editor sprite from an object.
        /// </summary>
        /// <param name="sprite"></param>
        /// <returns></returns>
        public SpriteExim ExportSpriteData(Sprite sprite)
        {
            return new()
            {
                Path = sprite.Path,
                XPosition = sprite.X,
                YPosition = sprite.Y,
            };
        }
        /// <summary>
        /// Fetch a static editor sprite from an object.
        /// </summary>
        /// <param name="sprite"></param>
        /// <returns></returns>
        public SpriteExim ExportStaticSpriteData(Sprite sprite)
        {
            return new()
            {
                Path = sprite.Path,
                XPosition = sprite.X,
                YPosition = sprite.Y,
            };
        }
        /// <summary>
        /// Fetch a block from an object.
        /// </summary>
        /// <param name="block"></param>
        /// <returns></returns>
        public BlockExim ExportBlockData(Block block)
        {
            return block.Component switch
            {
                Sprite sprite => new()
                {
                    XPosition = block.XPosition,
                    YPosition = block.YPosition,
                    ID = block.ID,
                    Sprite = ExportSpriteData(sprite)
                },
                VisualNovelEngine.Engine.Game.Component.TextField textField => new()
                {
                    XPosition = block.XPosition,
                    YPosition = block.YPosition,
                    ID = block.ID,
                    TextField = ExportTextFieldData(textField)
                },
                VisualNovelEngine.Engine.Game.Component.Button button => new()
                {
                    XPosition = block.XPosition,
                    YPosition = block.YPosition,
                    ID = block.ID,
                    Button = ExportButtonData(button)
                },
                InputField inputField => new()
                {
                    XPosition = block.XPosition,
                    YPosition = block.YPosition,
                    ID = block.ID,
                    InputField = ExportInputFieldData(inputField)
                },
                _ => new()
                {
                    XPosition = block.XPosition,
                    YPosition = block.YPosition,
                    ID = block.ID
                },
            };
        }
        /// <summary>
        /// Fetch a static block from an object.
        /// </summary>
        /// <param name="block"></param>
        /// <returns></returns>
        public BlockExim ExportStaticBlockData(Block block)
        {
            return block.Component switch
            {
                Sprite sprite => new()
                {
                    XPosition = block.XPosition,
                    YPosition = block.YPosition,
                    ID = block.ID,
                    Sprite = ExportStaticSpriteData(sprite)
                },
                VisualNovelEngine.Engine.Game.Component.Button button => new()
                {
                    XPosition = block.XPosition,
                    YPosition = block.YPosition,
                    ID = block.ID,
                    StaticButton = ExportStaticButtonData(button)
                },
                InputField inputField => new()
                {
                    XPosition = block.XPosition,
                    YPosition = block.YPosition,
                    ID = block.ID,
                    InputField = ExportStaticInputFieldData(inputField)
                },
                DropBox dropBox => new()
                {
                    XPosition = block.XPosition,
                    YPosition = block.YPosition,
                    ID = block.ID,
                    DropBox = ExportDropBoxData(dropBox)
                },
                Toggle toggle => new()
                {
                    XPosition = block.XPosition,
                    YPosition = block.YPosition,
                    ID = block.ID,
                    Toggle = ExportToggleData(toggle)
                },
                Slider slider => new()
                {
                    XPosition = block.XPosition,
                    YPosition = block.YPosition,
                    ID = block.ID,
                    Slider = ExportSliderData(slider)
                },
                _ => new()
                {
                    XPosition = block.XPosition,
                    YPosition = block.YPosition,
                    ID = block.ID
                }
            };
        }
        /// <summary>
        /// Fetch a textfield from an object.
        /// </summary>
        /// <param name="textField"></param>
        /// <returns></returns>
        public TextFieldExim ExportTextFieldData(VisualNovelEngine.Engine.Game.Component.TextField textField)
        {
            return new()
            {
                XPosition = textField.XPosition,
                YPosition = textField.YPosition,
                Width = textField.Width,
                Height = textField.Height,
                BorderWidth = textField.BorderWidth,
                HorizontalTextMargin = textField.HorizontalTextMargin,
                VerticalTextMargin = textField.VerticalTextMargin,
                Text = textField.Text,
                IsVisible = textField.IsVisible.ToString(),
                WordWrap = textField.WordWrap.ToString(),
                Font = null,
                Color = [textField.Color.R, textField.Color.G, textField.Color.B, textField.Color.A],
                BorderColor = [textField.BorderColor.R, textField.BorderColor.G, textField.BorderColor.B, textField.BorderColor.A]
            };
        }
        /// <summary>
        /// Fetch a textbox from an object.
        /// </summary>
        /// <param name="textBox"></param>
        /// <returns></returns>
        public TextBoxExIm ExportTextBoxData(TextBox textBox)
        {
            return new()
            {
                CharactersPerSecond = textBox.CPSTextSpeed,
                Color = [.. ExportColorData(textBox.Color)],
                BorderColor = [.. ExportColorData(textBox.BorderColor)],
                PositionType = (int)textBox.TextBoxPositionType,
                HorizontalTextMargin = textBox.HorizontalTextMargin,
                VerticalTextMargin = textBox.VerticalTextMargin,
                WordWrap = textBox.WordWrap,
                Title = textBox.Title,
                Content = [.. textBox.Content]
            };
        }
        /// <summary>
        /// Export a button from an object
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public ButtonComponentExIm ExportButtonData(VisualNovelEngine.Engine.Game.Component.Button button)
        {
            return new()
            {
                XPosition = button.XPosition,
                YPosition = button.YPosition,
                Width = button.Width,
                Height = button.Height,
                BorderWidth = button.BorderWidth,
                TextColor = [.. ExportColorData(button.TextColor)],
                Color = [.. ExportColorData(button.Color)],
                HoverColor = [.. ExportColorData(button.HoverColor)],
                BorderColor = [.. ExportColorData(button.BorderColor)],
                Text = button.Text,
                Action = ExportActionData(button.Action),
            };
        }
        /// <summary>
        /// Export a button's component from an object
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public ButtonComponentExIm ExportStaticButtonData(VisualNovelEngine.Engine.Game.Component.Button button)
        {
            return new()
            {
                XPosition = button.XPosition,
                YPosition = button.YPosition,
                Width = button.Width,
                Height = button.Height,
                BorderWidth = button.BorderWidth,
                TextColor = [.. ExportColorData(button.TextColor)],
                Color = [.. ExportColorData(button.Color)],
                HoverColor = [.. ExportColorData(button.HoverColor)],
                BorderColor = [.. ExportColorData(button.BorderColor)],
                Text = button.Text,
                Action = BuildTimelineIndependentAction(button.Action),
            };
        }
        /// <summary>
        /// Export an inputfield from an object.
        /// </summary>
        /// <param name="inputField"></param>
        /// <returns></returns>
        public InputFieldExim ExportInputFieldData(InputField inputField)
        {
            return new()
            {
                XPosition = inputField.XPosition,
                YPosition = inputField.YPosition,
                Width = inputField.Width,
                Height = inputField.Height,
                BorderWidth = inputField.BorderWidth,
                Color = [.. ExportColorData(inputField.Color)],
                SelectedColor = [.. ExportColorData(inputField.SelectedColor)],
                HoverColor = [.. ExportColorData(inputField.HoverColor)],
                BorderColor = [.. ExportColorData(inputField.BorderColor)],
                PlaceholderText = inputField.Placeholder,
                ButtonText = inputField.Button.Text,
                ButtonAction = ExportActionData(inputField.Button.Action),
            };
        }
        /// <summary>
        /// Export a static inputfield from an object.
        /// </summary>
        /// <param name="inputField"></param>
        /// <returns></returns>
        public InputFieldExim ExportStaticInputFieldData(InputField inputField)
        {
            return new()
            {
                XPosition = inputField.XPosition,
                YPosition = inputField.YPosition,
                Width = inputField.Width,
                Height = inputField.Height,
                BorderWidth = inputField.BorderWidth,
                Color = [.. ExportColorData(inputField.Color)],
                SelectedColor = [.. ExportColorData(inputField.SelectedColor)],
                HoverColor = [.. ExportColorData(inputField.HoverColor)],
                BorderColor = [.. ExportColorData(inputField.BorderColor)],
                PlaceholderText = inputField.Placeholder,
                ButtonText = inputField.Button.Text,
                ButtonAction = BuildTimelineIndependentAction(inputField.Button.Action),
            };
        }
        /// <summary>
        /// Export a dropbox from an object.
        /// </summary>
        /// <param name="dropBox"></param>
        /// <returns></returns>
        public DropBoxExim ExportDropBoxData(DropBox dropBox)
        {
            return new()
            {
                XPosition = dropBox.XPosition,
                YPosition = dropBox.YPosition,
                Width = dropBox.Width,
                Height = dropBox.Height,
                Color = [.. ExportColorData(dropBox.Color)],
                HoverColor = [.. ExportColorData(dropBox.HoverColor)],
                BorderColor = [.. ExportColorData(dropBox.BorderColor)],
                TextColor = [.. ExportColorData(dropBox.Color)],
                Options = [.. dropBox.Options.Select(ExportDropBoxOptionData)],
            };
        }
        /// <summary>
        /// Export a dropbox option from an object.
        /// </summary>
        /// <param name="dropBoxOption"></param>
        /// <returns></returns>
        public DropBoxOptionExim ExportDropBoxOptionData(VisualNovelEngine.Engine.Game.Component.Button dropBoxOption)
        {
            return new()
            {
                Text = dropBoxOption.Text,
                Action = BuildTimelineIndependentAction(dropBoxOption.Action)
            };
        }
        /// <summary>
        /// Export a menu from an object.
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public MenuExim ExportMenuData(Menu menu)
        {
            return new()
            {
                ID = menu.ID,
                XPosition = menu.XPosition,
                YPosition = menu.YPosition,
                Width = menu.Width,
                Height = menu.Height,
                Color = [.. ExportColorData(menu.Color)],
                BorderColor = [.. ExportColorData(menu.BorderColor)],
                BlockList = [.. menu.BlockList.Select(ExportBlockData)],
                Static = bool.FalseString
            };
        }
        /// <summary>
        /// Export a static menu from an object.
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public MenuExim ExportStaticMenuData(Menu menu)
        {
            return new()
            {
                ID = menu.ID,
                XPosition = menu.XPosition,
                YPosition = menu.YPosition,
                Width = menu.Width,
                Height = menu.Height,
                Color = [.. ExportColorData(menu.Color)],
                BorderColor = [.. ExportColorData(menu.BorderColor)],
                BlockList = [.. menu.BlockList.Select(ExportStaticBlockData)],
                Static = bool.TrueString
            };
        }
        /// <summary>
        /// Export a slider from an object.
        /// </summary>
        /// <param name="slider"></param>
        /// <returns></returns>
        public SliderExim ExportSliderData(Slider slider)
        {
            return new()
            {
                XPosition = slider.XPosition,
                YPosition = slider.YPosition,
                Width = slider.Width,
                Height = slider.Height,
                Color = [.. ExportColorData(slider.Color)],
                BorderColor = [.. ExportColorData(slider.BorderColor)],
                DragColor = [.. ExportColorData(slider.DragColor)],
                value = (int)slider.Value,
                DragRadius = slider.SliderDragRadius,
                Action = BuildTimelineIndependentAction(slider.Action)
            };
        }
        /// <summary>
        /// Export a toggle from an object.
        /// </summary>
        /// <param name="toggle"></param>
        /// <returns></returns>/
        public ToggleExim ExportToggleData(Toggle toggle)
        {
            return new()
            {
                XPosition = toggle.XPosition,
                YPosition = toggle.YPosition,
                Color = [.. ExportColorData(toggle.Color)],
                BorderColor = [.. ExportColorData(toggle.BorderColor)],
                ActivatedColor = [.. ExportColorData(toggle.ToggledColor)],
                BoxSize = toggle.BoxSize,
                Toggled = toggle.IsToggled.ToString(),
                TextXOffset = toggle.TextXOffset,
                Text = toggle.Text,
                Action = BuildTimelineIndependentAction(toggle.SettingsAction)
            };
        }
        /// <summary>
        /// Export the editor.
        /// </summary>
        /// <param name="editor"></param>
        /// <returns></returns>
        public EditorExIm ExportEditorData(Editor editor)
        {
            return new()
            {
                ID = editor.IDGenerator.ID,
                ProjectName = Regex.Replace(editor.ProjectName, @"[^a-zA-Z0-9\s]", ""),
                ProjectPath = editor.SaveFilePath,
                WindowWidth = 800,
                WindowHeight = 800,
                ToolBar = ExportGroupData(editor.Toolbar),
                Scenes = [.. editor.SceneList.Select(ExportEditorSceneData)],
                Variables = [.. editor.GameVariables.Select(ExportVariableData)]
            };
        }
        /// <summary>
        /// Export group from an object.
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public GroupExIm ExportGroupData(Group group)
        {
            //Check if the group's components are buttons
            bool isButton = group.ComponentList.All(x => x is Button);
            ButtonExIm[] buttons = null;
            ComponentExIm[] components = null;
            switch (isButton)
            {
                case true:
                    buttons = [.. group.ComponentList.Select(x => x as Button).Select(ExportEditorButtonData)];
                    components = [];
                    if (buttons.Length < group.ComponentList.Count())
                    {
                        components = [.. group.ComponentList.Select(x => x as Component).Select(ExportComponentData)];
                    }
                    return new()
                    {
                        XPosition = group.XPosition,
                        YPosition = group.YPosition,
                        Static = group.IsStatic.ToString(),
                        Width = group.Width,
                        Height = group.Height,
                        BorderWidth = group.BorderWidth,
                        Buttons = [.. buttons],
                        MaximumHorizontalComponentCount = group.MaximumHorizontalComponentCount,
                        Components = [.. components]
                    };
                case false:
                    buttons = [];
                    components = [.. group.ComponentList.Select(x => x as Component).Select(ExportComponentData)];
                    return new()
                    {
                        XPosition = group.XPosition,
                        YPosition = group.YPosition,
                        Static = group.IsStatic.ToString(),
                        Width = group.Width,
                        Height = group.Height,
                        BorderWidth = group.BorderWidth,
                        Buttons = [.. buttons],
                        MaximumHorizontalComponentCount = group.MaximumHorizontalComponentCount,
                        Components = [.. components]
                    };
            }
        }
        /// <summary>
        /// Export a button from an object.
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public ButtonExIm ExportEditorButtonData(Button button)
        {
            return new()
            {
                XPosition = button.XPosition,
                YPosition = button.YPosition,
                Text = button.Text,
                Command = ExportCommandData(button.Command),
                Type = (int)button.Type
            };
        }
        /// <summary>
        /// Export a component from an object.
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        public ComponentExIm ExportComponentData(Component component)
        {
            switch (component.IsObjectStatic)
            {
                case true:
                    return new()
                    {
                        ID = component.ID,
                        Name = component.Name,
                        XPosition = component.XPosition,
                        YPosition = component.YPosition,
                        RenderingObject = ExportStaticRenderingObjectData(component.RenderingObject)
                    };
                case false:
                    return new()
                    {
                        ID = component.ID,
                        Name = component.Name,
                        XPosition = component.XPosition,
                        YPosition = component.YPosition,
                        RenderingObject = ExportRenderingObjectData(component.RenderingObject)
                    };
            }

        }
        /// <summary>
        /// Export a scene from an object.
        /// </summary>
        /// <param name="scene"></param>
        /// <returns></returns>
        public SceneExIm ExportEditorSceneData(Scene scene)
        {
            return scene.BackgroundOption switch
            {
                Game.Component.Scene.BackgroundOption.SolidColor => new()
                {
                    ID = scene.ID,
                    Name = scene.Name,
                    Background = scene.BackgroundOption.ToString(),
                    SolidColor = ExportColorData(scene.BackgroundColor.Value),
                    Components = [.. scene.ComponentList.Select(x => x as Component).Select(ExportComponentData)],
                    GroupList = [.. scene.ComponentGroupList.Select(ExportGroupData)],
                    Timeline = ExportTimelineData(scene.Timeline)
                },
                Game.Component.Scene.BackgroundOption.GradientHorizontal => new()
                {
                    ID = scene.ID,
                    Name = scene.Name,
                    Background = scene.BackgroundOption.ToString(),
                    GradientColor = ExportGradientColor(scene.BackgroundGradientColor),
                    Components = [.. scene.ComponentList.Select(x => x as Component).Select(ExportComponentData)],
                    GroupList = [.. scene.ComponentGroupList.Select(ExportGroupData)],
                    Timeline = ExportTimelineData(scene.Timeline)
                },
                Game.Component.Scene.BackgroundOption.GradientVertical => new()
                {
                    ID = scene.ID,
                    Name = scene.Name,
                    Background = scene.BackgroundOption.ToString(),
                    GradientColor = ExportGradientColor(scene.BackgroundGradientColor),
                    Components = [.. scene.ComponentList.Select(x => x as Component).Select(ExportComponentData)],
                    GroupList = [.. scene.ComponentGroupList.Select(ExportGroupData)],
                    Timeline = ExportTimelineData(scene.Timeline)
                },
                Game.Component.Scene.BackgroundOption.Image => new()
                {
                    ID = scene.ID,
                    Name = scene.Name,
                    Background = scene.BackgroundOption.ToString(),
                    ImageTexture = scene.BackgroundImage.ToString(),
                    Components = [.. scene.ComponentList.Select(x => x as Component).Select(ExportComponentData)],
                    GroupList = [.. scene.ComponentGroupList.Select(ExportGroupData)],
                    Timeline = ExportTimelineData(scene.Timeline)
                },
            };
        }
        /// <summary>
        /// Export a rendering object from an object.
        /// </summary>
        /// <param name="permanentRenderingObject"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public RenderingObjectExIm ExportRenderingObjectData(IRenderingObject permanentRenderingObject)
        {
            return permanentRenderingObject switch
            {
                Sprite sprite => new()
                {
                    Static = false.ToString(),
                    Sprite = ExportSpriteData(sprite)
                },
                VisualNovelEngine.Engine.Game.Component.TextField textField => new()
                {
                    Static = false.ToString(),
                    TextField = ExportTextFieldData(textField)
                },
                TextBox textBox => new()
                {
                    Static = false.ToString(),
                    Textbox = ExportTextBoxData(textBox)
                },
                Block block => new()
                {
                    Static = false.ToString(),
                    Block = ExportBlockData(block)
                },
                VisualNovelEngine.Engine.Game.Component.Button button => new()
                {
                    Static = false.ToString(),
                    Button = ExportButtonData(button)
                },
                InputField inputField => new()
                {
                    Static = false.ToString(),
                    InputField = ExportInputFieldData(inputField)
                },
                DropBox dropBox => new()
                {
                    Static = false.ToString(),
                    DropBox = ExportDropBoxData(dropBox)
                },
                Menu menu => new()
                {
                    Static = false.ToString(),
                    Menu = ExportMenuData(menu)
                },
                Slider slider => new()
                {
                    Static = false.ToString(),
                    Slider = ExportSliderData(slider)
                },
                Toggle toggle => new()
                {
                    Static = false.ToString(),
                    Toggle = ExportToggleData(toggle)
                },
                _ => throw new Exception("Rendering object type not found!"),
            };
        }
        /// <summary>
        /// Export a static rendering object from an object.
        /// </summary>
        /// <param name="permanentRenderingObject"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public RenderingObjectExIm ExportStaticRenderingObjectData(IRenderingObject permanentRenderingObject)
        {
            return permanentRenderingObject switch
            {
                Sprite sprite => new()
                {
                    Static = true.ToString(),
                    Sprite = ExportStaticSpriteData(sprite)
                },
                Block block => new()
                {
                    Static = true.ToString(),
                    StaticBlock = ExportStaticBlockData(block)
                },
                VisualNovelEngine.Engine.Game.Component.Button button => new()
                {
                    Static = true.ToString(),
                    StaticButton = ExportStaticButtonData(button)
                },
                InputField inputField => new()
                {
                    Static = true.ToString(),
                    InputField = ExportStaticInputFieldData(inputField)
                },
                DropBox dropBox => new()
                {
                    Static = true.ToString(),
                    DropBox = ExportDropBoxData(dropBox)
                },
                Menu menu => new()
                {
                    Static = true.ToString(),
                    Menu = ExportStaticMenuData(menu)
                },
                Slider slider => new()
                {
                    Static = true.ToString(),
                    Slider = ExportSliderData(slider)
                },
                Toggle toggle => new()
                {
                    Static = true.ToString(),
                    Toggle = ExportToggleData(toggle)
                },
                _ => throw new Exception("Rendering object type not found!"),
            };
        }
        /// <summary>
        /// Export the timeline data.
        /// </summary>
        /// <param name="timeline"></param>
        /// <returns></returns>
        public TimelineExIm ExportTimelineData(Timeline timeline)
        {
            return new()
            {
                XPosition = timeline.XPosition,
                YPosition = timeline.YPosition,
                Actions = [.. timeline.Actions.Select(ExportActionData)],
                TimelineIndependentActions = [.. timeline.TimelineIndepententActions.Select(x => ExportTimelineIndependentActionData((IAction)x))]
            };
        }
        /// <summary>
        /// Export the action data.
        /// </summary>
        /// <param name="actionData"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public ActionExim ExportActionData(IAction actionData)
        {
            return actionData switch
            {
                EmptyAction => new() { Type = "EmptyAction" },
                CreateMenuAction createMenuAction => new()
                {
                    Type = "CreateMenuAction",
                    Menu = ExportMenuData(createMenuAction.Menu),
                    StaticMenu = null
                },
                LoadSceneAction loadSceneAction => new()
                {
                    Type = "LoadSceneAction",
                    SceneID = loadSceneAction.sceneID,
                    TriggerVariableName = loadSceneAction.TriggerVariableName
                },
                NativeLoadSceneAction nativeLoadSceneAction => new()
                {
                    Type = "NativeLoadSceneAction",
                    SceneID = nativeLoadSceneAction.sceneID
                },
                AddSpriteAction addSpriteAction => new()
                {
                    Type = "AddSpriteAction",
                    Sprite = ExportSpriteData(addSpriteAction.sprite)
                },
                ChangeSpriteAction changeSpriteAction => new()
                {
                    Type = "ChangeSpriteAction"
                },
                DecrementVariableAction decrementVariableAction => new()
                {
                    Type = "DecrementVariableAction",
                    VariableName = decrementVariableAction.VariableName,
                    ImpendingVariableName = decrementVariableAction.DecrementVariableName
                },
                IncrementVariableAction incrementVariableAction => new()
                {
                    Type = "IncrementVariableAction",
                    ImpendingVariableName = incrementVariableAction.IncrementVariableName
                },
                RemoveSpriteAction removeSpriteAction => new()
                {
                    Type = "RemoveSpriteAction",
                    Sprite = ExportSpriteData(removeSpriteAction.sprite)
                },
                SetBoolVariableAction setBoolVariableAction => new()
                {
                    Type = "SetBoolVariableAction",
                    VariableName = setBoolVariableAction.VariableName,
                    VariableValue = setBoolVariableAction.Value.ToString()
                },
                SetVariableFalseAction setVariableFalseAction => new()
                {
                    Type = "SetVariableFalseAction",
                    VariableName = setVariableFalseAction.VariableName
                },
                SetVariableTrueAction setVariableTrueAction => new()
                {
                    Type = "SetVariableTrueAction",
                    VariableName = setVariableTrueAction.VariableName
                },
                TextBoxCreateAction textBoxCreateAction => new()
                {
                    Type = "TextBoxCreateAction",
                    TextBox = ExportTextBoxData(textBoxCreateAction.TextBox)
                },
                TintSpriteAction tintSpriteAction => new()
                {
                    Type = "TintSpriteAction",
                    Sprite = ExportSpriteData(tintSpriteAction.sprite),
                    TintColor = ExportColorData(tintSpriteAction.color)
                },
                ToggleVariableAction toggleVariableAction => new()
                {
                    Type = "ToggleVariableAction",
                    VariableName = toggleVariableAction.VariableName
                }
            };
        }
        /// <summary>
        /// Export the timeline independent action data.
        /// </summary>
        /// <param name="actionData"></param>
        /// <returns></returns>
        public ActionExim ExportTimelineIndependentActionData(IAction actionData)
        {
            return actionData switch
            {
                CreateMenuAction createMenuAction => new()
                {
                    Type = "CreateMenuAction",
                    Menu = null,
                    StaticMenu = ExportStaticMenuData(createMenuAction.Menu)
                },
                LoadSceneAction loadSceneAction => new()
                {
                    Type = "LoadSceneAction",
                    SceneID = loadSceneAction.sceneID,
                    TriggerVariableName = loadSceneAction.TriggerVariableName
                },
                NativeLoadSceneAction nativeLoadSceneAction => new()
                {
                    Type = "NativeLoadSceneAction",
                    SceneID = nativeLoadSceneAction.sceneID
                },
                SetVariableValueAction setVariableValueAction => new()
                {
                    Type = "SetVariableValueAction",
                    VariableName = setVariableValueAction.VariableName,
                    BlockComponentID = setVariableValueAction.ComponentID
                },
                SwitchStaticMenuAction switchStaticMenuAction => new()
                {
                    Type = "SwitchStaticMenuAction",
                    DisablingMenuID = switchStaticMenuAction.DisablingMenu.ID,
                    EnablingMenuID = switchStaticMenuAction.EnablingMenu.ID
                }
            };
        }
        /// <summary>
        /// Build the game data.
        /// </summary>
        /// <param name="editor"></param>
        /// <returns></returns>
        public GameExim BuildGameData(Editor editor)
        {
            return new()
            {
                Title = editor.ProjectName,
                WindowWidth = editor.ScreenWidth,
                WindowHeight = editor.ScreenHeight,
                Scenes = BuildScenesData([.. editor.SceneList]),
                Variables = BuildVariablesData([.. Editor.GameVariables])
            };
        }
        /// <summary>
        /// Build the scenes data
        /// </summary>
        /// <param name="scene"></param>
        /// <returns></returns>
        public SceneExim[] BuildScenesData(Scene[] scene)
        {
            return [.. scene.Select(BuildSceneData)];
        }
        /// <summary>
        /// Build a scene datum
        /// </summary>
        /// <param name="scene"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public SceneExim BuildSceneData(Scene scene)
        {
            return scene.BackgroundOption switch
            {
                Game.Component.Scene.BackgroundOption.SolidColor => new()
                {
                    ID = scene.ID,
                    Name = scene.Name,
                    BackgroundType = (int)scene.BackgroundOption,
                    SolidColor = ExportColorData(scene.BackgroundColor.Value),
                    ActionList = BuildTimelineData(scene.Timeline)
                },
                Game.Component.Scene.BackgroundOption.GradientHorizontal => new()
                {
                    ID = scene.ID,
                    Name = scene.Name,
                    BackgroundType = (int)scene.BackgroundOption,
                    GradientColor = ExportGradientColor(scene.BackgroundGradientColor),
                    ActionList = BuildTimelineData(scene.Timeline)
                },
                Game.Component.Scene.BackgroundOption.GradientVertical => new()
                {
                    ID = scene.ID,
                    Name = scene.Name,
                    BackgroundType = (int)scene.BackgroundOption,
                    GradientColor = ExportGradientColor(scene.BackgroundGradientColor),
                    ActionList = BuildTimelineData(scene.Timeline)
                },
                Game.Component.Scene.BackgroundOption.Image => new()
                {
                    ID = scene.ID,
                    Name = scene.Name,
                    BackgroundType = (int)scene.BackgroundOption,
                    ImageTexture = scene.BackgroundImage.ToString(),
                    ActionList = BuildTimelineData(scene.Timeline)
                },
                _ => throw new Exception("Background type not found!"),
            };
        }
        /// <summary>
        /// Build the timeline data
        /// </summary>
        /// <param name="timeline"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public ActionExim[] BuildTimelineData(Timeline timeline)
        {
            List<ActionExim> actions = [];
            foreach (IAction item in timeline.Actions)
            {
                switch (item)
                {
                    case AddSpriteAction
                    or ChangeSpriteAction
                    or DecrementVariableAction
                    or IncrementVariableAction
                    or RemoveSpriteAction
                    or SetBoolVariableAction
                    or SetVariableFalseAction
                    or SetVariableTrueAction
                    or SetVariableValueAction
                    or TextBoxCreateAction
                    or TintSpriteAction
                    or ToggleVariableAction
                    or EmptyAction
                    or CreateMenuAction
                    or LoadSceneAction
                    or NativeLoadSceneAction
                    or AddSpriteAction:
                        actions.Add(BuildEventData(item));
                        break;
                    case SetVariableValueAction
                    or SwitchStaticMenuAction:
                        continue;
                    default:
                        throw new Exception("Event type not found!");
                }
            }
            return [.. actions];
        }
        /// <summary>
        /// Build variable data
        /// </summary>
        /// <param name="variables"></param>
        /// <returns></returns>
        public VariableExim[] BuildVariablesData(Variable[] variables)
        {
            return [.. variables.Select(BuildVariableData)];
        }
        /// <summary>
        /// build the variable datum.
        /// </summary>
        /// <param name="variable"></param>
        /// <returns></returns>
        public VariableExim BuildVariableData(Variable variable)
        {
            return ExportVariableData(variable);
        }
        /// <summary>
        /// Build the action datum.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public ActionExim BuildEventData(IAction action)
        {
            return action switch
            {
                EmptyAction => new() { Type = "EmptyAction" },
                CreateMenuAction createMenuAction => new()
                {
                    Type = "CreateMenuAction",
                    Menu = createMenuAction.StaticExport is false ? ExportMenuData(createMenuAction.Menu) : null,
                    StaticMenu = createMenuAction.StaticExport is true ? ExportStaticMenuData(createMenuAction.Menu) : null
                },
                LoadSceneAction loadSceneAction => new()
                {
                    Type = "LoadSceneAction",
                    SceneID = loadSceneAction.sceneID,
                    TriggerVariableName = loadSceneAction.TriggerVariableName
                },
                NativeLoadSceneAction nativeLoadSceneAction => new()
                {
                    Type = "NativeLoadSceneAction",
                    SceneID = nativeLoadSceneAction.sceneID
                },
                AddSpriteAction addSpriteAction => new()
                {
                    Type = "AddSpriteAction",
                    Sprite = ExportSpriteData(addSpriteAction.sprite)
                },
                ChangeSpriteAction changeSpriteAction => new()
                {
                    Type = "ChangeSpriteAction"
                },
                DecrementVariableAction decrementVariableAction => new()
                {
                    Type = "DecrementVariableAction",
                    VariableName = decrementVariableAction.VariableName,
                    ImpendingVariableName = decrementVariableAction.DecrementVariableName
                },
                IncrementVariableAction incrementVariableAction => new()
                {
                    Type = "IncrementVariableAction",
                    ImpendingVariableName = incrementVariableAction.IncrementVariableName
                },
                RemoveSpriteAction removeSpriteAction => new()
                {
                    Type = "RemoveSpriteAction",
                    Sprite = ExportSpriteData(removeSpriteAction.sprite)
                },
                SetBoolVariableAction setBoolVariableAction => new()
                {
                    Type = "SetBoolVariableAction",
                    VariableName = setBoolVariableAction.VariableName,
                    VariableValue = setBoolVariableAction.Value.ToString()
                },
                SetVariableFalseAction setVariableFalseAction => new()
                {
                    Type = "SetVariableFalseAction",
                    VariableName = setVariableFalseAction.VariableName
                },
                SetVariableTrueAction setVariableTrueAction => new()
                {
                    Type = "SetVariableTrueAction",
                    VariableName = setVariableTrueAction.VariableName
                },
                TextBoxCreateAction textBoxCreateAction => new()
                {
                    Type = "TextBoxCreateAction",
                    TextBox = ExportTextBoxData(textBoxCreateAction.TextBox)
                },
                TintSpriteAction tintSpriteAction => new()
                {
                    Type = "TintSpriteAction",
                    Sprite = ExportSpriteData(tintSpriteAction.sprite),
                    TintColor = ExportColorData(tintSpriteAction.color)
                },
                ToggleVariableAction toggleVariableAction => new()
                {
                    Type = "ToggleVariableAction",
                    VariableName = toggleVariableAction.VariableName
                },
                _ => throw new Exception("Event type is either not Timeline Dependent or is not found!"),
            };
        }
        /// <summary>
        /// Build the timeline independent action datum.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public ActionExim BuildTimelineIndependentAction(IAction action)
        {
            return action switch
            {
                CreateMenuAction createMenuAction => new()
                {
                    Type = "CreateMenuAction",
                    StaticMenu = ExportStaticMenuData(createMenuAction.Menu)
                },
                LoadSceneAction loadSceneAction => new()
                {
                    Type = "LoadSceneAction",
                    SceneID = loadSceneAction.sceneID,
                    TriggerVariableName = loadSceneAction.TriggerVariableName
                },
                NativeLoadSceneAction nativeLoadSceneAction => new()
                {
                    Type = "NativeLoadSceneAction",
                    SceneID = nativeLoadSceneAction.sceneID
                },
                SetVariableValueAction setVariableValueAction => new()
                {
                    Type = "SetVariableValueAction",
                    VariableName = setVariableValueAction.VariableName
                },
                SwitchStaticMenuAction switchStaticMenuAction => new()
                {
                    Type = "SwitchStaticMenuAction"
                },
                _ => throw new Exception("Event type is either not Timeline independent or is not found!"),
            };
        }
    }
}