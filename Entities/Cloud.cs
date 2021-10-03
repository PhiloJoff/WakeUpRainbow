using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using WakeUpRainbow.Core;

namespace WakeUpRainbow.Entities
{
    public class Cloud : GameEntity
    {
        public Cloud(Texture2D texture2D, int width, int height) : base(texture2D, width, height)
        {
        }
        public Cloud(Texture2D texture2D, int width, int height, Vector2 pos) : base(texture2D, width, height)
        {
            _pos = pos;
        }
    }
}
