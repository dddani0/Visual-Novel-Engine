using EngineEditor.Interface;
using Raylib_cs;

namespace EngineEditor.Component
{
    public class Editor : IEditor
    {
        private Group Toolbar { get; set; }
        internal List<Group> ComponentGroupList { get; set; } = [];
        internal List<IDynamicComponent> ComponentList { get; set; } = [];
        public Editor()
        {
            Toolbar = new Group(100, 100, 70, 70, 5, Color.Red, Color.Black, Color.Gray, GroupType.SolidColor, []);
            Toolbar.AddComponent(new Component(this, Toolbar, "Button", 100, 100, 70, 70, 5, Color.Red, Color.Black, Color.Gray, Color.DarkGray));
            Toolbar.AddComponent(new Component(this, Toolbar, "Butts", 100, 100, 70, 70, 5, Color.Red, Color.Black, Color.Gray, Color.DarkGray));
            Toolbar.AddComponent(new Component(this, Toolbar, "Butts", 100, 100, 70, 70, 5, Color.Red, Color.Black, Color.Gray, Color.DarkGray));
            Toolbar.AddComponent(new Component(this, Toolbar, "Butts", 100, 100, 70, 70, 5, Color.Red, Color.Black, Color.Gray, Color.DarkGray));
            Toolbar.AddComponent(new Component(this, Toolbar, "Butts", 100, 100, 70, 70, 5, Color.Red, Color.Black, Color.Gray, Color.DarkGray));
            Toolbar.AddComponent(new Component(this, Toolbar, "Butts", 100, 100, 70, 70, 5, Color.Red, Color.Black, Color.Gray, Color.DarkGray));
            Toolbar.AddComponent(new Component(this, Toolbar, "Butts", 100, 100, 70, 70, 5, Color.Red, Color.Black, Color.Gray, Color.DarkGray));
            Toolbar.AddComponent(new Component(this, Toolbar, "Butts", 100, 100, 70, 70, 5, Color.Red, Color.Black, Color.Gray, Color.DarkGray));
            Toolbar.AddComponent(new Component(this, Toolbar, "Butts", 100, 100, 70, 70, 5, Color.Red, Color.Black, Color.Gray, Color.DarkGray));
            Toolbar.AddComponent(new Component(this, Toolbar, "Butts", 100, 100, 70, 70, 5, Color.Red, Color.Black, Color.Gray, Color.DarkGray));
            Toolbar.AddComponent(new Component(this, Toolbar, "Butts", 100, 100, 70, 70, 5, Color.Red, Color.Black, Color.Gray, Color.DarkGray));
        }
        public void Build()
        {

        }

        public void Save()
        {

        }

        public void Update()
        {
            Toolbar.Show();
            ComponentGroupList.ForEach(component => component.Show());
            Raylib.ClearBackground(Color.LightGray);
            //Only render each component from the component list if the component is not in a group
            foreach (var component in ComponentList)
            {
                if (component.IsInGroup() is true) continue;
                IComponent castedComponent = (IComponent)component;
                castedComponent.Render();
            }
        }
    }
}