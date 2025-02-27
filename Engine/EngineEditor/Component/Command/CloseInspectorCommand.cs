using Raylib_cs;
using TemplateGame.Component;
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
            switch (InspectorWindow.ActiveEvent is not null)
            {
                case true:

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
                                case Button button:
                                    //fetch fields and read out values.
                                    //button.Text = InspectorWindow.ActiveComponent.Name;
                                    break;
                                case TextBox textBox:
                                    //textBox.Text = InspectorWindow.ActiveComponent.Name;
                                    break;
                                case Block block:

                                    break;
                                case Menu menu:
                                    //Save color
                                    byte[] colorRGB = new byte[3];
                                    for (int i = 0; i < InspectorWindow.ComponentList.Count; i++)
                                    {
                                        IComponent currentComponent = InspectorWindow.ComponentList[i];
                                        if (currentComponent is Label label)
                                        {
                                            if (label.Text.Contains("Color"))
                                            {
                                                TextField redTextField = InspectorWindow.ComponentList[i + 2] as TextField;
                                                TextField greenTextField = InspectorWindow.ComponentList[i + 4] as TextField;
                                                TextField blueTextField = InspectorWindow.ComponentList[i + 6] as TextField;
                                                colorRGB[0] = byte.Parse(redTextField.Text);
                                                colorRGB[1] = byte.Parse(greenTextField.Text);
                                                colorRGB[2] = byte.Parse(blueTextField.Text);
                                                break;
                                            }
                                        }
                                    }
                                    menu.MenuColor = new Color()
                                    {
                                        R = colorRGB[0],
                                        G = colorRGB[1],
                                        B = colorRGB[2],
                                        A = 255
                                    };
                                    //Convert dropdowns to blocks (if available).
                                    List<Block> blockList = [];
                                    for (int i = 0; i < InspectorWindow.ComponentList.Count; i++)
                                    {
                                        IComponent currentComponent = InspectorWindow.ComponentList[i];
                                        if (currentComponent is DropDown dropDown)
                                        {
                                            string selectedBlockTitle = dropDown.Button.Text;
                                            Button selectedBlockDropDownOption = dropDown.FilteredButtonList.FirstOrDefault(button => button.Text.Equals(selectedBlockTitle));
                                            if (selectedBlockDropDownOption is null) break;
                                            //innentől nem okés
                                            Component associatedComponent = (Component)selectedBlockDropDownOption.Component;
                                            Block selectedBlock = (Block)associatedComponent.RenderingObject;
                                            blockList.Add(selectedBlock);
                                        }
                                    }
                                    menu.BlockList = blockList;
                                    break;
                            }
                            return;
                        }
                    }
                    break;
            }
            InspectorWindow.DropActiveComponent();
            Editor.ActiveScene.InspectorWindow = null;
        }
    }
}