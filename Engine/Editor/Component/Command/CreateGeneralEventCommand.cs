using Raylib_cs;
using VisualNovelEngine.Engine.Game.Component;
using VisualNovelEngine.Engine.Game.Component.Action;
using VisualNovelEngine.Engine.Game.Component.Action.TimelineDependent;
using VisualNovelEngine.Engine.Game.Interface;
using VisualNovelEngine.Engine.Editor.Interface;

namespace VisualNovelEngine.Engine.Editor.Component.Command
{
    /// <summary>
    /// Represents an empty command.
    /// </summary>
    public class CreateGeneralEventCommand : ICommand
    {
        /// <summary>
        /// Supported action creation type
        /// </summary>
        public enum ActionType
        {
            CreateMenuAction,
            LoadSceneAction,
            NativeLoadSceneAction,
            AddSpriteAction,
            ChangeSpriteAction,
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
        /// <summary>
        /// The action to create
        /// </summary>
        internal ActionType Action;
        /// <summary>
        /// The game.
        /// </summary>
        private VisualNovelEngine.Engine.Game.Component.Game Game { get; set; }
        /// <summary>
        /// The editor.
        /// </summary>
        private readonly Editor Editor;
        /// <summary>
        /// The event.
        /// </summary>
        internal IAction Event { get; set; }
        /// <summary>
        /// constructor for general event creation
        /// </summary>
        /// <param name="editor"></param>
        /// <param name="actionType"></param>
        public CreateGeneralEventCommand(Editor editor, ActionType actionType)
        {
            Editor = editor;
            Game = Editor.Game;
            Action = actionType;
        }
        /// <summary>
        /// Creates a general event.
        /// </summary>
        public void Execute()
        {
            //switch return
            Event = Action switch
            {
                ActionType.CreateMenuAction => new CreateMenuAction(Game, null, null),
                ActionType.LoadSceneAction => new LoadSceneAction(Game, 0, "Variable here"),
                ActionType.NativeLoadSceneAction => new NativeLoadSceneAction(Game, 0),
                ActionType.AddSpriteAction => new AddSpriteAction(null, Game),
                ActionType.ChangeSpriteAction => new ChangeSpriteAction(null, "", Game)
                {
                    replacementSprite = new Sprite("replacement sprite")
                },
                ActionType.DecrementVariableAction => new DecrementVariableAction(Game, "variable name", "decrementing variable name"),
                ActionType.EmptyAction => new EmptyAction(Game),
                ActionType.IncrementVariableAction => new IncrementVariableAction(Game, "variable name", "incrementing variable name"),
                ActionType.RemoveSpriteAction => new RemoveSpriteAction(null, Game),
                ActionType.SetBoolVariableAction => new SetBoolVariableAction(Game, "variable name", true),
                ActionType.SetVariableFalseAction => new SetVariableFalseAction(Game, "variable name"),
                ActionType.SetVariableTrueAction => new SetVariableTrueAction(Game, "variable name"),
                ActionType.TextBoxCreateAction => new TextBoxCreateAction(null),
                ActionType.TintSpriteAction => new TintSpriteAction(null, Color.White, null),
                ActionType.ToggleVariableAction => new ToggleVariableAction(Game, "varialbe name"),
                _ => throw new System.Exception("Action not found"),
            };
            Editor.ActiveScene.Timeline.AddAction(Event);
        }
    }
}