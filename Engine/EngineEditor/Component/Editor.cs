using System.Numerics;
using EngineEditor.Component.Command;
using EngineEditor.Interface;
using Raylib_cs;

namespace EngineEditor.Component
{
    public class Editor : IEditor
    {
        private Group Toolbar { get; set; }
        internal List<Group> ComponentGroupList { get; set; } = [];
        internal List<IDinamicComponent> ComponentList { get; set; } = [];
        public Editor()
        {
            IDGenerator iDGenerator = new();
            var CreateButton = new Button(this, 100, 100, "Create", 100, 50, 5, Color.Red, Color.Black, Color.Gray, null);
            CreateButton.AddCommand(new ShowSideWindowCommand(this, CreateButton, new Vector2(0, 75), []));
            Toolbar = new Group(100, 100, 70, 70, 5, Color.Red, Color.Black, Color.Gray, GroupType.SolidColor, 1, [CreateButton]);
            ComponentGroupList.Add(Toolbar);
        }
        public void Build()
        {

        }

        public void Save()
        {

        }

        public void Update()
        {
            Toolbar.Update();
            ComponentGroupList.ForEach(component => component.Update());
            Raylib.ClearBackground(Color.Gray);
            //Only render each component from the component list if the component is not in a group
            for (int i = 0; i < ComponentList.Count; i++)
            {
                IDinamicComponent? component = ComponentList[i];
                if (component.IsInGroup() is true) continue;
                IComponent castedComponent = (IComponent)component;
                castedComponent.Render();
            }
        }
    }
}