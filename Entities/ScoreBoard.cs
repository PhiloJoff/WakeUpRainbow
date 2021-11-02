using Microsoft.Xna.Framework.Graphics;
using PhiloEngine.src;
using System;
using System.Collections.Generic;
using System.Text;
using WakeUpRainbow.Core;

namespace WakeUpRainbow.Entities
{
    public class ScoreBoard : GameEntity
    {
        private string _score;
        public string Score { get => _score; set => _score = value; }

        private GraphicsDevice _graphics;

        private Dictionary<string,GameEntity> _entities;
        public ScoreBoard(GraphicsDevice graphicsDevice) : base()
        {
            _graphics = graphicsDevice;
        }

        public ScoreBoard(GraphicsDevice graphicsDevice, Texture2D texture2D, int width, int height) : base(texture2D, width, height)
        {
            _graphics = graphicsDevice;
        }

        public void InitEntities()
        {
            _entities = new Dictionary<string, GameEntity>();

            Texture2D texture = PhiloUtils.CreateTexture2D(_graphics, 5, 5, Microsoft.Xna.Framework.Color.Red);
            BasicEntity entity = new BasicEntity(texture, texture.Width, texture.Height);
            _entities.Add("Red", entity);

            texture = PhiloUtils.CreateTexture2D(_graphics, 5, 5, Microsoft.Xna.Framework.Color.Orange);
            entity = new BasicEntity(texture, texture.Width, texture.Height);
            _entities.Add("Orange", entity);

            texture = PhiloUtils.CreateTexture2D(_graphics, 5, 5, Microsoft.Xna.Framework.Color.Yellow);
            entity = new BasicEntity(texture, texture.Width, texture.Height);
            _entities.Add("Yellow", entity);

            texture = PhiloUtils.CreateTexture2D(_graphics, 5, 5, Microsoft.Xna.Framework.Color.Green);
            entity = new BasicEntity(texture, texture.Width, texture.Height);
            _entities.Add("Green", entity);

            texture = PhiloUtils.CreateTexture2D(_graphics, 5, 5, Microsoft.Xna.Framework.Color.Cyan);
            entity = new BasicEntity(texture, texture.Width, texture.Height);
            _entities.Add("Cyan", entity);

            texture = PhiloUtils.CreateTexture2D(_graphics, 5, 5, Microsoft.Xna.Framework.Color.Blue);
            entity = new BasicEntity(texture, texture.Width, texture.Height);
            _entities.Add("Blue", entity);

            texture = PhiloUtils.CreateTexture2D(_graphics, 5, 5, Microsoft.Xna.Framework.Color.Purple);
            entity = new BasicEntity(texture, texture.Width, texture.Height);
            _entities.Add("Purple", entity);

        }
    }
}
