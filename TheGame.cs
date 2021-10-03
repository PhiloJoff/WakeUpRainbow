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
