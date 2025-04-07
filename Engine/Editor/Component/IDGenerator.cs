namespace VisualNovelEngine.Engine.Editor.Component
{
    /// <summary>
    /// Generate a unique integer value.
    /// </summary>
    public class IDGenerator
    {
        /// <summary>
        /// The current ID.
        /// </summary>
        public int ID { get; private set; }
        /// <summary>
        /// Creates a new ID generator.
        /// </summary>
        /// <param name="startingIndex"></param>
        public IDGenerator(int startingIndex)
        {
            ID = startingIndex;
        }
        /// <summary>
        /// Generates a new ID.
        /// </summary>
        /// <returns></returns>
        public int GenerateID() => ID++;
    }
}