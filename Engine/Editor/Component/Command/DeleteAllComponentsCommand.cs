using VisualNovelEngine.Engine.Editor.Interface;

namespace VisualNovelEngine.Engine.Editor.Component.Command
{
    /// <summary>
    /// Clear the scene of all components
    /// </summary>
    class DeleteAllComponentsCommand : ICommand
    {
        internal Editor Editor { get; set; }

        private CloseErrorWindowCommand CloseShowWindowCommand { get; set; }
        public DeleteAllComponentsCommand(Editor editor)
        {
            Editor = editor;
            CloseShowWindowCommand = new CloseErrorWindowCommand(editor);
        }

        public void Execute()
        {
            if (Editor.ActiveScene.ComponentList.Count == 0) return;
            Editor.ActiveScene.ComponentList.Clear();
            Editor.ActiveScene.ComponentGroupList.Clear();
            CloseShowWindowCommand.Execute();
        }
    }
}