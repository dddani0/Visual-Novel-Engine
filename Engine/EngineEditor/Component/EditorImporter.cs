using System.Text.Json;
using Raylib_cs;
using ICommand = VisualNovelEngine.Engine.EngineEditor.Interface.ICommand;
using VisualNovelEngine.Engine.EngineEditor.Component.Command;
using static VisualNovelEngine.Engine.EngineEditor.Component.Command.CreateComponentCommand;
using VisualNovelEngine.Engine.EngineEditor.Interface;
using VisualNovelEngine.Engine.PortData;
using TemplateGame.Interface;
using TemplateGame.Component;
using System.Text.RegularExpressions;
using TemplateGame.Component.Action.TimelineDependent;
using TemplateGame.Component.Action;
using EngineEditor.Component;

namespace VisualNovelEngine.Engine.EngineEditor.Component
{
    public class EditorImporter
    {
        Editor Editor { get; set; }
        internal EditorConfigurationImport EditorButtonConfigurationImport { get; set; }
        internal EditorEXIM EditorImport { get; set; }
        public EditorImporter(Editor editor, string editorConfigPath, string editorDataPath)
        {
            Editor = editor;
            //Editor config
            string rawFile = File.ReadAllText(editorConfigPath) ?? throw new Exception("Editor preference file not found!");
            EditorButtonConfigurationImport = JsonSerializer.Deserialize<EditorConfigurationImport>(rawFile) ?? throw new Exception("Editor preference file not found!");
            //Editor data
            rawFile = File.ReadAllText(editorDataPath) ?? throw new Exception("Editor data file not found!");
            EditorImport = JsonSerializer.Deserialize<EditorEXIM>(rawFile) ?? throw new Exception("Editor data file not found!");
        }
        /// <summary>
        /// Fetches a color from an import object.
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public Color FetchColorFromImport(int[] color) => new()
        {
            R = (byte)color[0],
            G = (byte)color[1],
            B = (byte)color[2],
            A = (byte)color[3]
        };
        public Scene FetchSceneFromImport(SceneEXIM sceneImport)
        {
            return new Scene(Editor,
            sceneImport.Name,
             [.. sceneImport.Components.Select(FetchComponentFromImport)],
             [.. sceneImport.GroupList.Select(FetchGroupFromImport)]);
        }
        public IEvent FetchEventFromImport(EventEXIM eventImport)
        {
            return eventImport.Type switch
            {
                "EmptyAction" => new EmptyAction(null),
                _ => throw new Exception("Event type not found!"),
            };
        }
        public Group FetchToolBarFromImport(GroupEXIM toolBarImport)
        {
            return new Group(
                Editor,
                toolBarImport.XPosition,
                toolBarImport.YPosition,
                toolBarImport.Width,
                toolBarImport.Height,
                toolBarImport.BorderWidth,
                FetchColorFromImport(EditorButtonConfigurationImport.BaseColor),
                FetchColorFromImport(EditorButtonConfigurationImport.BorderColor),
                FetchColorFromImport(EditorButtonConfigurationImport.HoverColor),
                GroupType.SolidColor,
                4,
                [.. toolBarImport.Buttons.Select(FetchButtonFromImport)]
            );
        }
        public Group FetchGroupFromImport(GroupEXIM groupImport)
        {
            return new Group(
                Editor,
                groupImport.XPosition,
                groupImport.YPosition,
                groupImport.Width,
                groupImport.Height,
                groupImport.BorderWidth,
                FetchColorFromImport(EditorButtonConfigurationImport.BaseColor),
                FetchColorFromImport(EditorButtonConfigurationImport.BorderColor),
                FetchColorFromImport(EditorButtonConfigurationImport.HoverColor),
                GroupType.SolidColor,
                4,
                [.. groupImport.Buttons.Select(FetchButtonFromImport)]
            );
        }
        Component FetchComponentFromImport(ComponentEXIM componentImport)
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
        //DANI IDE SÜSS!
        public IPermanentRenderingObject FetchRenderingObjectFromImport(RenderingObjectEXIM renderingObjectImport)
        {
            return renderingObjectImport.Type switch
            {
                "Sprite" => new Sprite(renderingObjectImport.Path),
                _ => throw new Exception("Rendering object type not found!"),
            };
        }
        public Button FetchButtonFromImport(ButtonEXIM buttonImport)
        {
            return new Button(Editor,
                buttonImport.XPosition,
                buttonImport.YPosition,
                buttonImport.Text,
                Editor.ComponentWidth,
                Editor.ComponentHeight,
                Editor.ComponentBorderWidth,
                FetchColorFromImport(EditorButtonConfigurationImport.BaseColor),
                FetchColorFromImport(EditorButtonConfigurationImport.BorderColor),
                FetchColorFromImport(EditorButtonConfigurationImport.HoverColor),
                FetchCommandFromImport(buttonImport.Command),
                (Button.ButtonType)buttonImport.Type
            );
        }
        public ICommand FetchCommandFromImport(CommandImport commandImport)
        {
            switch (commandImport.Type)
            {
                case "EmptyCommand":
                    return new EmptyCommand();
                case "CreateComponentCommand":
                    return new CreateComponentCommand(Editor, (RenderingObjectType)commandImport.RenderingObjectType);
                case "ShowSideWindowCommand":
                    if (commandImport.Buttons == null) throw new Exception("Buttons not found!");
                    if (commandImport.DependentButton == null) throw new Exception("Dependent button name not found!");
                    string parentButtonName = commandImport.DependentButton;
                    Button[] buttons = [.. commandImport.Buttons.Select(FetchButtonFromImport)];
                    foreach (Button button in buttons)
                    {
                        button.Width = EditorButtonConfigurationImport.SideButtonWidth;
                        button.Height = EditorButtonConfigurationImport.SideButtonHeight;
                        button.BorderWidth = EditorButtonConfigurationImport.SideButtonBorderWidth;
                    }
                    return new ShowSideWindowCommand(Editor, parentButtonName, buttons);
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
                    return new ShowErrorCommand(Editor, (ErrorWindow.ErrorType)commandImport.ErrorType, commandImport.ErrorMessage, [.. commandImport.WarningButtons.Select(FetchButtonFromImport)]);
                case "DeleteAllComponentsCommand":
                    return new DeleteAllComponentsCommand(Editor);
                default:
                    throw new Exception("Command type not found!");
            }
        }
        public CommandImport ExportCommandData(ICommand command)
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
                case ShowSideWindowCommand showSideWindowCommand:
                    return new()
                    {
                        Type = "ShowSideWindowCommand",
                        DependentButton = showSideWindowCommand.DependentButtonName,
                        Buttons = [.. showSideWindowCommand.Buttons.Select(ExportButtonData)]
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
                case BuildProjectCommand buildProjectCommand:
                    return new()
                    {
                        Type = "BuildProjectCommand"
                    };
                default:
                    throw new Exception("Command type not found!");
            }
        }
        public EditorEXIM ExportEditorData(Editor editor)
        {
            return new()
            {
                ID = editor.IDGenerator.CurrentID(),
                ProjectName = Regex.Replace(editor.ProjectName, @"[^a-zA-Z0-9\s]", ""),
                WindowWidth = 800,
                WindowHeight = 800,
                ToolBar = ExportGroupData(editor.Toolbar),
                Scenes = [.. editor.SceneList.Select(ExportSceneData)],
            };
        }
        public GroupEXIM ExportGroupData(Group group)
        {
            ButtonEXIM[] buttons = [.. group.ComponentList.Select(x => x as Button).Select(ExportButtonData)];
            ComponentEXIM[] components = null;
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
        public ButtonEXIM ExportButtonData(Button button)
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
        public ComponentEXIM ExportComponentData(Component component)
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
        public SceneEXIM ExportSceneData(Scene scene)
        {
            return new()
            {
                ID = scene.ID,
                Name = scene.Name,
                Components = [.. scene.ComponentList.Select(x => x as Component).Select(ExportComponentData)],
                GroupList = [.. scene.ComponentGroupList.Select(ExportGroupData)],
                Timeline = ExportTimelineData(scene.Timeline)
            };
        }
        public RenderingObjectEXIM ExportRenderingObjectData(IPermanentRenderingObject permanentRenderingObject)
        {
            switch (permanentRenderingObject)
            {
                case Sprite sprite:
                    return new()
                    {
                        Type = "Sprite",
                        Path = sprite.Path
                    };
                default:
                    throw new Exception("Rendering object type not found!");
            }
        }

        public TimelineEXIM ExportTimelineData(Timeline timeline)
        {
            return new()
            {
                Events = [.. timeline.Events.Select(ExportEventData)]
            };
        }

        public EventEXIM ExportEventData(IEvent eventData)
        {
            return eventData switch
            {
                EmptyAction emptyAction => new() { Type = "EmptyAction" },
                CreateMenuAction createMenuAction => new() { Type = "CreateMenuAction" },
                LoadSceneAction loadSceneAction => new() { Type = "LoadSceneAction" },
                NativeLoadSceneAction nativeLoadSceneAction => new() { Type = "NativeLoadSceneAction" },
                AddSpriteAction addSpriteAction => new() { Type = "AddSpriteAction" },
                ChangeSpriteAction changeSpriteAction => new() { Type = "ChangeSpriteAction" },
                CreateVariableAction createVariableAction => new() { Type = "CreateVariableAction" },
                DecrementVariableAction decrementVariableAction => new() { Type = "DecrementVariableAction" },
                IncrementVariableAction incrementVariableAction => new() { Type = "IncrementVariableAction" },
                RemoveSpriteAction removeSpriteAction => new() { Type = "RemoveSpriteAction" },
                SetBoolVariableAction setBoolVariableAction => new() { Type = "SetBoolVariableAction" },
                SetVariableFalseAction setVariableFalseAction => new() { Type = "SetVariableFalseAction" },
                SetVariableTrueAction setVariableTrueAction => new() { Type = "SetVariableTrueAction" },
                TextBoxCreateAction textBoxCreateAction => new() { Type = "TextBoxCreateAction" },
                TintSpriteAction tintSpriteAction => new() { Type = "TintSpriteAction" },
                ToggleVariableAction toggleVariableAction => new() { Type = "ToggleVariableAction" },
                _ => throw new Exception("Event type not found!"),
            };
        }
    }
}