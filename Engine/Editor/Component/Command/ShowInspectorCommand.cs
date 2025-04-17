using System.Linq;
using System.Runtime.CompilerServices;
using Raylib_cs;
using VisualNovelEngine.Engine.Game.Interface;
using VisualNovelEngine.Engine.Editor.Interface;

namespace VisualNovelEngine.Engine.Editor.Component.Command
{
    /// <summary>
    /// Shows the inspector window.
    /// </summary>
    public class ShowInspectorCommand : ICommand
    {
        internal Editor Editor { get; set; }
        private readonly InspectorWindow Window;
        internal IAction? Action { get; set; } = null;
        private ISettingsAction? SettingsAction { get; set; } = null;
        internal int EnabledRowComponentCount;
        internal int XPosition { get; set; }
        internal int YPosition { get; set; }

        public ShowInspectorCommand(Editor editor, int enabledRowComponentCount)
        {
            Editor = editor;
            EnabledRowComponentCount = enabledRowComponentCount;
            XPosition = Raylib.GetScreenWidth() / 2 - Editor.InspectorWindowWidth / 2;
            YPosition = Raylib.GetScreenHeight() / 2 - Editor.InspectorWindowHeight / 2;
            Window = new InspectorWindow(editor, XPosition, YPosition, EnabledRowComponentCount);
        }

        public ShowInspectorCommand(Editor editor, IAction eventData, int enabledRowComponentCount)
        {
            Editor = editor;
            Action = eventData;
            EnabledRowComponentCount = enabledRowComponentCount;
            XPosition = Raylib.GetScreenWidth() / 2 - Editor.InspectorWindowWidth / 2;
            YPosition = Raylib.GetScreenHeight() / 2 - Editor.InspectorWindowHeight / 2;
            Window = new InspectorWindow(editor, XPosition, YPosition, EnabledRowComponentCount);
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        public void Execute()
        {
            //Set event to active event in the inspector.
            switch (Window is not null)
            {
                case true:
                    if (Action is not null)
                    {
                        //Delete active component
                        Window.DropActiveComponent();
                        //Enable the window
                        Window.Active = true;
                        //Set the active component to the event.
                        Window.SetActiveComponent(Action);
                        //Set the inspector window to the active scene.
                        Editor.ActiveScene.InspectorWindow = Window;
                        //Disable all buttons temporary
                        Editor.DisableComponents();
                    }
                    else
                    {
                        //Search for the first selected component and set it as the active component.
                        //Convert the selected component to a component.
                        Component ccomp = (Component)Editor.ActiveScene.ComponentList.FirstOrDefault(x =>
                        {
                            Component component = (Component)x;
                            return component.IsSelected;
                        });
                        Window.SetActiveComponent(ccomp);
                        Window.Active = true;
                        Editor.ActiveScene.InspectorWindow = Window;
                        ccomp.IsSelected = false;
                        Editor.DisableComponents();
                    }
                    return;
                case false:
                    Window.DropActiveComponent();
                    break;
            }
        }
    }
}