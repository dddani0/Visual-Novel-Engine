using VisualNovelEngine.Engine.EngineEditor.Interface;
using Raylib_cs;
using TemplateGame.Component;
using System.Text.Json;
using System.Text.RegularExpressions;
using EngineEditor.Component;
using System.Numerics;
using VisualNovelEngine.Engine.EngineEditor.Component.Command;
using System.Reflection.Emit;
using Timer = TemplateGame.Component.Timer;

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
        /// The scene configuration button in the editor.
        /// </summary>
        internal Button SceneConfigurationButton { get; set; }
        /// <summary>
        /// The game configuration button in the editor.
        /// </summary>
        internal Button GameConfigurationButton { get; set; }
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
        private ShowErrorCommand ShowExitErrorCommand { get; set; }
        internal Timer MouseMoveTimer { get; set; }
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
            //
            MouseMoveTimer = new(0.5f);
            //
            ShowExitErrorCommand = new(this, "Exit?", [new Button(this, 0, 0, "Quit", true, ButtonWidth, ButtonHeight, ButtonBorderWidth, BaseColor, BorderColor, HoverColor, new ExitWindowCommand(), Button.ButtonType.Trigger)]);
            //Create scene configuration button
            Label windowTitle = new Label(Raylib.GetScreenWidth() / 2 - MiniWindowWidth / 2, Raylib.GetScreenHeight() / 2 - MiniWindowHeight / 2, "Scene configuration");
            Label sceneName = new Label(Raylib.GetScreenWidth() / 2 - MiniWindowWidth / 2, Raylib.GetScreenHeight() / 2 - MiniWindowHeight / 2, "Name:");
            TextField sceneNameField = new TextField(this, Raylib.GetScreenWidth() / 2 - MiniWindowWidth / 2, Raylib.GetScreenHeight() / 2 - MiniWindowHeight / 2, ComponentWidth, ComponentHeight, ComponentBorderWidth, ActiveScene.Name, Raylib.GetFontDefault(), false);
            SceneConfigurationButton = new(this, Raylib.GetScreenWidth() - ButtonWidth, 100, "Scene Configuration", false, ButtonWidth, ButtonHeight, ButtonBorderWidth, BaseColor, BorderColor, HoverColor, new ShowMiniWindowComand(this, [windowTitle, sceneName, sceneNameField], EngineEditor.Component.MiniWindow.miniWindowType.Vertical), Button.ButtonType.Trigger);
            //Create game configuration button
            Label gameWindowTitle = new Label(Raylib.GetScreenWidth() / 2 - MiniWindowWidth / 2, Raylib.GetScreenHeight() / 2 - MiniWindowHeight / 2, "Game configuration");
            Label gameName = new Label(Raylib.GetScreenWidth() / 2 - MiniWindowWidth / 2, Raylib.GetScreenHeight() / 2 - MiniWindowHeight / 2, "Name:");
            TextField gameNameField = new TextField(this, Raylib.GetScreenWidth() / 2 - MiniWindowWidth / 2, Raylib.GetScreenHeight() / 2 - MiniWindowHeight / 2, ComponentWidth, ComponentHeight, ComponentBorderWidth, ProjectName, Raylib.GetFontDefault(), false);
            Label windowResolutionTitle = new Label(Raylib.GetScreenWidth() / 2 - MiniWindowWidth / 2, Raylib.GetScreenHeight() / 2 - MiniWindowHeight / 2, "Window resolution");
            Label windowResolutionWidth = new Label(Raylib.GetScreenWidth() / 2 - MiniWindowWidth / 2, Raylib.GetScreenHeight() / 2 - MiniWindowHeight / 2, "Window width:");
            TextField windowResolutionWidthField = new TextField(this, Raylib.GetScreenWidth() / 2 - MiniWindowWidth / 2, Raylib.GetScreenHeight() / 2 - MiniWindowHeight / 2, ComponentWidth, ComponentHeight, ComponentBorderWidth, Raylib.GetScreenWidth().ToString(), Raylib.GetFontDefault(), false);
            Label windowResolutionHeigth = new Label(Raylib.GetScreenWidth() / 2 - MiniWindowWidth / 2, Raylib.GetScreenHeight() / 2 - MiniWindowHeight / 2, "Window height:");
            TextField windowResolutionHeigthField = new TextField(this, Raylib.GetScreenWidth() / 2 - MiniWindowWidth / 2, Raylib.GetScreenHeight() / 2 - MiniWindowHeight / 2, ComponentWidth, ComponentHeight, ComponentBorderWidth, Raylib.GetScreenHeight().ToString(), Raylib.GetFontDefault(), false);
            GameConfigurationButton = new(this, Raylib.GetScreenWidth() - 2 * ButtonWidth, 100, "Game Configuration", false, ButtonWidth, ButtonHeight, ButtonBorderWidth, BaseColor, BorderColor, HoverColor, new ShowMiniWindowComand(this, [gameWindowTitle, gameName, gameNameField, windowResolutionTitle, windowResolutionHeigth, windowResolutionHeigthField, windowResolutionWidth, windowResolutionWidth], EngineEditor.Component.MiniWindow.miniWindowType.Vertical), Button.ButtonType.Trigger);
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
            SceneBar = new MiniWindow(this, false, 0, 0, Raylib.GetScreenWidth(), 100, ComponentBorderWidth, BaseColor, BorderColor, EngineEditor.Component.MiniWindow.miniWindowType.Horizontal, [.. SceneButtonList]);
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
            CloseButtonBaseColor = EditorImporter.FetchColorFromImport(EditorImporter.EditorPreferencesImport.CloseButtonBaseColor);
            CloseButtonBorderColor = EditorImporter.FetchColorFromImport(EditorImporter.EditorPreferencesImport.CloseButtonBorderColor);
            CloseButtonHoverColor = EditorImporter.FetchColorFromImport(EditorImporter.EditorPreferencesImport.CloseButtonHoverColor);
            //
            InspectorButtonBaseColor = EditorImporter.FetchColorFromImport(EditorImporter.EditorPreferencesImport.InspectorButtonBaseColor);
            InspectorButtonBorderColor = EditorImporter.FetchColorFromImport(EditorImporter.EditorPreferencesImport.InspectorButtonBorderColor);
            InspectorButtonHoverColor = EditorImporter.FetchColorFromImport(EditorImporter.EditorPreferencesImport.InspectorButtonHoverColor);
            //
            BaseColor = EditorImporter.FetchColorFromImport(EditorImporter.EditorPreferencesImport.BaseColor);
            BorderColor = EditorImporter.FetchColorFromImport(EditorImporter.EditorPreferencesImport.BorderColor);
            TextColor = EditorImporter.FetchColorFromImport(EditorImporter.EditorPreferencesImport.TextColor);
            HoverColor = EditorImporter.FetchColorFromImport(EditorImporter.EditorPreferencesImport.HoverColor);
            EditorColor = EditorImporter.FetchColorFromImport(EditorImporter.EditorPreferencesImport.EditorColor);
            //
            Toolbar = EditorImporter.FetchToolBarFromImport(EditorImporter.EditorExIm.ToolBar);
            ProjectName = EditorImporter.EditorExIm.ProjectName;
            SceneList = [.. EditorImporter.EditorExIm.Scenes.Select(EditorImporter.FetchEditorSceneFromImport)];
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
            SceneBar.Show();
            Toolbar.Update();
            SceneConfigurationButton.Render();
            GameConfigurationButton.Render();
            ActiveScene.Update();
            ExitWindow();
            MoveCamera();
            for (int i = 0; i < MiniWindow.Count; i++)
            {
                MiniWindow[i].Show();
            }
            ErrorWindow?.Show();
        }

        private void ExitWindow()
        {
            if (Raylib.WindowShouldClose())
            {
                ShowExitErrorCommand.Execute();
            }
        }

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
                MouseMoveTimer.ResetTimer();
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
            MouseMoveTimer.ResetTimer();
        }

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
            ActiveScene.Timeline.AddGeneralEventButton.IsLocked = true;
            ActiveScene.Timeline.ConfigureTimelineButton.IsLocked = true;
            ActiveScene.Timeline.RemoveEventsButton.IsLocked = true;
            GameConfigurationButton.IsLocked = true;
            SceneConfigurationButton.IsLocked = true;
        }

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
            ActiveScene.Timeline.AddGeneralEventButton.IsLocked = false;
            ActiveScene.Timeline.ConfigureTimelineButton.IsLocked = false;
            ActiveScene.Timeline.RemoveEventsButton.IsLocked = false;
            GameConfigurationButton.IsLocked = false;
            SceneConfigurationButton.IsLocked = false;
        }
        /// <summary>
        /// Generates a unique number ID.
        /// </summary>
        /// <returns></returns>
        internal int GenerateID() => IDGenerator.GenerateID();
    }
}