namespace VisualNovelEngine.Engine.EngineEditor.Component
{
    public class IDGenerator
    {
        public int ID { get; private set; }
        public IDGenerator(int startingIndex)
        {
            ID = startingIndex;
        }
        public int CurrentID() => ID;
        public int GenerateID() => ID++;
    }
}