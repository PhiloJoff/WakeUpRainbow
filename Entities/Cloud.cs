using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public void ToMove(Movement move)
        {
            switch (move)
            {
                case Movement.None:
                    _isMovedX = false;
                    _isMovedY = false;
                    break;
                case Movement.Left:
                    _isMovedX = true;
                    _speedX += (_speedX * _acceleration);
                    _pos.X -= _speedX;
                    break;
                case Movement.Right:
                    _isMovedX = true;
                    _speedX += (_speedX * _acceleration);
                    _pos.X += _speedX;
                    break;
                case Movement.Up:
                    _isMovedY = true;
                    _speedY += (_speedY * _acceleration);
                    _pos.Y -= _speedY;
                    break;
                case Movement.Down:
                    _isMovedY = true;
                    _speedY += (_speedY * _acceleration);
                    _pos.Y += _speedY;
                    break;
                default:
                    break;
            }
            if (_speedX > _speedMax)
                _speedX = _speedMax;
            if (_speedY > _speedMax)
                _speedY = _speedMax;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if(!_isMovedX && _speedX > _speedInit)
            {
                _speedX -= (_speedX * _acceleration);
                if (_speedX < _speedInit)
                    _speedX = _speedInit;
            }
            if (!_isMovedY && _speedY > _speedInit)
            {
                _speedY -= (_speedY * _acceleration);
                if (_speedY < _speedInit)
                    _speedY = _speedInit;
            }
        }

    }
}
