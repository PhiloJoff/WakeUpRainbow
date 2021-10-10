using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PhiloEngine;
using PhiloEngine.src;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using WakeUpRainbow.Entities;

namespace WakeUpRainbow.Scenes
{
    public class SceneMain : Scene
    {
        
        protected Cloud _cloud;
        /*protected ColorFood _colorFood;*/
        protected List<ColorFood> _colorFoods;
        private List<Color> _availableColors;
        private int _maxCurrentFoods = 3;
        private int _currentFoods = 0;
        private int _nextSpawnColorTime = 0;
        private int _currentTimeElapsed = 0;

        private Texture2D _textureFood;

        public SceneMain(MainGame mainGame) : base(mainGame)
        {
            _textureFood = PhiloUtils.CreateTexture2D(mainGame.GraphicsDevice, 30, 30);
            _cloud = new Cloud(_textureFood, _textureFood.Width, _textureFood.Height);

            _colorFoods = new List<ColorFood>();
            _textureFood = PhiloUtils.CreateTexture2D(mainGame.GraphicsDevice, ColorFood.defaultWidth, ColorFood.defaultHeight);

            _availableColors = new List<Color> { Color.Red, Color.Blue, Color.Green };

            Load();
        }

        public override void Load()
        {
            base.Load();
            _entityManager.AddEntity(_cloud);
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
                if (_cloud.Pos.X > 0)
                {
                    _cloud.IsMovedX = true;
                    _cloud.ToMove(Cloud.Move.Left);
                }
            }

            if (_inputManager.KeyDown(Keys.Right))
            {
                if (_cloud.Pos.X < _mainGame.Graphics.PreferredBackBufferWidth - _cloud.Width)
                {
                    _cloud.IsMovedX = true;
                    _cloud.ToMove(Cloud.Move.Right);
                }
            }

            if (_inputManager.KeyDown(Keys.Up))
            {
                if (_cloud.Pos.Y > 0)
                {
                    _cloud.IsMovedY = true;
                    _cloud.ToMove(Cloud.Move.Up);
                }
            }

            if (_inputManager.KeyDown(Keys.Down))
            {
                if (_cloud.Pos.Y < _mainGame.Graphics.PreferredBackBufferHeight - _cloud.Height)
                {
                    _cloud.IsMovedY = true;
                    _cloud.ToMove(Cloud.Move.Down);
                }
            }

            if (!_inputManager.KeyDown(Keys.Right) && !_inputManager.KeyDown(Keys.Left))
                _cloud.IsMovedX = false;

            if (!_inputManager.KeyDown(Keys.Up) && !_inputManager.KeyDown(Keys.Down))
                _cloud.IsMovedY = false;

            if (_colorFoods.Count > 0)
            {
                foreach (ColorFood colorFood in _colorFoods)
                {
                    if (PhiloUtils.IsColide(_cloud.Rectangle, colorFood.Rectangle))
                    {
                        _cloud.Color = colorFood.Color;
                        _entityManager.RemoveEntity(colorFood);
                        _colorFoods.Remove(colorFood);
                        break;
                    }

                }
                Debug.WriteLine($"Nombre d'entité : {_entityManager.Count()}");
            }

            GenerateFoods(gameTime);

        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        private void GenerateFoods(GameTime gameTime)
        {
            if (_currentFoods < _maxCurrentFoods)
            {
                Random random = new Random(DateTime.Now.Millisecond);
                //Premiere generatation de foods
                if (_nextSpawnColorTime == 0)
                {

                    ColorFood colorFood = ColorFood.CreateColorFood(_textureFood, _availableColors[random.Next(_availableColors.Count)], _mainGame.Graphics.PreferredBackBufferWidth, _mainGame.Graphics.PreferredBackBufferHeight);
                    _colorFoods.Add(colorFood);
                    _entityManager.AddEntity(colorFood);
                    _nextSpawnColorTime = random.Next(1000, 4500);
                    _currentFoods = _colorFoods.Count;
                } 
                else
                {
                    _currentTimeElapsed += gameTime.ElapsedGameTime.Milliseconds;
                    if (_currentTimeElapsed >= _nextSpawnColorTime)
                    {
                        ColorFood colorFood = ColorFood.CreateColorFood(_textureFood, _availableColors[random.Next(_availableColors.Count)], _mainGame.Graphics.PreferredBackBufferWidth, _mainGame.Graphics.PreferredBackBufferHeight);
                        _colorFoods.Add(colorFood);
                        _entityManager.AddEntity(colorFood);
                        _currentTimeElapsed = 0;
                        _currentFoods = _colorFoods.Count;
                    }
                }

            }
        }


    }
}
