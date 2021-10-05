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
        private bool _isActive;
        public bool IsActive { get => _isActive; set => _isActive = value; }
        public ColorFood(Texture2D texture2D) : base(texture2D)
        {
            _width = 5;
            _height = 5;
            _isActive = true;
        }

        public ColorFood(Texture2D texture2D, Vector2 pos) : base(texture2D)
        {
            _width = 5;
            _height = 5;
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
    }
}
