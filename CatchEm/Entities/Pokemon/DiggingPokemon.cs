using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;

namespace CatchEm
{
    public class DiggingPokemon : PathPokemon
    {
        public static List<Texture2D> textures = new List<Texture2D>()
        {
            Core.content.Load<Texture2D>(Content.Textures.player),
            Core.content.Load<Texture2D>(Content.Textures.player),
            Core.content.Load<Texture2D>(Content.Textures.player),
            Core.content.Load<Texture2D>(Content.Textures.player)
        };

        public DiggingPokemon(Vector2 position, List<Vector2> path, float speed = 2, Tween tween = Tween.LinearPause) : base(textures, position, path, speed, tween)
        {
            getComponent<Sprite>().setRenderLayer(100000001);
        }

    }
}
