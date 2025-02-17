using VisualNovelEngine.Engine.EngineEditor.Interface;

namespace VisualNovelEngine.Engine.EngineEditor.Component.Command
{
    class DeleteAllComponentCommand : ICommand
    {
        internal Editor Editor { get; set; }

        public DeleteAllComponentCommand(Editor editor)
        {
            Editor = editor;
        }

        public void Execute()
        {
            if (Editor.ActiveScene.ComponentList.Count == 0) return;
            Editor.ActiveScene.ComponentList.Clear();
            Editor.ActiveScene.ComponentGroupList.Clear();
        }
    }
}