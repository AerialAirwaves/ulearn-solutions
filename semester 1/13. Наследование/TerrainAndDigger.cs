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
            return result;
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return false;
        }
    }
}
