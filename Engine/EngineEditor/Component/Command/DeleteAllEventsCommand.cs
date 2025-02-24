using VisualNovelEngine.Engine.EngineEditor.Interface;

namespace VisualNovelEngine.Engine.EngineEditor.Component.Command
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteAllEventsCommand : ICommand
    {
        internal Editor Editor { get; set; }
        public DeleteAllEventsCommand(Editor editor)
        {
            Editor = editor;
        }
        public void Execute()
        {
            Editor.ActiveScene.Timeline.Events.Clear();
            Editor.ActiveScene.Timeline.EventButtons.Clear();
        }
    }
}