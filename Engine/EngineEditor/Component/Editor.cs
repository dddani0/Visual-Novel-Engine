using VisualNovelEngine.Engine.EngineEditor.Interface;
using Raylib_cs;
using TemplateGame.Component;
using System.Text.Json;
using System.Text.RegularExpressions;
using EngineEditor.Component;
using System.Numerics;
using VisualNovelEngine.Engine.EngineEditor.Component.Command;

namespace VisualNovelEngine.Engine.EngineEditor.Component
{
    public class Editor : IEditor
    {
        /// <summary>
        /// The path to the data folder.
        /// </summary>
        internal const string currentFolderPath = "../../../Engine/Data/";
        /// <summary>
        /// The path to the editor configuration file.
        /// </summary>
        internal const string EditorConfigPath = currentFolderPath + "EditorConfig.json";
        /// <summary>
        /// The path to the editor file.
        /// </summary>
        internal const string RelativeEditorPath = currentFolderPath + "Editor.json";
        /// <summary>
        /// The path to the save file.
        /// </summary>
        internal string SaveFilePath { get; set; } = currentFolderPath;
        /// <summary>
        /// Instance of a Game
        /// </summary>
        internal Game Game { get; set; }
        internal Camera2D Camera { get; set; }
        /// <summary>
        /// The width of the general component type.
        /// </summary>
        internal int ComponentWidth { get; set; }
        /// <summary>
        /// The height of the general component type.
        /// </summary>
        internal int ComponentHeight { get; set; }
        /// <summary>
        /// The border width of the general component type.
        /// </summary>
        internal int ComponentBorderWidth { get; set; }
        /// <summary>
        /// The width of the general button type.
        /// </summary>
        internal int ButtonWidth { get; set; }
        /// <summary>
        /// The height of the general button type.
        /// </summary>
        internal int ButtonHeight { get; set; }
        /// <summary>
        /// The border width of the general button type.
        /// </summary>
        internal int ButtonBorderWidth { get; set; }
        /// <summary>
        /// The width of the small button type.
        /// </summary>
        internal int SmallButtonWidth { get; set; }
        /// <summary>
        /// The height of the small button type.
        /// </summary>
        internal int SmallButtonHeight { get; set; }
        /// <summary>
        /// The border width of the small button type.
        /// </summary>
        internal int SmallButtonBorderWidth { get; set; }
        /// <summary>
        /// The width of the side button type.
        /// </summary>
        internal int SideButtonWidth { get; set; }
        /// <summary>
        /// The height of the side button type.
        /// </summary>
        internal int SideButtonHeight { get; set; }
        /// <summary>
        /// The border width of the side button type.
        /// </summary>
        internal int SideButtonBorderWidth { get; set; }
        /// <summary>
        /// The number of characters that can be displayed in a component.
        /// </summary>
        internal int ComponentEnabledCharacterCount { get; set; }
        /// <summary>
        /// The width of the inspector window.
        /// </summary>
        internal int InspectorWindowWidth { get; set; }
        /// <summary>
        /// The height of the inspector window.
        /// </summary>
        internal int InspectorWindowHeight { get; set; }
        /// <summary>
        /// The border width of the inspector window.
        /// </summary>
        internal int InspectorWindowBorderWidth { get; set; }
        /// <summary>
        /// The base color of the editor.
        /// </summary>
        internal Color BaseColor { get; set; }
        /// <summary>
        /// The border color of the editor.
        /// </summary>
        internal Color BorderColor { get; set; }
        /// <summary>
        /// The general color of the text in the editor.
        /// </summary>
        internal Color TextColor { get; set; }
        /// <summary>
        /// The color of the hover effect in the editor.
        /// </summary>
        internal Color HoverColor { get; set; }
        /// <summary>
        /// The color of the editor.
        /// </summary>
        internal Color EditorColor { get; set; }
        /// <summary>
        /// The base color of the inspector button.
        /// </summary>
        internal Color InspectorButtonBaseColor { get; set; }
        /// <summary>
        /// The border color of the inspector button.
        /// </summary>
        internal Color InspectorButtonBorderColor { get; set; }
        /// <summary>
        /// The hover color of the inspector button.
        /// </summary>
        internal Color InspectorButtonHoverColor { get; set; }
        /// <summary>
        /// The base color of the close button.
        /// </summary>
        internal Color CloseButtonBaseColor { get; set; }
        /// <summary>
        /// The border color of the close button.
        /// </summary>
        internal Color CloseButtonBorderColor { get; set; }
        /// <summary>
        /// The hover color of the close button.
        /// </summary>
        internal Color CloseButtonHoverColor { get; set; }
        /// <summary>
        /// The toolbar of the editor.
        /// </summary>
        internal Group Toolbar { get; set; }
        /// <summary>
        /// The name of the project.
        /// </summary>
        internal string ProjectName { get; set; }
        /// <summary>
        /// The ID generator.
        /// </summary>
        internal IDGenerator IDGenerator { get; set; }
        /// <summary>
        /// The editor importer object, which is used to editor related data into the project.
        /// </summary>
        internal EditorImporter EditorImporter { get; set; }
        /// <summary>
        /// The list of scenes in the editor.
        /// </summary>
        internal List<Scene> SceneList { get; set; } = [];
        /// <summary>
        /// The active scene in the editor.
        /// </summary>
        internal Scene ActiveScene { get; set; }
        internal MiniWindow SceneBar { get; set; }
        /// <summary>
        /// The list of mini windows in the editor.
        /// </summary>
        internal List<MiniWindow> MiniWindow { get; set; } = [];
        /// <summary>
        /// Instance of the error window.
        /// </summary>
        internal ErrorWindow? ErrorWindow { get; set; } = null;
        public bool Busy => ActiveScene.InspectorWindow?.Active is true;
        /// <summary>
        /// The constructor of the editor.
        /// </summary>
        public Editor()
        {
            // instance of the game
            Game = new();
            // instance of the editor importer
            EditorImporter = new(this, EditorConfigPath, RelativeEditorPath);
            EditorConfigImport();
            // Create the sceneb
            List<Button> SceneButtonList = [];
            foreach (Scene scene in SceneList)
            {
                var button = new Button(this, 0, 0, scene.Name, false, ButtonWidth, ButtonHeight, ButtonBorderWidth, BaseColor, BorderColor, HoverColor, new ChangeSceneCommand(this), Button.ButtonType.Trigger);
                ChangeSceneCommand changeSceneCommand = (ChangeSceneCommand)button.Command;
                changeSceneCommand.SceneButton = button;
                if (scene == ActiveScene) SceneButtonList.Insert(0, button);
                else SceneButtonList.Add(button);
            }
            SceneButtonList.Add(new Button(this, 0, 0, "Add", true, ButtonWidth, ButtonHeight, ButtonBorderWidth, BaseColor, BorderColor, HoverColor, new CreateNewSceneCommand(this), Button.ButtonType.Trigger));
            SceneBar = new MiniWindow(this, 0, 0, Raylib.GetScreenWidth(), 100, ComponentBorderWidth, BaseColor, BorderColor, EngineEditor.Component.MiniWindow.miniWindowType.Horizontal, [.. SceneButtonList]);
            //
            SaveFilePath += Regex.Replace(ProjectName, @"[^a-zA-Z0-9\s]", "") + ".json";
            //
            Camera = new Camera2D();
        }
        /// <summary>
        /// Imports the editor configuration from external file.
        /// </summary>
        private void EditorConfigImport()
        {
            IDGenerator = new(EditorImporter.EditorImport.ID);
            //
            ComponentWidth = EditorImporter.EditorButtonConfigurationImport.ComponentWidth;
            ComponentHeight = EditorImporter.EditorButtonConfigurationImport.ComponentHeight;
            ComponentBorderWidth = EditorImporter.EditorButtonConfigurationImport.ComponentBorderWidth;
            ComponentEnabledCharacterCount = EditorImporter.EditorButtonConfigurationImport.ComponentEnabledCharacterCount;
            //
            ButtonWidth = EditorImporter.EditorButtonConfigurationImport.ButtonWidth;
            ButtonHeight = EditorImporter.EditorButtonConfigurationImport.ButtonHeight;
            ButtonBorderWidth = EditorImporter.EditorButtonConfigurationImport.ButtonBorderWidth;
            //
            SmallButtonWidth = EditorImporter.EditorButtonConfigurationImport.SmallButtonWidth;
            SmallButtonHeight = EditorImporter.EditorButtonConfigurationImport.SmallButtonHeight;
            SmallButtonBorderWidth = EditorImporter.EditorButtonConfigurationImport.SmallButtonBorderWidth;
            //
            SideButtonWidth = EditorImporter.EditorButtonConfigurationImport.SideButtonWidth;
            SideButtonHeight = EditorImporter.EditorButtonConfigurationImport.SideButtonHeight;
            SideButtonBorderWidth = EditorImporter.EditorButtonConfigurationImport.SideButtonBorderWidth;
            //
            InspectorWindowWidth = EditorImporter.EditorButtonConfigurationImport.InspectorWidth;
            InspectorWindowHeight = EditorImporter.EditorButtonConfigurationImport.InspectorHeight;
            InspectorWindowBorderWidth = EditorImporter.EditorButtonConfigurationImport.InspectorBorderWidth;
            //
            CloseButtonBaseColor = EditorImporter.FetchColorFromImport(EditorImporter.EditorButtonConfigurationImport.CloseButtonBaseColor);
            CloseButtonBorderColor = EditorImporter.FetchColorFromImport(EditorImporter.EditorButtonConfigurationImport.CloseButtonBorderColor);
            CloseButtonHoverColor = EditorImporter.FetchColorFromImport(EditorImporter.EditorButtonConfigurationImport.CloseButtonHoverColor);
            //
            InspectorButtonBaseColor = EditorImporter.FetchColorFromImport(EditorImporter.EditorButtonConfigurationImport.InspectorButtonBaseColor);
            InspectorButtonBorderColor = EditorImporter.FetchColorFromImport(EditorImporter.EditorButtonConfigurationImport.InspectorButtonBorderColor);
            InspectorButtonHoverColor = EditorImporter.FetchColorFromImport(EditorImporter.EditorButtonConfigurationImport.InspectorButtonHoverColor);
            //
            BaseColor = EditorImporter.FetchColorFromImport(EditorImporter.EditorButtonConfigurationImport.BaseColor);
            BorderColor = EditorImporter.FetchColorFromImport(EditorImporter.EditorButtonConfigurationImport.BorderColor);
            TextColor = EditorImporter.FetchColorFromImport(EditorImporter.EditorButtonConfigurationImport.TextColor);
            HoverColor = EditorImporter.FetchColorFromImport(EditorImporter.EditorButtonConfigurationImport.HoverColor);
            EditorColor = EditorImporter.FetchColorFromImport(EditorImporter.EditorButtonConfigurationImport.EditorColor);
            //
            Toolbar = EditorImporter.FetchToolBarFromImport(EditorImporter.EditorImport.ToolBar);
            ProjectName = EditorImporter.EditorImport.ProjectName;
            SceneList = [.. EditorImporter.EditorImport.Scenes.Select(EditorImporter.FetchSceneFromImport)];
            ActiveScene = SceneList[0];
        }
        /// <summary>
        /// Builds the editor into a playable game data.
        /// </summary>
        public void Build()
        {

        }
        /// <summary>
        /// Saves the editor data into a file.
        /// </summary>
        public void Save()
        {
            var editorJson = JsonSerializer.Serialize(EditorImporter.ExportEditorData(this), new JsonSerializerOptions
            {
                WriteIndented = true
            });
            File.WriteAllText(SaveFilePath, editorJson);
        }
        /// <summary>
        /// Updates the editor every frame.
        /// </summary>
        public void Update()
        {
            Raylib.ClearBackground(EditorColor);
            ActiveScene.Update();
            SceneBar.Show();
            Toolbar.Update();
            DragCamera();
            for (int i = 0; i < MiniWindow.Count; i++)
            {
                MiniWindow[i].Show();
            }
            ErrorWindow?.Show();
        }

        private void DragCamera()
        {
            if (Raylib.IsMouseButtonDown(MouseButton.Right))
            {
                Vector2 delta = Raylib.GetMouseDelta();
                delta = Raymath.Vector2Scale(delta, -1.0f / Camera.Zoom);
                //
                Camera2D camera = Camera;
                camera.Offset = Raymath.Vector2Add(camera.Target, delta);
                //
                Camera = camera;
            }
        }
        /// <summary>
        /// Generates a unique number ID.
        /// </summary>
        /// <returns></returns>
        internal int GenerateID() => IDGenerator.GenerateID();
    }
}