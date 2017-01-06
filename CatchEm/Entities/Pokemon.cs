using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;

namespace CatchEm
{
    public class Pokemon : Entity, IVelocity
    {
        public const int POSITION_OFFSET = 10;
        public static int PokemonLayer = 100;
        public bool Caught { get; private set; }
        public Vector2 velocity { get; set; }
        private bool _reached_player = false;
        int _caught_index = 0;
        public Pokemon(Texture2D texture, Vector2 position)
        {
            this.position = position;
            addComponent(new Sprite(texture));
            getComponent<Sprite>().setRenderLayer(100);
            addComponent(new BoxCollider());
            getComponent<BoxCollider>().physicsLayer = (1 << 2);
        }

        public void Catch(int caught_index)
        {
            if (Caught != true)
            {
                Caught = true;
                _caught_index = caught_index;
                getComponent<Collider>().enabled = false;
                getComponent<Sprite>().setRenderLayer(PokemonLayer);
                PokemonLayer++;
                
            }
        }

        public override void update()
        {
            Vector2 old_position = position;

            base.update();
            if (Caught)
            {
                Player player = (Player)scene.findEntity("player");
                if (player == null)
                    return;
                var dist_x = Math.Abs(player.position.X - position.X);
                var dist_y = Math.Abs(player.position.Y - position.Y);
                float total_distance = (float)Math.Sqrt(dist_x * dist_x + dist_y * dist_y);
                if (player.positions.Count > _caught_index * POSITION_OFFSET)
                {
                    var dest_position = player.positions[_caught_index * POSITION_OFFSET];
                    if (!_reached_player)
                    {
                        position = Movement.MoveTowards(position, dest_position, total_distance * 0.1f);

                        float x_from_dest = Math.Abs(dest_position.X - position.X);
                        float y_from_dest = Math.Abs(dest_position.Y - position.Y);
                        float total_distance_from_dest = (float)Math.Sqrt(x_from_dest * x_from_dest + y_from_dest * y_from_dest);
                        if (total_distance_from_dest < 20)
                            _reached_player = true;
                    }
                    else
                    {
                        position = dest_position;
                    }
                }
            }
            else
            {
                UncaughtMovementProcessor();
                position += new Vector2(velocity.X * Time.deltaTime, velocity.Y * Time.deltaTime);
            }

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
