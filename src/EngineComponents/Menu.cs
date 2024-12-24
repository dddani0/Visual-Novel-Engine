using EngineComponents.Interfaces;
using Raylib_cs;

namespace EngineComponents
{
    /// <summary>
    /// Represents a menu.
    /// </summary>
    class Menu : IPermanentRenderingObject
    {
        private int XPosition { get; set; }
        private int YPosition { get; set; }
        private int Width { get; set; }
        private int Height { get; set; }
        internal bool IsFullScreen { get; set; }
        private List<Block> BlockList { get; set; }
        internal Color WindowColor { get; set; }
        internal Color WindowBorderColor { get; set; }
        internal Game Game { get; set; }

        public Menu(Game game, int xPos, int yPos, int width, int height, bool isFullScreen, List<Block> blockList, Color windowColor, Color windowBorderColor)
        {
            Game = game;
            IsFullScreen = isFullScreen;
            BlockList = blockList;
            WindowColor = windowColor;
            WindowBorderColor = windowBorderColor;
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
            Raylib.DrawRectangle(XPosition, YPosition, Width, Height, WindowColor);
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