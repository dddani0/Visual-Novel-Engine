namespace TemplateGame.Interface
{
    /// <summary>
    /// Interface for objects that are rendered permanently on the screen.
    /// </summary>
    public interface IPermanentRenderingObject
    {
        abstract void Render();
        abstract bool Enabled();
    }
}
