using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Digger
{
    class Terrain : ICreature
    {
        public string GetImageFileName()
        {
            return "Terrain.png";
        }

        public int GetDrawingPriority()
        {
            return 1;
        }

        public CreatureCommand Act(int x, int y) => new CreatureCommand();

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return true;
        }
    }
   
    public class Player : ICreature
    {
        public string GetImageFileName()
        {
            return "Digger.png";
        }

        public int GetDrawingPriority()
        {
            return 0;
        }

        public CreatureCommand Act(int x, int y)
        {
            var result = new CreatureCommand();
            switch (Game.KeyPressed)
            {
                case Keys.Up:
                    if (0 <= y - 1)
                        result.DeltaY = -1;
                    break;
                case Keys.Down:
                    if (y + 1 < Game.MapHeight)
                        result.DeltaY = 1;
                    break;
                case Keys.Left:
                    if (0 <= x - 1)
                        result.DeltaX = -1;
                    break;
                case Keys.Right:
                    if (x + 1 < Game.MapWidth)
                        result.DeltaX = 1;
                    break;
                default:
                    break;
            }
            if (Game.Map[x + result.DeltaX, y + result.DeltaY] is Digger.Sack)
                (result.DeltaX, result.DeltaY) = (0, 0);
            return result;
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject is Gold)
                Game.Scores += 10;
            return conflictedObject is Sack;
        }
    }

    public class Sack : ICreature
    {
        private byte fallCount = 0;
        private static readonly CreatureCommand crash
            = new CreatureCommand() { TransformTo = new Gold() };

        public string GetImageFileName()
        {
            return "Sack.png";
        }

        public int GetDrawingPriority()
        {
            return 3;
        }

        private bool CanFall(int x, int y)
        {
            if (Game.MapHeight <= y + 1)
                return false;
            var cellBelow = Game.Map[x, y + 1];
            return (cellBelow is null) || ((fallCount > 0) && (cellBelow is Player));
        }

        private CreatureCommand Fall()
        {
            fallCount++;
            return new CreatureCommand() { DeltaX = 0, DeltaY = 1 };
        }

        private bool CanCrash() => fallCount > 1;

        public CreatureCommand Act(int x, int y)
        { 
            if (CanFall(x, y))
                return Fall();

            if (CanCrash())
                return crash;

            fallCount = 0;
            return new CreatureCommand();
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return false;
        }
    }

    public class Gold : ICreature
    {
        public string GetImageFileName()
        {
            return "Gold.png";
        }

        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand() { DeltaX = 0, DeltaY = 0 };
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return conflictedObject is Player;
        }

        public int GetDrawingPriority()
        {
            return 2;
        }
    }
}
