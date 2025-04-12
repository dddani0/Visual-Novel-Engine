using VisualNovelEngine.Engine.Game.Interface;

namespace VisualNovelEngine.Engine.Game.Component
{
    /// <summary>
    /// Represents a block.
    /// The block is dependent on a parent menu object.
    /// </summary>
    public class Block : IRenderingObject
    {
        internal int ID { get; private set; }
        /// <summary>
        /// The relative - to the parent - position on the X axis.
        /// </summary>
        internal int XPosition { get; set; }
        /// <summary>
        /// The relative - to the parent - position on the Y axis.
        /// </summary>
        internal int YPosition { get; set; }
        /// <summary>
        /// The rendering component of the block.
        /// The abstract component can be a Button, TextField, or Sprite.
        /// </summary>
        internal IRenderingObject Component { get; set; }
        /// <summary>
        /// Constructor for the block.
        /// </summary>
        /// <param name="x">Position on the X axis.</param>
        /// <param name="y">Position on the Y axis.</param>
        /// <param name="component">Rendering component.</param>
        public Block(int x, int y, IRenderingObject component, int id)
        {
            ID = id;
            XPosition = x;
            YPosition = y;
            Component = component;
        }
        internal void SetComponent(IRenderingObject component) => Component = component;
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
