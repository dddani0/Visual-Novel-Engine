namespace VisualNovelEngine.Engine.EngineEditor.Component
{
    public class IDGenerator
    {
        public int ID { get; private set; }
        public IDGenerator()
        {
            ID = 0;
        }
        public int GenerateID() => ID++;
    }
}