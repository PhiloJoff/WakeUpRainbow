using Microsoft.Xna.Framework.Graphics;
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

        private List<GameEntity> _entities;
        public ScoreBoard() : base()
        {
        }

        public ScoreBoard(Texture2D texture2D, int width, int height) : base(texture2D, width, height)
        {
        }

    }
}
