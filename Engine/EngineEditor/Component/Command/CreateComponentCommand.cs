using VisualNovelEngine.Engine.EngineEditor.Interface;
using Raylib_cs;
using TemplateGame.Component;
using VisualNovelEngine.Engine.EngineEditor.Component;
using TemplateGame.Interface;

namespace VisualNovelEngine.Engine.EngineEditor.Component.Command
{
    /// <summary>
    /// Represents an empty command.
    /// </summary>
    public class CreateComponentCommand : ICommand
    {
        public enum RenderingObjectType
        {
            Sprite
        }
        private RenderingObjectType RenderableObjectType;
        private readonly Editor Editor;
        private Component Component;

        public CreateComponentCommand(Editor editor, RenderingObjectType componentType)
        {
            Editor = editor;
            RenderableObjectType = componentType;
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        public void Execute()
        {
            var id = Editor.GenerateID();
            switch (RenderableObjectType)
            {
                case RenderingObjectType.Sprite:

                    Component = new Component(id, Editor, null, $"New Sprite({id})", Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, Editor.BaseColor, Editor.BorderColor, Editor.HoverColor, Editor.HoverColor, new Sprite("NullPath"));
                    break;
            }
            Editor.ComponentList.Add(Component);
        }
    }
}