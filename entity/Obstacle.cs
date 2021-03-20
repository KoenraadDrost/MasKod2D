﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasKod2D.entity
{
    public class Obstacle : StaticEntity
    {
        public Obstacle(Vector2D pos, World w, Texture2D t) : base(pos, w, t)
        {
            Color[] data = new Color[50 * 50];
            for (int i = 0; i < (50 * 50); ++i) data[i] = Color.Blue;
            Texture.SetData<Color>(data);
        }
    }
}