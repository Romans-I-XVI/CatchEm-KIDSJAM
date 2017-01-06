using System;
using Nez;
using Nez.Sprites;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace CatchEm
{
    public class FlyingPokemon : Entity
    {
        public static List<Texture2D> textures = new List<Texture2D>()
        {
            Core.content.Load<Texture2D>(Content.Textures.player),
            Core.content.Load<Texture2D>(Content.Textures.player),
            Core.content.Load<Texture2D>(Content.Textures.player),
            Core.content.Load<Texture2D>(Content.Textures.player),  
        };

        public float PathWidth;

        public FlyingPokemon(Vector2 position, float path_width)
        {
            PathWidth = path_width;
            this.position = position;
            addComponent(new Sprite(textures[Nez.Random.range(0, 4)]));
            addComponent(new BoxCollider());
        }
    }
}
