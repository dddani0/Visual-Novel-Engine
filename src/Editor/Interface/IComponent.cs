namespace Editor.Interface
{
    /// <summary>
    /// Represents a component.
    /// </summary>
    interface IComponent
    {
        void OnCreate();
        void OnUpdate();
        void OnDestroy();
    }
}