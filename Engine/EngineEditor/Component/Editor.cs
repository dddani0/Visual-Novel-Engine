using VisualNovelEngine.Engine.EngineEditor.Interface;
using Raylib_cs;
using TemplateGame.Component;
using VisualNovelEngine.Engine.EngineEditor.Component.Command;
using System.Text.Json;
using VisualNovelEngine.Engine.PortData;

namespace VisualNovelEngine.Engine.EngineEditor.Component
{
    public class Editor : IEditor
    {
        internal const string currentFolderPath = "../../../Engine/Data/";
        internal const string EditorConfigPath = currentFolderPath + "EditorConfig.json";
        internal const string RelativeEditorPath = currentFolderPath + "Editor.json";
        internal int ComponentWidth { get; set; }
        internal int ComponentHeight { get; set; }
        internal int ComponentBorderWidth { get; set; }
        internal int ComponentEnabledCharacterCount { get; set; }
        internal EditorImporter EditorImporter { get; set; }
        internal Color BaseColor { get; set; }
        internal Color BorderColor { get; set; }
        internal Color TextColor { get; set; }
        internal Color HoverColor { get; set; }
        private Group Toolbar { get; set; }
        internal List<Group> ComponentGroupList { get; set; } = [];
        internal List<IDinamicComponent> ComponentList { get; set; } = [];
        internal Window InspectorWindow { get; set; }
        internal IDGenerator IDGenerator { get; set; }
        public Editor()
        {
            IDGenerator = new();
            EditorImporter = new(this, EditorConfigPath, RelativeEditorPath);
            EditorConfigImport();
        }

        private void EditorConfigImport()
        {
            ComponentWidth = EditorImporter.EditorButtonConfigurationImport.ComponentWidth;
            ComponentHeight = EditorImporter.EditorButtonConfigurationImport.ComponentHeight;
            ComponentBorderWidth = EditorImporter.EditorButtonConfigurationImport.ComponentBorderWidth;
            ComponentEnabledCharacterCount = EditorImporter.EditorButtonConfigurationImport.ComponentEnabledCharacterCount;
            //
            BaseColor = EditorImporter.FetchColorFromImport(EditorImporter.EditorButtonConfigurationImport.BaseColor);
            BorderColor = EditorImporter.FetchColorFromImport(EditorImporter.EditorButtonConfigurationImport.BorderColor);
            TextColor = EditorImporter.FetchColorFromImport(EditorImporter.EditorButtonConfigurationImport.TextColor);
            HoverColor = EditorImporter.FetchColorFromImport(EditorImporter.EditorButtonConfigurationImport.HoverColor);
            //
            Toolbar = EditorImporter.FetchToolBarFromImport(EditorImporter.EditorImport.ToolBar);
        }

        public void Build()
        {

        }

        public void Save()
        {

        }

        public void Update()
        {
            Toolbar.Update();
            ComponentGroupList.ForEach(component => component.Update());
            Raylib.ClearBackground(Color.Gray);
            //Only render each component from the component list if the component is not in a group
            for (int i = 0; i < ComponentList.Count; i++)
            {
                IDinamicComponent? component = ComponentList[i];
                if (component.IsInGroup() is true) continue;
                IComponent castedComponent = (IComponent)component;
                castedComponent.Render();
            }
        }
        /// <summary>
        /// Generates a unique number ID.
        /// </summary>
        /// <returns></returns>
        internal long GenerateID() => IDGenerator.GenerateID();
    }
}