using System.Text.Json;
using Raylib_cs;
using VisualNovelEngine.Engine.PortData;
using ICommand = VisualNovelEngine.Engine.EngineEditor.Interface.ICommand;
using VisualNovelEngine.Engine.EngineEditor.Component.Command;
using static VisualNovelEngine.Engine.EngineEditor.Component.Command.CreateComponentCommand;
using VisualNovelEngine.Engine.EngineEditor.Interface;

namespace VisualNovelEngine.Engine.EngineEditor.Component
{
    public class EditorImporter
    {
        Editor Editor { get; set; }
        internal EditorConfigurationImport EditorButtonConfigurationImport { get; set; }
        internal EditorImport EditorImport { get; set; }
        public EditorImporter(Editor editor, string editorConfigPath, string editorDataPath)
        {
            Editor = editor;
            //Editor config
            string rawFile = File.ReadAllText(editorConfigPath) ?? throw new Exception("Editor preference file not found!");
            EditorButtonConfigurationImport = JsonSerializer.Deserialize<EditorConfigurationImport>(rawFile) ?? throw new Exception("Editor preference file not found!");
            //Editor data
            rawFile = File.ReadAllText(editorDataPath) ?? throw new Exception("Editor data file not found!");
            EditorImport = JsonSerializer.Deserialize<EditorImport>(rawFile) ?? throw new Exception("Editor data file not found!");
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
        public Scene FetchSceneFromImport(SceneImport sceneImport)
        {
            return new Scene(Editor,
            sceneImport.Name,
             [.. sceneImport.Components.Select(FetchComponentFromImport)],
             [.. sceneImport.GroupList.Select(FetchGroupFromImport)]);
        }
        public Group FetchToolBarFromImport(GroupImport toolBarImport)
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

        public Group FetchGroupFromImport(GroupImport groupImport)
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

        Component FetchComponentFromImport(ComponentImport componentImport)
        {
            return null;
        }

        public Button FetchButtonFromImport(ButtonImport buttonImport)
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
                    if (commandImport.ParentButtonName == null) throw new Exception("Parent button name not found!");
                    string parentButtonName = commandImport.ParentButtonName;
                    Button[] buttons = [.. commandImport.Buttons.Select(FetchButtonFromImport)];
                    return new ShowSideWindowCommand(Editor, parentButtonName, buttons);
                case "ShowWindowCommand":
                    //Create an IComponent array with elements from CommandImport.Components and CommandImport.Buttons
                    IComponent[] components = commandImport.Components?.Select(componentImport => FetchComponentFromImport(componentImport)).Cast<IComponent>()
                        .Concat(commandImport.Buttons?.Select(buttonImport => FetchButtonFromImport(buttonImport)).Cast<IComponent>() ?? [])
                        .ToArray() ?? [];
                    return new ShowInspectorCommand(Editor,
                        commandImport.EnabledRowComponentCount,
                        commandImport.XPosition,
                        commandImport.YPosition,
                        components);
                default:
                    throw new Exception("Command type not found!");
            }
        }
    }
}