using Raylib_cs;
using TemplateGame.Component;
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
        internal ActionType Action;
        private Game Game { get; set; }
        private readonly Editor Editor;
        internal IEvent Event { get; set; }

        public CreateGeneralEventCommand(Editor editor, ActionType actionType)
        {
            Editor = editor;
            Game = Editor.Game;
            Action = actionType;
        }

        /// <summary>
        /// Creates a default event
        /// </summary>
        public void Execute()
        {
            Event = Action switch
            {
                ActionType.CreateMenuAction => new CreateMenuAction(Game, null, null),
                ActionType.LoadSceneAction => new LoadSceneAction(Game, 0, "Empty"),
                ActionType.NativeLoadSceneAction => new NativeLoadSceneAction(Game, 0),
                ActionType.AddSpriteAction => new AddSpriteAction(null, Game),
                ActionType.ChangeSpriteAction => new ChangeSpriteAction(null, null, Game),
                ActionType.CreateVariableAction => new CreateVariableAction(Game, null),
                ActionType.DecrementVariableAction => new DecrementVariableAction(Game, "", ""),
                ActionType.EmptyAction => new EmptyAction(Game),
                ActionType.IncrementVariableAction => new IncrementVariableAction(Game, "", ""),
                ActionType.RemoveSpriteAction => new RemoveSpriteAction(null, Game),
                ActionType.SetBoolVariableAction => new SetBoolVariableAction(Game, "", false),
                ActionType.SetVariableFalseAction => new SetVariableFalseAction(Game, ""),
                ActionType.SetVariableTrueAction => new SetVariableTrueAction(Game, ""),
                ActionType.TextBoxCreateAction => new TextBoxCreateAction(null),
                ActionType.TintSpriteAction => new TintSpriteAction(null, Color.White, null),
                ActionType.ToggleVariableAction => new ToggleVariableAction(Game, ""),
                _ => null
            };
            Editor.ActiveScene.Timeline.AddEvent(Event);
        }
    }
}