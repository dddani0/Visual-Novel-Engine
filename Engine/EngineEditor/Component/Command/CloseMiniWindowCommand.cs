using TemplateGame.Component;
using VisualNovelEngine.Engine.EngineEditor.Interface;

namespace VisualNovelEngine.Engine.EngineEditor.Component.Command
{
    public class CloseMiniWindowCommand : ICommand
    {
        private readonly Editor Editor;
        private MiniWindow MiniWindow { get; set; }
        /// <summary>
        /// Constructor for toolbar associated purposes
        /// </summary>
        /// <param name="editor"></param>
        /// <param name="buttonName"></param>
        /// <param name="buttons"></param>
        public CloseMiniWindowCommand(Editor editor, MiniWindow miniWindow)
        {
            Editor = editor;
            MiniWindow = miniWindow;
        }

        public void Execute()
        {
            //Save variables
            if (MiniWindow.HasVariableComponent is true)
            {
                for (int i = 0; i < Editor.GameVariables.Count; i++)
                {
                    Variable variable = Editor.GameVariables[i];
                    TextField nameTextFiled = (TextField)MiniWindow.VariableComponentList[i * 3];
                    TextField valueTextFiled = (TextField)MiniWindow.VariableComponentList[i * 3 + 1];
                    TextField typeTextFiled = (TextField)MiniWindow.VariableComponentList[i * 3 + 2];
                    variable.Name = nameTextFiled.Text;
                    variable.Value = valueTextFiled.Text;
                    variable.Type = (VariableType)Enum.Parse(typeof(VariableType), typeTextFiled.Text);
                }
            }
            Editor.MiniWindow.Remove(MiniWindow);
        }
    }
}