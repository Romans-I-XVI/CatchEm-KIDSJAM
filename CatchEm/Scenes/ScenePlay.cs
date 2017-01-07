using System;
using System.IO;
using System.Reflection;
using Microsoft.Xna.Framework;
using Nez;
using Newtonsoft.Json;
using Microsoft.CSharp;
using System.Collections.Generic;

namespace CatchEm
{
    public class ScenePlay : Scene
    {
        public ScenePlay()
        {
            //setDesignResolution(1280, 720, SceneResolutionPolicy.BestFit, 46, 26);
            var map_height = 5000;
            var map_width = 5000;
            addRenderer(new DefaultRenderer());

            // Create Borders
            addEntity(new Wall(-50, map_height - 100, (map_width + 100) / 50, 500 / 50));
            addEntity(new Wall(-50, 100, map_width + 100, 500 / 50));
            addEntity(new Wall(100 - 500, 50, 500 / 50, (map_height - 100) / 50));
            addEntity(new Wall(map_width - 100, 50, 500 / 50, (map_height - 100) / 50));


            // Add Player
            var player = new Player(camera);
            addEntity(player);
            addEntity(new AimArrow(player));

            // Add Flying Pokemon
            var bird = new FlyingPokemon(new Vector2(3550, 4350), new List<Vector2>() { new Vector2(3550 - 800, 4350), new Vector2(3550 + 800, 4350) }, 3);
            bird.RespawnRate = 200;
            addEntity(bird);
            addEntity(new FlyingPokemon(new Vector2(1300, 3800), new List<Vector2>() { new Vector2(650, 3800), new Vector2(1700, 3800) }, 4));
            addEntity(new FlyingPokemon(new Vector2(1300, 3800), new List<Vector2>() { new Vector2(1950, 4200), new Vector2(2200, 4200) }, 2));
            addEntity(new FlyingPokemon(new Vector2(1300, 3800), new List<Vector2>() { new Vector2(2250, 4200), new Vector2(2600, 4200) }, 2));
            addEntity(new FlyingPokemon(new Vector2(3050, 3050), new List<Vector2>() { 
                new Vector2(3050, 3050-50),
                new Vector2(3250, 3250-50),
                new Vector2(3450, 3050-50),
                new Vector2(3650, 3000-50),
                new Vector2(3450, 3250-50),
                new Vector2(3250, 3050-50),
            }, 3, Tween.Linear));

            // Add Jumping Pokemon
            addEntity(new JumpingPokemon(new Vector2(625, 4400)));
            addEntity(new JumpingPokemon(new Vector2(2750 + 25, 3500 - 400)));

            // Add Static Pokemon
            addEntity(new SmallStaticPokemon(new Vector2(650 + 25, 4850 + 25)));
            addEntity(new LargeStaticPokemon(new Vector2(200, 3700)));
            addEntity(new LargeStaticPokemon(new Vector2(4800, 4850)));

            // Add Walking Pokemon
            addEntity(new WalkingPokemon(new Vector2(1300, 3800), new List<Vector2>() { new Vector2(2550, 4850 + 25), new Vector2(3000, 4850 + 25) }, 1));
            addEntity(new WalkingPokemon(new Vector2(1300, 3800), new List<Vector2>() { new Vector2(2100, 4850 + 25), new Vector2(2450, 4850 + 25) }, 1));

            // Add Digging Pokemon
            addEntity(new DiggingPokemon(new Vector2(3450 + 25, 4900 + 30), new List<Vector2>() { new Vector2(3450 + 25, 4900 ), new Vector2(3450 + 25, 4850 + 200) }, 0.85f));
            addEntity(new DiggingPokemon(new Vector2(3550 + 25, 4900 + 30), new List<Vector2>() { new Vector2(3550 + 25, 4900 ), new Vector2(3550 + 25, 4850 + 200) }, 0.8f));
            addEntity(new DiggingPokemon(new Vector2(3650 + 25, 4900 + 30), new List<Vector2>() { new Vector2(3650 + 25, 4900 ), new Vector2(3650 + 25, 4850 + 200) }, 0.75f));

            // Add Ghost Pokemon
            addEntity(new GhostPokemon(new Vector2(0, 0)));
            addEntity(new GhostPokemon(new Vector2(0, 0)));
            


            // Add Walls
            dynamic wall_data = JsonConvert.DeserializeObject(Utilities.ReadEmbeddedResource("WallLayout.json"));
            foreach (var wall in wall_data)
            {
                addEntity(new Wall((int)wall.x, (int)wall.y, (int)wall.width, (int)wall.height));
            }

        }
    }
}
