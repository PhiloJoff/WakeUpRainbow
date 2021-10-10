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
        private int defaultWidth = 8;
        private int defaultHeight = 8;
        private bool _isActive;
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
            ColorFood colorFood = new ColorFood(texture2D)
            {
                Color = color
            };

            Random random = new Random(DateTime.Now.Millisecond);
            int posX = random.Next(0 + colorFood.Width, PosMaxX - colorFood.Width);
            int posY = random.Next(0 + colorFood.Height, PosMaxY - colorFood.Height);

            colorFood.Pos = new Vector2(posX, posY);
            Debug.WriteLine(colorFood.Pos);
            return colorFood;
        }
    }
}
