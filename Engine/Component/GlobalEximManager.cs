using TemplateGame.Component;
using VisualNovelEngine.Engine.EngineEditor.Component;
using VisualNovelEngine.Engine.PortData;

namespace VisualNovelEngine.Engine.Component
{
    /// <summary>
    /// Manages the import and export of global data from the game and editor.
    /// </summary>
    public class GlobalEximManager
    {
        internal Game Game { get; set; }
        internal Editor Editor { get; set; }
        internal GameImporter GameImporter { get; set; }
        internal EditorExImManager EditorImporter { get; set; }
        public GlobalEximManager(Game game, Editor editor)
        {
            Game = game;
            Editor = editor;
            GameImporter = Game.GameImport;
            EditorImporter = Editor.EditorImporter;
        }

        internal Sprite FetchSpriteFromImport()
        {
            return null;
        }
        internal SpriteExim ExportSpriteFromEditor()
        {
            return null;
        }
    }
}