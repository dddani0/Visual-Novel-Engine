using Raylib_cs;
using TemplateGame.Interface;
using VisualNovelEngine.Engine.EngineEditor.Interface;

namespace VisualNovelEngine.Engine.EngineEditor.Component
{
    public class Timeline : IWindow
    {
        internal int XPosition { get; set; }
        internal int YPosition { get; set; }
        internal List<IEvent> Events { get; set; } = [];

        public void Show()
        {
            for (int i = 0; i < Events.Count; i++)
            {
                Raylib.DrawRectangle(XPosition, YPosition, 200, 20, Color.White);
            }
        }

        internal void AddEvent(IEvent action)
        {
            Events.Add(action);
        }

    }
}