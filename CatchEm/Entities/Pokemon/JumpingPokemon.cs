using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;

namespace CatchEm
{
    public class JumpingPokemon : Pokemon
    {
        public static List<Texture2D> textures = new List<Texture2D>()
        {
            Core.content.Load<Texture2D>(Content.Textures.player),
            Core.content.Load<Texture2D>(Content.Textures.player),
            Core.content.Load<Texture2D>(Content.Textures.player),
            Core.content.Load<Texture2D>(Content.Textures.player)
        };
        public JumpingPokemon(Vector2 position) : base(textures, position)
        {
            var collider = getComponent<Collider>();
            collider.collidesWithLayers = (1 << 2) | (1 << 3);
            addComponent(new Friction(collider));
            addComponent(new Gravity());
        }

        public override void update()
        {
            getComponent<Friction>().process();
            base.update();
        }
    }
}
