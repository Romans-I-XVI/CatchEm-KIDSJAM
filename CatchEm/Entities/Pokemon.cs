using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;

namespace CatchEm
{
    public class Pokemon : Entity, IVelocity
    {
        public Vector2 velocity { get; set; }
        List<Texture2D> _textures;
        int _current_texture;
        Vector2 _old_position = new Vector2();

        public float RespawnRate = 50;

        public Pokemon(List<Texture2D> textures, Vector2 position)
        {
            _current_texture = Nez.Random.range(0, textures.Count);
            _textures = textures;
            this.position = position;
            addComponent(new Sprite(_textures[_current_texture]));
            getComponent<Sprite>().setRenderLayer(100);
            addComponent(new BoxCollider());
            getComponent<BoxCollider>().physicsLayer = (1 << 2);
        }

        public void Catch(int caught_index)
        {
            scene.addEntity(new CaughtPokemon(_textures[_current_texture], position, caught_index));
            var sprite = getComponent<Sprite>();
            var collider = getComponent<BoxCollider>();
            sprite.enabled = false;
            collider.enabled = false;
            Core.schedule(RespawnRate, (obj) => {
                var previous_layer_depth = getComponent<Sprite>().renderLayer;
                removeComponent<Sprite>();
                _current_texture = Nez.Random.range(0, _textures.Count);
                var new_texture = _textures[_current_texture];
                sprite = new Sprite(new_texture);
                sprite.setRenderLayer(previous_layer_depth);
                addComponent(sprite);
                collider.width = new_texture.Width;
                collider.height = new_texture.Height;
                collider.enabled = true;
            });
        }

        public override void update()
        {
            base.update();

            position += new Vector2(velocity.X * Time.deltaTime, velocity.Y * Time.deltaTime);

            var sprite = getComponent<Sprite>();
            if (_old_position.X < position.X)
                sprite.flipX = false;
            else if (_old_position.X > position.X)
                sprite.flipX = true;
            
            _old_position = position;
        }

    }
}
