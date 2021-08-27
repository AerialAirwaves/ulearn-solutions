using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digger
{
    public class Terrain : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand() { DeltaX = 0, DeltaY = 0 };
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return true;
        }

        public int GetDrawingPriority()
        {
            return 2;
        }

        public string GetImageFileName()
        {
            return "Terrain.png";
        }
    }


    public class Player : ICreature
    {
        public static int X = 0;
        public static int Y = 0;

        public CreatureCommand Act(int x, int y)
        {
            X = x;
            Y = y;
            var deltaX = 0;
            var deltaY = 0;
            if (Game.KeyPressed == System.Windows.Forms.Keys.Left)
                deltaX = -1;
            if (Game.KeyPressed == System.Windows.Forms.Keys.Up)
                deltaY = -1;
            if (Game.KeyPressed == System.Windows.Forms.Keys.Right)
                deltaX = 1;
            if (Game.KeyPressed == System.Windows.Forms.Keys.Down)
                deltaY = 1;
            if (!(x + deltaX >= 0 
                && x + deltaX < Game.MapWidth 
                && y + deltaY >= 0 
                && y + deltaY < Game.MapHeight))
            {
                deltaX = 0;
                deltaY = 0;
            } 
            else 
                if (Game.Map[x + deltaX, y + deltaY] != null)
                    if (Game.Map[x + deltaX, y + deltaY] is Sack)
                    {
                        deltaX = 0;
                        deltaY = 0;
                    }
            return new CreatureCommand() { DeltaX = deltaX, DeltaY = deltaY };
        }
        
        public bool DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject is Gold)
                Game.Scores += 10;
            return conflictedObject is Sack || conflictedObject is Monster;
        }

        public int GetDrawingPriority()
        {
            return 1;
        }
        
        public string GetImageFileName()
        {
            return "Digger.png";
        }
    }

    public class Sack : ICreature
    {
        private int timeInFly = 0;

        public CreatureCommand Act(int x, int y)
        {
            if (y < Game.MapHeight - 1)
            {
                if (Game.Map[x, y + 1] == null || (timeInFly > 0 
                    && (Game.Map[x, y + 1] is Player || Game.Map[x, y + 1] is Monster)))
                {
                    timeInFly++;
                    return new CreatureCommand() { DeltaX = 0, DeltaY = 1 };
                }
            }
            if (timeInFly > 1)
            {
                return new CreatureCommand() { DeltaX = 0, DeltaY = 0, TransformTo = new Gold() };
            }
            timeInFly = 0;
            return new CreatureCommand() { DeltaX = 0, DeltaY = 0 };
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return false;
        }

        public int GetDrawingPriority()
        {
            return 5;
        }

        public string GetImageFileName()
        {
            return "Sack.png";
        }
    }

    public class Gold : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand() { DeltaX = 0, DeltaY = 0 };
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {

            return conflictedObject is Player || conflictedObject is Monster;
        }

        public int GetDrawingPriority()
        {
            return 3;
        }

        public string GetImageFileName()
        {
            return "Gold.png";
        }
    }

    public class Monster : ICreature
    {
        public virtual CreatureCommand Act(int x, int y)
        {
            var deltaX = 0;
            var deltaY = 0;
            for (var i = 0; i < Game.MapWidth; i++)
                for (var j = 0; j < Game.MapHeight; j++)
                {
                    if (Game.Map[i, j] is Player)
                    {
                        if (x > i && IsFree(Game.Map[x - 1, y]))
                            deltaX--;
                        else if (x < i && IsFree(Game.Map[x + 1, y]))
                            deltaX++;
                        else if (y < j && IsFree(Game.Map[x, y + 1]))
                            deltaY++;
                        else if (y > j && IsFree(Game.Map[x, y - 1]))
                            deltaY--;
                        return new CreatureCommand() { DeltaX = deltaX, DeltaY = deltaY };
                    }
                }
            return new CreatureCommand() { DeltaX = 0, DeltaY = 0 };
        }
        public bool IsFree(ICreature conflictedObject)
        {
            return !(conflictedObject is Sack || conflictedObject is Monster || conflictedObject is Terrain) || conflictedObject is Bomb;
        }

        public virtual bool DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject is Monster || conflictedObject is EbnutiyMonster)
                Game.Scores += 10;
            return conflictedObject is Sack || conflictedObject is Monster;
        }

        public int GetDrawingPriority()
        {
            return 0;
        }

        public virtual string GetImageFileName()
        {
            return "Monster.png";
        }
    }

    public class EbnutiyMonster : Monster 
    {
        private int direction = -1;
        private Random random = new Random(); 

        public override CreatureCommand Act(int x, int y)
        {
            if (direction == -1)
            {
                direction = random.Next(10) % 4;
            }
            var deltaX = 0;
            var deltaY = 0;
            var countOfErrors = -1;
            while (((deltaX, deltaY) == (0, 0) || !IsSerouslyFree(x + deltaX, y + deltaY)) && countOfErrors < 10)
            {
                countOfErrors++;
                direction = random.Next(10) % 4;
                (deltaX, deltaY) = GetNewMove(x, y);
            }
            if (countOfErrors == 10)
                return new CreatureCommand() { DeltaX = 0, DeltaY = 0 };
            return new CreatureCommand() { DeltaX = deltaX, DeltaY = deltaY };
        }

        public bool IsSerouslyFree(int x, int y)
        {
            if ((x >= 0
                && x < Game.MapWidth
                && y >= 0
                && y < Game.MapHeight))
                return IsFree(Game.Map[x, y]);
            else return false;
        }

        private (int, int) GetNewMove(int x, int y)
        {
            var deltaX = 0;
            var deltaY = 0;
            switch (direction)
            {
                case 1:
                    deltaX++;
                    break;
                case 0:
                    deltaY++;
                    break;
                case 3:
                    deltaX--;
                    break;
                case 2:
                    deltaY--;
                    break;
            }
            if (!(x + deltaX >= 0
                && x + deltaX < Game.MapWidth
                && y + deltaY >= 0
                && y + deltaY < Game.MapHeight))
            {
                deltaX = 0;
                deltaY = 0;
                direction = random.Next(10) % 4;
            }
            return (deltaX, deltaY);
        }

        public override string GetImageFileName()
        {
            return "EbnutiyMonster.png";
        }
    }

    public class Bomb : Monster
    {
        public override CreatureCommand Act(int x, int y) => new CreatureCommand();
        public override bool DeadInConflict(ICreature conflictedObject)
        {
            return true;
        }
        public override string GetImageFileName()
        {
            return "Bomb.png";
        }
    }
}