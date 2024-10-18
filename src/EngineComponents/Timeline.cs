namespace EngineComponents
{
    class Timeline
    {
        /// <summary>
        /// Timeline contains the list of actions (Sprite change, textbox placement, etc.), which plays out during a scene.
        /// </summary>
        int ActionIndex { get; set; }
        int MaximumActionCount { get; set; }
    }
}