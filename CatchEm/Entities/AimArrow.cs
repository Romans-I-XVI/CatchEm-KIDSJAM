using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez;
using Nez.Sprites;

namespace CatchEm
{
    public class AimArrow : Entity
    {
        public static Texture2D texture = Core.content.Load<Texture2D>(Content.Textures.arrow);
        Vector2 _mouse_position = new Vector2();
        Vector2 _stick_offset;

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

            if (Input.gamePads[0].isConnected())
            {
                var stick_offset = Input.gamePads[0].getRightStick(0.1f);
                stick_offset.Y *= -1;
                if (stick_offset != Vector2.Zero)
                {
                    _stick_offset = Movement.MoveTowards(_stick_offset, stick_offset, Movement.TotalDistance(_stick_offset, stick_offset) * 0.25f);
                }
                _mouse_position = position + _stick_offset;
                
            }
            else
            {
                _mouse_position = scene.camera.mouseToWorldPoint();
            }

            rotation = (float)Math.Atan2(_mouse_position.Y - position.Y, _mouse_position.X - position.X);
            _player._aim_direction = rotation;
        }


    }
}
