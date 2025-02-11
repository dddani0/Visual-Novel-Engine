using VisualNovelEngine.Engine.EngineEditor.Interface;

namespace VisualNovelEngine.Engine.EngineEditor.Component.Command
{
    public class CloseInspectorCommand : ICommand
    {
        Editor Editor { get; set; }
        private InspectorWindow InspectorWindow { get; set; }
        public CloseInspectorCommand(Editor editor, InspectorWindow inspectorWindow)
        {
            Editor = editor;
            InspectorWindow = inspectorWindow;
        }
        public void Execute()
        {
            InspectorWindow.DropActiveComponent();
            Editor.ActiveScene.InspectorWindow = null;
        }
    }
}