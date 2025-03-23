using Raylib_cs;
using VisualNovelEngine.Engine.Editor.Interface;

namespace VisualNovelEngine.Engine.Editor.Component
{
    /// <summary>
    /// Represents a scene
    /// </summary>
    public class Scene : IWindow
    {
        /// <summary>
        /// The editor instance.
        /// </summary>
        private Editor Editor { get; set; }
        /// <summary>
        /// The ID of the scene.
        /// </summary>
        internal readonly int ID;
        /// <summary>
        /// The name of the scene.
        /// </summary>
        internal string Name { get; set; }
        /// <summary>
        /// Whether the scene is active.
        /// </summary>
        internal bool IsActive { get; set; } = true;
        /// <summary>
        /// The timeline of the scene.
        /// </summary>
        internal Timeline Timeline { get; set; }
        /// <summary>
        /// The list of components in the scene.
        /// </summary>
        internal List<IDinamicComponent> ComponentList { get; set; } = [];
        /// <summary>
        /// The list of groups in the scene.
        /// </summary>
        internal List<Group> ComponentGroupList { get; set; } = [];
        /// <summary>
        /// The inspector window of the scene.
        /// </summary>
        internal InspectorWindow? InspectorWindow { get; set; } = null;
        /// <summary>
        /// The background option of the scene.
        /// </summary>
        internal TemplateGame.Component.Scene.BackgroundOption BackgroundOption { get; set; } = TemplateGame.Component.Scene.BackgroundOption.SolidColor;
        /// <summary>
        /// The background color of the scene.
        /// </summary>
        internal Color? BackgroundColor { get; set; }
        /// <summary>
        /// The background image of the scene.
        /// </summary>
        internal Texture2D? BackgroundImage { get; set; }
        /// <summary>
        /// The background gradient color of the scene.
        /// </summary>
        internal Color[]? BackgroundGradientColor { get; set; } = { Color.Black, Color.Black };
        /// <summary>
        /// Creates a new scene.
        /// </summary>
        /// <param name="editor"></param>
        /// <param name="sceneName"></param>
        /// <param name="components"></param>
        /// <param name="groups"></param>
        public Scene(Editor editor, string sceneName, IDinamicComponent[] components, Group[] groups)
        {
            Editor = editor;
            ID = Editor.GenerateID();
            Name = sceneName;
            Timeline = new(Editor, 0, 600);
            if (components != null) ComponentList.AddRange(components);
            if (groups != null) ComponentGroupList.AddRange(groups);
            BackgroundColor = Color.Black;
        }
        /// <summary>
        /// Updates the scene.
        /// </summary>
        internal void Update()
        {
            if (IsActive is false) return;
            Show();
            Timeline.Show();
            if (InspectorWindow == null) return;
            InspectorWindow.Show();
        }
        /// <summary>
        /// Shows the scene.
        /// </summary>
        public void Show()
        {
            for (int i = 0; i < ComponentGroupList.Count; i++)
            {
                ComponentGroupList[i].Update();
            }
            for (int i = 0; i < ComponentList.Count; i++)
            {
                IDinamicComponent? component = ComponentList[i];
                if (component.IsInGroup() is true) continue;
                IComponent castedComponent = (IComponent)component;
                castedComponent.Render();
            }
        }
    }
}