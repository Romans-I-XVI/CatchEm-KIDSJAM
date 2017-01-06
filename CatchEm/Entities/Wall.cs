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
            width = width * texture.Width;
            height = height * texture.Height;
            transform.position = new Vector2(x + width / 2, y + height / 2);
            transform.scale = new Vector2(width / texture.Width, height / texture.Height);
            
            var sprite = new TiledSprite(texture);
            sprite.textureScale = new Vector2(1f / (width / texture.Width), 1f / (height / texture.Height));
            sprite.setRenderLayer(100000000);
            addComponent(sprite);

            var collider = new BoxCollider();
            collider.physicsLayer = (1 << 2);
            addComponent(collider);
        }

        public override void update()
        {
            base.update();
        }
    }
}
