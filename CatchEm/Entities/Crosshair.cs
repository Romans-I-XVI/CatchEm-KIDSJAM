using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;

namespace CatchEm
{
    public class Crosshair : Entity
    {
        public static Texture2D texture = Core.content.Load<Texture2D>(Content.Textures.crosshair);

        public Crosshair()
        {
            addComponent(new Sprite(texture));
            transform.position = new Microsoft.Xna.Framework.Vector2(200, 4500);
        }

        public override void update()
        {
            base.update();
            transform.position = scene.camera.mouseToWorldPoint();
        }
    }
}
