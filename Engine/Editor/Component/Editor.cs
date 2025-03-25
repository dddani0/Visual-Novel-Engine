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
        internal string CurrentFolderPath { get; set; }
        /// <summary>
        /// The path to the editor configuration file.
        /// </summary>
        internal string EditorConfigPath { get; set; }
        /// <summary>
        /// The path to the editor file.
        /// </summary>
        internal string RelativeEditorPath { get; set; }
        /// <summary>
        /// The path to the save file.
        /// </summary>
        internal string SaveFilePath { get; set; }
        internal string BuildPath { get; set; }
        /// <summary>
        /// Instance of a Game
        /// </summary>
        internal VisualNovelEngine.Engine.Game.Component.Game Game { get; set; }
        internal List<Variable> GameVariables { get; set; } = [];
        /// <summary>
        /// The extent which the screen moves, when decided to
        /// </summary>
        internal const int MoveOffset = 150;
        /// <summary>
        /// The offset, which the mouse requires to move
        /// </summary>
        internal const int MoveMouseOffset = 50;
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
        /// The ID generator.
        /// </summary>
        internal IDGenerator IDGenerator { get; set; }
        /// <summary>
        /// The editor importer object, which is used to editor related data into the project.
        /// </summary>
        internal EditorExImManager EditorImporter { get; set; }
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
            //
            CurrentFolderPath = projectPath;
            //
            EditorConfigPath = @"D:\GithubRepository\Visual-Novel-Engine\Engine\Data\EditorConfig.json";
            //
            RelativeEditorPath = @"D:\GithubRepository\Visual-Novel-Engine\Engine\Data\Editor.json";
            //Save file path
            SaveFilePath = CurrentFolderPath;
            //
            BuildPath = CurrentFolderPath;
            //
            ProjectName = title;
            //
            Engine.ChangeTitle($"Editor - {title}");
            //
            Game = new(Engine.ProjectPath);
            // instance of the editor importer
            EditorImporter = new(this, EditorConfigPath, RelativeEditorPath);
            EditorConfigImport();
            //
            MouseMoveTimer = new(0.5f);
            //
            ShowExitErrorCommand = new(this, "Exit?", [new Button(this, 0, 0, "Quit", true, ButtonWidth, ButtonHeight, ButtonBorderWidth, BaseColor, BorderColor, HoverColor, new ExitWindowCommand(this), Button.ButtonType.Trigger)]);
            // Create the scene
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
            SceneBar = new MiniWindow(this, false, false, false, 0, 0, Raylib.GetScreenWidth(), 100, ComponentBorderWidth, BaseColor, BorderColor, MiniWindowType.Horizontal, [.. SceneButtonList]);
            //
            SaveFilePath += Regex.Replace(ProjectName, @"[^a-zA-Z0-9\s]", "") + ".json";
        }
        /// <summary>
        /// Imports the editor configuration from external file.
        /// </summary>
        private void EditorConfigImport()
        {
            IDGenerator = new(EditorImporter.EditorExIm.ID);
            //
            ComponentWidth = EditorImporter.EditorPreferencesImport.ComponentWidth;
            ComponentHeight = EditorImporter.EditorPreferencesImport.ComponentHeight;
            ComponentBorderWidth = EditorImporter.EditorPreferencesImport.ComponentBorderWidth;
            ComponentEnabledCharacterCount = EditorImporter.EditorPreferencesImport.ComponentEnabledCharacterCount;
            //
            ButtonWidth = EditorImporter.EditorPreferencesImport.ButtonWidth;
            ButtonHeight = EditorImporter.EditorPreferencesImport.ButtonHeight;
            ButtonBorderWidth = EditorImporter.EditorPreferencesImport.ButtonBorderWidth;
            //
            SmallButtonWidth = EditorImporter.EditorPreferencesImport.SmallButtonWidth;
            SmallButtonHeight = EditorImporter.EditorPreferencesImport.SmallButtonHeight;
            SmallButtonBorderWidth = EditorImporter.EditorPreferencesImport.SmallButtonBorderWidth;
            //
            SideButtonWidth = EditorImporter.EditorPreferencesImport.SideButtonWidth;
            SideButtonHeight = EditorImporter.EditorPreferencesImport.SideButtonHeight;
            SideButtonBorderWidth = EditorImporter.EditorPreferencesImport.SideButtonBorderWidth;
            //
            InspectorWindowWidth = EditorImporter.EditorPreferencesImport.InspectorWidth;
            InspectorWindowHeight = EditorImporter.EditorPreferencesImport.InspectorHeight;
            InspectorWindowBorderWidth = EditorImporter.EditorPreferencesImport.InspectorBorderWidth;
            //
            MiniWindowWidth = EditorImporter.EditorPreferencesImport.MiniWindowWidth;
            MiniWindowHeight = EditorImporter.EditorPreferencesImport.MiniWindowHeight;
            MiniWindowBorderWidth = EditorImporter.EditorPreferencesImport.MiniWindowBorderWidth;
            //
            CloseButtonBaseColor = EditorExImManager.FetchColorFromImport(EditorImporter.EditorPreferencesImport.CloseButtonBaseColor);
            CloseButtonBorderColor = EditorExImManager.FetchColorFromImport(EditorImporter.EditorPreferencesImport.CloseButtonBorderColor);
            CloseButtonHoverColor = EditorExImManager.FetchColorFromImport(EditorImporter.EditorPreferencesImport.CloseButtonHoverColor);
            //
            InspectorButtonBaseColor = EditorExImManager.FetchColorFromImport(EditorImporter.EditorPreferencesImport.InspectorButtonBaseColor);
            InspectorButtonBorderColor = EditorExImManager.FetchColorFromImport(EditorImporter.EditorPreferencesImport.InspectorButtonBorderColor);
            InspectorButtonHoverColor = EditorExImManager.FetchColorFromImport(EditorImporter.EditorPreferencesImport.InspectorButtonHoverColor);
            //
            BaseColor = EditorExImManager.FetchColorFromImport(EditorImporter.EditorPreferencesImport.BaseColor);
            BorderColor = EditorExImManager.FetchColorFromImport(EditorImporter.EditorPreferencesImport.BorderColor);
            TextColor = EditorExImManager.FetchColorFromImport(EditorImporter.EditorPreferencesImport.TextColor);
            HoverColor = EditorExImManager.FetchColorFromImport(EditorImporter.EditorPreferencesImport.HoverColor);
            EditorColor = EditorExImManager.FetchColorFromImport(EditorImporter.EditorPreferencesImport.EditorColor);
            //
            ProjectName = EditorImporter.EditorExIm.ProjectName;
            Toolbar = EditorImporter.FetchToolBarFromImport(EditorImporter.EditorExIm.ToolBar);
            SceneList = [.. EditorImporter.EditorExIm.Scenes.Select(EditorImporter.FetchEditorSceneFromImport)];
            ActiveScene = SceneList[0];
        }
        /// <summary>
        /// Builds the editor's data into a playable game data.
        /// Creates timeline and variable export suitable for playing the game
        /// </summary>
        public void Build()
        {
            //Build game data
            var GameBuildData = JsonSerializer.Serialize(EditorImporter.BuildScenesData([.. SceneList]), new JsonSerializerOptions
            {
                WriteIndented = true
            });
            File.WriteAllText(BuildPath + ProjectName + "build.json", GameBuildData);
            //Build variables data
            var GameVariablesData = JsonSerializer.Serialize(EditorImporter.BuildVariablesData([.. GameVariables]), new JsonSerializerOptions
            {
                WriteIndented = true
            });
            File.WriteAllText(BuildPath + ProjectName + "variables.json", GameVariablesData);
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