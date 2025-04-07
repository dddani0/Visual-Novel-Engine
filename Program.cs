namespace VisualNovelEngine
{
    class Program
    {
        internal static VisualNovelEngine.Engine.Component.Engine Engine { get; set; }
        public static void Main()
        {
            Engine = new();
            Engine.Process();
        }
    }
}