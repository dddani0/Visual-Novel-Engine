using EngineComponents.Interfaces;

namespace EngineComponents.Actions
{
    /// <summary>
    /// Creates a menu.
    /// </summary>
    class CreateMenuAction : IEvent
    {
        private readonly Game Game;
        private readonly Menu Menu;
        Block[] Blocks { get; }
        /// <summary>
        /// Constructor for the CreateMenuAction.
        /// </summary>
        /// <param name="game">Active game</param>
        /// <param name="menu">Menu to be rendered.</param>
        /// <param name="blocks">Children blocks inside the menu.</param>
        public CreateMenuAction(Game game, Menu menu, Block[] blocks)
        {
            Game = game;
            Menu = menu;
            Blocks = blocks;
        }
        /// <summary>
        /// Performs the event.
        /// </summary>
        public void PerformEvent()
        {
            Menu.BlockList.AddRange(Blocks);
            Menu.Render();
        }
    }
}