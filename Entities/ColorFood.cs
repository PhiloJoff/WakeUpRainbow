using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using WakeUpRainbow.Core;

namespace WakeUpRainbow.Entities
{
    public class ColorFood : GameEntity
    {
        public static int defaultWidth = 8;
        public static int defaultHeight = 8;
        private bool _isActive = true;
        public bool IsActive { get => _isActive; set => _isActive = value; }
        public ColorFood(Texture2D texture2D) : base(texture2D)
        {
            _width = defaultWidth;
            _height = defaultHeight;
            _isActive = true;
        }

        public ColorFood(Texture2D texture2D, Vector2 pos) : base(texture2D)
        {
            _width = defaultWidth;
            _height = defaultHeight;
            _pos = pos;
            _isActive = true;
        }
        public ColorFood(Texture2D texture2D, Vector2 pos, Color color) : this(texture2D, pos)
        {
            _color = color;
        }
        public ColorFood(Texture2D texture2D, Vector2 pos, Color color, bool isActive) : this(texture2D, pos, color)
        {
            _isActive = isActive;
        }

        public ColorFood(Texture2D texture2D, int width, int height, Vector2 pos) : base(texture2D, width, height)
        {
            _width = width;
            _height = height;
            _pos = pos;
            _isActive = true;
        }

        public ColorFood(Texture2D texture2D, int width, int height, Vector2 pos, Color color) : this(texture2D, width, height, pos)
        {
            _color = color;
        }
        public ColorFood(Texture2D texture2D, int width, int height, Vector2 pos, Color color, bool isActive) : this(texture2D, width, height, pos, color)
        {
            _isActive = isActive;
        }


        public static ColorFood CreateColorFood(Texture2D texture2D, Color color, int PosMaxX, int PosMaxY)
        {
            Random random = new Random(DateTime.Now.Millisecond);

            int posX = random.Next(0, PosMaxX - defaultWidth);
            int posY = random.Next(0, PosMaxY - defaultHeight);
            ColorFood colorFood = new ColorFood(texture2D, new Vector2(posX, posY))
            {
                Color = color
            };

            return colorFood;
        }

        public void Move()
        {
            _speedX += (_speedX * _acceleration);
            if (_speedX > _speedMax)
                _speedX = _speedMax;
            _pos.X -= _speedX;
        }

    }
}
