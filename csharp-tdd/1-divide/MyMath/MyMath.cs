using System;
namespace MyMath
{
    /// <summary>
    /// Task 1 
    /// </summary>
    public class Matrix
    {
        /// <summary>
        /// dividing a mtrix i guess 
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="num"></param>
        /// <returns>res</returns>
        public static int[,] Divide(int[,] matrix, int num)
        {
            if (num == 0)
            {
                System.Console.WriteLine("Num cannot be 0");
                return null;
            }
            if (matrix == null)
                return null;
            
            int[,] res = new int[matrix.GetLength(0), matrix.GetLength(1)];
            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(1); j++)
                    res[i,j] = matrix[i,j]/num;

            return res;
        }
    }
}