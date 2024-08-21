using System;
using System.Collections.Generic;

public class List 
{
    public static List<int> Divide(List<int> list1, List<int> list2, int listLength)
    {
        List<int> res = new List<int>();
        for( int i = 0; i < listLength; i++)
        {
            try
            {
                
                    res.Add(div(list1[i], list2[i]));
                
            }
            catch(ArgumentOutOfRangeException)
            {
                Console.WriteLine("Out of range");
            }
        }
        return res;
    }

    static int div(int a, int b)
    {
        int res = 0;
        try
        {
            res = a / b;
        }
        catch(DivideByZeroException)
        {
            Console.WriteLine("Cannot divide by zero");
            res = 0;
        }
        return res;
    }
}