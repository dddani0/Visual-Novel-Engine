using VisualNovelEngine.Engine.Game.Interface;
using VisualNovelEngine.Engine.Editor.Interface;

namespace VisualNovelEngine.Engine.Editor.Component.Command
{
    /// <summary>
    /// Delete an action from the timeline
    /// </summary>
    public class DeleteActionCommand : ICommand
    {
        Editor Editor { get; set; }
        public DeleteActionCommand(Editor editor)
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
            int actionIndex = Editor.ActiveScene.Timeline.Actions.FindIndex(x => x == impendingAction);
            //Remove the action from the timeline
            Editor.ActiveScene.Timeline.Actions.RemoveAt(actionIndex);
            //Remove the button from the timeline
            Editor.ActiveScene.Timeline.ActionButtons.RemoveAt(actionIndex);
        }
    }
}