using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;

namespace CatchEm
{
    public class Pokeball : Entity, IVelocity
    {
        static List<Texture2D> textures = new List<Texture2D>()
        {
            Core.content.Load<Texture2D>(Content.Textures.ball_1),
            Core.content.Load<Texture2D>(Content.Textures.ball_2),
            Core.content.Load<Texture2D>(Content.Textures.ball_3),
            Core.content.Load<Texture2D>(Content.Textures.ball_4),
            Core.content.Load<Texture2D>(Content.Textures.ball_4),
            Core.content.Load<Texture2D>(Content.Textures.ball_4),
            //Core.content.Load<Texture2D>(Content.Textures.ball_5)
        };
        static SoundEffect snd_catch_pokemon = Core.content.Load<SoundEffect>(Content.Sounds.@catch);
        static SoundEffect snd_catch_large_pokemon = Core.content.Load<SoundEffect>(Content.Sounds.catch2);
        static SoundEffect snd_miss_pokemon = Core.content.Load<SoundEffect>(Content.Sounds.miss);

        public Vector2 velocity { get; set; }
        public bool HasCollided = false;
        private bool HitPokemon = false;
        Player _player;

        public Pokeball(Player player, Vector2 position, Vector2 velocity)
        {
            this._player = player;
            this.position = position;
            this.velocity = velocity;
            addComponent(new Sprite(textures[Nez.Random.range(0, textures.Count)]));
            var main_collider = new CircleCollider();
            main_collider.collidesWithLayers = (1 << 2) | (1 << 3) | (1 << 4);
            addComponent(main_collider);
            addComponent(new Gravity());
            addComponent(new Friction(main_collider));
        }

        public override void update()
        {
            base.update();

            CollisionResult collisionResult;
            collisionResult = getComponent<Friction>().process();

            if (!HasCollided && collisionResult.collider != null && collisionResult.collider.entity.getComponent<Sprite>() != null && collisionResult.collider.entity.getComponent<Sprite>().enabled)
            {
                HasCollided = true;
                if (collisionResult.collider.entity is Pokemon && _player != null)
                {
                    var pokemon = (Pokemon)collisionResult.collider.entity;

                    if (pokemon is LargeStaticPokemon || pokemon is OnyxPokemon)
                        snd_catch_large_pokemon.Play();
                    else
                        snd_catch_pokemon.Play();

                    pokemon.Catch(_player.NumberOfPokemon);
                    _player.NumberOfPokemon++;
                    HitPokemon = true;
                }
                else
                    snd_miss_pokemon.Play(0.7f, 0, 0);
            }

            position += new Vector2(velocity.X * Time.deltaTime, velocity.Y * Time.deltaTime);

            if (HasCollided) {
                float fade_rate = (HitPokemon) ? 0.7f : 0.95f;
                getComponent<Sprite>().color *= fade_rate;
                if (getComponent<Sprite>().color.A < 0.1f)
                    destroy();
            }

        }
    }
}
