using System;

namespace Billiards
{
    public static class BilliardsTask
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="directionRadians">Угол направелния движения шара</param>
        /// <param name="wallInclinationRadians">Угол</param>
        /// <returns></returns>
        public static double BounceWall(double directionRadians, double wallInclinationRadians)
        {
            return wallInclinationRadians * 2 - directionRadians;
        }
    }
}