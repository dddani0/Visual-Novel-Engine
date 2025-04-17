using VisualNovelEngine.Engine.Editor.Interface;
using Raylib_cs;
using VisualNovelEngine.Engine.Game.Component;
using VisualNovelEngine.Engine.Game.Interface;
using VisualNovelEngine.Engine.Game.Component.Action;

namespace VisualNovelEngine.Engine.Editor.Component.Command
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
        /// <summary>
        /// Creates a new instance of <see cref="CreateComponentCommand"/>.
        /// </summary>
        /// <param name="editor"></param>
        /// <param name="componentType"></param>
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
                    Component staticSpriteBlockComponent = new(id, Editor, null, $"New Block({id})", Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, Editor.BaseColor, Editor.BorderColor, Editor.HoverColor, Editor.HoverColor, staticSpriteBlock)
                    {
                        IsObjectStatic = true
                    }; ;
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
                    VisualNovelEngine.Engine.Game.Component.TextField textField = new(textFieldBlock, 0, 0, 100, 100, 1, 0, 0, "New TextField", Raylib.GetFontDefault(), false, true, Color.Black, Color.Black);
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
                    //Create button action
                    IButtonAction buttonAction = new VisualNovelEngine.Engine.Game.Component.Action.NativeLoadSceneAction(Editor.Game, 0);
                    //Add to timeline
                    Editor.ActiveScene.Timeline.AddAction((IAction)buttonAction);
                    //Create button
                    VisualNovelEngine.Engine.Game.Component.Button button = new(Editor.Game, buttonBlock, Raylib.GetFontDefault(), 0, 0, 1, 250, 250, "Text", Color.Black, Color.Black, Color.Black, Color.Black, buttonAction);
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
                    //Create static Button action
                    var staticButtonAction = new VisualNovelEngine.Engine.Game.Component.Action.TimelineIndependent.SetVariableValueAction(Editor.Game, "VariableName", Editor.Game.GameImport, 0);
                    //Add to timeline
                    Editor.ActiveScene.Timeline.AddTimelineIndependentAction(staticButtonAction);
                    //Create static button
                    VisualNovelEngine.Engine.Game.Component.Button staticButton = new(Editor.Game, staticButtonBlock, Raylib.GetFontDefault(), 0, 0, 1, 250, 250, "Text", Color.Black, Color.Black, Color.Black, Color.Black, staticButtonAction);
                    //Assign block to static button
                    staticButtonBlock.Component = staticButton;
                    //Create block component
                    id = Editor.GenerateID();
                    Component staticButtonBlockComponent = new(id, Editor, null, $"Button Block({id})", Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, Editor.BaseColor, Editor.BorderColor, Editor.HoverColor, Editor.HoverColor, staticButtonBlock)
                    {
                        IsObjectStatic = true
                    };
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
                    //Create slider action
                    ISettingsAction sliderAction = new VisualNovelEngine.Engine.Game.Component.Action.TimelineIndependent.SetVariableValueAction(Editor.Game, "Variable name", Editor.Game.GameImport, 0);
                    //Add to timeline
                    Editor.ActiveScene.Timeline.AddTimelineIndependentAction(sliderAction);
                    //Create slider
                    Slider slider = new(sliderBlock, 0, 0, 1, 250, 250, 25, Color.Black, Color.Black, Color.Black, sliderAction);
                    //Assign Slider to block component
                    sliderBlock.Component = slider;
                    //Create block component
                    id = Editor.GenerateID();
                    Component sliderBlockComponent = new(id, Editor, null, $"Slider Block({id})", Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, Editor.BaseColor, Editor.BorderColor, Editor.HoverColor, Editor.HoverColor, sliderBlock)
                    {
                        IsObjectStatic = true
                    };
                    //Create slider component
                    id = Editor.GenerateID();
                    Component = new Component(id, Editor, null, $"New Slider({id})", Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, Editor.BaseColor, Editor.BorderColor, Editor.HoverColor, Editor.HoverColor, slider)
                    {
                        IsObjectStatic = true
                    };
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
                    Component dropBoxBlockComponent = new(id, Editor, null, $"DropBox Static Block({id})", Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, Editor.BaseColor, Editor.BorderColor, Editor.HoverColor, Editor.HoverColor, dropBoxBlock)
                    {
                        IsObjectStatic = true
                    };
                    //Create dropbox component
                    id = Editor.GenerateID();
                    Component = new Component(id, Editor, null, $"New DropBox({id})", Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, Editor.BaseColor, Editor.BorderColor, Editor.HoverColor, Editor.HoverColor, dropBox)
                    {
                        IsObjectStatic = true
                    };
                    //Add block component to scene
                    Editor.ActiveScene.ComponentList.Add(dropBoxBlockComponent);
                    break;
                case RenderingObjectType.InputField:
                    //Create block for inputfield
                    Block inputFieldBlock = new(0, 0, null, id);
                    //Create inputfield action
                    IButtonAction inputFieldAction = new VisualNovelEngine.Engine.Game.Component.Action.NativeLoadSceneAction(Editor.Game, 0);
                    //Add to timeline
                    Editor.ActiveScene.Timeline.AddAction((IAction)inputFieldAction);
                    //Create inputfield
                    InputField inputField = new(Editor.Game, inputFieldBlock, 0, 0, 250, 200, 250, "Placeholder", "Ok", Color.Black, Color.Black, Color.Black, Color.Black, inputFieldAction);
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
                    //Create static inputfield action
                    ISettingsAction staticInputFieldAction = new VisualNovelEngine.Engine.Game.Component.Action.TimelineIndependent.SetVariableValueAction(Editor.Game, "VariableName", Editor.Game.GameImport, 0);
                    //Add to timeline
                    Editor.ActiveScene.Timeline.AddTimelineIndependentAction(staticInputFieldAction);
                    //Create inputfield
                    InputField staticInputField = new(Editor.Game, staticInputFieldBlock, 0, 0, 250, 200, 250, "Placeholder", "Ok", Color.Black, Color.Black, Color.Black, Color.Black, staticInputFieldAction);
                    //Assign InputField to block component
                    staticInputFieldBlock.Component = staticInputField;
                    //Create block component
                    id = Editor.GenerateID();
                    Component staticInputFieldBlockComponent = new(id, Editor, null, $"InputField Block({id})", Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, Editor.BaseColor, Editor.BorderColor, Editor.HoverColor, Editor.HoverColor, staticInputFieldBlock)
                    {
                        IsObjectStatic = true
                    };
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
                    //Create toggle action
                    ISettingsAction toggleAction = new VisualNovelEngine.Engine.Game.Component.Action.TimelineIndependent.SetVariableValueAction(Editor.Game, "Variable name", Editor.Game.GameImport, 0);
                    //Add to timeline
                    Editor.ActiveScene.Timeline.AddTimelineIndependentAction(toggleAction);
                    //Create toggle
                    Toggle toggle = new(toggleBlock, 0, 0, 50, 55, "Toggled", false, Color.Black, Color.Black, Color.White, toggleAction);
                    //Assign toggle to block component
                    toggleBlock.Component = toggle;
                    //Create block component
                    id = Editor.GenerateID();
                    Component toggleBlockComponent = new(id, Editor, null, $"Toggle Block({id})", Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, Editor.BaseColor, Editor.BorderColor, Editor.HoverColor, Editor.HoverColor, toggleBlock)
                    {
                        IsObjectStatic = true
                    };
                    //Create toggle component
                    id = Editor.GenerateID();
                    Component = new Component(id, Editor, null, $"New Toggle({id})", Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, Editor.BaseColor, Editor.BorderColor, Editor.HoverColor, Editor.HoverColor, toggle)
                    {
                        IsObjectStatic = true
                    };
                    //Add block component to scene
                    Editor.ActiveScene.ComponentList.Add(toggleBlockComponent);
                    break;
            }
            Editor.ActiveScene.ComponentList.Add(Component);
        }
    }
}