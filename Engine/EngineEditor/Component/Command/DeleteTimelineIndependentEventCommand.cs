using TemplateGame.Interface;
using VisualNovelEngine.Engine.EngineEditor.Interface;

namespace VisualNovelEngine.Engine.EngineEditor.Component.Command
{
    public class DeleteTimelienIndependentEventCommand : ICommand
    {
        Editor Editor { get; set; }
        public DeleteTimelienIndependentEventCommand(Editor editor)
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