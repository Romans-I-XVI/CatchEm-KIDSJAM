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

            // Add Static Pokemon
            addEntity(new JumpingPokemon(new Vector2(625, 4400)));



            // Add Walls
            dynamic wall_data = JsonConvert.DeserializeObject(Utilities.ReadEmbeddedResource("WallLayout.json"));
            foreach (var wall in wall_data)
            {
                addEntity(new Wall((int)wall.x, (int)wall.y, (int)wall.width, (int)wall.height));
            }

        }
    }
}
