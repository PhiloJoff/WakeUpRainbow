using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using PhiloEngine;
using PhiloEngine.src;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using WakeUpRainbow.Entities;

namespace WakeUpRainbow.Scenes
{
    public class SceneMain : Scene
    {

        //Entities
        private ScoreBoard _scoreBoard;
        protected Cloud _cloud;
        protected List<ColorFood> _colorFoods;

        //Entities features
        private List<Color> _availableColors;
        private readonly int _maxCurrentFoods = 3;
        private int _currentFoods = 0;

        //Gestion du temps pour spawn les foods
        private int _nextSpawnColorTime = 0;
        private int _currentTimeElapsed = 0;

        // Usefull variables
        private Texture2D _texture;
        private Color[] _eatColors;
        private Rectangle _gameplayZone;
        private SpriteFont _font;
        private int _score;
        private readonly int _colGameplayZone = 9;
        private readonly int _rowGameplayZone = 9;
        private Rectangle[,] _gridGameplayZone;

        //Combinations possibilitys
        private Dictionary<string, List<Color>> _colorCombinations;
        
        private List<Color> _eatColorsOrder;
        private List<Color> _eatColorsCurrent;

        public SceneMain(MainGame mainGame) : base(mainGame)
        {
            _texture = PhiloUtils.CreateTexture2D(mainGame.GraphicsDevice, 30, 30, Color.White);
            _cloud = new Cloud(_texture, _texture.Width, _texture.Height);

            _texture = PhiloUtils.CreateBorderTexture2D(mainGame.GraphicsDevice, mainGame.Graphics.PreferredBackBufferWidth, 30, Color.Transparent, Color.White, 3);
            _scoreBoard = new ScoreBoard(_texture, _texture.Width, _texture.Height);

            _colorFoods = new List<ColorFood>();
            _texture = PhiloUtils.CreateTexture2D(mainGame.GraphicsDevice, ColorFood.defaultWidth, ColorFood.defaultHeight);


            _availableColors = new List<Color> { Color.Red, Color.Blue, Color.Green };

            _gameplayZone = new Rectangle(0, _scoreBoard.Height, mainGame.Graphics.PreferredBackBufferWidth, mainGame.Graphics.PreferredBackBufferHeight - _scoreBoard.Height);
            _eatColors = new Color[2];

            _gridGameplayZone = new Rectangle[_colGameplayZone, _rowGameplayZone];

            //initialization grid gameplay

            //initialization color
            _initCombination();
            Load();
        }

        public override void Load()
        {
            base.Load();
            _entityManager.AddEntity(_cloud);
            _entityManager.AddEntity(_scoreBoard);
            _font = _mainGame.Content.Load<SpriteFont>(@"Fonts\Courier_New");
        }

