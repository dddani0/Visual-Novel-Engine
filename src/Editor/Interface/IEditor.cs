namespace Editor.Interface
{
    /// <summary>
    /// Represents an editor.
    /// </summary>
    interface IEditor
    {
        void Save();
        void Build();
        void Update();
    }
}