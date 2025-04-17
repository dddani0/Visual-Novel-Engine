using System.ComponentModel;
using System.Text.Json;
using Raylib_cs;
using VisualNovelEngine.Engine.Game.Component.Action;
using VisualNovelEngine.Engine.Game.Component.Action.TimelineDependent;
using VisualNovelEngine.Engine.Game.Component.Action.TimelineIndependent;
using VisualNovelEngine.Engine.Game.Interface;
using VisualNovelEngine.Engine.PortData;

namespace VisualNovelEngine.Engine.Game.Component
{
    /// <summary>
    /// The "GameImporter" class is the class that fetches raw data into objects, which the templategame can use.
    /// </summary>
    public class GameEximManager
    {
        /// <summary>
        /// The Game object.
        /// </summary>
        Game Game { get; set; }
        internal GameExim GameExim { get; set; }
        /// <summary>
        /// The cache of created blocks.
        /// </summary>
        internal List<Block> BlockListCache { get; set; } = [];
        internal List<Menu> MenuListCache { get; set; } = [];
        public GameEximManager(Game game, string BuildPath)
        {
            Game = game;
            GameExim = FetchGameEximFromImport(BuildPath);
        }
        /// <summary>
        /// Fetches the game settings from the json file.
        /// </summary>
        /// <param name="BuildPath"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>/
        internal GameExim FetchGameEximFromImport(string BuildPath)
        {
            string rawFile = File.ReadAllText(BuildPath);
            var rawGame = JsonSerializer.Deserialize<GameExim>(rawFile);
            if (rawGame != null) return rawGame;
            else throw new InvalidOperationException("Failed to load game settings, because the file is null.");
        }
        internal Scene FetchSceneFromImport(SceneExim sceneImport)
        {
            Timeline timeline = new();
            timeline.ActionList.AddRange([.. sceneImport.ActionList.Select(FetchTimelineDependentActionFromImport)]);
            return (Scene.BackgroundOption)sceneImport.BackgroundType switch
            {
                Scene.BackgroundOption.Image => new(sceneImport.Name, Game)
                {
                    ID = sceneImport.ID,
                    Background = (Scene.BackgroundOption)sceneImport.BackgroundType,
                    imageTexture = Raylib.LoadTexture(sceneImport.ImageTexture),
                    Timeline = timeline
                },
                Scene.BackgroundOption.GradientVertical or Scene.BackgroundOption.GradientHorizontal => new(sceneImport.Name, Game)
                {
                    ID = sceneImport.ID,
                    Background = (Scene.BackgroundOption)sceneImport.BackgroundType,
                    gradientColor = sceneImport.GradientColor == null ? [] : sceneImport.GradientColor.Length == 0 ? [] : [new()
                        {
                            R = (byte)sceneImport.GradientColor[0],
                            G = (byte)sceneImport.GradientColor[1],
                            B = (byte)sceneImport.GradientColor[2],
                            A = (byte)sceneImport.GradientColor[3]
                        },
                        new()
                        {
                            R = (byte)sceneImport.GradientColor[4],
                            G = (byte)sceneImport.GradientColor[5],
                            B = (byte)sceneImport.GradientColor[6],
                            A = (byte)sceneImport.GradientColor[7]
                        }],
                    Timeline = timeline
                },
                Scene.BackgroundOption.SolidColor => new(sceneImport.Name, Game)
                {
                    ID = sceneImport.ID,
                    Background = (Scene.BackgroundOption)sceneImport.BackgroundType,
                    solidColor = sceneImport.SolidColor == null ? Color.Black : sceneImport.SolidColor.Length == 0 ? new() : new()
                    {
                        R = (byte)sceneImport.SolidColor[0],
                        G = (byte)sceneImport.SolidColor[1],
                        B = (byte)sceneImport.SolidColor[2],
                        A = (byte)sceneImport.SolidColor[3]
                    },
                    Timeline = timeline
                },
                _ => throw new InvalidOperationException("Failed to load scene settings, because the background type is not recognized."),
            };
        }
        /// <summary>
        /// Creates a sprite from the importer class
        /// </summary>
        /// <param name="rawAction"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        internal Sprite FetchSpriteFromImport(SpriteExim rawAction)
        {
            if (rawAction.Path == null)
            {
                throw new InvalidOperationException("Failed to load scene settings, because the sprite path is null.");
            }
            if (File.Exists(Game.ProjectPath + rawAction.Path) is false)
            {
                throw new InvalidOperationException("Failed to load scene settings, because the sprite path is invalid.");
            }
            return new Sprite(Game.ProjectPath + rawAction.Path);
        }
        /// <summary>
        /// Creates a sprite from the importer class
        /// </summary>
        /// <param name="spriteImport"></param>
        /// <param name="block"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        internal Sprite FetchSpriteFromImport(SpriteExim spriteImport, Block block)
        {
            if (spriteImport.Path == null)
            {
                throw new InvalidOperationException("Failed to load scene settings, because the sprite path is null.");
            }
            if (spriteImport.XPosition == null)
            {
                throw new InvalidOperationException("Failed to load scene settings, because the sprite x position is null.");
            }
            if (spriteImport.YPosition == null)
            {
                throw new InvalidOperationException("Failed to load scene settings, because the sprite y position is null.");
            }
            if (File.Exists(Game.ProjectPath + spriteImport.Path) is false)
            {
                //Throw a warning
            }
            return new Sprite(Game.ProjectPath + spriteImport.Path, block, spriteImport.XPosition.Value, spriteImport.YPosition.Value);
        }
        /// <summary>
        /// Creates a variable from the importer class
        /// </summary>
        /// <param name="variableImport"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        internal Variable FetchVariableFromImport(ActionExim variableImport)
        {
            if (variableImport.VariableName == null)
            {
                throw new InvalidOperationException("Failed to load scene settings, because the variable name is null.");
            }
            if (variableImport.VariableValue == null)
            {
                throw new InvalidOperationException("Failed to load scene settings, because the variable value is null.");
            }
            if (variableImport.VariableType == null)
            {
                throw new InvalidOperationException("Failed to load scene settings, because the variable type is null.");
            }
            return new Variable(variableImport.VariableName, variableImport.VariableValue, (VariableType)variableImport.VariableType);
        }

