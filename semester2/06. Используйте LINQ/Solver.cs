using System;
using System.Linq;

namespace GaussAlgorithm
{
    public class Solver
    {
        private const double Accuracy = 1e-9;
        
        public double[] Solve(double[][] matrix, double[] freeMembers)
        {
            int rowsCount = matrix.Length, columnsCount = matrix[0].Length;
            var answerRows = Enumerable.Repeat(-1, columnsCount).ToArray();
            var augmentedMatrix = matrix
                .Select((row, i) => row.Concat(new[] {freeMembers[i]}).ToArray())
                .ToArray();

            for (int column = 0, row = 0; row < rowsCount && column < columnsCount; column++)
            {
                var maxByAbsInColumn = Enumerable.Range(row, rowsCount - row)
                    .Select(rowIndex => (absValue: Math.Abs(augmentedMatrix[rowIndex][column]), rowIndex))
                    .Max();
                if (maxByAbsInColumn.absValue < Accuracy)
                    continue;
                Swap(augmentedMatrix, row, maxByAbsInColumn.rowIndex);
                
                for (var modifiableRow = 0; modifiableRow < rowsCount; modifiableRow++)
                    if (modifiableRow != row)
                    {
                        var coefficient = augmentedMatrix[modifiableRow][column] / augmentedMatrix[row][column];
                        for (var c = column; c <= columnsCount; c++)
                            augmentedMatrix[modifiableRow][c] -= coefficient * augmentedMatrix[row][c];
                    }

                answerRows[column] = row++;
            }

            if (IsUnsolvable(augmentedMatrix))
                throw new NoSolutionException(matrix, freeMembers, augmentedMatrix);

            return answerRows.Select(
                    (row, col) => row < 0 ? .0 : augmentedMatrix[row][columnsCount] / augmentedMatrix[row][col])
                .ToArray();
        }

        private static void Swap<T>(T[] array, int i, int j) => (array[i], array[j]) = (array[j], array[i]);

        private static bool IsUnsolvable(double[][] augmentedMatrix)
            => augmentedMatrix.Where(row => Math.Abs(row[row.Length - 1]) >= Accuracy)
                .Select(row => row.Take(row.Length - 1))
                .Any(mainRow => mainRow.All(scalar => Math.Abs(scalar) < Accuracy));
    }
}
