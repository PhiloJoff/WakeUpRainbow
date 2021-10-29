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
using WakeUpRainbow.Core;

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
        private Vector2 _scorePos;
        private readonly int _colGameplayZone = 9;
        private readonly int _rowGameplayZone = 9;
        private CellBox[,] _gridGameplayZone;
        private CellBox _lastCellBox;

        //Combinations possibilitys
        private Dictionary<string, List<Color>> _colorCombinations;
        
        private List<Color> _eatColorsOrder;
        private List<Color> _eatColorsCurrent;

        public SceneMain(MainGame mainGame) : base(mainGame)
        {
            _texture = PhiloUtils.CreateBorderTexture2D(mainGame.GraphicsDevice, mainGame.Graphics.PreferredBackBufferWidth, 30, Color.Transparent, Color.White, 3);
            _scoreBoard = new ScoreBoard(_texture, _texture.Width, _texture.Height);

            _gameplayZone = new Rectangle(0, _scoreBoard.Height, mainGame.Graphics.PreferredBackBufferWidth, mainGame.Graphics.PreferredBackBufferHeight - _scoreBoard.Height);

            _texture = PhiloUtils.CreateTexture2D(mainGame.GraphicsDevice, 30, 30, Color.White);
            _cloud = new Cloud(_texture, _texture.Width, _texture.Height, new Vector2(0, _scoreBoard.Height));

            _colorFoods = new List<ColorFood>();


            _availableColors = new List<Color> { Color.Red, Color.Blue, Color.Green };

            _eatColors = new Color[2];

            _gridGameplayZone = new CellBox[_colGameplayZone, _rowGameplayZone];

            _scorePos = new Vector2(_scoreBoard.Pos.X + 20, _scoreBoard.Pos.Y + 3);

            //lastcellbox init
            _lastCellBox = new CellBox();

            //initialization grid gameplay
            InitGridGameplayZone(_gameplayZone.Width, _gameplayZone.Height, _colGameplayZone, _rowGameplayZone);

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
            
            
            ColorFoodCollision();
            

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
            _texture = PhiloUtils.CreateTexture2D(_mainGame.GraphicsDevice, ColorFood.defaultWidth, ColorFood.defaultHeight);
            if (_currentTimeElapsed >= _nextSpawnColorTime)
            {
                Random random = new Random(DateTime.Now.Millisecond);
                ColorFood colorFood = ColorFood.CreateColorFood(_texture, _availableColors[random.Next(_availableColors.Count)], _gameplayZone.Width, _gameplayZone.Height);
                _colorFoods.Add(colorFood);
                _entityManager.AddEntity(colorFood);
                _currentTimeElapsed = 0;
                _currentFoods = _colorFoods.Count;
                _nextSpawnColorTime = random.Next(1000, 4500);

            }
            
        }


        /// <summary>
        /// Return Color combination available
        /// </summary>
        /// <param name="colors">Array with 2 colors to combinate</param>
        /// <returns>Return the combination color if avaible</returns>
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
            spriteBatch.DrawString(font, $"SCORE : {score} pts", _scorePos, Color.White);
        }
 
        /// <summary>
        /// Initialize the grid gameplay zone
        /// </summary>
        /// <param name="width">width of the gameplayzone</param>
        /// <param name="height">height of the gameplayzone</param>
        /// <param name="cols">number of columns in the gameplayzone</param>
        /// <param name="rows">number of rows in the gameplayzone</param>
        private void InitGridGameplayZone(int width, int height, int cols, int rows)
        {
            int cellWidth = (int)((1f / cols) * width);
            int cellHeigth = (int)((1f / rows) * height);
            Texture2D cellTexture = PhiloUtils.CreateBorderTexture2D(_mainGame.GraphicsDevice, cellWidth, cellHeigth, Color.Transparent, Color.White, 1);
            for (int x = 0; x < cols; x ++)
            {
                for (int y = 0; y < rows ; y++)
                {
                    Vector2 cellPos = new Vector2(_gameplayZone.X, _gameplayZone.Y) + new Vector2((float) x / cols * width, (float) y / rows * height);
                    _gridGameplayZone[x, y] = new CellBox(cellTexture, cellWidth, cellHeigth, cellPos)
                    {
                        Rectangle = new Rectangle(x / cols * width, y / rows * height, cellWidth, cellHeigth)
                    };

                    _entityManager.AddEntity(_gridGameplayZone[x, y]);
                }
            }
        }

        private void ColorFoodCollision()
        {
            foreach (ColorFood colorFood in _colorFoods)
            {
                if (_colorFoods.Count > 0)
                {
                    if (PhiloUtils.IsColide((int)_cloud.Pos.X, (int)_cloud.Pos.Y, _cloud.Width, _cloud.Height, (int)colorFood.Pos.X, (int)colorFood.Pos.Y, colorFood.Width, colorFood.Height))
                    {

                        // Check if the first element is null (== Color.Transparent)
                        if (_eatColors[0] == Color.Transparent)
                        {
                            _eatColors[0] = colorFood.Color;
                            _cloud.Color = colorFood.Color;
                            _score += 10;
                        }
                        else
                        {
                            _eatColors[1] = colorFood.Color;
                            _score += 10;
                            Color combinationColor = GetCombination(_eatColors);
                            if (!_availableColors.Contains(combinationColor) && combinationColor != Color.Transparent)
                            {
                                _availableColors.Add(combinationColor);
                                _score += 5;

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
                        DetectPosCell(_cloud);

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
        }


        private CellBox DetectPosCell(GameEntity entity)
        {

            for (int x = 0; x < _gridGameplayZone.GetLength(0); x++)
            {
                for (int y = 0; y < _gridGameplayZone.GetLength(1); y++)

                    if (PhiloUtils.IsColide((int)entity.Pos.X, (int)entity.Pos.Y, entity.Width, entity.Height, 
                            (int)_gridGameplayZone[x,y].Pos.X, (int)_gridGameplayZone[x, y].Pos.Y, _gridGameplayZone[x, y].Width, _gridGameplayZone[x, y].Height))
                    {
                        Debug.WriteLine($"[{x},{y}] {_gridGameplayZone[x, y]}");
                        return _gridGameplayZone[x, y];
                    }
            }

            return null;
        }

    }
}
