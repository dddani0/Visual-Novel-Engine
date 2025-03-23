using System.Text.Json;
using Raylib_cs;
using ICommand = VisualNovelEngine.Engine.EngineEditor.Interface.ICommand;
using VisualNovelEngine.Engine.EngineEditor.Component.Command;
using TemplateGame.Component;
using static VisualNovelEngine.Engine.EngineEditor.Component.Command.CreateComponentCommand;
using VisualNovelEngine.Engine.PortData;
using TemplateGame.Interface;
using System.Text.RegularExpressions;
using TemplateGame.Component.Action.TimelineDependent;
using TemplateGame.Component.Action;
using System.Collections.Concurrent;
using TemplateGame.Component.Action.TimelineIndependent;
using VisualNovelEngine.Engine.EngineEditor.Interface;
using Namespace;
using Engine.EngineEditor.Component.Command;
using EngineEditor.Component.Command;

namespace VisualNovelEngine.Engine.EngineEditor.Component
{
    /// <summary>
    /// Imports editor data and preferences from the associated JSON files.
    //  Exports Game related data from the Editor to the associated JSON file.
    /// </summary>
    public class EditorExImManager
    {
        Editor Editor { get; set; }
        internal EditorPreferencesIm EditorPreferencesImport { get; set; }
        internal EditorExIm EditorExIm { get; set; }
        internal GameExim GameExim { get; set; }
        internal GameImporter GameImporter { get; set; }
        public EditorExImManager(Editor editor, string editorConfigPath, string editorDataPath)
        {
            Editor = editor;
            //Editor config
            string rawFile = File.ReadAllText(editorConfigPath) ?? throw new Exception("Editor preference file not found!");
            EditorPreferencesImport = JsonSerializer.Deserialize<EditorPreferencesIm>(rawFile) ?? throw new Exception("Editor preference file not found!");
            //Editor data
            rawFile = File.ReadAllText(editorDataPath) ?? throw new Exception("Editor data file not found!");
            EditorExIm = JsonSerializer.Deserialize<EditorExIm>(rawFile) ?? throw new Exception("Editor data file not found!");
            //Game data
            GameExim = Editor.Game.GameSettings;
            //Game importer
            GameImporter = Editor.Game.GameImport;
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
            TemplateGame.Component.Scene.BackgroundOption backgroundOption = sceneImport.Background switch
            {
                "SolidColor" => TemplateGame.Component.Scene.BackgroundOption.SolidColor,
                "GradientVertical" => TemplateGame.Component.Scene.BackgroundOption.GradientVertical,
                "GradientHorizontal" => TemplateGame.Component.Scene.BackgroundOption.GradientHorizontal,
                "Image" => TemplateGame.Component.Scene.BackgroundOption.Image,
                _ => throw new Exception("Background type not found!")
            };
            Texture2D? image;
            Color[]? gradientColor;
            Color? solidColor;
            if (backgroundOption == TemplateGame.Component.Scene.BackgroundOption.Image)
            {
                image = Raylib.LoadTexture(sceneImport.ImageTexture);
                return new Scene(Editor,
                sceneImport.Name,
                [.. sceneImport.Components.Select(FetchComponentFromImport)],
                [.. sceneImport.GroupList.Select(FetchGroupFromImport)])
                {
                    BackgroundOption = backgroundOption,
                    BackgroundImage = image
                };
            }
            else if (backgroundOption == TemplateGame.Component.Scene.BackgroundOption.GradientHorizontal || backgroundOption == TemplateGame.Component.Scene.BackgroundOption.GradientVertical)
            {
                gradientColor = FetchGradientColorFromImport(sceneImport.GradientColor);
                return new Scene(Editor,
                sceneImport.Name,
                [.. sceneImport.Components.Select(FetchComponentFromImport)],
                [.. sceneImport.GroupList.Select(FetchGroupFromImport)])
                {
                    BackgroundOption = backgroundOption,
                    BackgroundGradientColor = gradientColor
                };
            }
            else
            {
                solidColor = FetchColorFromImport(sceneImport.SolidColor);
                return new Scene(Editor,
                sceneImport.Name,
                [.. sceneImport.Components.Select(FetchComponentFromImport)],
                [.. sceneImport.GroupList.Select(FetchGroupFromImport)])
                {
                    BackgroundOption = backgroundOption,
                    BackgroundColor = solidColor
                };
            }

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
        public IAction FetchEventFromImport(ActionExim ActionImport)
        {
            return ActionImport.Type switch
            {
                "EmptyAction" => new EmptyAction(Editor.Game),
                _ => throw new Exception("Event type not found!"),
            };
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
            );
        }
        /// <summary>
        /// Fetch an editor related component from an import object.
        /// </summary>
        /// <param name="componentImport"></param>
        /// <returns></returns>
        public Component FetchComponentFromImport(ComponentExIm componentImport)
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
        public DropDown FetchDropDownFromImport(DropDownExim dropDownImport)
        {
            return new(
                Editor,
                dropDownImport.XPosition,
                dropDownImport.YPosition,
                Editor.ComponentWidth,
                Editor.ComponentHeight,
                Editor.ComponentBorderWidth,
                (DropDown.FilterType)dropDownImport.Filter
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
        public IComponent FetchEditorComponentFromImport(RenderingComponentExIm renderingComponentImport)
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
        public IPermanentRenderingObject FetchRenderingObjectFromImport(RenderingObjectExIm renderingObjectImport)
        {
            if (renderingObjectImport.Sprite != null)
            {
                return new Sprite(renderingObjectImport.Sprite.Path);
            }
            if (renderingObjectImport.Block != null)
            {
                return GameImporter.FetchBlockFromImport(renderingObjectImport.Block);
            }
            else if (renderingObjectImport.Textbox != null)
            {
                return FetchTextBoxFromImport(renderingObjectImport.Textbox);
            }
            else if (renderingObjectImport.Button != null)
            {
                return GameImporter.FetchButtonFromImport(renderingObjectImport.Button, null);
            }
            else if (renderingObjectImport.InputField != null)
            {
                return GameImporter.FetchInputFieldFromImport(renderingObjectImport.InputField, null);
            }
            else if (renderingObjectImport.DropBox != null)
            {
                return GameImporter.FetchDropBoxFromImport(renderingObjectImport.DropBox, null);
            }
            else if (renderingObjectImport.Menu != null)
            {
                return GameImporter.FetchMenuFromImport(renderingObjectImport.Menu);
            }
            else if (renderingObjectImport.Slider != null)
            {
                return GameImporter.FetchSliderFromImport(renderingObjectImport.Slider, null);
            }
            else if (renderingObjectImport.TextField != null)
            {
                return GameImporter.FetchTextFieldFromImport(renderingObjectImport.TextField, null);
            }
            else if (renderingObjectImport.StaticBlock != null)
            {
                return GameImporter.FetchBlockFromImport(renderingObjectImport.StaticBlock);
            }
            else if (renderingObjectImport.StaticInputField != null)
            {
                return GameImporter.FetchInputFieldFromImport(renderingObjectImport.StaticInputField, null);
            }
            else if (renderingObjectImport.Toggle != null)
            {
                return GameImporter.FetchToggleFromImport(renderingObjectImport.Toggle, null);
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
                        return new ShowMiniWindowComand(Editor, commandImport.HasVariable == "true", commandImport.HasSceneRelatedComponent == "true", [.. commandImport.Buttons.Select(FetchEditorButtonFromImport)], MiniWindow.miniWindowType.Vertical);
                    }
                    else
                    {
                        return new ShowMiniWindowComand(Editor, commandImport.HasVariable == "true", commandImport.HasSceneRelatedComponent == "true", [.. commandImport.WindowComponents.Select(FetchEditorComponentFromImport)], MiniWindow.miniWindowType.Vertical);
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
                    if (commandImport.ErrorType == null) throw new Exception("Error type not found!");
                    return new ShowErrorCommand(Editor, commandImport.ErrorMessage, [.. commandImport.WarningButtons.Select(FetchEditorButtonFromImport)]);
                case "DeleteAllComponentsCommand":
                    return new DeleteAllComponentsCommand(Editor);
                default:
                    throw new Exception("Command type not found!");
            }
        }
        //////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////
        public int[] ExportColorData(Color color)
        {
            return [color.R, color.G, color.B, color.A];
        }
        public int[] ExportGradientColor(Color[] gradientColor)
        {
            return [.. gradientColor.SelectMany(ExportColorData)];
        }
        public VariableExim ExportVariableData(Variable variable)
        {
            return new()
            {
                Name = variable.Name,
                Value = variable.Value,
                Type = (int)variable.Type
            };
        }
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
                case DropDown dropDown:
                    return new() { DropDown = ExportDropDownData(dropDown) };
                case Button button:
                    return new() { Button = ExportEditorButtonData(button) };
                default:
                    throw new Exception("Rendering component type not found!");
            }
        }
        public LabelExIm ExportLabelData(Label label)
        {
            return new()
            {
                XPosition = label.XPosition,
                YPosition = label.YPosition,
                Text = label.Text
            };
        }
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
        public DropDownExim ExportDropDownData(DropDown dropDown)
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
        public SpriteExim ExportSpriteData(Sprite sprite)
        {
            return new()
            {
                Path = sprite.Path,
                XPosition = sprite.X,
                YPosition = sprite.Y,
            };
        }
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
                TemplateGame.Component.TextField textField => new()
                {
                    XPosition = block.XPosition,
                    YPosition = block.YPosition,
                    ID = block.ID,
                    TextField = ExportTextFieldData(textField)
                },
                TemplateGame.Component.Button button => new()
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
        public BlockExim ExportStaticBlockData(Block block)
        {
            IPermanentRenderingObject permanentRenderingObject = ((Block)block.Component).Component;
            return permanentRenderingObject switch
            {
                Sprite sprite => new()
                {
                    XPosition = block.XPosition,
                    YPosition = block.YPosition,
                    ID = block.ID,
                    Sprite = ExportSpriteData(sprite)
                },
                TemplateGame.Component.TextField textField => new()
                {
                    XPosition = block.XPosition,
                    YPosition = block.YPosition,
                    ID = block.ID,
                    TextField = ExportTextFieldData(textField)
                },
                TemplateGame.Component.Button button => new()
                {
                    XPosition = block.XPosition,
                    YPosition = block.YPosition,
                    ID = block.ID,
                    Button = ExportStaticButtonData(button)
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
                }
            };
        }
        public TextFieldExim ExportTextFieldData(TemplateGame.Component.TextField textField)
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
        public ButtonComponentExIm ExportButtonData(TemplateGame.Component.Button button)
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
                Action = ExportEventData(button.Action),
            };
        }
        public ButtonComponentExIm ExportStaticButtonData(TemplateGame.Component.Button button)
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
                ButtonAction = ExportEventData(inputField.Button.Action),
            };
        }
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
        public DropBoxOptionExim ExportDropBoxOptionData(TemplateGame.Component.Button dropBoxOption)
        {
            return new()
            {
                Text = dropBoxOption.Text,
                Action = BuildTimelineIndependentAction(dropBoxOption.Action)
            };
        }
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
        public EditorExIm ExportEditorData(Editor editor)
        {
            return new()
            {
                ID = editor.IDGenerator.CurrentID(),
                ProjectName = Regex.Replace(editor.ProjectName, @"[^a-zA-Z0-9\s]", ""),
                ProjectPath = editor.SaveFilePath,
                WindowWidth = 800,
                WindowHeight = 800,
                ToolBar = ExportGroupData(editor.Toolbar),
                Scenes = [.. editor.SceneList.Select(ExportEditorSceneData)],
                Variables = [.. editor.GameVariables.Select(ExportVariableData)]
            };
        }
        public GroupExIm ExportGroupData(Group group)
        {
            ButtonExIm[] buttons = [.. group.ComponentList.Select(x => x as Button).Select(ExportEditorButtonData)];
            ComponentExIm[] components = null;
            if (buttons.Length < group.ComponentList.Count())
            {
                components = [.. group.ComponentList.Select(x => x as Component).Select(ExportComponentData)];
            }
            buttons ??= [];
            components ??= [];
            return new()
            {
                XPosition = group.XPosition,
                YPosition = group.YPosition,
                Width = group.Width,
                Height = group.Height,
                BorderWidth = group.BorderWidth,
                Buttons = [.. buttons],
                Components = [.. components]
            };
        }
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
        public ComponentExIm ExportComponentData(Component component)
        {
            return new()
            {
                ID = component.ID,
                Name = component.Name,
                XPosition = component.XPosition,
                YPosition = component.YPosition,
                RenderingObject = ExportRenderingObjectData(component.RenderingObject)
            };
        }
        public SceneExIm ExportEditorSceneData(Scene scene)
        {
            return scene.BackgroundOption switch
            {
                TemplateGame.Component.Scene.BackgroundOption.SolidColor => new()
                {
                    ID = scene.ID,
                    Name = scene.Name,
                    Background = scene.BackgroundOption.ToString(),
                    SolidColor = ExportColorData(scene.BackgroundColor.Value),
                    Components = [.. scene.ComponentList.Select(x => x as Component).Select(ExportComponentData)],
                    GroupList = [.. scene.ComponentGroupList.Select(ExportGroupData)],
                    Timeline = ExportTimelineData(scene.Timeline)
                },
                TemplateGame.Component.Scene.BackgroundOption.GradientHorizontal => new()
                {
                    ID = scene.ID,
                    Name = scene.Name,
                    Background = scene.BackgroundOption.ToString(),
                    GradientColor = ExportGradientColor(scene.BackgroundGradientColor),
                    Components = [.. scene.ComponentList.Select(x => x as Component).Select(ExportComponentData)],
                    GroupList = [.. scene.ComponentGroupList.Select(ExportGroupData)],
                    Timeline = ExportTimelineData(scene.Timeline)
                },
                TemplateGame.Component.Scene.BackgroundOption.GradientVertical => new()
                {
                    ID = scene.ID,
                    Name = scene.Name,
                    Background = scene.BackgroundOption.ToString(),
                    GradientColor = ExportGradientColor(scene.BackgroundGradientColor),
                    Components = [.. scene.ComponentList.Select(x => x as Component).Select(ExportComponentData)],
                    GroupList = [.. scene.ComponentGroupList.Select(ExportGroupData)],
                    Timeline = ExportTimelineData(scene.Timeline)
                },
                TemplateGame.Component.Scene.BackgroundOption.Image => new()
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
        public RenderingObjectExIm ExportRenderingObjectData(IPermanentRenderingObject permanentRenderingObject)
        {
            return permanentRenderingObject switch
            {
                Sprite sprite => new() { Sprite = ExportSpriteData(sprite) },
                TemplateGame.Component.TextField textField => new() { TextField = ExportTextFieldData(textField) },
                TextBox textBox => new() { Textbox = ExportTextBoxData(textBox) },
                Block block => new() { Block = ExportBlockData(block) },
                TemplateGame.Component.Button button => new() { Button = ExportButtonData(button) },
                InputField inputField => new() { InputField = ExportInputFieldData(inputField) },
                DropBox dropBox => new() { DropBox = ExportDropBoxData(dropBox) },
                Menu menu => new() { Menu = ExportMenuData(menu) },
                Slider slider => new() { Slider = ExportSliderData(slider) },
                Toggle toggle => new() { Toggle = ExportToggleData(toggle) },
                _ => throw new Exception("Rendering object type not found!"),
            };
        }
        public TimelineExIm ExportTimelineData(Timeline timeline)
        {
            return new()
            {
                Actions = [.. timeline.Actions.Select(ExportEventData)]
            };
        }
        public ActionExim ExportEventData(IAction actionData)
        {
            return actionData switch
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
                SetVariableValueAction setVaraiableValueAction => new()
                {
                    Type = "SetVaraiableValueAction",
                    VariableName = setVaraiableValueAction.VariableName
                },
                SwitchStaticMenuAction switchStaticMenuAction => new()
                {
                    Type = "SwitchStaticMenuAction"
                },
                _ => throw new Exception("Event type is either not Timeline Dependent or is not found!"),
            };
        }
        //////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Export the color data.
        /// </summary>
        /// <param name="scene"></param>
        /// <returns></returns>
        public SceneExim[] BuildScenesData(Scene[] scene)
        {
            return [.. scene.Select(BuildSceneData)];
        }
        /// <summary>
        /// Export the build scene data.
        /// </summary>
        /// <param name="scene"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public SceneExim BuildSceneData(Scene scene)
        {
            return scene.BackgroundOption switch
            {
                TemplateGame.Component.Scene.BackgroundOption.SolidColor => new()
                {
                    ID = scene.ID,
                    Name = scene.Name,
                    Background = scene.BackgroundOption.ToString(),
                    SolidColor = ExportColorData(scene.BackgroundColor.Value),
                    ActionList = BuildTimelineData(scene.Timeline)
                },
                TemplateGame.Component.Scene.BackgroundOption.GradientHorizontal => new()
                {
                    ID = scene.ID,
                    Name = scene.Name,
                    Background = scene.BackgroundOption.ToString(),
                    GradientColor = ExportGradientColor(scene.BackgroundGradientColor),
                    ActionList = BuildTimelineData(scene.Timeline)
                },
                TemplateGame.Component.Scene.BackgroundOption.GradientVertical => new()
                {
                    ID = scene.ID,
                    Name = scene.Name,
                    Background = scene.BackgroundOption.ToString(),
                    GradientColor = ExportGradientColor(scene.BackgroundGradientColor),
                    ActionList = BuildTimelineData(scene.Timeline)
                },
                TemplateGame.Component.Scene.BackgroundOption.Image => new()
                {
                    ID = scene.ID,
                    Name = scene.Name,
                    Background = scene.BackgroundOption.ToString(),
                    ImageTexture = scene.BackgroundImage.ToString(),
                    ActionList = BuildTimelineData(scene.Timeline)
                },
                _ => throw new Exception("Background type not found!"),
            };
        }
        /// <summary>
        /// Export the timeline build data.
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
        /// Export the variable data to build version.
        /// </summary>
        /// <param name="variables"></param>
        /// <returns></returns>
        public VariableExim[] BuildVariablesData(Variable[] variables)
        {
            return [.. variables.Select(BuildVariableData)];
        }
        /// <summary>
        /// Export the variable datum to build version
        /// </summary>
        /// <param name="variable"></param>
        /// <returns></returns>
        public VariableExim BuildVariableData(Variable variable)
        {
            return ExportVariableData(variable);
        }
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
                SetVariableValueAction setVaraiableValueAction => new()
                {
                    Type = "SetVaraiableValueAction",
                    VariableName = setVaraiableValueAction.VariableName
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