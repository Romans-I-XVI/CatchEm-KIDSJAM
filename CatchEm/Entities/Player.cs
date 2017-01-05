using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;

namespace CatchEm
{
    public class Player : Entity, IVelocity
    {
        public static Texture2D texture = Core.content.Load<Texture2D>(Content.Textures.player);
        public Vector2 velocity { get; set; }
        BoxCollider _collider_feet;
        BoxCollider _collider_head;


        public Player(Camera scene_camera)
        {
            scene_camera.zoomOut(0.1f);
            var camera = new FollowCamera(this, scene_camera);
            camera.mapLockEnabled = true;
            camera.mapSize = new Vector2(5000, 5000);
            camera.followLerp = 0.5f;
            addComponent(camera);
            transform.position = new Vector2(150, 4500);
            addComponent(new Sprite(texture));
            addComponent(new Movement());
            addComponent(new Gravity());

            // add colliders
            _collider_feet = new BoxCollider(-(texture.Width / 2) + 2, texture.Height / 2 - 10, texture.Width - 4, 10);
            _collider_head = new BoxCollider(-(texture.Width / 2) + 2, -(texture.Height / 2), texture.Width - 4, 10);
            addComponent(_collider_feet);
            addComponent(_collider_head);
        }


        public override void update()
        {
            base.update();

            var deltaMovement = new Vector2(velocity.X * Time.deltaTime, velocity.Y * Time.deltaTime);

            CollisionResult collisionResult;

            // Handle feet collision
            if (_collider_feet.collidesWithAny(ref deltaMovement, out collisionResult))
            {
                Debug.log("collision result: {0}", collisionResult);
                getComponent<Gravity>().enabled = false;
                velocity = new Vector2(velocity.X, -velocity.Y * 0.2f);
                if (Math.Abs(velocity.Y) < 10)
                    velocity = new Vector2(velocity.X, 0);
                getComponent<Movement>().state = Movement.states.idle;
            }
            else
            {
                getComponent<Gravity>().enabled = true;
            }

            if (_collider_head.collidesWithAny(ref deltaMovement, out collisionResult))
            {
                velocity = new Vector2(velocity.X, 0);
            }

            position += deltaMovement;
        }
    }
}
