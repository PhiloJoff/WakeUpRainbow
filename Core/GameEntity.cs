using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PhiloEngine.src;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace WakeUpRainbow.Core
{
    public abstract class GameEntity : IGameEntity
    {
        public enum Movement
        {
            None,
            Left,
            Right,
            Up,
            Down
        }

        protected float _speedX = 1f;
        public float SpeedX { get => _speedX; set => _speedX = value; }
        protected float _speedY = 1f;
        public float SpeedY { get => _speedY; set => _speedY = value; }
        protected float _speedInit = 1f;

        protected float _speedMax = 10f;
        public float SpeedMax { get => _speedMax; }

        protected float _acceleration = 0.05f;
        public float Acceleration { get => _acceleration; }

        protected bool _isMovedX = false;
        public bool IsMovedX { get => _isMovedX; set => _isMovedX = value; }

        protected bool _isMovedY = false;
        public bool IsMovedY { get => _isMovedY; set => _isMovedY = value; }

        protected Texture2D _texture2D;
        public Texture2D Texture2D { get =>  _texture2D; set =>  _texture2D = value; }

        protected Vector2 _pos;
        public Vector2 Pos { get => _pos; set => _pos = value; }

        protected Color _color;
        public Color Color { get => _color; set => _color = value; }

        protected float _rotation;
        public float Rotation { get => _rotation; set => _rotation = value; }

        protected Vector2 _origin;
        public Vector2 Origin { get => _origin; set => _origin = value; }

        protected float _scale;
        public float Scale { get => _scale; set => _scale = value; }

        protected SpriteEffects _spriteEffects;
        public SpriteEffects SpriteEffects { get => _spriteEffects; set => _spriteEffects = value; }

        protected float _layerDepth;
        public float LayerDepth { get => _layerDepth; set => _layerDepth = value; }

        protected int _drawOrder;
        public int DrawOrder { get => _drawOrder; set => _drawOrder = value; }

        protected int _width;
        public int Width { get => _width; set => _width = value; }

        protected int _height;
        public int Height { get => _height; set => _height = value; }

        protected int _updateOrder;
        public int UpdateOrder { get => _updateOrder; set => _updateOrder = value; }

        protected Rectangle _rectangle;
        public Rectangle Rectangle { get => _rectangle; set => _rectangle = value; }


        public GameEntity()
        {
            _texture2D = null;
            _width = 0;
            _height = 0;
            _pos = Vector2.Zero;
            _color = Color.White;
            _rotation = 0;
            _origin = Vector2.Zero;
            _scale = 1.0f;
            _spriteEffects = SpriteEffects.None;
            _layerDepth = 0;
            _drawOrder = 0;
            _updateOrder = 0;
            _rectangle = new Rectangle((int)_pos.X, (int)_pos.Y, _width, _height);
        }
        public GameEntity(Texture2D texture2D) : this()
        {
            _texture2D = texture2D;
        }
        public GameEntity(Texture2D texture2D, int width, int height) : this(texture2D)
        {
            _width = width;
            _height = height;
        }

        public virtual void Load()
        {
        }

        public virtual void Unload()
        {
        }

        public virtual void Update(GameTime gameTime)
        {
            _rectangle = new Rectangle((int)_pos.X, (int)_pos.Y, _width, _height);
        }

    }
}
