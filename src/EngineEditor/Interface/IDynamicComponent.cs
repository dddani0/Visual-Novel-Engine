namespace EngineEditor.Interface
{
    /// <summary>
    /// Represents a dynamic component.
    /// </summary>
    public interface IDynamicComponent
    {
        void Move();
        bool IsInGroup();
    }
}