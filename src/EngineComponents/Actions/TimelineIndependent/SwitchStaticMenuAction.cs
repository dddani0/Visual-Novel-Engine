using EngineComponents.Interfaces;

namespace EngineComponents.Actions.TimelineIndependent
{
    /// <summary>
    /// Switches between two menus, by disabling the prior, and enabling the latter.
    /// </summary>
    public class SwitchStaticMenuAction : IEvent, ISettingsEvent
    {
        private readonly Game Game;
        private readonly GameLoader GameLoader;
        private Menu DisablingMenu { get; set; }
        private Menu EnablingMenu { get; set; }
        private readonly long disableMenuID;
        private readonly long enablingMenuID;
        /// <summary>
        /// Constructor for the SwitchStaticMenuAction.
        /// </summary>
        /// <param name="game">Active game.</param>
        /// <param name="previousMenu">Menu to be rendered.</param>
        /// <param name="show">Show or hide the menu.</param>
        public SwitchStaticMenuAction(Game game, GameLoader gameLoader, long disableMenuID, long enalbeMenuID)
        {
            Game = game;
            GameLoader = gameLoader;
            this.disableMenuID = disableMenuID;
            this.enablingMenuID = enalbeMenuID;
        }
        /// <summary>
        /// Performs the event.
        /// </summary>
        public void PerformEvent()
        {
            var DisablingMenu = GameLoader.MenuListCache.First(x => x.ID == disableMenuID);
            var EnablingMenu = GameLoader.MenuListCache.First(x => x.ID == enablingMenuID);
            DisablingMenu.IsVisible = false;
            EnablingMenu.IsVisible = true;
        }
    }
}