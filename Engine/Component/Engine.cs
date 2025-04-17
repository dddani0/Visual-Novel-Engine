using Raylib_cs;
using VisualNovelEngine.Engine.Component.Command;
using VisualNovelEngine.Engine.Editor.Component;
using VisualNovelEngine.Engine.Editor.Component.Command;

namespace VisualNovelEngine.Engine.Component
{
    public enum EngineState
    {
        Default,
        Editor,
        Game,
        Exit
    }
    /// <summary>
    /// Engine is the base of the Visual Novel Engine.
    /// It contains the Editor and the Game and connects them together.
    /// Create a new project, import a project or open a game project.
    /// </summary>
    public class Engine
    {
        /// <summary>
        /// The Editor of the Engine.
        /// </summary>
        internal Editor.Component.Editor Editor { get; set; }
        /// <summary>
        /// The Game of the Engine.
        /// </summary>
        internal Game.Component.Game Game { get; set; }
        /// <summary>
        /// The current state of the Engine.
        /// </summary>
        internal EngineState State { get; private set; }
        /// <summary>
        /// The title of the Engine.
        /// </summary>
        internal string Title { get; set; } = "Vizuális Novella Motor";
        /// <summary>
        /// The width of the Engine.
        /// </summary>
        internal int Width { get; set; } = 800;
        /// <summary>
        /// The height of the Engine.
        /// </summary>
        internal int Height { get; set; } = 300;
        /// <summary>
        /// The exit flag of the Engine.
        /// </summary>
        internal bool Exit { get; set; } = false;
        /// <summary>
        /// The button to import a project.
        /// </summary>
        internal Button ImportProject { get; set; }
        /// <summary>
        /// The button to create a new project.
        /// </summary>
        internal Button NewProject { get; set; }
        /// <summary>
        /// The button to open a game project.
        /// </summary>
        internal Button ImportBuild { get; set; }
        /// <summary>
        /// The button to open the repository.
        /// </summary>
        private Button OpenRepository { get; set; }
        internal Window? Window { get; set; } = null;

        /// <summary>
        /// Create a new Engine.
        /// </summary>
        public Engine()
        {
            //Set base window attributes.
            Raylib.InitWindow(Width, Height, Title);
            //Set the state to PreState.
            ChangeState(EngineState.Default);
            //Prestate buttons
            NewProject = new Button("Új projekt", Width / 2 - 125, 80, 210, 50, Color.RayWhite, Color.Gray, new OpenWindowCommand(this, WindowType.NewProject));
            ImportProject = new Button("Projekt importálás", Width / 2 - 125, 140, 210, 50, Color.RayWhite, Color.Gray, new OpenWindowCommand(this, WindowType.ImportProject));
            ImportBuild = new Button("Játék megnyitása", Width / 2 - 125, 200, 210, 50, Color.RayWhite, Color.Gray, new OpenWindowCommand(this, WindowType.PlayProject));
            OpenRepository = new Button("GitHub", 10, Height - 50 - 10, 100, 50, Color.RayWhite, Color.Gray, new OpenLinkCommand("https://github.com/dddani0/Visual-Novel-Engine"));
        }
        /// <summary>
        /// The main loop of the engine.
        /// </summary>
        public void Process()
        {
            while (Exit is false)
            {
                Raylib.BeginDrawing();
                switch (State)
                {
                    case EngineState.Default:
                        if (Raylib.WindowShouldClose()) ChangeState(EngineState.Exit);
                        Raylib.ClearBackground(Color.Black);
                        Raylib.DrawText(Title, 125, 0, 50, Color.RayWhite);
                        NewProject.Render();
                        ImportProject.Render();
                        ImportBuild.Render();
                        OpenRepository.Render();
                        Window?.Show();
                        break;
                    case EngineState.Editor:
                        Editor.Update();
                        break;
                    case EngineState.Game:
                        Game.Update();
                        break;
                    case EngineState.Exit:
                        Exit = true;
                        break;
                }
                Raylib.EndDrawing();
            }
            Raylib.CloseWindow();
        }
        /// <summary>
        /// Change the state of the engine automata.
        /// </summary>
        /// <param name="state"></param>
        public void ChangeState(EngineState state) => State = state;
        /// <summary>
        /// Set the title of the engine window.
        /// </summary>
        /// <param name="title"></param>
        public void SetWindowTitle(string title)
        {
            Title = title;
            Raylib.SetWindowTitle(title);
        }
        /// <summary>
        /// Create a new editor.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public Editor.Component.Editor CreateEditor(string path, string title)
        {
            Editor = new Editor.Component.Editor(this, title, path);
            ChangeState(EngineState.Editor);
            return Editor;
        }
        /// <summary>
        /// Create instance of an existing editor.
        /// This is used when the user imports a project.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public VisualNovelEngine.Engine.Editor.Component.Editor CreateEditor(string path)
        {
            Editor = new Editor.Component.Editor(this, path);
            ChangeState(EngineState.Editor);
            return Editor;
        }
        /// <summary>
        /// Create a new game instance.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public VisualNovelEngine.Engine.Game.Component.Game CreateGame(string path)
        {
            Game = new Game.Component.Game(path);
            ChangeState(EngineState.Game);
            return Game;
        }
    }
}