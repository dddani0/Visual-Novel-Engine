using VisualNovelEngine.Engine.Game.Interface;

namespace VisualNovelEngine.Engine.Game.Component.Action
{
    /// <summary>
    /// Creates a menu.
    /// </summary>
    class CreateMenuAction : IAction, ISettingsEvent, IButtonEvent
    {
        private readonly Game Game;
        internal Menu Menu;
        internal bool StaticExport { get; set; }
        private Block[] Blocks { get; }
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
        public void PerformAction()
        {
            Menu.BlockList.AddRange(Blocks);
            Menu.Render();
        }
    }
}