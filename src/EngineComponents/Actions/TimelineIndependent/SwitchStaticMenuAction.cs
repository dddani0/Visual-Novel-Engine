using EngineComponents.Interfaces;

namespace EngineComponents.Actions.TimelineIndependent
{
    /// <summary>
    /// Switches the static menu.
    /// </summary>
    public class SwitchStaticMenuAction : IEvent
    {
        private readonly Game Game;
        private Menu DisablingMenu { get; }
        private Menu EnablingMenu { get; }
        /// <summary>
        /// Constructor for the SwitchStaticMenuAction.
        /// </summary>
        /// <param name="game">Active game.</param>
        /// <param name="previousMenu">Menu to be rendered.</param>
        /// <param name="show">Show or hide the menu.</param>
        public SwitchStaticMenuAction(Game game, Menu previousMenu, Menu nextMenu)
        {
            Game = game;
            DisablingMenu = previousMenu;
            EnablingMenu = nextMenu;
        }
        /// <summary>
        /// Performs the event.
        /// </summary>
        public void PerformEvent()
        {
            DisablingMenu.IsVisible = false;
            EnablingMenu.IsVisible = true;
        }
    }
}