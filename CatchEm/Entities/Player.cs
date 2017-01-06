using System;
using System.Collections.Generic;
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
        public List<Vector2> positions = new List<Vector2>();
        Collider _collider_feet;
        Collider _collider_head;
        Collider _collider_side1;
        Collider _collider_side2;
        const float ball_speed = 1000;
        int _caught_pokemon = 1;


        public Player(Camera scene_camera)
        {
            name = "player";
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
            _collider_feet = new BoxCollider(-(texture.Width / 2) + 10, texture.Height / 2 - 10, texture.Width - 20, 10);
            _collider_feet = new BoxCollider(-(texture.Width / 2) + 10, texture.Height / 2 - 10, texture.Width - 20, 10);
            _collider_head = new BoxCollider(-(texture.Width / 2) + 10, -(texture.Height / 2), texture.Width - 20, 10);
            _collider_side1 = new PolygonCollider(new Vector2[] {
                new Vector2(-texture.Width / 2 + 10, -texture.Height / 2 + 25),
                new Vector2(-texture.Width / 2, -texture.Height / 2 + 10 + 25),
                new Vector2(-texture.Width / 2, texture.Height / 2 - 10),
                new Vector2(-texture.Width / 2 + 10, texture.Height / 2)
            });
            _collider_side2 = new PolygonCollider(new Vector2[] {
                new Vector2(texture.Width / 2 - 10, -texture.Height / 2 + 25),
                new Vector2(texture.Width / 2, -texture.Height / 2 + 10 + 25),
                new Vector2(texture.Width / 2, texture.Height / 2 - 10),
                new Vector2(texture.Width / 2 - 10, texture.Height / 2)
            });
            _collider_head.collidesWithLayers = 2;
            _collider_feet.collidesWithLayers = 2;
            _collider_side1.collidesWithLayers = 2;
            _collider_side2.collidesWithLayers = 2;
            addComponent(_collider_feet);
            addComponent(_collider_head);
            addComponent(_collider_side1);
            addComponent(_collider_side2);
        }


        public override void update()
        {
            foreach (Pokemon item in scene.entitiesOfType<Pokemon>())
            {
                if (!item.Caught)
                {
                    item.Catch(_caught_pokemon);
                    _caught_pokemon++;
                }
            }
            base.update();

            var deltaMovement = new Vector2(velocity.X * Time.deltaTime, velocity.Y * Time.deltaTime);

            deltaMovement = process_collisions(deltaMovement);

            position += deltaMovement;


            if (Input.leftMouseButtonPressed)
            {
                var mouse_position = scene.camera.position + Input.mousePosition - new Vector2(1280 / 2, 720 / 2);
                float mouse_rotation = (float)Math.Atan2(mouse_position.Y - position.Y, mouse_position.X - position.X);
                scene.addEntity(new Pokeball(position, new Vector2((float)(Math.Cos(mouse_rotation) * ball_speed), (float)(Math.Sin(mouse_rotation) * ball_speed))));
            }

            positions.Add(position);
            while (positions.Count > (_caught_pokemon+1) * Pokemon.POSITION_OFFSET)
            {
                positions.RemoveAt(0);
            }

        }

        Vector2 process_collisions(Vector2 deltaMovement)
        {
            CollisionResult collisionResult;

            // Handle feet collision
            if (_collider_feet.collidesWithAny(ref deltaMovement, out collisionResult))
            {
                getComponent<Gravity>().enabled = false;
                velocity = new Vector2(velocity.X, -velocity.Y * 0.18f);
                if (Math.Abs(velocity.Y) < 10)
                    velocity = new Vector2(velocity.X, 0);
                getComponent<Movement>().state = Movement.states.idle;
            }
            else
            {
                getComponent<Gravity>().enabled = true;
            }


            if (_collider_side1.collidesWithAny(ref deltaMovement, out collisionResult))
            {
                velocity = new Vector2(-velocity.X * 0.2f, velocity.Y);
            }


            if (_collider_side2.collidesWithAny(ref deltaMovement, out collisionResult))
            {
                velocity = new Vector2(-velocity.X * 0.2f, velocity.Y);
            }


            if (_collider_head.collidesWithAny(ref deltaMovement, out collisionResult))
            {
                velocity = new Vector2(velocity.X, -velocity.Y * 0.2f);
            }


            return deltaMovement;
        }
    }
}
