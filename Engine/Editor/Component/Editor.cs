using VisualNovelEngine.Engine.Editor.Interface;
using Raylib_cs;
using VisualNovelEngine.Engine.Game.Component;
using System.Text.Json;
using System.Text.RegularExpressions;
using EngineEditor.Component;
using System.Numerics;
using VisualNovelEngine.Engine.Editor.Component.Command;
using System.Reflection.Emit;
using Timer = VisualNovelEngine.Engine.Game.Component.Timer;

namespace VisualNovelEngine.Engine.Editor.Component
{
    public class Editor : IEditor
    {
        /// <summary>
        /// The engine of the editor.
        /// </summary>
        internal Engine.Component.Engine Engine { get; set; }
        /// <summary>
        /// The path to the data folder.
        /// </summary>
        internal string FolderPath { get; set; }
        /// <summary>
        /// The path to the editor configuration file.
        /// </summary>
        internal string EditorConfigPath { get; set; }
        /// <summary>
        /// The path to the save file.
        /// </summary>
        internal string SaveFilePath { get; set; }
        /// <summary>
        /// Build path of the project
        /// </summary>
        internal string BuildPath { get; set; }
        /// <summary>
        /// Instance of a Game
        /// </summary>
        internal Game.Component.Game Game { get; set; }
        /// <summary>
        /// The path to the game build file.
        /// </summary>
        internal List<Variable> GameVariables { get; set; } = [];
        /// <summary>
        /// The extent which the screen moves
        /// </summary>
        internal const int MoveOffset = 150;
        /// <summary>
        /// The offset, which the mouse requires to move
        /// </summary>
        internal const int MoveMouseOffset = 50;
        /// <summary>
        /// The extent which the mouse moves
        /// </summary>
        internal int MouseXMoveExtent = 0;
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
        /// The width of the mini window.
        /// </summary>
        internal int MiniWindowWidth { get; set; }
        /// <summary>
        /// The height of the mini window.
        /// </summary>
        internal int MiniWindowHeight { get; set; }
        /// <summary>
        /// The border width of the mini window.
        /// </summary>
        internal int MiniWindowBorderWidth { get; set; }
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
        /// Screen width
        /// </summary>
        internal int ScreenWidth { get; set; }
        /// <summary>
        /// Screen height
        /// </summary>
        internal int ScreenHeight { get; set; }
        /// <summary>
        /// The ID generator.
        /// </summary>
        internal IDGenerator IDGenerator { get; set; }
        /// <summary>
        /// The editor importer object, which is used to editor related data into the project.
        /// </summary>
        internal EditorEXIMManager EditorEXIMManager { get; set; }
        /// <summary>
        /// The list of scenes in the editor.
        /// </summary>
        internal List<Scene> SceneList { get; set; } = [];
        /// <summary>
        /// The active scene in the editor.
        /// </summary>
        internal Scene ActiveScene { get; set; }
        /// <summary>
        /// The list of buttons in the editor.
        /// </summary>
        internal List<IButton> ButtonList { get; set; } = [];
        /// <summary>
        /// The scene bar in the editor
        /// </summary>
        internal MiniWindow SceneBar { get; set; }
        /// <summary>
        /// The list of mini windows in the editor.
        /// </summary>
        internal List<MiniWindow> MiniWindow { get; set; } = [];
        /// <summary>
        /// Instance of the dynamic error window.
        /// </summary>
        internal ErrorWindow? ErrorWindow { get; set; } = null;
        /// <summary>
        /// The command to show the exit error window.
        /// </summary>
        private ShowErrorCommand ShowExitErrorCommand { get; set; }
        /// <summary>
        /// The timer for the mouse move.
        /// </summary>
        internal Timer MouseMoveTimer { get; set; }
        /// <summary>
        /// Represents if the editor is busy.
        /// </summary>
        public bool Busy => ActiveScene.InspectorWindow?.Active is true;
        /// <summary>
        /// Create a new editor.
        /// </summary>
        /// <param name="engine"></param>
        public Editor(Engine.Component.Engine engine, string title, string projectPath)
        {
            Engine = engine;
            ProjectName = title;
            FolderPath = projectPath;
            EditorConfigPath = projectPath + "EditorConfig.json";
            SaveFilePath = @"../../../Engine/Data/PlaceholderEditor.json";
            File.Copy(@"../../../Engine/Data/EditorConfig.json", EditorConfigPath, true);
            File.Copy(@"../../../Engine/Data/PlaceholderGameBuild.json", $"{projectPath}PlaceholderGameBuild.json", true);
            BuildPath = $"{FolderPath}{title}Build.json";
            Engine.SetWindowTitle($"Editor - {title}");
            // Load null data to Game.
            Game = new(Engine, @"../../../Engine/Data/PlaceholderGameBuild.json");
            // instance of the editor importer
            EditorEXIMManager = new(this, EditorConfigPath, SaveFilePath);
            EditorConfigImport();
            ProjectName = title;
            //
            MouseMoveTimer = new(0.5f);
            //
            ShowExitErrorCommand = new(this, "Exit?", [new Button(this, 0, 0, "Quit", ButtonWidth, ButtonHeight, ButtonBorderWidth, BaseColor, BorderColor, HoverColor, new ExitWindowCommand(this), Button.ButtonType.Trigger)]);
            // Create the scene
            List<Button> SceneButtonList = [];
            foreach (Scene scene in SceneList)
            {
                var button = new Button(this, 0, 0, scene.Name, ButtonWidth, ButtonHeight, ButtonBorderWidth, BaseColor, BorderColor, HoverColor, new ChangeSceneCommand(this), Button.ButtonType.Trigger);
                ChangeSceneCommand changeSceneCommand = (ChangeSceneCommand)button.Command;
                changeSceneCommand.SceneButton = button;
                if (scene == ActiveScene) SceneButtonList.Insert(0, button);
                else SceneButtonList.Add(button);
            }
            SceneButtonList.Add(new Button(this, 0, 0, "Add", ButtonWidth, ButtonHeight, ButtonBorderWidth, BaseColor, BorderColor, HoverColor, new CreateNewSceneCommand(this), Button.ButtonType.Trigger));
            SceneBar = new MiniWindow(this, false, false, false, 0, 0, Raylib.GetScreenWidth(), 100, ComponentBorderWidth, BaseColor, BorderColor, MiniWindowType.Horizontal, [.. SceneButtonList]);
            //
            SaveFilePath = $"{FolderPath}{ProjectName}.json".Replace(" ", string.Empty);
        }
        /// <summary>
        /// Create a new Editor, and load existing project.
        /// </summary>
        /// <param name="engine"></param>
        /// <param name="projectPath"></param>
        public Editor(Engine.Component.Engine engine, string projectPath)
        {
            Engine = engine;
            FolderPath = projectPath[..(projectPath.LastIndexOf('/') + 1)];
            ProjectName = projectPath.Split('/').Last().Split('.').First();
            EditorConfigPath = $"{FolderPath}EditorConfig.json";
            SaveFilePath = projectPath;
            BuildPath = $"{FolderPath}{ProjectName}Build.json";
            // Load null data to Game.
            Game = new(Engine, FolderPath + "PlaceholderGameBuild.json");
            // instance of the editor importer
            EditorEXIMManager = new(this, EditorConfigPath, SaveFilePath);
            EditorConfigImport();
            //
            Engine.SetWindowTitle($"Editor - {ProjectName}");
            //
            MouseMoveTimer = new(0.5f);
            //
            ShowExitErrorCommand = new(this, "Exit?", [new Button(this, 0, 0, "Quit", ButtonWidth, ButtonHeight, ButtonBorderWidth, BaseColor, BorderColor, HoverColor, new ExitWindowCommand(this), Button.ButtonType.Trigger)]);
            // Create the scene
            List<Button> SceneButtonList = [];
            foreach (Scene scene in SceneList)
            {
                var button = new Button(this, 0, 0, scene.Name, ButtonWidth, ButtonHeight, ButtonBorderWidth, BaseColor, BorderColor, HoverColor, new ChangeSceneCommand(this), Button.ButtonType.Trigger);
                ChangeSceneCommand changeSceneCommand = (ChangeSceneCommand)button.Command;
                changeSceneCommand.SceneButton = button;
                if (scene == ActiveScene) SceneButtonList.Insert(0, button);
                else SceneButtonList.Add(button);
            }
            SceneButtonList.Add(new Button(this, 0, 0, "Add", ButtonWidth, ButtonHeight, ButtonBorderWidth, BaseColor, BorderColor, HoverColor, new CreateNewSceneCommand(this), Button.ButtonType.Trigger));
            SceneBar = new MiniWindow(this, false, false, false, 0, 0, Raylib.GetScreenWidth(), 100, ComponentBorderWidth, BaseColor, BorderColor, MiniWindowType.Horizontal, [.. SceneButtonList]);
        }
        /// <summary>
        /// Imports the editor configuration from external file.
        /// </summary>
        private void EditorConfigImport()
        {
            IDGenerator = new(EditorEXIMManager.EditorExIm.ID);
            //
            ComponentWidth = EditorEXIMManager.EditorPreferencesImport.ComponentWidth;
            ComponentHeight = EditorEXIMManager.EditorPreferencesImport.ComponentHeight;
            ComponentBorderWidth = EditorEXIMManager.EditorPreferencesImport.ComponentBorderWidth;
            ComponentEnabledCharacterCount = EditorEXIMManager.EditorPreferencesImport.ComponentEnabledCharacterCount;
            //
            ButtonWidth = EditorEXIMManager.EditorPreferencesImport.ButtonWidth;
            ButtonHeight = EditorEXIMManager.EditorPreferencesImport.ButtonHeight;
            ButtonBorderWidth = EditorEXIMManager.EditorPreferencesImport.ButtonBorderWidth;
            //
            SmallButtonWidth = EditorEXIMManager.EditorPreferencesImport.SmallButtonWidth;
            SmallButtonHeight = EditorEXIMManager.EditorPreferencesImport.SmallButtonHeight;
            SmallButtonBorderWidth = EditorEXIMManager.EditorPreferencesImport.SmallButtonBorderWidth;
            //
            SideButtonWidth = EditorEXIMManager.EditorPreferencesImport.SideButtonWidth;
            SideButtonHeight = EditorEXIMManager.EditorPreferencesImport.SideButtonHeight;
            SideButtonBorderWidth = EditorEXIMManager.EditorPreferencesImport.SideButtonBorderWidth;
            //
            InspectorWindowWidth = EditorEXIMManager.EditorPreferencesImport.InspectorWidth;
            InspectorWindowHeight = EditorEXIMManager.EditorPreferencesImport.InspectorHeight;
            InspectorWindowBorderWidth = EditorEXIMManager.EditorPreferencesImport.InspectorBorderWidth;
            //
            MiniWindowWidth = EditorEXIMManager.EditorPreferencesImport.MiniWindowWidth;
            MiniWindowHeight = EditorEXIMManager.EditorPreferencesImport.MiniWindowHeight;
            MiniWindowBorderWidth = EditorEXIMManager.EditorPreferencesImport.MiniWindowBorderWidth;
            //
            CloseButtonBaseColor = EditorEXIMManager.FetchColorFromImport(EditorEXIMManager.EditorPreferencesImport.CloseButtonBaseColor);
            CloseButtonBorderColor = EditorEXIMManager.FetchColorFromImport(EditorEXIMManager.EditorPreferencesImport.CloseButtonBorderColor);
            CloseButtonHoverColor = EditorEXIMManager.FetchColorFromImport(EditorEXIMManager.EditorPreferencesImport.CloseButtonHoverColor);
            //
            InspectorButtonBaseColor = EditorEXIMManager.FetchColorFromImport(EditorEXIMManager.EditorPreferencesImport.InspectorButtonBaseColor);
            InspectorButtonBorderColor = EditorEXIMManager.FetchColorFromImport(EditorEXIMManager.EditorPreferencesImport.InspectorButtonBorderColor);
            InspectorButtonHoverColor = EditorEXIMManager.FetchColorFromImport(EditorEXIMManager.EditorPreferencesImport.InspectorButtonHoverColor);
            //
            BaseColor = EditorEXIMManager.FetchColorFromImport(EditorEXIMManager.EditorPreferencesImport.BaseColor);
            BorderColor = EditorEXIMManager.FetchColorFromImport(EditorEXIMManager.EditorPreferencesImport.BorderColor);
            TextColor = EditorEXIMManager.FetchColorFromImport(EditorEXIMManager.EditorPreferencesImport.TextColor);
            HoverColor = EditorEXIMManager.FetchColorFromImport(EditorEXIMManager.EditorPreferencesImport.HoverColor);
            EditorColor = EditorEXIMManager.FetchColorFromImport(EditorEXIMManager.EditorPreferencesImport.EditorColor);
            //
            ProjectName = EditorEXIMManager.EditorExIm.ProjectName;
            ScreenWidth = EditorEXIMManager.EditorExIm.WindowWidth;
            ScreenHeight = EditorEXIMManager.EditorExIm.WindowHeight;
            Toolbar = EditorEXIMManager.FetchToolBarFromImport(EditorEXIMManager.EditorExIm.ToolBar);
            SceneList = [.. EditorEXIMManager.EditorExIm.Scenes.Select(EditorEXIMManager.FetchEditorSceneFromImport)];
            ActiveScene = SceneList[0];
        }
        /// <summary>
        /// Builds the editor's data into a playable game data.
        /// Creates timeline and variable export suitable for playing the game
        /// </summary>
        public void Build()
        {
            var GameBuildData = JsonSerializer.Serialize(EditorEXIMManager.BuildGameData(this), new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(BuildPath, GameBuildData);
        }
        /// <summary>
        /// Saves the editor data into a file.
        /// </summary>
        public void Save()
        {
            var editorJson = JsonSerializer.Serialize(EditorEXIMManager.ExportEditorData(this), new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(SaveFilePath, editorJson);
        }
        /// <summary>
        /// Updates the editor every frame.
        /// </summary>
        public void Update()
        {
            Raylib.ClearBackground(EditorColor);
            SceneBar.Show();
            Toolbar.Update();
            ActiveScene.Update();
            ExitWindow();
            MoveCamera();
            for (int i = 0; i < MiniWindow.Count; i++) MiniWindow[i].Show();
            ErrorWindow?.Show();
        }
        /// <summary>
        /// Exits the editor window.
        /// </summary>
        private void ExitWindow()
        {
            if (Raylib.WindowShouldClose())
            {
                ShowExitErrorCommand.Execute();
            }
        }
        /// <summary>
        /// Moves the camera on the X axis.
        /// </summary>
        private void MoveCamera()
        {
            //Left side 
            Raylib.DrawRectangleLines(0, SceneBar.Height + Toolbar.Height, MoveMouseOffset, Raylib.GetScreenHeight() - ActiveScene.Timeline.Height - SceneBar.Height - Toolbar.Height, Color.Black);
            //right side
            Raylib.DrawRectangleLines(Raylib.GetScreenWidth() - MoveMouseOffset, SceneBar.Height + Toolbar.Height, MoveMouseOffset, Raylib.GetScreenHeight() - ActiveScene.Timeline.Height - SceneBar.Height - Toolbar.Height, Color.Black);

            if (MouseMoveTimer.OnCooldown())
            {
                MouseMoveTimer.DecreaseTimer();
                return;
            }
            if (Raylib.GetMouseY() <= SceneBar.Height + Toolbar.Height || Raylib.GetMouseY() >= Raylib.GetScreenHeight() - ActiveScene.Timeline.Height)
            {
                MouseMoveTimer.Reset();
                return;
            }
            if (Raylib.GetMouseX() >= Raylib.GetScreenWidth() - MoveMouseOffset)
            {
                foreach (Component item in ActiveScene.ComponentList)
                {
                    item.XPosition -= MoveOffset;
                }
                foreach (Group item in ActiveScene.ComponentGroupList)
                {
                    item.XPosition -= MoveOffset;
                }
                MouseXMoveExtent++;
            }
            //Move leftway
            else if (Raylib.GetMouseX() <= MoveMouseOffset)
            {
                foreach (Component item in ActiveScene.ComponentList)
                {
                    item.XPosition += MoveOffset;
                }
                foreach (Group item in ActiveScene.ComponentGroupList)
                {
                    item.XPosition += MoveOffset;
                }
                MouseXMoveExtent--;
            }
            MouseMoveTimer.Reset();
        }
        /// <summary>
        /// Disables all components in the editor.
        /// </summary>
        internal void DisableComponents()
        {
            foreach (Button button in Toolbar.ComponentList)
            {
                button.IsLocked = true;
            }
            foreach (Button button in SceneBar.ButtonComponentList)
            {
                button.IsLocked = true;
            }
            foreach (Component component in ActiveScene.ComponentList)
            {
                component.IsLocked = true;
            }
            ActiveScene.Timeline.AddGeneralAction.IsLocked = true;
            ActiveScene.Timeline.RemoveActionsButton.IsLocked = true;
        }
        /// <summary>
        /// Enables all components in the editor.
        /// </summary>
        internal void EnableComponents()
        {
            foreach (Button button in Toolbar.ComponentList)
            {
                button.IsLocked = false;
            }
            foreach (Button button in SceneBar.ButtonComponentList)
            {
                button.IsLocked = false;
            }
            foreach (Component component in ActiveScene.ComponentList)
            {
                component.IsLocked = false;
            }
            ActiveScene.Timeline.AddGeneralAction.IsLocked = false;
            ActiveScene.Timeline.RemoveActionsButton.IsLocked = false;
        }
        /// <summary>
        /// Generates a unique number ID.
        /// </summary>
        /// <returns></returns>
        internal int GenerateID() => IDGenerator.GenerateID();
    }
}