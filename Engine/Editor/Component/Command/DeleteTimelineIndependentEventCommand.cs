using TemplateGame.Interface;
using VisualNovelEngine.Engine.Editor.Interface;

namespace VisualNovelEngine.Engine.Editor.Component.Command
{
    /// <summary>
    /// Command to delete an independent event from the timeline.
    /// </summary>
    public class DeleteTimelineIndependentEventCommand : ICommand
    {
        Editor Editor { get; set; }
        public DeleteTimelineIndependentEventCommand(Editor editor)
        {
            Editor = editor;
        }
        public void Execute()
        {
            //Delete event in the inspector
            IAction impendingAction = Editor.ActiveScene.InspectorWindow.ActiveAction;
            //Close the inspector window
            CloseInspectorCommand closeInspectorCommand = new(Editor, Editor.ActiveScene.InspectorWindow);
            closeInspectorCommand.Execute();
            //Delete the event
            //Get index of the current action
            int actionIndex = Editor.ActiveScene.Timeline.TimelineIndepententActions.FindIndex(x => x == impendingAction);
            //Remove the action from the timeline
            Editor.ActiveScene.Timeline.TimelineIndepententActions.RemoveAt(actionIndex);
            //Remove the button from the timeline
            Editor.ActiveScene.Timeline.TimelineIndepententActionButtons.RemoveAt(actionIndex);
        }
    }
}