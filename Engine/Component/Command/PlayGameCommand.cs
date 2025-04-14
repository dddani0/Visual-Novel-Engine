using VisualNovelEngine.Engine.Editor.Interface;

namespace VisualNovelEngine.Engine.Component.Command
{
    /// <summary>
    /// Play the game.
    /// </summary>
    public class PlayGameCommand : ICommand
    {
        private Engine Engine { get; set; }

        public PlayGameCommand(Engine engine)
        {
            Engine = engine;
        }

        public void Execute() => Engine.Game = Engine.CreateGame(Engine.Window.ProjectPathInputField.Text);
    }
}