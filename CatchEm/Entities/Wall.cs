using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;

namespace CatchEm
{
    public class Wall : Entity
    {
        public static Texture2D wall1 = Core.content.Load<Texture2D>(Content.Textures.wall);
        public static List<Texture2D> walls2 = new List<Texture2D>() {
        Core.content.Load<Texture2D>(Content.Textures.wall_1),
        Core.content.Load<Texture2D>(Content.Textures.wall_2),
        Core.content.Load<Texture2D>(Content.Textures.wall_3),
        };

        public Wall(int x, int y, int width, int height, int type)
        {
            width = width * wall1.Width;
            height = height * wall1.Height;
            transform.position = new Vector2(x + width / 2, y + height / 2);
            transform.scale = new Vector2(width / wall1.Width, height / wall1.Height);

            TiledSprite sprite;
            if (type == 2)
                sprite = new TiledSprite(wall1);
            else
                sprite = new TiledSprite(walls2[Nez.Random.range(0, walls2.Count)]);
                
            sprite.textureScale = new Vector2(1f / (width / wall1.Width), 1f / (height / wall1.Height));
            sprite.setRenderLayer(100000000);
            addComponent(sprite);

            BoxCollider collider;
            if (type == 2)
                collider = new BoxCollider();
            else
                collider = new BoxCollider(-25, -15, 50, 40);
            collider.physicsLayer = (1 << 2);
            addComponent(collider);
        }

        public override void update()
        {
            base.update();
        }
    }
}
