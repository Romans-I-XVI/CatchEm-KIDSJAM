using System;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;

namespace CatchEm
{
    public class Crosshair : Entity
    {
        public static Texture2D texture = Core.content.Load<Texture2D>(Content.Textures.crosshair);
        Camera _follow_camera;

        public Crosshair(Camera follow_camera)
        {
            _follow_camera = follow_camera;
            addComponent(new Sprite(texture));
            transform.position = new Microsoft.Xna.Framework.Vector2(200, 4500);
        }

        public override void update()
        {
            base.update();
            transform.position = Input.mousePosition + (_follow_camera.position - new Microsoft.Xna.Framework.Vector2(1280/2, 720/2));
            Console.WriteLine(transform.position);
        }
    }
}
