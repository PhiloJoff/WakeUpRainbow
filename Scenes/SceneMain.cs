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
        protected ColorFood _colorFood;
        protected List<ColorFood> _colorFoods;
        public SceneMain(MainGame mainGame) : base(mainGame)
        {
            Texture2D textureCloud = PhiloUtils.CreateTexture2D(mainGame.GraphicsDevice, 30, 30);
            _cloud = new Cloud(textureCloud, textureCloud.Width, textureCloud.Height);

            _colorFoods = new List<ColorFood>();
            Texture2D textureFood = PhiloUtils.CreateTexture2D(mainGame.GraphicsDevice, 5, 5);
            _colorFood = new ColorFood(textureFood, textureFood.Width, textureFood.Height, new Vector2(100,100), Color.Red);
            _colorFoods.Add(_colorFood);

            Load();
        }

        public override void Load()
        {
            base.Load();
            _entityManager.AddEntity(_cloud);
            _entityManager.AddEntity(_colorFood);
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

            if(!_inputManager.KeyDown(Keys.Right) && !_inputManager.KeyDown(Keys.Left))
                _cloud.IsMovedX = false;

            if (!_inputManager.KeyDown(Keys.Up) && !_inputManager.KeyDown(Keys.Down))
                _cloud.IsMovedY = false;

            if (PhiloUtils.IsColide(_cloud.Rectangle, _colorFood.Rectangle)) 
            {
                _cloud.Color = _colorFood.Color;
            }


        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
