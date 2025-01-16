namespace EngineEditor.Interface
{
    /// <summary>
    /// Represents a component.
    /// </summary>
    interface IComponent : ITool
    {
        void Edit();
        void Scale();
        void Destroy();
    }
}