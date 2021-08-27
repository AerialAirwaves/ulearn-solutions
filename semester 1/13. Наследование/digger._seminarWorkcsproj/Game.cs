using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digger
{
    public class Game
    {

        public ICreature[,] Map;
        public int Scores;
        public bool IsOver;

        public Keys KeyPressed;
        public int MapWidth => Map.GetLength(0);
        public int MapHeight => Map.GetLength(1);

        public void CreateMap()
        {
            var random = new Random();
            var width = random.Next(10) + 15;
            var height = random.Next(10) + 15;

            var coofSpaces = 0.2;
            var coofSack = 0.08;
            var coofGold = 0.05;
            var coofMonster = 0.08;
            var coofEbnutiyMonster = 0.04;
            var coofBomb = 0.03;

            var field = width * height;
            Map = CreatureMapCreator.CreateMap(MapBuilderTask(
                width,
                height,
                (int)(field * coofSpaces),
                (int)(field * coofSack),
                (int)(field * coofGold),
                (int)(field * coofMonster),
                (int)(field * coofEbnutiyMonster),
                (int)(field * coofBomb)));
        }

        public static void CreateFeatures(char[,] map, char entity, int count, Random random)
        {
            var x = 0;
            var y = 0;
            while (count > 0)
            {
                x = random.Next(map.GetLength(0));
                y = random.Next(map.GetLength(1));
                if (map[x, y] == 'T')
                {
                    count--;
                    map[x, y] = entity;
                }
            }
        }

        public static string MapBuilderTask(
        int width,
        int height,
        int spacesCount,
        int sackCount,
        int goldCount,
        int monsterCount,
        int ebnutiyMonsterCount,
        int bombCount)
        {
            var map = new char[width, height];


            var result = "";

            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < height; j++)
                {
                    map[i, j] = 'T';
                }
            }

            var random = new Random();

            var x = random.Next(width);
            var y = random.Next(height);
            map[0, 0] = 'P';

            CreateFeatures(map, ' ', spacesCount, random);
            CreateFeatures(map, 'S', sackCount, random);
            CreateFeatures(map, 'G', goldCount, random);
            CreateFeatures(map, 'M', monsterCount, random);
            CreateFeatures(map, 'E', ebnutiyMonsterCount, random);
            CreateFeatures(map, 'B', bombCount, random);

            map[1, 1] = 'T';
            map[1, 0] = 'T';
            map[0, 1] = 'T';


            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < height; j++)
                {
                    result += map[i, j];
                }
                result += "\r\n";
            }

            return result;
        }
    }
}