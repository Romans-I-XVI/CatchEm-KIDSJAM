using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;

namespace CatchEm
{
    public class OnyxPokemon : PathPokemon
    {
        public static List<Texture2D> textures = new List<Texture2D>()
        {
            Core.content.Load<Texture2D>(Content.Textures.Pokemon.onyx),
        };

        public OnyxPokemon(Vector2 position, List<Vector2> path, float speed = 5, Tween tween = Tween.LinearPause) : base(textures, position, path, speed, tween)
        {
            RespawnRate = 300;
        }

    }
}