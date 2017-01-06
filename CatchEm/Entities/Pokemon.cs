using System;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;

namespace CatchEm
{
    public class Pokemon : Entity
    {
        public const int POSITION_OFFSET = 10;
        public bool Caught { get; private set; }
        int _caught_index = 0;
        public Pokemon(Texture2D texture)
        {
            addComponent(new Sprite(texture));
            addComponent(new BoxCollider());
        }

        public void Catch(int caught_index)
        {
            if (Caught != true)
            {
                Caught = true;
                _caught_index = caught_index;
            }
        }

        public override void update()
        {
            base.update();
            if (Caught)
            {
                Player player = (Player)scene.findEntity("player");
                if (player == null)
                    return;
                var dist_x = Math.Abs(player.position.X - position.X);
                var dist_y = Math.Abs(player.position.Y - position.Y);
                float total_distance = (float)Math.Sqrt(dist_x * dist_x + dist_y * dist_y);
                Console.WriteLine(total_distance);
                Console.WriteLine(_caught_index / 10f);
                if (player.positions.Count > _caught_index * POSITION_OFFSET)
                    position = player.positions[_caught_index * POSITION_OFFSET];
            }
        }
    }
}
