using VisualNovelEngine.Engine.EngineEditor.Interface;
using Raylib_cs;
using TemplateGame.Component;
using VisualNovelEngine.Engine.EngineEditor.Component.Command;
using System.Text.Json;
using VisualNovelEngine.Engine.PortData;
using System.Text.RegularExpressions;
using EngineEditor.Component;

namespace VisualNovelEngine.Engine.EngineEditor.Component
{
    public class Editor : IEditor
    {
        internal const string currentFolderPath = "../../../Engine/Data/";
        internal const string EditorConfigPath = currentFolderPath + "EditorConfig.json";
        internal const string RelativeEditorPath = currentFolderPath + "Editor.json";
        internal string SaveFilePath { get; set; } = currentFolderPath;
        internal Game Game { get; set; }
        internal int ComponentWidth { get; set; }
        internal int ComponentHeight { get; set; }
        internal int ComponentBorderWidth { get; set; }
        internal int ButtonWidth { get; set; }
        internal int ButtonHeight { get; set; }
        internal int ButtonBorderWidth { get; set; }
        internal int SmallButtonWidth { get; set; }
        internal int SmallButtonHeight { get; set; }
        internal int SmallButtonBorderWidth { get; set; }
        internal int SideButtonWidth { get; set; }
        internal int SideButtonHeight { get; set; }
        internal int SideButtonBorderWidth { get; set; }
        internal int ComponentEnabledCharacterCount { get; set; }
        internal int InspectorWindowWidth { get; set; }
        internal int InspectorWindowHeight { get; set; }
        internal int InspectorWindowBorderWidth { get; set; }
        internal Color BaseColor { get; set; }
        internal Color BorderColor { get; set; }
        internal Color TextColor { get; set; }
        internal Color HoverColor { get; set; }
        internal Color EditorColor { get; set; }
        internal Color InspectorButtonBaseColor { get; set; }
        internal Color InspectorButtonBorderColor { get; set; }
        internal Color InspectorButtonHoverColor { get; set; }
        internal Color CloseButtonBaseColor { get; set; }
        internal Color CloseButtonBorderColor { get; set; }
        internal Color CloseButtonHoverColor { get; set; }
        internal Group Toolbar { get; set; }
        internal string ProjectName { get; set; }
        internal IDGenerator IDGenerator { get; set; }
        internal EditorImporter EditorImporter { get; set; }
        internal List<Scene> SceneList { get; set; } = [];
        internal Scene ActiveScene { get; set; }
        internal List<MiniWindow> MiniWindow { get; set; } = [];
        internal ErrorWindow? ErrorWindow { get; set; } = null;
        public bool Busy => ActiveScene.InspectorWindow?.Active is true;
        public Editor()
        {
            Game = new();
            EditorImporter = new(this, EditorConfigPath, RelativeEditorPath);
            EditorConfigImport();
            SaveFilePath += Regex.Replace(ProjectName, @"[^a-zA-Z0-9\s]", "") + ".json";
        }

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

        public void Build()
        {

        }

        public void Save()
        {
            var editorJson = JsonSerializer.Serialize(EditorImporter.ExportEditorData(this), new JsonSerializerOptions
            {
                WriteIndented = true
            });
            File.WriteAllText(SaveFilePath, editorJson);
        }

        public void Update()
        {
            Raylib.ClearBackground(EditorColor);
            ActiveScene.Update();
            Toolbar.Update();
            for (int i = 0; i < MiniWindow.Count; i++)
            {
                MiniWindow[i].Show();
            }
            ErrorWindow?.Show();
        }
        /// <summary>
        /// Generates a unique number ID.
        /// </summary>
        /// <returns></returns>
        internal int GenerateID() => IDGenerator.GenerateID();
    }
}