using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PhiloEngine;
using PhiloEngine.src;
using System.Diagnostics;
using WakeUpRainbow.Scenes;

namespace WakeUpRainbow
{
    public class TheGame : MainGame
    {
        Scene MainScene;

        public TheGame() : base()
        {
        }

        protected override void Initialize()
        {
            base.Initialize();
            _graphics.PreferredBackBufferWidth = 960;  // set this value to the desired width of your window
            _graphics.PreferredBackBufferHeight = 540;   // set this value to the desired height of your window
            _graphics.ApplyChanges();
            MainScene = new SceneMain(this);
            GameState.SwitchScene(MainScene);

        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
