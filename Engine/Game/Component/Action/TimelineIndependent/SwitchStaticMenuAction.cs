using VisualNovelEngine.Engine.Game.Interface;

namespace VisualNovelEngine.Engine.Game.Component.Action.TimelineIndependent
{
    /// <summary>
    /// Switches between two menus, by disabling the prior, and enabling the latter.
    /// </summary>
    public class SwitchStaticMenuAction : IAction, ISettingsAction
    {
        private readonly Game Game;
        private readonly GameEximManager GameLoader;
        internal Menu DisablingMenu { get; set; }
        internal Menu EnablingMenu { get; set; }
        private readonly int disableMenuID;
        private readonly int enablingMenuID;
        /// <summary>
        /// Constructor for the SwitchStaticMenuAction.
        /// </summary>
        /// <param name="game">Active game.</param>
        /// <param name="previousMenu">Menu to be rendered.</param>
        /// <param name="show">Show or hide the menu.</param>
        public SwitchStaticMenuAction(Game game, GameEximManager gameLoader, int disableMenuID, int enalbeMenuID)
        {
            Game = game;
            GameLoader = gameLoader;
            this.disableMenuID = disableMenuID;
            this.enablingMenuID = enalbeMenuID;
        }
        /// <summary>
        /// Performs the event.
        /// </summary>
        public void PerformAction()
        {
            var DisablingMenu = GameLoader.MenuListCache.First(x => x.ID == disableMenuID);
            var EnablingMenu = GameLoader.MenuListCache.First(x => x.ID == enablingMenuID);
            DisablingMenu.IsVisible = false;
            EnablingMenu.IsVisible = true;
        }
    }
}