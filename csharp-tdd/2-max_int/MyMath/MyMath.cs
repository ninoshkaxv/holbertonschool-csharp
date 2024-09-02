using System.Collections.Generic;

namespace MyMath
{
    ///<summary> Ncmon dont fail me now </summary>
    public static class Matrix
    {
        ///<summary> copied from oggal to try and see if XML works </summary>
        public static int [,] Divide(int [,] matrix, int num){
            if (num == 0)
            {
                System.Console.WriteLine("Num cannot be 0");
                return null;
            }
            if (matrix == null)
                return null;
            
            int[,] res = new int[matrix.GetLength(0),matrix.GetLength(1)];
            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(1); j++)
                    res[i,j] = matrix[i,j]/num;

            return res;
            
        }
    }

    ///<summary> task0 </summary>
    public static class Operations
    {

        ///<summary> max int from List </summary>
        public static int Max(List<int> nums)
        {
            int? res = null;
            if (nums != null)
            {
                foreach(int i in nums)
                {
                    if (!(res >= i))
                        res = i;
                }
            }
            return res.GetValueOrDefault();
        }

    }
}