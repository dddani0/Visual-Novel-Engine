using Raylib_cs;
using VisualNovelEngine.Engine.Game.Interface;

namespace VisualNovelEngine.Engine.Game.Component
{
    /// <summary>
    /// Represents a menu, which is a collection of blocks.
    /// A Block is a component with coordinates.
    /// </summary>
    public class Menu : IRenderingObject
    {
        internal int ID { get; set; }
        /// <summary>
        /// The absolute position on the X axis.
        /// </summary>
        internal int XPosition { get; set; }
        /// <summary>
        /// The absolute position on the Y axis.
        /// </summary>
        internal int YPosition { get; set; }
        /// <summary>
        /// The width of the menu.
        /// </summary>
        internal int Width { get; set; }
        /// <summary>
        /// The height of the menu.
        /// </summary>
        internal int Height { get; set; }
        /// <summary>
        /// Displays the menu in full screen.
        /// </summary>
        internal bool IsFullScreen { get; set; }
        /// <summary>
        /// The list of blocks in the menu.
        /// </summary>
        internal List<Block> BlockList { get; set; }
        /// <summary>
        /// The background color of the menu.
        /// </summary>
        internal Color Color { get; set; }
        /// <summary>
        /// The border color of the menu.
        /// </summary>
        internal Color BorderColor { get; set; }
        /// <summary>
        /// The border width of the menu.
        /// </summary>
        internal bool IsVisible { get; set; }
        /// <summary>
        /// The parent game object.
        /// </summary>
        internal Game Game { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="game"></param>
        /// <param name="id"></param>
        /// <param name="xPos"></param>
        /// <param name="yPos"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="isFullScreen"></param>
        /// <param name="blockList"></param>
        /// <param name="windowColor"></param>
        /// <param name="windowBorderColor"></param>
        public Menu(Game game, int id, int xPos, int yPos, int width, int height, bool isFullScreen, List<Block> blockList, Color windowColor, Color windowBorderColor)
        {
            Game = game;
            ID = id;
            IsFullScreen = isFullScreen;
            BlockList = blockList;
            Color = windowColor;
            BorderColor = windowBorderColor;
            //
            Width = isFullScreen ? Raylib.GetScreenWidth() : width;
            Height = isFullScreen ? Raylib.GetScreenHeight() : height;
            //
            XPosition = isFullScreen ? Raylib.GetScreenWidth() / 2 - Width / 2 : xPos - Width / 2; //placeholder
            YPosition = isFullScreen ? Raylib.GetScreenHeight() / 2 - Height / 2 : yPos - Height / 2; //placeholder
        }

        /// <summary>
        /// Renders the menu.
        /// </summary>
        public void Render()
        {
            Raylib.DrawRectangle(XPosition, YPosition, Width, Height, Color);
            foreach (Block block in BlockList)
            {
                block.Render();
            }
        }
        /// <summary>
        /// Checks if the menu is enabled.
        /// </summary>
        /// <returns></returns>
        public bool Enabled() => IsVisible;
    }
}