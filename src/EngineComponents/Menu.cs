using EngineComponents.Interfaces;
using Raylib_cs;

namespace EngineComponents
{
    /// <summary>
    /// Represents a menu.
    /// </summary>
    class Menu : IPermanentRenderingObject
    {
        internal int XPosition { get; set; }
        internal int YPosition { get; set; }
        private int Width { get; set; }
        private int Height { get; set; }
        internal bool IsFullScreen { get; set; }
        internal List<Block> BlockList { get; set; }
        internal Color MenuColor { get; set; }
        internal Color MenuBorderColor { get; set; }
        internal Game Game { get; set; }

        public Menu(Game game, int xPos, int yPos, int width, int height, bool isFullScreen, List<Block> blockList, Color windowColor, Color windowBorderColor)
        {
            Game = game;
            IsFullScreen = isFullScreen;
            BlockList = blockList;
            MenuColor = windowColor;
            MenuBorderColor = windowBorderColor;
            //
            XPosition = isFullScreen ? Raylib.GetScreenWidth() / 2 : xPos; //placeholder
            YPosition = isFullScreen ? Raylib.GetScreenHeight() / 2 : yPos;
            //
            Width = isFullScreen ? Raylib.GetScreenWidth() : width;
            Height = isFullScreen ? Raylib.GetScreenHeight() : height;
        }

        /// <summary>
        /// Renders the menu.
        /// </summary>
        public void Render()
        {
            Raylib.DrawRectangle(XPosition, YPosition, Width, Height, MenuColor);
            foreach (Block block in BlockList)
            {
                block.Render();
            }
        }
        /// <summary>
        /// Checks if the menu is enabled.
        /// </summary>
        /// <returns></returns>
        public bool Enabled()
        {
            return true;
        }
    }
}