        public override void Unload()
        {
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (_inputManager.KeyDown(Keys.Escape))
                _mainGame.Exit();

            if (_inputManager.KeyDown(Keys.Left))
            {
                if (_cloud.Pos.X >= _gameplayZone.Left)
                {
                    _cloud.IsMovedX = true;
                    _cloud.ToMove(Cloud.Movement.Left);
                    if (_cloud.Pos.X < _gameplayZone.Left)
                        _cloud.Pos = new Vector2(_gameplayZone.Left, _cloud.Pos.Y);
                }
            }

            if (_inputManager.KeyDown(Keys.Right))
            {
                if (_cloud.Pos.X <= _gameplayZone.Right - _cloud.Width)
                {
                    _cloud.IsMovedX = true;
                    _cloud.ToMove(Cloud.Movement.Right);
                    if (_cloud.Pos.X > _gameplayZone.Right - _cloud.Width)
                        _cloud.Pos = new Vector2(_gameplayZone.Right - _cloud.Width, _cloud.Pos.Y);
                }
            }

            if (_inputManager.KeyDown(Keys.Up))
            {
                if (_cloud.Pos.Y >= _gameplayZone.Top)
                {
                    _cloud.IsMovedY = true;
                    _cloud.ToMove(Cloud.Movement.Up);
                    if (_cloud.Pos.Y < _gameplayZone.Top)
                        _cloud.Pos = new Vector2(_cloud.Pos.X, _gameplayZone.Top);
                }
            }

            if (_inputManager.KeyDown(Keys.Down))
            {
                if (_cloud.Pos.Y <= _gameplayZone.Bottom - _cloud.Height)
                {
                    _cloud.IsMovedY = true;
                    _cloud.ToMove(Cloud.Movement.Down);
                    if (_cloud.Pos.Y > _gameplayZone.Bottom - _cloud.Height)
                        _cloud.Pos = new Vector2( _cloud.Pos.X, _gameplayZone.Bottom - _cloud.Height);
                }
            }

            if (!_inputManager.KeyDown(Keys.Right) && !_inputManager.KeyDown(Keys.Left))
                _cloud.IsMovedX = false;

            if (!_inputManager.KeyDown(Keys.Up) && !_inputManager.KeyDown(Keys.Down))
                _cloud.IsMovedY = false;


            foreach (ColorFood colorFood in _colorFoods)
            {
                if (_colorFoods.Count > 0)
                {
                    if (PhiloUtils.IsColide((int) _cloud.Pos.X, (int)_cloud.Pos.Y, _cloud.Width, _cloud.Height, (int)colorFood.Pos.X, (int)colorFood.Pos.Y, colorFood.Width, colorFood.Height))
                    {

                        // Check if the first element is null (== Color.Transparent)
                        if (_eatColors[0] == Color.Transparent)
                        {
                            _eatColors[0] = colorFood.Color;
                            _cloud.Color = colorFood.Color;
                        }
                        else
                        {
                            _eatColors[1] = colorFood.Color;
                            Color combinationColor = GetCombination(_eatColors);
                            if (!_availableColors.Contains(combinationColor) && combinationColor != Color.Transparent)
                            {
                                _availableColors.Add(combinationColor);

                                if (combinationColor == _eatColorsOrder[_eatColorsCurrent.Count])
                                {
                                    _eatColorsCurrent.Add(combinationColor);

                                }
                                Debug.WriteLine(_eatColorsCurrent.Count);
                            }

                            //Reinitialize the array of eatFood
                            _eatColors = new Color[2];
                            _cloud.Color = Color.White;
                        }

                        RemoveColorFood(colorFood);
                        break;
                    }

                    colorFood.Move();
                    
                    if (colorFood.Pos.X < _gameplayZone.Left)
                    {
                        RemoveColorFood(colorFood);
                        break;
                    }
                }
            }

            if (_currentFoods < _maxCurrentFoods)
                GenerateFoods(gameTime);



        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            DrawScore(_spriteBatch, _font, _score);
        }

        private void _initCombination()
        {
            _colorCombinations = new Dictionary<string, List<Color>>
            {
                { "Red", new List<Color> { Color.Red, Color.Red } },
                { "Blue", new List<Color> { Color.Blue, Color.Blue } },
                { "Green", new List<Color> { Color.Green, Color.Green } },
                { "Yellow", new List<Color> { Color.Green, Color.Red } },
                { "Orange", new List<Color> { Color.Red, Color.Yellow } },
                { "Cyan", new List<Color> { Color.Blue, Color.Green } },
                { "Purple", new List<Color> { Color.Red, Color.Blue } }
            };

            _eatColorsOrder = new List<Color>()
            {
                Color.Red, Color.Orange, Color.Yellow, Color.Green, Color.Cyan, Color.Blue, Color.Purple
            };

            _eatColorsCurrent = new List<Color>();
        }

        private void GenerateFoods(GameTime gameTime)
        {
            _currentTimeElapsed += gameTime.ElapsedGameTime.Milliseconds;
            if (_currentTimeElapsed >= _nextSpawnColorTime)
            {
                Random random = new Random(DateTime.Now.Millisecond);
                ColorFood colorFood = ColorFood.CreateColorFood(_texture, _availableColors[random.Next(_availableColors.Count)], _mainGame.Graphics.PreferredBackBufferWidth, _mainGame.Graphics.PreferredBackBufferHeight);
                _colorFoods.Add(colorFood);
                _entityManager.AddEntity(colorFood);
                _currentTimeElapsed = 0;
                _currentFoods = _colorFoods.Count;
                _nextSpawnColorTime = random.Next(1000, 4500);

            }
            
        }


        //Return Color.Transparent if combination is not available
        private Color GetCombination(Color[] colors)
        {
            if (colors.Length != 2)
                throw new Exception();
            List<Color> colorsList = new List<Color> { colors[0], colors[1] };

            foreach (KeyValuePair<string, List<Color>> keyValue in _colorCombinations)
            {
                if(PhiloUtils.UnorderedEqual(keyValue.Value, colorsList))
                {
                    return PhiloUtils.GetColorByName(keyValue.Key);
                }
            }

            return Color.Transparent;
        }

        private void RemoveColorFood(ColorFood colorFood)
        {
            _entityManager.RemoveEntity(colorFood);
            _colorFoods.Remove(colorFood);
            _currentFoods = _colorFoods.Count;
        }

        private void DrawScore(SpriteBatch spriteBatch, SpriteFont font, int score)
        {
            spriteBatch.DrawString(font, $"SCORE : {score}", Vector2.Zero, Color.White);
        }



    }
}