        internal Variable FetchVariableFromImport(VariableExim variableImport)
        {
            return variableImport.Type switch
            {
                1 => new Variable(variableImport.Name, variableImport.Value, VariableType.String),
                2 => new Variable(variableImport.Name, variableImport.Value, VariableType.Int),
                3 => new Variable(variableImport.Name, variableImport.Value, VariableType.Float),
                4 => new Variable(variableImport.Name, variableImport.Value, VariableType.Boolean),
                _ => throw new InvalidOperationException("Failed to load variable settings, because the variable type is not recognized."),
            };
        }
        /// <summary>
        /// Creates a button with a Timeline dependent or a general event attached to it from the importer class
        /// </summary>
        /// <param name="buttonImport"></param>
        /// <param name="block"></param>
        /// <returns></returns>
        internal Button FetchButtonFromImport(ButtonComponentExIm buttonImport, Block block)
        {
            return new Button(
                Game,
                block,
                new Font() { BaseSize = 30, GlyphPadding = 5 },
                buttonImport.XPosition,
                buttonImport.YPosition,
                buttonImport.BorderWidth,
                buttonImport.Width,
                buttonImport.Height,
                buttonImport.Text,
                new Color()
                {
                    R = (byte)buttonImport.TextColor[0],
                    G = (byte)buttonImport.TextColor[1],
                    B = (byte)buttonImport.TextColor[2],
                    A = (byte)buttonImport.TextColor[3]
                },
                new Color()
                {
                    R = (byte)buttonImport.Color[0],
                    G = (byte)buttonImport.Color[1],
                    B = (byte)buttonImport.Color[2],
                    A = (byte)buttonImport.Color[3]
                },
                new Color()
                {
                    R = (byte)buttonImport.BorderColor[0],
                    G = (byte)buttonImport.BorderColor[1],
                    B = (byte)buttonImport.BorderColor[2],
                    A = (byte)buttonImport.BorderColor[3]
                },
                new Color()
                {
                    R = (byte)buttonImport.HoverColor[0],
                    G = (byte)buttonImport.HoverColor[1],
                    B = (byte)buttonImport.HoverColor[2],
                    A = (byte)buttonImport.HoverColor[3]
                },
                (IButtonAction)FetchTimelineDependentActionFromImport(buttonImport.Action)
            );
        }
        /// <summary>
        /// Creates a button with a Timeline independent or a general event attached to it from the importer class
        /// </summary>
        /// <param name="staticButtonImport"></param>
        /// <param name="block"></param>
        /// <returns></returns>
        internal Button FetchStaticButtonFromImport(ButtonComponentExIm staticButtonImport, Block block)
        {
            return new Button(
                            Game,
                            block,
                            new Font() { BaseSize = 30, GlyphPadding = 5 },
                            staticButtonImport.XPosition,
                            staticButtonImport.YPosition,
                            staticButtonImport.BorderWidth,
                            staticButtonImport.Width,
                            staticButtonImport.Height,
                            staticButtonImport.Text,
                            new Color()
                            {
                                R = (byte)staticButtonImport.TextColor[0],
                                G = (byte)staticButtonImport.TextColor[1],
                                B = (byte)staticButtonImport.TextColor[2],
                                A = (byte)staticButtonImport.TextColor[3]
                            },
                            new Color()
                            {
                                R = (byte)staticButtonImport.Color[0],
                                G = (byte)staticButtonImport.Color[1],
                                B = (byte)staticButtonImport.Color[2],
                                A = (byte)staticButtonImport.Color[3]
                            },
                            new Color()
                            {
                                R = (byte)staticButtonImport.BorderColor[0],
                                G = (byte)staticButtonImport.BorderColor[1],
                                B = (byte)staticButtonImport.BorderColor[2],
                                A = (byte)staticButtonImport.BorderColor[3]
                            },
                            new Color()
                            {
                                R = (byte)staticButtonImport.HoverColor[0],
                                G = (byte)staticButtonImport.HoverColor[1],
                                B = (byte)staticButtonImport.HoverColor[2],
                                A = (byte)staticButtonImport.HoverColor[3]
                            },
                            FetchTimelineIndependentActionFromImport(staticButtonImport.Action)
                        );
        }
        /// <summary>
        /// Creates an option from the importer class
        /// </summary>
        /// <param name="DropBoxOptionImport"></param>
        /// <param name="block"></param>
        /// <returns></returns>
        internal Button FetchDropBoxOptionFromImport(DropBoxExim dropBoxImport, DropBoxOptionExim DropBoxOptionImport, Block block)
        {
            //Each dropbox option's Y position is calculated by the height of the dropbox and the index of the option in the DropBox.cs class.
            return DropBox.GetDropBoxOption(
                Game,
                block,
                new Font() { BaseSize = 10, GlyphPadding = 2 },
                0,
                0,
                0,
                dropBoxImport.Width,
                dropBoxImport.Height,
                DropBoxOptionImport.Text,
                new Color()
                {
                    R = (byte)dropBoxImport.TextColor[0],
                    G = (byte)dropBoxImport.TextColor[1],
                    B = (byte)dropBoxImport.TextColor[2],
                    A = (byte)dropBoxImport.TextColor[3]
                },
                new Color()
                {
                    R = (byte)dropBoxImport.Color[0],
                    G = (byte)dropBoxImport.Color[1],
                    B = (byte)dropBoxImport.Color[2],
                    A = (byte)dropBoxImport.Color[3]
                },
                new Color()
                {
                    R = (byte)dropBoxImport.BorderColor[0],
                    G = (byte)dropBoxImport.BorderColor[1],
                    B = (byte)dropBoxImport.BorderColor[2],
                    A = (byte)dropBoxImport.BorderColor[3]
                },
                new Color()
                {
                    R = (byte)dropBoxImport.HoverColor[0],
                    G = (byte)dropBoxImport.HoverColor[1],
                    B = (byte)dropBoxImport.HoverColor[2],
                    A = (byte)dropBoxImport.HoverColor[3]
                },
                FetchTimelineIndependentActionFromImport(DropBoxOptionImport.Action)
            );
        }
        /// <summary>
        /// Creates a dropbox from the importer class
        /// </summary>
        /// <param name="dropBoxImport"></param>
        /// <param name="block"></param>
        /// <returns></returns>
        internal DropBox FetchDropBoxFromImport(DropBoxExim dropBoxImport, Block block)
        {
            return new DropBox(
                                    block,
                                    dropBoxImport.XPosition,
                                    dropBoxImport.YPosition,
                                    dropBoxImport.Width,
                                    dropBoxImport.Height,
                                    dropBoxImport.Options.Select(option => FetchDropBoxOptionFromImport(dropBoxImport, option, block)).ToArray(),
                                    new Color()
                                    {
                                        R = (byte)dropBoxImport.Color[0],
                                        G = (byte)dropBoxImport.Color[1],
                                        B = (byte)dropBoxImport.Color[2],
                                        A = (byte)dropBoxImport.Color[3]
                                    },
                                    new Color()
                                    {
                                        R = (byte)dropBoxImport.BorderColor[0],
                                        G = (byte)dropBoxImport.BorderColor[1],
                                        B = (byte)dropBoxImport.BorderColor[2],
                                        A = (byte)dropBoxImport.BorderColor[3]
                                    },
                                    new Color()
                                    {
                                        R = (byte)dropBoxImport.HoverColor[0],
                                        G = (byte)dropBoxImport.HoverColor[1],
                                        B = (byte)dropBoxImport.HoverColor[2],
                                        A = (byte)dropBoxImport.HoverColor[3]
                                    });
        }
        /// <summary>
        /// Creates an input field from the importer class
        /// </summary>
        /// <param name="inputFieldImport"></param>
        /// <param name="block"></param>
        /// <returns></returns>
        internal InputField FetchInputFieldFromImport(InputFieldExim inputFieldImport, Block block)
        {
            return new InputField(
                Game,
                block,
                inputFieldImport.XPosition,
                inputFieldImport.YPosition,
                inputFieldImport.ButtonYOffset,
                inputFieldImport.Width,
                inputFieldImport.Height,
                inputFieldImport.PlaceholderText,
                inputFieldImport.ButtonText,
                new Color()
                {
                    R = (byte)inputFieldImport.Color[0],
                    G = (byte)inputFieldImport.Color[1],
                    B = (byte)inputFieldImport.Color[2],
                    A = (byte)inputFieldImport.Color[3]
                },
                new Color()
                {
                    R = (byte)inputFieldImport.BorderColor[0],
                    G = (byte)inputFieldImport.BorderColor[1],
                    B = (byte)inputFieldImport.BorderColor[2],
                    A = (byte)inputFieldImport.BorderColor[3]
                },
                new Color()
                {
                    R = (byte)inputFieldImport.HoverColor[0],
                    G = (byte)inputFieldImport.HoverColor[1],
                    B = (byte)inputFieldImport.HoverColor[2],
                    A = (byte)inputFieldImport.HoverColor[3]
                },
                new Color()
                {
                    R = (byte)inputFieldImport.SelectedColor[0],
                    G = (byte)inputFieldImport.SelectedColor[1],
                    B = (byte)inputFieldImport.SelectedColor[2],
                    A = (byte)inputFieldImport.SelectedColor[3]
                },
                (IButtonAction)FetchTimelineDependentActionFromImport(inputFieldImport.ButtonAction)
            );
        }
        /// <summary>
        /// Creates a static input field from the importer class
        /// </summary>
        /// <param name="staticInputFieldImport"></param>
        /// <param name="block"></param>
        /// <returns></returns>
        internal InputField FetchStaticInputFieldFromImport(InputFieldExim staticInputFieldImport, Block block)
        {
            return new InputField(
                Game,
                block,
                staticInputFieldImport.XPosition,
                staticInputFieldImport.YPosition,
                staticInputFieldImport.ButtonYOffset,
                staticInputFieldImport.Width,
                staticInputFieldImport.Height,
                staticInputFieldImport.PlaceholderText,
                staticInputFieldImport.ButtonText,
                new Color()
                {
                    R = (byte)staticInputFieldImport.Color[0],
                    G = (byte)staticInputFieldImport.Color[1],
                    B = (byte)staticInputFieldImport.Color[2],
                    A = (byte)staticInputFieldImport.Color[3]
                },
                new Color()
                {
                    R = (byte)staticInputFieldImport.BorderColor[0],
                    G = (byte)staticInputFieldImport.BorderColor[1],
                    B = (byte)staticInputFieldImport.BorderColor[2],
                    A = (byte)staticInputFieldImport.BorderColor[3]
                },
                new Color()
                {
                    R = (byte)staticInputFieldImport.HoverColor[0],
                    G = (byte)staticInputFieldImport.HoverColor[1],
                    B = (byte)staticInputFieldImport.HoverColor[2],
                    A = (byte)staticInputFieldImport.HoverColor[3]
                },
                new Color()
                {
                    R = (byte)staticInputFieldImport.SelectedColor[0],
                    G = (byte)staticInputFieldImport.SelectedColor[1],
                    B = (byte)staticInputFieldImport.SelectedColor[2],
                    A = (byte)staticInputFieldImport.SelectedColor[3]
                },
                FetchTimelineIndependentActionFromImport(staticInputFieldImport.ButtonAction)
            );
        }
        /// <summary>
        /// Creates a slider from the importer class
        /// </summary>
        /// <param name="sliderImport"></param>
        /// <param name="block"></param>
        /// <returns></returns>
        internal Slider FetchSliderFromImport(SliderExim sliderImport, Block block)
        {
            return new Slider(
                block,
                sliderImport.XPosition,
                sliderImport.YPosition,
                sliderImport.Width,
                sliderImport.Height,
                sliderImport.BorderWidth,
                sliderImport.DragRadius,
                new Color()
                {
                    R = (byte)sliderImport.DragColor[0],
                    G = (byte)sliderImport.DragColor[1],
                    B = (byte)sliderImport.DragColor[2],
                    A = (byte)sliderImport.DragColor[3]
                },
                new Color()
                {
                    R = (byte)sliderImport.Color[0],
                    G = (byte)sliderImport.Color[1],
                    B = (byte)sliderImport.Color[2],
                    A = (byte)sliderImport.Color[3]
                },
                new Color()
                {
                    R = (byte)sliderImport.BorderColor[0],
                    G = (byte)sliderImport.BorderColor[1],
                    B = (byte)sliderImport.BorderColor[2],
                    A = (byte)sliderImport.BorderColor[3]
                },
                FetchTimelineIndependentActionFromImport(sliderImport.Action)
            );
        }
        /// <summary>
        /// Creates a toggle from the importer class
        /// </summary>
        /// <param name="toggleImport"></param>
        /// <param name="block"></param>
        /// <returns></returns>
        internal Toggle FetchToggleFromImport(ToggleExim toggleImport, Block block)
        {
            return new Toggle(
                 block,
                 toggleImport.XPosition,
                 toggleImport.YPosition,
                 toggleImport.BoxSize,
                 toggleImport.TextXOffset,
                 toggleImport.Text,
                 false,
                 new Color()
                 {
                     R = (byte)toggleImport.Color[0],
                     G = (byte)toggleImport.Color[1],
                     B = (byte)toggleImport.Color[2],
                     A = (byte)toggleImport.Color[3]
                 },
                 new Color()
                 {
                     R = (byte)toggleImport.BorderColor[0],
                     G = (byte)toggleImport.BorderColor[1],
                     B = (byte)toggleImport.BorderColor[2],
                     A = (byte)toggleImport.BorderColor[3]
                 },
                 new Color()
                 {
                     R = (byte)toggleImport.ActivatedColor[0],
                     G = (byte)toggleImport.ActivatedColor[1],
                     B = (byte)toggleImport.ActivatedColor[2],
                     A = (byte)toggleImport.ActivatedColor[3]
                 },
                 FetchTimelineIndependentActionFromImport(toggleImport.Action)
             );
        }
        /// <summary>
        /// Creates a text field from the importer class
        /// </summary>
        /// <param name="textFieldImport"></param>
        /// <param name="block"></param>
        /// <returns></returns>
        internal TextField FetchTextFieldFromImport(TextFieldExim textFieldImport, Block block)
        {
            return new TextField(
                block,
                textFieldImport.XPosition,
                textFieldImport.YPosition,
                textFieldImport.Width,
                textFieldImport.Height,
                textFieldImport.BorderWidth,
                textFieldImport.HorizontalTextMargin,
                textFieldImport.VerticalTextMargin,
                textFieldImport.Text,
                new Font() { BaseSize = 30, GlyphPadding = 5 },
                textFieldImport.IsVisible == "True",
                textFieldImport.WordWrap == "True",
                new Color()
                {
                    R = (byte)textFieldImport.Color[0],
                    G = (byte)textFieldImport.Color[1],
                    B = (byte)textFieldImport.Color[2],
                    A = (byte)textFieldImport.Color[3]
                },
                new Color()
                {
                    R = (byte)textFieldImport.BorderColor[0],
                    G = (byte)textFieldImport.BorderColor[1],
                    B = (byte)textFieldImport.BorderColor[2],
                    A = (byte)textFieldImport.BorderColor[3]
                }
            );
        }
        /// <summary>
        /// Creates a block from the importer class
        /// </summary>
        /// <param name="blockImport"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        internal Block FetchBlockFromImport(BlockExim blockImport)
        {
            var newBlock = new Block(
                blockImport.XPosition,
                blockImport.YPosition,
                null,
                blockImport.ID
                );
            // The block has a button component.
            if (blockImport.Button != null)
                newBlock.SetComponent(FetchButtonFromImport(blockImport.Button, newBlock));
            // The block has an InputField component.
            else if (blockImport.InputField != null)
                newBlock.SetComponent(FetchInputFieldFromImport(blockImport.InputField, newBlock));
            // The block has a TextField component.
            else if (blockImport.TextField != null)
                newBlock.SetComponent(FetchTextFieldFromImport(blockImport.TextField, newBlock));
            // The block has a Sprite component.
            else if (blockImport.Sprite != null)
                newBlock.SetComponent(FetchSpriteFromImport(blockImport.Sprite, newBlock));
            else
                throw new InvalidOperationException("Failed to load scene settings, because either the component type attached to the block is not recognized or the type is static.");
            BlockListCache.Add(newBlock);
            return newBlock;
        }
        /// <summary>
        /// Creates a static block from the importer class
        /// </summary>
        /// <param name="staticBlockImport"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        internal Block FetchStaticBlockFromImport(BlockExim staticBlockImport)
        {
            var newBlock = new Block(
                    staticBlockImport.XPosition,
                    staticBlockImport.YPosition,
                    null,
                    staticBlockImport.ID
                );
            // The block has a static button component.
            if (staticBlockImport.StaticButton != null)
                newBlock.SetComponent(FetchStaticButtonFromImport(staticBlockImport.StaticButton, newBlock));
            // The block has a dropbox component.
            else if (staticBlockImport.DropBox != null)
                newBlock.SetComponent(FetchDropBoxFromImport(staticBlockImport.DropBox, newBlock));
            // The block has an InputField component.
            else if (staticBlockImport.StaticInputField != null)
                newBlock.SetComponent(FetchStaticInputFieldFromImport(staticBlockImport.StaticInputField, newBlock));
            // The block has a Slider component.
            else if (staticBlockImport.Slider != null)
                newBlock.SetComponent(FetchSliderFromImport(staticBlockImport.Slider, newBlock));
            // The block has a Toggle component.
            else if (staticBlockImport.Toggle != null)
                newBlock.SetComponent(FetchToggleFromImport(staticBlockImport.Toggle, newBlock));
            // The block has a TextField component.
            else if (staticBlockImport.TextField != null)
                newBlock.SetComponent(FetchTextFieldFromImport(staticBlockImport.TextField, newBlock));
            // The block has a Sprite component.
            else if (staticBlockImport.Sprite != null)
                newBlock.SetComponent(FetchSpriteFromImport(staticBlockImport.Sprite, newBlock));
            else
                throw new InvalidOperationException("Failed to load scene settings, because either the component type attached to the block is not recognized or the type is not static.");
            BlockListCache.Add(newBlock);
            return newBlock;
        }
        /// <summary>
        /// Creates a menu with static or non-static components from the importer class
        /// </summary>
        /// <param name="menuImport"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        internal Menu FetchMenuFromImport(MenuExim menuImport)
        {
            if (menuImport.XPosition == null)
            {
                throw new InvalidOperationException("Failed to load scene settings, because the menu X position is null.");
            }
            if (menuImport.YPosition == null)
            {
                throw new InvalidOperationException("Failed to load scene settings, because the menu Y position is null.");
            }
            if (menuImport.Width == null)
            {
                throw new InvalidOperationException("Failed to load scene settings, because the menu width is null.");
            }
            if (menuImport.Height == null)
            {
                throw new InvalidOperationException("Failed to load scene settings, because the menu height is null.");
            }
            if (menuImport.FullScreen == null)
            {
                throw new InvalidOperationException("Failed to load scene settings, because the menu fullscreen is null.");
            }
            if (menuImport.Color == null)
            {
                throw new InvalidOperationException("Failed to load scene settings, because the menu color is null.");
            }
            if (menuImport.BorderColor == null)
            {
                throw new InvalidOperationException("Failed to load scene settings, because the menu border color is null.");
            }
            var menuXPosition = menuImport.XPosition.Value;
            var menuYPosition = menuImport.YPosition.Value;
            var menuWidth = menuImport.Width.Value;
            var menuHeight = menuImport.Height.Value;
            var menuFullScreen = menuImport.FullScreen == "True";
            var windowColor = new Color()
            {
                R = (byte)menuImport.Color[0],
                G = (byte)menuImport.Color[1],
                B = (byte)menuImport.Color[2],
                A = (byte)menuImport.Color[3]
            };
            var windowBorderColor = new Color()
            {
                R = (byte)menuImport.BorderColor[0],
                G = (byte)menuImport.BorderColor[1],
                B = (byte)menuImport.BorderColor[2],
                A = (byte)menuImport.BorderColor[3]
            };
            int id = menuImport.ID;
            var menu = new Menu(
                Game,
                id,
                menuXPosition,
                menuYPosition,
                menuWidth,
                menuHeight,
                menuFullScreen,
                [],
                windowColor,
                windowBorderColor);
            MenuListCache.Add(menu);
            if (menuImport.BlockList == null) return menu;
            menu.BlockList.AddRange(menuImport.Static == "True" ? menuImport.BlockList.Select(block => FetchStaticBlockFromImport(block)) : menuImport.BlockList.Select(block => FetchBlockFromImport(block)));
            return menu;
        }
        /// <summary>
        /// Creates a timeline independent event from the importer class
        /// </summary>
        /// <param name="actionImport"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        internal IAction FetchTimelineDependentActionFromImport(ActionExim actionImport)
        {
            switch (actionImport.Type)
            {
                case "TextBoxCreateAction":
                    // Add the textbox to the timeline.
                    if (actionImport.TextBox == null)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the textbox is null.");
                    }
                    if (actionImport.TextBox.CharactersPerSecond.HasValue is false)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the characters per second is null.");
                    }
                    if (actionImport.TextBox.Color == null)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the textbox color is null.");
                    }
                    if (actionImport.TextBox.BorderColor == null)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the textbox border is null.");
                    }
                    if (actionImport.TextBox.PositionType.HasValue is false)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the position type is null.");
                    }
                    if (actionImport.TextBox.HorizontalTextMargin.HasValue is false)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the horizontal text margin is null.");
                    }
                    if (actionImport.TextBox.VerticalTextMargin.HasValue is false)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the vertical text margin is null.");
                    }
                    if (actionImport.TextBox.WordWrap.HasValue is false)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the word wrap is null.");
                    }
                    if (actionImport.TextBox.Title == null)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the textbox title is null.");
                    }
                    if (actionImport.TextBox.Content == null)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the textbox content is null.");
                    }
                    var charactersPerSecond = actionImport.TextBox.CharactersPerSecond.Value;
                    var font = new Font() { BaseSize = 30, GlyphPadding = 5 };
                    var textboxcolor = actionImport.TextBox.Color == null || actionImport.TextBox.Color.Length < 4 ?
                    new Color() { R = 0, G = 0, B = 0, A = 255 } :
                    new Color()
                    {
                        R = (byte)actionImport.TextBox.Color[0],
                        G = (byte)actionImport.TextBox.Color[1],
                        B = (byte)actionImport.TextBox.Color[2],
                        A = (byte)actionImport.TextBox.Color[3]
                    };
                    var textboxborder = actionImport.TextBox.BorderColor == null || actionImport.TextBox.BorderColor.Length < 4 ?
                    new Color() { R = 0, G = 0, B = 0, A = 255 } :
                    new Color()
                    {
                        R = (byte)actionImport.TextBox.BorderColor[0],
                        G = (byte)actionImport.TextBox.BorderColor[1],
                        B = (byte)actionImport.TextBox.BorderColor[2],
                        A = (byte)actionImport.TextBox.BorderColor[3]
                    };
                    var positionType = (TextBox.PositionType)actionImport.TextBox.PositionType.Value;
                    var horizontalTextMargin = actionImport.TextBox.HorizontalTextMargin.Value;
                    var verticalTextMargin = actionImport.TextBox.VerticalTextMargin.Value;
                    var wordWrap = actionImport.TextBox.WordWrap.Value;
                    var textBoxTitle = actionImport.TextBox.Title;
                    var textBoxContent = actionImport.TextBox.Content;
                    //
                    var textBox = TextBox.CreateNewTextBox(
                        Game,
                        charactersPerSecond,
                        font,
                        textboxcolor,
                        textboxborder,
                        positionType,
                        horizontalTextMargin,
                        verticalTextMargin,
                        wordWrap,
                        textBoxTitle,
                        [.. textBoxContent]);
                    //
                    return new TextBoxCreateAction(textBox);
                case "AddSpriteAction":
                    // Add the sprite to the timeline.
                    if (actionImport.Sprite == null)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the sprite is null.");
                    }
                    var sprite = FetchSpriteFromImport(actionImport.Sprite);
                    return new AddSpriteAction(sprite, Game);
                case "TintSpriteAction":
                    // Add the tint action to the timeline.
                    if (actionImport.Sprite == null)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the sprite is null.");
                    }
                    if (actionImport.TintColor == null)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the tint color is null.");
                    }
                    var tintSprite = FetchSpriteFromImport(actionImport.Sprite);
                    var tintColor = new Color()
                    {
                        R = (byte)actionImport.TintColor[0],
                        G = (byte)actionImport.TintColor[1],
                        B = (byte)actionImport.TintColor[2],
                        A = (byte)actionImport.TintColor[3]
                    };
                    return new TintSpriteAction(tintSprite, tintColor, Game);
                case "ChangeSpriteAction":
                    //NINCS KÉSZ MÉG, PEDIG AZT HITTEM
                    return new ChangeSpriteAction(null, null, Game);
                case "RemoveSpriteAction":
                    // Add the remove action to the timeline.
                    if (actionImport.Sprite == null)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the sprite is null.");
                    }
                    var removeSprite = FetchSpriteFromImport(actionImport.Sprite);
                    return new RemoveSpriteAction(removeSprite, Game);
                case "LoadSceneAction":
                    // Add the load scene action to the timeline.
                    if (actionImport.SceneID.HasValue is false)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the scene id is null.");
                    }
                    var sceneId = actionImport.SceneID.Value;
                    return new LoadSceneAction(Game, sceneId, actionImport.TriggerVariableName);
                case "NativeLoadSceneAction":
                    // Add the native load scene action to the timeline.
                    if (actionImport.SceneID.HasValue is false)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the scene id is null.");
                    }
                    var nativeSceneId = actionImport.SceneID.Value;
                    return new NativeLoadSceneAction(Game, nativeSceneId);
                case "IncrementVariableAction":
                    // Add the increment variable action to the timeline.
                    if (actionImport.VariableName == null)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the variable name is null.");
                    }
                    if (actionImport.VariableValue == null)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the variable value is null.");
                    }
                    if (actionImport.ImpendingVariableName == null)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the impending variable type is null.");
                    }
                    return new IncrementVariableAction(Game, actionImport.VariableName, actionImport.ImpendingVariableName);
                case "DecrementVariableAction":
                    // Add the decrement variable action to the timeline.
                    if (actionImport.VariableName == null)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the variable name is null.");
                    }
                    if (actionImport.VariableValue == null)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the variable value is null.");
                    }
                    if (actionImport.ImpendingVariableName == null)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the impending variable type is null.");
                    }
                    return new DecrementVariableAction(Game, actionImport.VariableName, actionImport.ImpendingVariableName);
                case "SetVariableTrueAction":
                    // Add the set variable true action to the timeline.
                    if (actionImport.VariableName == null)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the variable name is null.");
                    }
                    return new SetVariableTrueAction(Game, actionImport.VariableName);
                case "SetVariableFalseAction":
                    // Add the set variable false action to the timeline.
                    if (actionImport.VariableName == null)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the variable name is null.");
                    }
                    return new SetVariableFalseAction(Game, actionImport.VariableName);
                case "SetBoolVariableAction":
                    // Add the set variable action to the timeline.
                    if (actionImport.VariableName == null)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the variable name is null.");
                    }
                    if (actionImport.VariableValue == null)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the variable value is null.");
                    }
                    return new SetBoolVariableAction(Game, actionImport.VariableName, bool.Parse(actionImport.VariableValue));
                case "ToggleVariableAction":
                    // Add the toggle variable action to the timeline.
                    if (actionImport.VariableName == null)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the variable name is null.");
                    }
                    return new ToggleVariableAction(Game, actionImport.VariableName);
                case "CreateMenuAction":
                    // Add the create menu action to the timeline.
                    if (actionImport.Menu == null)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the menu is null.");
                    }
                    var menu = FetchMenuFromImport(actionImport.Menu);
                    return new CreateMenuAction(Game, menu, [.. menu.BlockList]);
                default:
                    throw new InvalidOperationException("Failed to load scene settings, because Either the action type is not recognized, or the event is not a timeline dependent or a general one.");
            }
        }
        /// <summary>
        /// Creates a timeline independent event from the importer class
        /// Get event from the union of the timeline independent and general events.
        /// </summary>
        /// <param name="actionImport"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        internal ISettingsAction FetchTimelineIndependentActionFromImport(ActionExim actionImport)
        {
            switch (actionImport.Type)
            {
                case "NativeLoadSceneAction":
                    // Add the native load scene action to the timeline.
                    if (actionImport.SceneID.HasValue is false)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the scene id is null.");
                    }
                    var nativeSceneId = actionImport.SceneID.Value;
                    ISettingsAction NativeLoadSceneAction = (ISettingsAction)new NativeLoadSceneAction(Game, nativeSceneId);
                    return NativeLoadSceneAction;
                case "LoadSceneAction":
                    // Add the load scene action to the timeline.
                    if (actionImport.SceneID.HasValue is false)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the scene id is null.");
                    }
                    var sceneId = actionImport.SceneID.Value;
                    IAction LoadSceneAction = new LoadSceneAction(Game, sceneId, actionImport.TriggerVariableName);
                    return (ISettingsAction)LoadSceneAction;
                case "SetVariableValueAction":
                    // Add the set variable value action to the timeline.
                    // if (actionImport.VariableName == null)
                    // {
                    //     throw new InvalidOperationException("Failed to load scene settings, because the variable name is null.");
                    // }
                    // if (actionImport.BlockComponentID == null)
                    // {
                    //     throw new InvalidOperationException("Failed to load scene settings, because the variable value is null.");
                    // }
                    //ISettingsAction SetVariableValueAction = new SetVariableValueAction(Game, actionImport.VariableName, this, actionImport.BlockComponentID.Value);
                    ISettingsAction SetVariableValueAction = new SetVariableValueAction(Game, actionImport.VariableName, this, 1);
                    return SetVariableValueAction;
                case "CreateMenuAction":
                    // Add the create menu action to the timeline.
                    if (actionImport.StaticMenu == null)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the static menu is null.");
                    }
                    var menu = FetchMenuFromImport(actionImport.StaticMenu);
                    return new CreateMenuAction(Game, menu, [.. menu.BlockList]);
                case "SwitchStaticMenuAction":
                    // Add the switch static menu action to the timeline.
                    if (actionImport.DisablingMenuID == null)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the disabling menu id is null.");
                    }
                    if (actionImport.EnablingMenuID == null)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the enabling menu id is null.");
                    }
                    return new SwitchStaticMenuAction(Game, this, actionImport.DisablingMenuID.Value, actionImport.EnablingMenuID.Value);
                default:
                    throw new InvalidOperationException("Failed to load scene settings, because Either the action type is not recognized, or the event is not a timeline independent or a general one.");
            }
        }
    }
}