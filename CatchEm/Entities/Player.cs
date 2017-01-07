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

        public int NumberOfPokemon = 1;
        public Vector2 velocity { get; set; }
        public List<Vector2> positions = new List<Vector2>();
        Collider _collider_feet;
        Collider _collider_body;
        const float ball_speed = 1000;


        public Player(Camera scene_camera)
        {
            name = "player";
            var camera = new FollowCamera(this, scene_camera);

            camera.mapLockEnabled = true;
            camera.mapSize = new Vector2(5000, 5000);
            camera.followLerp = 0.5f;
            addComponent(camera);
            transform.position = new Vector2(300, 4500);
            addComponent(new Sprite(texture));
            addComponent(new Movement());
            addComponent(new Gravity());

            // add colliders
            _collider_feet = new BoxCollider(-(texture.Width / 2) + 10, texture.Height / 2 - 10, texture.Width - 20, 10);
            _collider_body = new BoxCollider(-(texture.Width / 2), -(texture.Height / 2), texture.Width, texture.Height);
            //_collider_body = new PolygonCollider(new Vector2[] {
            //    new Vector2(-texture.Width / 2 , -texture.Height / 2 ),
            //    new Vector2(texture.Width / 2, -texture.Height / 2 ),
            //    new Vector2(texture.Width / 2 , texture.Height / 2 - 10),
            //    new Vector2(texture.Width / 2 - 10, texture.Height / 2),
            //    new Vector2(-texture.Width / 2 + 10, texture.Height / 2),
            //    new Vector2(-texture.Width / 2 , texture.Height / 2 - 10),
            //});
            //_collid
            _collider_body.collidesWithLayers = (1 << 2);
            _collider_feet.collidesWithLayers = (1 << 2);
            addComponent(_collider_feet);
            addComponent(_collider_body);
            addComponent(new Friction(_collider_body, 1, 0.5f));
            
        }


        public override void update()
        {
            base.update();
            var deltaMovement = new Vector2(velocity.X * Time.deltaTime, velocity.Y * Time.deltaTime);

            deltaMovement = process_collisions(deltaMovement);

            position += deltaMovement;

            var mouse_position = scene.camera.position + Input.mousePosition - new Vector2(1280 / 2, 720 / 2);
            float mouse_rotation = (float)Math.Atan2(mouse_position.Y - position.Y, mouse_position.X - position.X);
            if (Input.leftMouseButtonPressed)
            {
                scene.addEntity(new Pokeball(this, position, new Vector2((float)(Math.Cos(mouse_rotation) * ball_speed), (float)(Math.Sin(mouse_rotation) * ball_speed))));
            }

            positions.Insert(0, position);
            while (positions.Count > (NumberOfPokemon+1) * CaughtPokemon.POSITION_OFFSET + 10)
            {
                positions.RemoveAt(positions.Count - 1);
            }


            // Flip sprite if necessary
            var sprite = getComponent<Sprite>();
            if (mouse_position.X < position.X)
                sprite.flipX = true;
            else if (mouse_position.X > position.X)
                sprite.flipX = false;

            getComponent<Friction>().process();
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

            return deltaMovement;
        }
    }
}
