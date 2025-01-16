using EngineEditor.Interface;
using Raylib_cs;

namespace EngineEditor.Component
{
    class Editor : IEditor
    {
        List<ITool> Toolbar { get; set; } = [];
        List<IComponent> Components { get; set; } = [];

        public Editor()
        {
        }
        public void Build()
        {

        }

        public void Save()
        {

        }

        public void Update()
        {
            Toolbar.ForEach(tool => tool.Render());
            Components.ForEach(component => component.Render());
        }
    }
}