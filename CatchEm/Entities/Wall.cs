using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;

namespace CatchEm
{
    public class Wall : Entity
    {
        public static Texture2D texture = Core.content.Load<Texture2D>(Content.Textures.wall);

        public Wall(int x, int y, int width, int height)
        {
            transform.position = new Vector2(x, y);
            addComponent(new Sprite(texture));
            //addComponent(new BoxCollider(-width / 2, -height / 2, width, height));
            addComponent(new BoxCollider());
            transform.scale = new Vector2(width / texture.Width, height / texture.Height);
        }

        public override void update()
        {
            base.update();
        }
    }
}
