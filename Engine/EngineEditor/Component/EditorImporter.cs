using System.Text.Json;
using Raylib_cs;
using VisualNovelEngine.Engine.PortData;
using ICommand = VisualNovelEngine.Engine.EngineEditor.Interface.ICommand;
using VisualNovelEngine.Engine.EngineEditor.Component.Command;
using static VisualNovelEngine.Engine.EngineEditor.Component.Command.CreateComponentCommand;

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
                [.. toolBarImport.ITool.Select(FetchButtonFromImport)]
            );
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
            return commandImport.Type switch
            {
                "EmptyCommand" => new EmptyCommand(),
                "CreateComponentCommand" => new CreateComponentCommand(Editor, (RenderingObjectType)commandImport.RenderingObjectType),
                _ => throw new Exception("Command type not found!"),
            };
        }
    }
}