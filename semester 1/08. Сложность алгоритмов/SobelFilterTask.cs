using System;

namespace Recognizer
{
    internal static class SobelFilterTask
    {
        private static double GetRadiusVectorLength(double x, double y)
        {
            return Math.Sqrt(x * x + y * y);
        }

        private static double ElementwisePixelsMatrixProductsSum(double[,] pixels,
            int x0, int y0, double[,] matrix, bool transposeMatrix)
        {
            int matrixSize = matrix.GetLength(0);
            double result = 0;
            for (int i = 0; i < matrixSize; i++)
                for (int j = 0; j < matrixSize; j++)
                    result += pixels[x0 + i, y0 + j]
                        * (transposeMatrix ? matrix[j, i] : matrix[i, j]);
            return result;
        }

        public static double[,] SobelFilter(double[,] g, double[,] sx)
        {
            var width = g.GetLength(0);
            var height = g.GetLength(1);
            var result = new double[width, height];
            var matrixShift = sx.GetLength(0) / 2;
            for (int x = matrixShift; x < width - matrixShift; x++)
                for (int y = matrixShift; y < height - matrixShift; y++)
                    result[x, y] = GetRadiusVectorLength(
                        ElementwisePixelsMatrixProductsSum(g,
                            x - matrixShift, y - matrixShift, sx, false),
                        ElementwisePixelsMatrixProductsSum(g,
                            x - matrixShift, y - matrixShift, sx, true));
            return result;
        }
    }
}