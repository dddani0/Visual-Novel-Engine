using Namespace;
using Raylib_cs;
using TemplateGame.Component;
using TemplateGame.Component.Action;
using TemplateGame.Component.Action.TimelineDependent;
using TemplateGame.Component.Action.TimelineIndependent;
using TemplateGame.Interface;
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
            switch (InspectorWindow.ActiveAction is not null)
            {
                case true:
                    foreach (IAction action in Editor.ActiveScene.Timeline.Events.Cast<IAction>())
                    {
                        if (action == InspectorWindow.ActiveAction)
                        {
                            switch (action)
                            {
                                case CreateMenuAction createMenuAction:
                                    //Save menu
                                    if ((InspectorWindow.ComponentList[5] as DropDown).Button.Component == null) break;
                                    if (((Component)(InspectorWindow.ComponentList[5] as DropDown).Button.Component).RenderingObject == null) break;
                                    createMenuAction.Menu = ((Component)(InspectorWindow.ComponentList[5] as DropDown).Button.Component).RenderingObject as Menu;
                                    break;
                                case LoadSceneAction loadSceneAction:
                                    //Save variable's name
                                    loadSceneAction.TriggerVariableName = (InspectorWindow.ComponentList[5] as TextField).Text;
                                    //Save loading scene's id
                                    loadSceneAction.sceneID = int.Parse((InspectorWindow.ComponentList[7] as TextField).Text);
                                    break;
                                case NativeLoadSceneAction nativeLoadSceneAction:
                                    //Save scene id
                                    nativeLoadSceneAction.sceneID = int.Parse((InspectorWindow.ComponentList[5] as TextField).Text);
                                    break;
                                case AddSpriteAction addSpriteAction:
                                    //Save sprite
                                    if ((InspectorWindow.ComponentList[5] as DropDown).Button.Component == null) break;
                                    if (((Component)(InspectorWindow.ComponentList[5] as DropDown).Button.Component).RenderingObject == null) break;
                                    addSpriteAction.sprite = (Sprite)((Component)(InspectorWindow.ComponentList[5] as DropDown).Button.Component).RenderingObject;
                                    break;
                                case ChangeSpriteAction changeSpriteAction:
                                    //Save old sprite
                                    if ((InspectorWindow.ComponentList[5] as DropDown).Button.Component == null) break;
                                    if (((Component)(InspectorWindow.ComponentList[5] as DropDown).Button.Component).RenderingObject == null) break;
                                    changeSpriteAction.sprite = (Sprite)((Component)(InspectorWindow.ComponentList[5] as DropDown).Button.Component).RenderingObject;
                                    //Save new sprite
                                    changeSpriteAction.replacementSprite = new Sprite((InspectorWindow.ComponentList[7] as TextField).Text);
                                    break;
                                case RemoveSpriteAction removeSpriteAction:
                                    //Save removing sprite
                                    if ((InspectorWindow.ComponentList[5] as DropDown).Button.Component == null) break;
                                    if (((Component)(InspectorWindow.ComponentList[5] as DropDown).Button.Component).RenderingObject == null) break;
                                    removeSpriteAction.sprite = (Sprite)((Component)(InspectorWindow.ComponentList[5] as DropDown).Button.Component).RenderingObject;
                                    break;
                                case DecrementVariableAction decrementVariableAction:
                                    //Save variable's name
                                    decrementVariableAction.VariableName = (InspectorWindow.ComponentList[6] as TextField).Text;
                                    //save decrement value
                                    decrementVariableAction.DecrementIntegerValue = int.Parse((InspectorWindow.ComponentList[8] as TextField).Text);
                                    break;
                                case IncrementVariableAction incrementVariableAction:
                                    //Save variable's name
                                    incrementVariableAction.VariableName = (InspectorWindow.ComponentList[6] as TextField).Text;
                                    //Save increment value
                                    incrementVariableAction.IncrementIntegerValue = int.Parse((InspectorWindow.ComponentList[8] as TextField).Text);
                                    break;
                                case SetBoolVariableAction setBoolVariableAction:
                                    //Save variable's name
                                    setBoolVariableAction.VariableName = (InspectorWindow.ComponentList[6] as TextField).Text;
                                    //Save value
                                    setBoolVariableAction.Value = (InspectorWindow.ComponentList[7] as ToggleButton).IsToggled;
                                    break;
                                case SetVariableFalseAction setVariableFalseAction:
                                    //save variable's name
                                    setVariableFalseAction.VariableName = (InspectorWindow.ComponentList[6] as TextField).Text;
                                    break;
                                case SetVariableTrueAction setVariableTrueAction:
                                    //save variable's name
                                    setVariableTrueAction.VariableName = (InspectorWindow.ComponentList[6] as TextField).Text;
                                    break;
                                case TextBoxCreateAction textBoxCreateAction:
                                    //Save textbox
                                    if ((InspectorWindow.ComponentList[5] as DropDown).Button.Component == null) break;
                                    if (((Component)(InspectorWindow.ComponentList[5] as DropDown).Button.Component).RenderingObject == null) break;
                                    textBoxCreateAction.TextBox = (TextBox)((Component)(InspectorWindow.ComponentList[5] as DropDown).Button.Component).RenderingObject;
                                    break;
                                case TintSpriteAction tintSpriteAction:
                                    //Save sprite
                                    if ((InspectorWindow.ComponentList[5] as DropDown).Button.Component == null) break;
                                    if (((Component)(InspectorWindow.ComponentList[5] as DropDown).Button.Component).RenderingObject == null) break;
                                    tintSpriteAction.sprite = (Sprite)((Component)(InspectorWindow.ComponentList[5] as DropDown).Button.Component).RenderingObject;
                                    //Save color
                                    TextField tintSpriteColorTextField = InspectorWindow.ComponentList[7] as TextField;
                                    tintSpriteAction.color = new Color()
                                    {
                                        R = byte.Parse(tintSpriteColorTextField.Text.Split(',')[0]),
                                        G = byte.Parse(tintSpriteColorTextField.Text.Split(',')[1]),
                                        B = byte.Parse(tintSpriteColorTextField.Text.Split(',')[2]),
                                        A = 255
                                    };
                                    break;
                                case ToggleVariableAction toggleVariableAction:
                                    //Save variable's name
                                    toggleVariableAction.VariableName = (InspectorWindow.ComponentList[5] as TextField).Text;
                                    break;
                                case EmptyAction:
                                    //Save empty action, does nothing
                                    break;
                                case SetVariableValueAction setVariableValueAction:
                                    //Save variable's name
                                    setVariableValueAction.VariableName = (InspectorWindow.ComponentList[5] as TextField).Text;
                                    //Save component
                                    if ((InspectorWindow.ComponentList[7] as DropDown).Button.Component == null) break;
                                    if (((Component)(InspectorWindow.ComponentList[7] as DropDown).Button.Component).RenderingObject == null) break;
                                    switch (((Component)(InspectorWindow.ComponentList[7] as DropDown).Button.Component).RenderingObject)
                                    {
                                        case Slider slider:
                                            //Save slider
                                            setVariableValueAction.SliderComponent = slider;
                                            break;
                                        case Toggle toggle:
                                            //Save toggle
                                            setVariableValueAction.ToggleComponent = toggle;
                                            break;
                                        case InputField inputField:
                                            //Save inputfield
                                            setVariableValueAction.InputField = inputField;
                                            break;
                                    }
                                    break;
                                case SwitchStaticMenuAction switchStaticMenuAction:
                                    //Save old menu
                                    if ((InspectorWindow.ComponentList[5] as DropDown).Button.Component == null) break;
                                    if (((Component)(InspectorWindow.ComponentList[5] as DropDown).Button.Component).RenderingObject == null) break;
                                    switchStaticMenuAction.DisablingMenu = (Menu)((Component)(InspectorWindow.ComponentList[5] as DropDown).Button.Component).RenderingObject;
                                    //Save new menu
                                    if ((InspectorWindow.ComponentList[7] as DropDown).Button.Component == null) break;
                                    if (((Component)(InspectorWindow.ComponentList[7] as DropDown).Button.Component).RenderingObject == null) break;
                                    switchStaticMenuAction.EnablingMenu = (Menu)((Component)(InspectorWindow.ComponentList[7] as DropDown).Button.Component).RenderingObject;
                                    break;
                            }
                        }
                    }
                    break;
                case false:
                    foreach (Component component in Editor.ActiveScene.ComponentList.Cast<Component>())
                    {
                        if (component == InspectorWindow.ActiveComponent)
                        {
                            var txtfield = (TextField)InspectorWindow.ComponentList[1];
                            component.Name = txtfield.Text;
                            switch (component.RenderingObject)
                            {
                                case Sprite sprite:
                                    //save sprite path
                                    sprite.Path = (InspectorWindow.ComponentList[5] as TextField).Text;
                                    //save sprite color
                                    TextField spriteColorTextField = InspectorWindow.ComponentList[7] as TextField;
                                    sprite.Color = new Color()
                                    {
                                        R = byte.Parse(spriteColorTextField.Text.Split(',')[0]),
                                        G = byte.Parse(spriteColorTextField.Text.Split(',')[1]),
                                        B = byte.Parse(spriteColorTextField.Text.Split(',')[2]),
                                        A = 255
                                    };
                                    break;
                                case TemplateGame.Component.Button button:
                                    //fetch fields and read out values.
                                    button.Text = (InspectorWindow.ComponentList[5] as TextField).Text;
                                    //Save color
                                    byte[] colorRGB = new byte[3];
                                    TextField ButtonColorTextField = InspectorWindow.ComponentList[7] as TextField;
                                    colorRGB[0] = byte.Parse(ButtonColorTextField.Text.Split(',')[0]);
                                    colorRGB[1] = byte.Parse(ButtonColorTextField.Text.Split(',')[1]);
                                    colorRGB[2] = byte.Parse(ButtonColorTextField.Text.Split(',')[2]);
                                    button.Color = new Color()
                                    {
                                        R = colorRGB[0],
                                        G = colorRGB[1],
                                        B = colorRGB[2],
                                        A = 255
                                    };
                                    //Save border color
                                    byte[] borderColorRGB = new byte[3];
                                    TextField ButtonBorderColorTextField = InspectorWindow.ComponentList[9] as TextField;
                                    colorRGB[0] = byte.Parse(ButtonColorTextField.Text.Split(',')[0]);
                                    colorRGB[1] = byte.Parse(ButtonColorTextField.Text.Split(',')[1]);
                                    colorRGB[2] = byte.Parse(ButtonColorTextField.Text.Split(',')[2]);
                                    button.BorderColor = new Color()
                                    {
                                        R = borderColorRGB[0],
                                        G = borderColorRGB[1],
                                        B = borderColorRGB[2],
                                        A = 255
                                    };
                                    //save hover color
                                    byte[] hoverColorRGB = new byte[3];
                                    TextField ButtonHoverColorTextField = InspectorWindow.ComponentList[11] as TextField;
                                    colorRGB[0] = byte.Parse(ButtonColorTextField.Text.Split(',')[0]);
                                    colorRGB[1] = byte.Parse(ButtonColorTextField.Text.Split(',')[1]);
                                    colorRGB[2] = byte.Parse(ButtonColorTextField.Text.Split(',')[2]);
                                    button.HoverColor = new Color()
                                    {
                                        R = hoverColorRGB[0],
                                        G = hoverColorRGB[1],
                                        B = hoverColorRGB[2],
                                        A = 255
                                    };
                                    break;
                                case TemplateGame.Component.TextField textField:
                                    //save textfield text
                                    textField.Text = (InspectorWindow.ComponentList[5] as TextField).Text;
                                    //save textfield color
                                    TextField textFieldColorTextField = InspectorWindow.ComponentList[7] as TextField;
                                    textField.Color = new Color()
                                    {
                                        R = byte.Parse(textFieldColorTextField.Text.Split(',')[0]),
                                        G = byte.Parse(textFieldColorTextField.Text.Split(',')[1]),
                                        B = byte.Parse(textFieldColorTextField.Text.Split(',')[2]),
                                        A = 255
                                    };
                                    //save textfield border color
                                    TextField textFieldBorderColorTextField = InspectorWindow.ComponentList[9] as TextField;
                                    textField.BorderColor = new Color()
                                    {
                                        R = byte.Parse(textFieldBorderColorTextField.Text.Split(',')[0]),
                                        G = byte.Parse(textFieldBorderColorTextField.Text.Split(',')[1]),
                                        B = byte.Parse(textFieldBorderColorTextField.Text.Split(',')[2]),
                                        A = 255
                                    };
                                    //save textfield wordwrap toggle
                                    ToggleButton textFieldWordWrapToggle = InspectorWindow.ComponentList[10] as ToggleButton;
                                    textField.WordWrap = textFieldWordWrapToggle.IsToggled;
                                    break;
                                case TextBox textBox:
                                    //save textbox title
                                    textBox.Title = (InspectorWindow.ComponentList[5] as TextField).Text;
                                    //Save content
                                    List<string> contentList = [];
                                    for (int i = 7; i < InspectorWindow.ComponentList.Count - 12; i++)
                                    {
                                        IComponent currentComponent = InspectorWindow.ComponentList[i];
                                        if (currentComponent is TextField textField)
                                        {
                                            contentList.Add(textField.Text);
                                        }
                                    }
                                    textBox.Content = [.. contentList];
                                    //Save horizontal margin
                                    textBox.HorizontalTextMargin = int.Parse((InspectorWindow.ComponentList[^12] as TextField).Text);
                                    //Save vertical margin
                                    textBox.VerticalTextMargin = int.Parse((InspectorWindow.ComponentList[^10] as TextField).Text);
                                    //Save CPS
                                    textBox.CPSTextSpeed = double.Parse((InspectorWindow.ComponentList[^8] as TextField).Text);
                                    //save color
                                    byte[] textBoxColorRGB = new byte[3];
                                    TextField textBoxColorTextField = InspectorWindow.ComponentList[^6] as TextField;
                                    textBox.Color = new Color()
                                    {
                                        R = byte.Parse(textBoxColorTextField.Text.Split(',')[0]),
                                        G = byte.Parse(textBoxColorTextField.Text.Split(',')[1]),
                                        B = byte.Parse(textBoxColorTextField.Text.Split(',')[2]),
                                        A = 255
                                    };
                                    //save border color
                                    byte[] textBoxBorderColorRGB = new byte[3];
                                    TextField textBoxBorderColorTextField = InspectorWindow.ComponentList[^4] as TextField;
                                    textBox.BorderColor = new Color()
                                    {
                                        R = byte.Parse(textBoxBorderColorTextField.Text.Split(',')[0]),
                                        G = byte.Parse(textBoxBorderColorTextField.Text.Split(',')[1]),
                                        B = byte.Parse(textBoxBorderColorTextField.Text.Split(',')[2]),
                                        A = 255
                                    };
                                    //Save wordwrap
                                    textBox.WordWrap = (InspectorWindow.ComponentList[^3] as ToggleButton).IsToggled;
                                    //Save textbox position type
                                    textBox.TextBoxPositionType = (InspectorWindow.ComponentList[^1] as DropDown).Button.PositionType;
                                    break;
                                case Menu menu:
                                    //Save position
                                    menu.XPosition = int.Parse((InspectorWindow.ComponentList[6] as TextField).Text);
                                    menu.YPosition = int.Parse((InspectorWindow.ComponentList[8] as TextField).Text);
                                    //Save width
                                    menu.Width = int.Parse((InspectorWindow.ComponentList[10] as TextField).Text);
                                    //Save height
                                    menu.Height = int.Parse((InspectorWindow.ComponentList[12] as TextField).Text);
                                    //Save fullscreen bool
                                    menu.IsFullScreen = (InspectorWindow.ComponentList[13] as ToggleButton).IsToggled;
                                    //Save color
                                    byte[] menuColorRGB = new byte[3];
                                    TextField ColorTextField = InspectorWindow.ComponentList[15] as TextField;
                                    menuColorRGB[0] = byte.Parse(ColorTextField.Text.Split(',')[0]);
                                    menuColorRGB[1] = byte.Parse(ColorTextField.Text.Split(',')[1]);
                                    menuColorRGB[2] = byte.Parse(ColorTextField.Text.Split(',')[2]);
                                    menu.Color = new Color()
                                    {
                                        R = menuColorRGB[0],
                                        G = menuColorRGB[1],
                                        B = menuColorRGB[2],
                                        A = 255
                                    };
                                    //Save border color
                                    byte[] menuBorderColorRGB = new byte[3];
                                    TextField BorderColorTextField = InspectorWindow.ComponentList[17] as TextField;
                                    menuBorderColorRGB[0] = byte.Parse(BorderColorTextField.Text.Split(',')[0]);
                                    menuBorderColorRGB[1] = byte.Parse(BorderColorTextField.Text.Split(',')[1]);
                                    menuBorderColorRGB[2] = byte.Parse(BorderColorTextField.Text.Split(',')[2]);
                                    menu.BorderColor = new Color()
                                    {
                                        R = menuBorderColorRGB[0],
                                        G = menuBorderColorRGB[1],
                                        B = menuBorderColorRGB[2],
                                        A = 255
                                    };
                                    //Save blocks
                                    List<Block> blockList = [];
                                    for (int i = 19; i < InspectorWindow.ComponentList.Count - 1; i++)
                                    {
                                        DropDown currentComponent = (DropDown)InspectorWindow.ComponentList[i];
                                        if (((Component)currentComponent.Button.Component) == null) continue;
                                        blockList.Add(
                                            new Block(currentComponent.XPosition,
                                            currentComponent.YPosition,
                                            ((Component)currentComponent.Button.Component).RenderingObject,
                                            ((Component)currentComponent.Button.Component).ID));
                                    }
                                    menu.BlockList = blockList;
                                    break;
                                case Block block:
                                    //Save block position
                                    block.XPosition = int.Parse((InspectorWindow.ComponentList[6] as TextField).Text);
                                    block.YPosition = int.Parse((InspectorWindow.ComponentList[8] as TextField).Text);
                                    //Save block component
                                    DropDown selectedComponentDropDown = (DropDown)InspectorWindow.ComponentList[10];
                                    if (selectedComponentDropDown.Button == null)
                                    {
                                        block.Component = null;
                                    }
                                    else
                                    {
                                        block.Component = (Component)selectedComponentDropDown.Button.Component == null ? null : (selectedComponentDropDown.Button.Component as Component).RenderingObject;
                                    }
                                    break;
                                case DropBox dropBox:
                                    //Save position
                                    dropBox.XPosition = int.Parse((InspectorWindow.ComponentList[6] as TextField).Text);
                                    dropBox.YPosition = int.Parse((InspectorWindow.ComponentList[8] as TextField).Text);
                                    //Save width
                                    dropBox.Width = int.Parse((InspectorWindow.ComponentList[10] as TextField).Text);
                                    //Save height
                                    dropBox.Height = int.Parse((InspectorWindow.ComponentList[12] as TextField).Text);
                                    //Save Color
                                    TextField dropBoxColorTextField = InspectorWindow.ComponentList[14] as TextField;
                                    dropBox.Color = new Color()
                                    {
                                        R = byte.Parse(dropBoxColorTextField.Text.Split(',')[0]),
                                        G = byte.Parse(dropBoxColorTextField.Text.Split(',')[1]),
                                        B = byte.Parse(dropBoxColorTextField.Text.Split(',')[2]),
                                        A = 255
                                    };
                                    //Save border color
                                    TextField dropBoxBorderColorTextField = InspectorWindow.ComponentList[16] as TextField;
                                    dropBox.BorderColor = new Color()
                                    {
                                        R = byte.Parse(dropBoxBorderColorTextField.Text.Split(',')[0]),
                                        G = byte.Parse(dropBoxBorderColorTextField.Text.Split(',')[1]),
                                        B = byte.Parse(dropBoxBorderColorTextField.Text.Split(',')[2]),
                                        A = 255
                                    };
                                    //Save button list
                                    List<TemplateGame.Component.Button> buttonList = [];
                                    for (int i = 18; i < InspectorWindow.ComponentList.Count - 2; i++)
                                    {
                                        TemplateGame.Component.Button currentComponent = (TemplateGame.Component.Button)InspectorWindow.ComponentList[i];
                                        buttonList.Add(currentComponent);
                                    }
                                    dropBox.Options = buttonList;
                                    break;
                                case Slider slider:
                                    //Save position
                                    slider.XPosition = int.Parse((InspectorWindow.ComponentList[6] as TextField).Text);
                                    slider.YPosition = int.Parse((InspectorWindow.ComponentList[8] as TextField).Text);
                                    //Save width
                                    slider.Width = int.Parse((InspectorWindow.ComponentList[10] as TextField).Text);
                                    //Save height
                                    slider.Height = int.Parse((InspectorWindow.ComponentList[12] as TextField).Text);
                                    //Save color
                                    TextField sliderColorTextField = InspectorWindow.ComponentList[14] as TextField;
                                    slider.Color = new Color()
                                    {
                                        R = byte.Parse(sliderColorTextField.Text.Split(',')[0]),
                                        G = byte.Parse(sliderColorTextField.Text.Split(',')[1]),
                                        B = byte.Parse(sliderColorTextField.Text.Split(',')[2]),
                                        A = 255
                                    };
                                    //Save border color
                                    TextField sliderBorderColorTextField = InspectorWindow.ComponentList[16] as TextField;
                                    slider.BorderColor = new Color()
                                    {
                                        R = byte.Parse(sliderBorderColorTextField.Text.Split(',')[0]),
                                        G = byte.Parse(sliderBorderColorTextField.Text.Split(',')[1]),
                                        B = byte.Parse(sliderBorderColorTextField.Text.Split(',')[2]),
                                        A = 255
                                    };
                                    //Save Slider color
                                    byte[] sliderSliderColorRGB = new byte[3];
                                    TextField sliderDragColor = InspectorWindow.ComponentList[18] as TextField;
                                    slider.DragColor = new Color()
                                    {
                                        R = byte.Parse(sliderDragColor.Text.Split(',')[0]),
                                        G = byte.Parse(sliderDragColor.Text.Split(',')[1]),
                                        B = byte.Parse(sliderDragColor.Text.Split(',')[2]),
                                        A = 255
                                    };
                                    //Save Slider drag radius
                                    slider.SliderDragRadius = int.Parse((InspectorWindow.ComponentList[20] as TextField).Text);
                                    //Save Slider value
                                    slider.Value = float.Parse((InspectorWindow.ComponentList[22] as TextField).Text);
                                    //Save Action
                                    //slider.Action = (Action)Enum.Parse(typeof(Action), (InspectorWindow.ComponentList[22] as TextField).Text);
                                    break;
                                case Toggle toggle:
                                    //Save position
                                    toggle.XPosition = int.Parse((InspectorWindow.ComponentList[6] as TextField).Text);
                                    toggle.YPosition = int.Parse((InspectorWindow.ComponentList[8] as TextField).Text);
                                    //Save Size
                                    toggle.BoxSize = int.Parse((InspectorWindow.ComponentList[10] as TextField).Text);
                                    //Save color
                                    byte[] toggleColorRGB = new byte[3];
                                    TextField toggleColorTextField = InspectorWindow.ComponentList[12] as TextField;
                                    toggle.Color = new Color()
                                    {
                                        R = byte.Parse(toggleColorTextField.Text.Split(',')[0]),
                                        G = byte.Parse(toggleColorTextField.Text.Split(',')[1]),
                                        B = byte.Parse(toggleColorTextField.Text.Split(',')[2]),
                                        A = 255
                                    };
                                    //Save border color
                                    byte[] toggleBorderColorRGB = new byte[3];
                                    TextField toggleBorderColorTextField = InspectorWindow.ComponentList[14] as TextField;
                                    toggle.BorderColor = new Color()
                                    {
                                        R = byte.Parse(toggleBorderColorTextField.Text.Split(',')[0]),
                                        G = byte.Parse(toggleBorderColorTextField.Text.Split(',')[1]),
                                        B = byte.Parse(toggleBorderColorTextField.Text.Split(',')[2]),
                                        A = 255
                                    };
                                    //Save toggled color
                                    byte[] toggleToggledColorRGB = new byte[3];
                                    TextField toggleToggledColorTextField = InspectorWindow.ComponentList[16] as TextField;
                                    toggle.ToggledColor = new Color()
                                    {
                                        R = byte.Parse(toggleToggledColorTextField.Text.Split(',')[0]),
                                        G = byte.Parse(toggleToggledColorTextField.Text.Split(',')[1]),
                                        B = byte.Parse(toggleToggledColorTextField.Text.Split(',')[2]),
                                        A = 255
                                    };
                                    //Save toggle
                                    toggle.IsToggled = (InspectorWindow.ComponentList[18] as ToggleButton).IsToggled;
                                    //Save toggle text
                                    toggle.Text = (InspectorWindow.ComponentList[20] as TextField).Text;
                                    //Save action
                                    //toggle.Action = (Action)Enum.Parse(typeof(Action), (InspectorWindow.ComponentList[22] as TextField).Text);
                                    break;
                                case InputField inputField:
                                    //Save position
                                    inputField.XPosition = int.Parse((InspectorWindow.ComponentList[6] as TextField).Text);
                                    inputField.YPosition = int.Parse((InspectorWindow.ComponentList[8] as TextField).Text);
                                    //Save button offset
                                    inputField.ButtonYOffset = int.Parse((InspectorWindow.ComponentList[10] as TextField).Text);
                                    //Save width
                                    inputField.Width = int.Parse((InspectorWindow.ComponentList[12] as TextField).Text);
                                    //Save height
                                    inputField.Height = int.Parse((InspectorWindow.ComponentList[14] as TextField).Text);
                                    //Save text
                                    inputField.Text = (InspectorWindow.ComponentList[16] as TextField).Text;
                                    //Save placeholder
                                    inputField.Placeholder = (InspectorWindow.ComponentList[18] as TextField).Text;
                                    //Save color
                                    byte[] inputFieldColorRGB = new byte[3];
                                    TextField inputFieldColorTextField = InspectorWindow.ComponentList[20] as TextField;
                                    inputField.Color = new Color()
                                    {
                                        R = byte.Parse(inputFieldColorTextField.Text.Split(',')[0]),
                                        G = byte.Parse(inputFieldColorTextField.Text.Split(',')[1]),
                                        B = byte.Parse(inputFieldColorTextField.Text.Split(',')[2]),
                                        A = 255
                                    };
                                    //Save border color
                                    byte[] inputFieldBorderColorRGB = new byte[3];
                                    TextField inputFieldBorderColorTextField = InspectorWindow.ComponentList[22] as TextField;
                                    inputField.BorderColor = new Color()
                                    {
                                        R = byte.Parse(inputFieldBorderColorTextField.Text.Split(',')[0]),
                                        G = byte.Parse(inputFieldBorderColorTextField.Text.Split(',')[1]),
                                        B = byte.Parse(inputFieldBorderColorTextField.Text.Split(',')[2]),
                                        A = 255
                                    };
                                    //Save hover color
                                    byte[] inputFieldHoverColorRGB = new byte[3];
                                    TextField inputFieldHoverColorTextField = InspectorWindow.ComponentList[24] as TextField;
                                    inputField.HoverColor = new Color()
                                    {
                                        R = byte.Parse(inputFieldHoverColorTextField.Text.Split(',')[0]),
                                        G = byte.Parse(inputFieldHoverColorTextField.Text.Split(',')[1]),
                                        B = byte.Parse(inputFieldHoverColorTextField.Text.Split(',')[2]),
                                        A = 255
                                    };
                                    break;
                            }
                        }
                    }
                    break;
            }
            InspectorWindow.DropActiveComponent();
            Editor.ActiveScene.InspectorWindow = null;
            Editor.EnableComponents();
        }
    }
}