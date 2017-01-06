using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;

namespace CatchEm
{
    public class Pokemon : Entity, IVelocity
    {
        public Vector2 velocity { get; set; }
        private bool _reached_player = false;
        Texture2D _texture;
        public Pokemon(Texture2D texture, Vector2 position)
        {
            _texture = texture;
            this.position = position;
            addComponent(new Sprite(texture));
            getComponent<Sprite>().setRenderLayer(100);
            addComponent(new BoxCollider());
            getComponent<BoxCollider>().physicsLayer = (1 << 2);
        }

        public void Catch(int caught_index)
        {
            scene.addEntity(new CaughtPokemon(_texture, position, caught_index));
        }

        public override void update()
        {
            Vector2 old_position = position;

            base.update();
            UncaughtMovementProcessor();
            position += new Vector2(velocity.X * Time.deltaTime, velocity.Y * Time.deltaTime);

            var sprite = getComponent<Sprite>();
            if (old_position.X < position.X)
                sprite.flipX = false;
            else if (old_position.X > position.X)
                sprite.flipX = true;
        }

        protected virtual void UncaughtMovementProcessor()
        {
        }
    }
}
