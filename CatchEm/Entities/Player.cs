using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez;
using Nez.Sprites;

namespace CatchEm
{
    public class Player : Entity, IVelocity
    {
        public static Texture2D texture = Core.content.Load<Texture2D>(Content.Textures.player);
        static SoundEffect snd_jump2 = Core.content.Load<SoundEffect>(Content.Sounds.jump2);

        public int NumberOfPokemon = 1;
        public Vector2 velocity { get; set; }
        public List<Vector2> positions = new List<Vector2>();
        Collider _collider_feet;
        Collider _collider_body;
        const float ball_speed = 1000;
        VirtualButton _fire;
        public float _aim_direction;


        public Player(Camera scene_camera)
        {
            _fire = new VirtualButton();
            _fire.addMouseLeftButton();
            _fire.addGamePadButton(0, Buttons.B);
            _fire.addGamePadButton(0, Buttons.RightTrigger);
            _fire.addGamePadButton(0, Buttons.RightShoulder);

            name = "player";
            scene_camera.zoomOut(0.2f);
            var camera = new FollowCamera(this, scene_camera);

            camera.mapLockEnabled = true;
            camera.mapSize = new Vector2(5000, 5000);
            camera.followLerp = 0.5f;
            addComponent(camera);
            transform.position = new Vector2(300, 4500);
            addComponent(new Sprite(texture));
            addComponent(new Movement());
            addComponent(new Gravity());
            getComponent<Movement>().Jumped += () => snd_jump2.Play();

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

            if (_fire.isPressed)
            {
                scene.addEntity(new Pokeball(this, position, new Vector2((float)(Math.Cos(_aim_direction) * ball_speed), (float)(Math.Sin(_aim_direction) * ball_speed))));
            }

            positions.Insert(0, position);
            while (positions.Count > (NumberOfPokemon+1) * CaughtPokemon.POSITION_OFFSET + 10)
            {
                positions.RemoveAt(positions.Count - 1);
            }


            // Flip sprite if necessary
            var sprite = getComponent<Sprite>();
            if (_aim_direction < -1.57f || _aim_direction > 1.57f)
                sprite.flipX = true;
            else if (_aim_direction > -1.57 && _aim_direction < 1.57)
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

                if (collisionResult.collider?.entity is HatchingPokemon) {
                    var pokemon = (HatchingPokemon)collisionResult.collider.entity;
                    if (!pokemon.Hatched) {
                        pokemon.Hatch();
                    }
                }
            }
            else
            {
                getComponent<Gravity>().enabled = true;
            }

            return deltaMovement;
        }
    }
}
