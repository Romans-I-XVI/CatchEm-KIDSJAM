using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;

namespace CatchEm
{
    public class AimArrow : Entity
    {
        public static Texture2D texture = Core.content.Load<Texture2D>(Content.Textures.arrow);

        Player _player;
        public AimArrow(Player player)
        {
            _player = player;
            addComponent(new Sprite(texture));
        }

        public override void update()
        {
            base.update();

            position = _player.position;

            var mouse_position = scene.camera.position + Input.mousePosition - new Vector2(1280 / 2, 720 / 2);

            rotation = (float)Math.Atan2(mouse_position.Y - position.Y, mouse_position.X - position.X);
        }
    }
}
