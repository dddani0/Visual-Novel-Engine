using VisualNovelEngine.Engine.EngineEditor.Interface;

namespace VisualNovelEngine.Engine.EngineEditor.Component
{
    public class Scene : IWindow
    {
        private Editor Editor { get; set; }
        private readonly int ID;
        string Name { get; set; }
        internal bool IsActive { get; set; } = true;
        internal Timeline Timeline { get; set; }
        internal List<IDinamicComponent> ComponentList { get; set; } = [];
        internal List<Group> ComponentGroupList { get; set; } = [];
        internal InspectorWindow InspectorWindow { get; set; }

        public Scene(Editor editor, string sceneName, IDinamicComponent[] components, Group[] groups)
        {
            Editor = editor;
            ID = Editor.GenerateID();
            Name = sceneName;
            Timeline = new();
            InspectorWindow = new(Editor, 0, 0, 1, null);
        }

        internal void Update()
        {
            if (IsActive is false) return;
            Show();
            InspectorWindow.Show();
        }

        public void Show()
        {
            for (int i = 0; i < ComponentGroupList.Count; i++)
            {
                ComponentGroupList[i].Update();
            }
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