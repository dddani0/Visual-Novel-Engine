using System.Linq;
using VisualNovelEngine.Engine.Editor.Interface;

namespace VisualNovelEngine.Engine.Editor.Component.Command
{
    /// <summary>
    /// Inspects a component
    /// </summary>
    public class InspectComponentCommand : ICommand
    {
        readonly Editor Editor;
        private InspectorWindow InspectorWindow;
        int ComponentID { get; set; }
        public InspectComponentCommand(Editor editor, int componentID)
        {
            Editor = editor;
            ComponentID = componentID;
        }

        public void Execute()
        {
            InspectorWindow = Editor.ActiveScene.InspectorWindow;
            for (int i = 0; i < Editor.ActiveScene.ComponentList.Count; i++)
            {
                Component component = (Component)Editor.ActiveScene.ComponentList[i];
                if (component.ID != ComponentID) continue;
                InspectorWindow.ActiveComponent = component;
                return;
            }
        }
    }
}