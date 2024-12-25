using EngineComponents.Interfaces;

namespace EngineComponents
{
    /// <summary>
    /// Represents a block within the menu component.
    /// </summary>
    class Block : IPermanentRenderingObject
    {
        int XPosition { get; set; }
        int YPosition { get; set; }
        IPermanentRenderingObject Component { get; }
        /// <summary>
        /// Constructor for the block.
        /// </summary>
        /// <param name="x">Position on the X axis.</param>
        /// <param name="y">Position on the Y axis.</param>
        /// <param name="component">Rendering component.</param>
        public Block(int x, int y, IPermanentRenderingObject component)
        {
            XPosition = x;
            YPosition = y;
            Component = component;
        }
        /// <summary>
        /// Renders the block.
        /// </summary>
        public void Render()
        {
            Component.Render();
        }
        /// <summary>
        /// Checks if the block is enabled.
        /// </summary>
        /// <returns></returns>
        public bool Enabled() => Component.Enabled();
    }
}