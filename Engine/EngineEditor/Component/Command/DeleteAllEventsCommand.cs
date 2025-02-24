using VisualNovelEngine.Engine.EngineEditor.Interface;

namespace VisualNovelEngine.Engine.EngineEditor.Component.Command
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteAllEventsCommand : ICommand
    {
        internal Editor Editor { get; set; }
        private CloseShowWindowCommand CloseShowWindowCommand { get; set; }
        public DeleteAllEventsCommand(Editor editor)
        {
            Editor = editor;
            CloseShowWindowCommand = new CloseShowWindowCommand(editor);
        }
        public void Execute()
        {
            Editor.ActiveScene.Timeline.Events.Clear();
            Editor.ActiveScene.Timeline.EventButtons.Clear();
            CloseShowWindowCommand.Execute();
        }
    }
}