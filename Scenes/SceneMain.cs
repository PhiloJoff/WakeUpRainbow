using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PhiloEngine;
using PhiloEngine.src;
using System;
using System.Collections.Generic;
using System.Text;
using WakeUpRainbow.Entities;

namespace WakeUpRainbow.Scenes
{
    public class SceneMain : Scene
    {
        protected Cloud _cloud; 
        public SceneMain(MainGame mainGame) : base(mainGame)
        {
            Texture2D texture = PhiloUtils.CreateTexture2D(mainGame.GraphicsDevice, 100, 100, Color.Red);
            _cloud = new Cloud(texture, texture.Width, texture.Height);
            _entityManager.AddEntity(_cloud);
        }

        public override void Load()
        {
            base.Load();
        }

        public override void Unload()
        {
            base.Unload();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (_mainGame.InputManager.KeyDown(Keys.Escape))
                _mainGame.Exit();
        }
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
