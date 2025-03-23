using VisualNovelEngine.Engine.Editor.Interface;

namespace VisualNovelEngine.Engine.Editor.Component.Command
{
    /// <summary>
    /// Clear the scene of all actions
    /// </summary>
    public class DeleteAllActionsCommand : ICommand
    {
        /// <summary>
        /// Editor instance
        /// </summary>
        internal Editor Editor { get; set; }
        private CloseErrorWindowCommand CloseShowWindowCommand { get; set; }
        public DeleteAllActionsCommand(Editor editor)
        {
            Editor = editor;
            CloseShowWindowCommand = new CloseErrorWindowCommand(editor);
        }
        public void Execute()
        {
            Editor.ActiveScene.Timeline.Actions.Clear();
            Editor.ActiveScene.Timeline.ActionButtons.Clear();
            if (Editor.ActiveScene.Timeline.TimelineIndepententActions.Count() > 0)
            {
                Editor.ActiveScene.Timeline.TimelineIndepententActions.Clear();
                Editor.ActiveScene.Timeline.TimelineIndepententActionButtons.Clear();
            }
            CloseShowWindowCommand.Execute();
        }
    }
}