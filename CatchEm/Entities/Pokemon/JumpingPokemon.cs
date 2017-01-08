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
            Core.content.Load<Texture2D>(Content.Textures.Pokemon.hitmonlee),
            Core.content.Load<Texture2D>(Content.Textures.Pokemon.pinsir),
            Core.content.Load<Texture2D>(Content.Textures.Pokemon.pikachu_1),
            Core.content.Load<Texture2D>(Content.Textures.Pokemon.pikachu_2)
        };

        readonly GameTimeSpan _timer = new GameTimeSpan();
        float _jump_interval;
        float _jump_strength;

        public JumpingPokemon(Vector2 position, float jump_interval = 2000, float jump_strength = 1000) : base(textures, position)
        {
            _jump_interval = jump_interval;
            _jump_strength = jump_strength;
            var collider = getComponent<Collider>();
            collider.collidesWithLayers = (1 << 2) | (1 << 3);
            addComponent(new Friction(collider));
            addComponent(new Gravity());
        }

        public override void update()
        {
            getComponent<Friction>().process();

            if (_timer.TotalMilliseconds >= _jump_interval)
            {
                velocity += new Vector2(0, -_jump_strength);
                _timer.Mark();
            }

            base.update();
            
        }
    }
}
