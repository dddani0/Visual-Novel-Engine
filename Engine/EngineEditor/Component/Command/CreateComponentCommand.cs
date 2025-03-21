using VisualNovelEngine.Engine.EngineEditor.Interface;
using Raylib_cs;
using TemplateGame.Component;
using TemplateGame.Interface;
using TemplateGame.Component.Action;

namespace VisualNovelEngine.Engine.EngineEditor.Component.Command
{
    /// <summary>
    /// Create a component command.
    /// </summary>
    public class CreateComponentCommand : ICommand
    {
        public enum RenderingObjectType
        {
            Sprite,
            staticSprite,
            TextBox,
            TextField,
            Menu,
            StaticMenu,
            Block,
            StaticBlock,
            Button,
            StaticButton,
            Slider,
            DropBox,
            InputField,
            StaticInputField,
            Toggle
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
                    Sprite sprite = new("Empty path");
                    Component = new Component(id, Editor, null, $"New Sprite({id})", Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, Editor.BaseColor, Editor.BorderColor, Editor.HoverColor, Editor.HoverColor, sprite);
                    break;
                case RenderingObjectType.staticSprite:
                    //Create block for static sprite with the sprite attached
                    Block staticSpriteBlock = new(0, 0, null, id);
                    //Create sprite with no block attached
                    id = Editor.GenerateID();
                    Sprite staticSprite = new($"Empty path({id})", staticSpriteBlock, 0, 0);
                    //Assign block to sprite
                    staticSpriteBlock.Component = staticSprite;
                    //Create block component
                    id = Editor.GenerateID();
                    Component staticSpriteBlockComponent = new(id, Editor, null, $"New Block({id})", Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, Editor.BaseColor, Editor.BorderColor, Editor.HoverColor, Editor.HoverColor, staticSpriteBlock);
                    //Create sprite component
                    id = Editor.GenerateID();
                    Component = new Component(id, Editor, null, $"New Sprite({id})", Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, Editor.BaseColor, Editor.BorderColor, Editor.HoverColor, Editor.HoverColor, staticSprite)
                    {
                        IsObjectStatic = true
                    };
                    //Add block component to scene
                    Editor.ActiveScene.ComponentList.Add(staticSpriteBlockComponent);
                    break;
                case RenderingObjectType.TextBox:
                    TextBox textBox = TextBox.CreateNewTextBox(Editor.Game, 1, Raylib.GetFontDefault(), Color.Black, Color.Black, TextBox.PositionType.defaultPosition, 1, 1, false, "", []);
                    Component = new Component(id, Editor, null, $"New TextBox({id})", Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, Editor.BaseColor, Editor.BorderColor, Editor.HoverColor, Editor.HoverColor, textBox);
                    break;
                case RenderingObjectType.TextField:
                    //Create parent block for TextField
                    Block textFieldBlock = new(0, 0, null, id);
                    //Create Textfield
                    TemplateGame.Component.TextField textField = new(textFieldBlock, 0, 0, 100, 100, 1, 0, 0, "New TextField", Raylib.GetFontDefault(), false, true, Color.Black, Color.Black);
                    //Assign TextField to Parent block as Component
                    textFieldBlock.Component = textField;
                    //Create Component for TextField's block
                    id = Editor.GenerateID();
                    Component textFieldBlockComponent = new(id, Editor, null, $"Textfield Block({id})", Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, Editor.BaseColor, Editor.BorderColor, Editor.HoverColor, Editor.HoverColor, textFieldBlock);
                    //Create Component for TextField
                    id = Editor.GenerateID();
                    Component = new Component(id, Editor, null, $"New TextField({id})", Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, Editor.BaseColor, Editor.BorderColor, Editor.HoverColor, Editor.HoverColor, textField);
                    //Add the component for textfield's block to scene
                    Editor.ActiveScene.ComponentList.Add(textFieldBlockComponent);
                    break;
                case RenderingObjectType.Menu:
                    Menu menu = new(Editor.Game, id, 0, 0, 10, 10, false, [], Color.Black, Color.Black);
                    id = Editor.GenerateID();
                    Component = new Component(id, Editor, null, $"New Menu({id})", Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, Editor.BaseColor, Editor.BorderColor, Editor.HoverColor, Editor.HoverColor, menu);
                    break;
                case RenderingObjectType.Block:
                    Block block = new(0, 0, null, id);
                    id = Editor.GenerateID();
                    Component = new Component(id, Editor, null, $"Empty Block({id})", Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, Editor.BaseColor, Editor.BorderColor, Editor.HoverColor, Editor.HoverColor, block);
                    break;
                case RenderingObjectType.StaticBlock:
                    Block staticBlock = new(0, 0, null, id);
                    id = Editor.GenerateID();
                    Component = new Component(id, Editor, null, $"New Static Block({id})", Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, Editor.BaseColor, Editor.BorderColor, Editor.HoverColor, Editor.HoverColor, staticBlock)
                    {
                        IsObjectStatic = true
                    };
                    break;
                case RenderingObjectType.StaticMenu:
                    Menu staticMenu = new(Editor.Game, id, 0, 0, 10, 10, false, [], Color.Black, Color.Black);
                    id = Editor.GenerateID();
                    Component = new Component(id, Editor, null, $"New static Menu({id})", Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, Editor.BaseColor, Editor.BorderColor, Editor.HoverColor, Editor.HoverColor, staticMenu)
                    {
                        IsObjectStatic = true
                    };
                    break;
                case RenderingObjectType.Button:
                    //Create block for button
                    Block buttonBlock = new(0, 0, null, id);
                    //Create button
                    TemplateGame.Component.Button button = new(Editor.Game, buttonBlock, Raylib.GetFontDefault(), 0, 0, 1, 250, 250, "Text", Color.Black, Color.Black, Color.Black, Color.Black, (IButtonEvent)new TemplateGame.Component.Action.NativeLoadSceneAction(Editor.Game, 0));
                    //Assign block to button
                    buttonBlock.Component = button;
                    //Create Component for button's block
                    id = Editor.GenerateID();
                    Component buttonBlockComponent = new(id, Editor, null, $"Button Block({id})", Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, Editor.BaseColor, Editor.BorderColor, Editor.HoverColor, Editor.HoverColor, buttonBlock);
                    //Create button component
                    id = Editor.GenerateID();
                    Component = new Component(id, Editor, null, $"New Button({id})", Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, Editor.BaseColor, Editor.BorderColor, Editor.HoverColor, Editor.HoverColor, button);
                    //Add block component to scene
                    Editor.ActiveScene.ComponentList.Add(buttonBlockComponent);
                    break;
                case RenderingObjectType.StaticButton:
                    //Create block for static button
                    Block staticButtonBlock = new(0, 0, null, id);
                    //Create static button
                    TemplateGame.Component.Button staticButton = new(Editor.Game, staticButtonBlock, Raylib.GetFontDefault(), 0, 0, 1, 250, 250, "Text", Color.Black, Color.Black, Color.Black, Color.Black, new TemplateGame.Component.Action.TimelineIndependent.SetVariableValueAction(Editor.Game, "VariableName", Editor.Game.GameImport, 0));
                    //Assign block to static button
                    staticButtonBlock.Component = staticButton;
                    //Create block component
                    id = Editor.GenerateID();
                    Component staticButtonBlockComponent = new(id, Editor, null, $"Button Block({id})", Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, Editor.BaseColor, Editor.BorderColor, Editor.HoverColor, Editor.HoverColor, staticButtonBlock);
                    //Create button component
                    Component = new Component(id, Editor, null, $"New Button({id})", Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, Editor.BaseColor, Editor.BorderColor, Editor.HoverColor, Editor.HoverColor, staticButton)
                    {
                        IsObjectStatic = true
                    };
                    //Add block component to scene
                    Editor.ActiveScene.ComponentList.Add(staticButtonBlockComponent);
                    break;
                case RenderingObjectType.Slider:
                    //Create block for slider
                    Block sliderBlock = new(0, 0, null, id);
                    //Create slider
                    Slider slider = new(sliderBlock, 0, 0, 1, 250, 250, 25, Color.Black, Color.Black, Color.Black, new TemplateGame.Component.Action.TimelineIndependent.SetVariableValueAction(Editor.Game, "Variable name", Editor.Game.GameImport, 0));
                    //Assign Slider to block component
                    sliderBlock.Component = slider;
                    //Create block component
                    id = Editor.GenerateID();
                    Component sliderBlockComponent = new(id, Editor, null, $"Slider Block({id})", Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, Editor.BaseColor, Editor.BorderColor, Editor.HoverColor, Editor.HoverColor, sliderBlock);
                    //Create slider component
                    id = Editor.GenerateID();
                    Component = new Component(id, Editor, null, $"New Slider({id})", Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, Editor.BaseColor, Editor.BorderColor, Editor.HoverColor, Editor.HoverColor, slider);
                    //Add block component to scene
                    Editor.ActiveScene.ComponentList.Add(sliderBlockComponent);
                    break;
                case RenderingObjectType.DropBox:
                    //Create block for dropbox
                    Block dropBoxBlock = new(0, 0, null, id);
                    //Create dropbox
                    DropBox dropBox = new(dropBoxBlock, 0, 0, 1, 250, [], Color.Black, Color.Black, Color.Black);
                    //Assign dropbox to block component
                    dropBoxBlock.Component = dropBox;
                    //Create block component
                    id = Editor.GenerateID();
                    Component dropBoxBlockComponent = new(id, Editor, null, $"DropBox Static Block({id})", Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, Editor.BaseColor, Editor.BorderColor, Editor.HoverColor, Editor.HoverColor, dropBoxBlock);
                    //Create dropbox component
                    id = Editor.GenerateID();
                    Component = new Component(id, Editor, null, $"New DropBox({id})", Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, Editor.BaseColor, Editor.BorderColor, Editor.HoverColor, Editor.HoverColor, dropBox);
                    //Add block component to scene
                    Editor.ActiveScene.ComponentList.Add(dropBoxBlockComponent);
                    break;
                case RenderingObjectType.InputField:
                    //Create block for inputfield
                    Block inputFieldBlock = new(0, 0, null, id);
                    //Create inputfield
                    InputField inputField = new(Editor.Game, inputFieldBlock, 0, 0, 250, 200, 250, "Placeholder", "Ok", Color.Black, Color.Black, Color.Black, Color.Black, (IButtonEvent)new TemplateGame.Component.Action.NativeLoadSceneAction(Editor.Game, 0));
                    //Assign InputField to block component
                    inputFieldBlock.Component = inputField;
                    //Create block component
                    id = Editor.GenerateID();
                    Component inputFieldBlockComponent = new(id, Editor, null, $"InputField Block({id})", Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, Editor.BaseColor, Editor.BorderColor, Editor.HoverColor, Editor.HoverColor, inputFieldBlock);
                    //Create inputfield component
                    id = Editor.GenerateID();
                    Component = new Component(id, Editor, null, $"New InputField({id})", Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, Editor.BaseColor, Editor.BorderColor, Editor.HoverColor, Editor.HoverColor, inputField);
                    //Add block component to scene
                    Editor.ActiveScene.ComponentList.Add(inputFieldBlockComponent);
                    break;
                case RenderingObjectType.StaticInputField:
                    //Create block for static inputfield
                    Block staticInputFieldBlock = new(0, 0, null, id);
                    //Create inputfield
                    InputField staticInputField = new(Editor.Game, staticInputFieldBlock, 0, 0, 250, 200, 250, "Placeholder", "Ok", Color.Black, Color.Black, Color.Black, Color.Black, new TemplateGame.Component.Action.TimelineIndependent.SetVariableValueAction(Editor.Game, "VariableName", Editor.Game.GameImport, 0));
                    //Assign InputField to block component
                    staticInputFieldBlock.Component = staticInputField;
                    //Create block component
                    id = Editor.GenerateID();
                    Component staticInputFieldBlockComponent = new(id, Editor, null, $"InputField Block({id})", Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, Editor.BaseColor, Editor.BorderColor, Editor.HoverColor, Editor.HoverColor, staticInputFieldBlock);
                    //Create inputfield component
                    id = Editor.GenerateID();
                    Component = new Component(id, Editor, null, $"New InputField({id})", Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, Editor.BaseColor, Editor.BorderColor, Editor.HoverColor, Editor.HoverColor, staticInputField)
                    {
                        IsObjectStatic = true
                    };
                    //Add block component to scene
                    Editor.ActiveScene.ComponentList.Add(staticInputFieldBlockComponent);
                    break;
                case RenderingObjectType.Toggle:
                    //Create block for toggle
                    Block toggleBlock = new(0, 0, null, id);
                    //Create toggle
                    Toggle toggle = new(toggleBlock, 0, 0, 50, 55, "Toggled", false, Color.Black, Color.Black, Color.White, new TemplateGame.Component.Action.TimelineIndependent.SetVariableValueAction(Editor.Game, "Variable name", Editor.Game.GameImport, 0));
                    //Assign toggle to block component
                    toggleBlock.Component = toggle;
                    //Create block component
                    id = Editor.GenerateID();
                    Component toggleBlockComponent = new(id, Editor, null, $"Toggle Block({id})", Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, Editor.BaseColor, Editor.BorderColor, Editor.HoverColor, Editor.HoverColor, toggleBlock);
                    //Create toggle component
                    id = Editor.GenerateID();
                    Component = new Component(id, Editor, null, $"New Toggle({id})", Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, Editor.BaseColor, Editor.BorderColor, Editor.HoverColor, Editor.HoverColor, toggle);
                    //Add block component to scene
                    Editor.ActiveScene.ComponentList.Add(toggleBlockComponent);
                    break;
            }
            Editor.ActiveScene.ComponentList.Add(Component);
        }
    }
}