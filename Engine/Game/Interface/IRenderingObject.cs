namespace VisualNovelEngine.Engine.Game.Interface
{
    /// <summary>
    /// Interface for objects that are rendered permanently on the screen.
    /// </summary>
    public interface IRenderingObject
    {
        abstract void Render();
        abstract bool Enabled();
    }
}
