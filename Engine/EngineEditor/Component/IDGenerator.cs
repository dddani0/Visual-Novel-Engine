namespace EngineEditor.Component
{
    public class IDGenerator
    {
        long ID { get; set; } = 0;
        public int GenerateID() => (int)ID++;
    }
}