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
            Sprite,
            TextField,
            TextBox,
            Menu,
            Block
        }
        internal RenderingObjectType RenderableObjectType;
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
                case RenderingObjectType.TextBox:
                    Component = new Component(id, Editor, null, $"New TextBox({id})", Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, Editor.BaseColor, Editor.BorderColor, Editor.HoverColor, Editor.HoverColor, TextBox.CreateNewTextBox(Editor.Game, 1, Raylib.GetFontDefault(), Color.Black, Color.Black, TextBox.PositionType.defaultPosition, 1, 1, false, "", []));
                    break;
                case RenderingObjectType.Menu:
                    Component = new Component(id, Editor, null, $"New Menu({id})", Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, Editor.BaseColor, Editor.BorderColor, Editor.HoverColor, Editor.HoverColor, new Menu(Editor.Game, Editor.GenerateID(), 0, 0, 10, 10, false, [], Color.Black, Color.Black));
                    break;
                case RenderingObjectType.Block:
                    Component = new Component(id, Editor, null, $"New Block({id})", Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, Editor.BaseColor, Editor.BorderColor, Editor.HoverColor, Editor.HoverColor, new Block(0, 0, null, Editor.GenerateID()));
                    break;
            }
            Editor.ActiveScene.ComponentList.Add(Component);
        }
    }
}