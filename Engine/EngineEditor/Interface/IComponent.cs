namespace VisualNovelEngine.Engine.EngineEditor.Interface
{
    /// <summary>
    /// Represents a generic component.
    /// </summary>
    public interface IComponent
    {
        int XPosition { get; set; }
        int YPosition { get; set; }
        void Render();
        void Update();
    }
}