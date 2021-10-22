using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using WakeUpRainbow.Core;
using Microsoft.Xna.Framework;

namespace WakeUpRainbow.Entities
{
    public class CellBox : GameEntity
    {

        public CellBox()
        {
        }
        public CellBox(Texture2D texture2D, int width, int height, Vector2 pos) : base(texture2D,  width,  height)
        {
            _pos = pos;
        }

        public override string ToString()
        {
            return $"Cellbox : Pos {_pos.ToString()}";
        }
    }

    
}
