using TemplateGame.Component.Action;
using TemplateGame.Component.Action.TimelineDependent;
using TemplateGame.Interface;
using VisualNovelEngine.Engine.EngineEditor.Interface;

namespace VisualNovelEngine.Engine.EngineEditor.Component.Command
{
    /// <summary>
    /// Represents an empty command.
    /// </summary>
    public class CreateGeneralEventCommand : ICommand
    {
        public enum ActionType
        {
            CreateMenuAction,
            LoadSceneAction,
            NativeLoadSceneAction,
            AddSpriteAction,
            ChangeSpriteAction,
            CreateVariableAction,
            DecrementVariableAction,
            EmptyAction,
            IncrementVariableAction,
            RemoveSpriteAction,
            SetBoolVariableAction,
            SetVariableFalseAction,
            SetVariableTrueAction,
            TextBoxCreateAction,
            TintSpriteAction,
            ToggleVariableAction
        }
        internal ActionType action;
        private readonly Editor Editor;
        internal IEvent Event { get; set; }

        public CreateGeneralEventCommand(Editor editor, ActionType actionType)
        {
            Editor = editor;
            action = actionType;
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        public void Execute()
        {
            Event = new EmptyAction(null); //action switch
                                           //{
                                           //ActionType.CreateMenuAction => new CreateMenuAction(Editor),
                                           //ActionType.LoadSceneAction => new LoadSceneAction(Editor),
                                           //};
            if (Editor.ActiveScene.Timeline.Events.Contains(Event)) return;
            Editor.ActiveScene.Timeline.AddEvent(Event);
        }
    }
}