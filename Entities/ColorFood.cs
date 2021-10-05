using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using WakeUpRainbow.Core;

namespace WakeUpRainbow.Entities
{
    public class ColorFood : GameEntity
    {

        public ColorFood(Texture2D texture2D) : base(texture2D)
        {
            _width = 5;
            _height = 5;
        }

        public ColorFood(Texture2D texture2D, Vector2 pos) : base(texture2D)
        {
            _width = 5;
            _height = 5;
            _pos = pos;
        }
        public ColorFood(Texture2D texture2D, Vector2 pos, Color color) : base(texture2D)
        {
            _width = 5;
            _height = 5;
            _pos = pos;
            _color = color;
        }

        public ColorFood(Texture2D texture2D, int width, int height) : base(texture2D, width, height)
        {
            _width = width;
            _height = height;
        }

        public ColorFood(Texture2D texture2D, int width, int height, Vector2 pos, Color color) : base(texture2D, width, height)
        {
            _width = 5;
            _height = 5;
            _pos = pos;
            _color = color;
        }
    }
}
