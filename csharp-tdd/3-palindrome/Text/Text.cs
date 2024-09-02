using System;
using System.Linq;

namespace Text
{
    /// <summary>
    /// Task 3, not sure what to do if this one works and the others dont 
    /// </summary>
    public class Str
    {
        /// <summary>
        /// Determines whether a string is a palindrome.
        /// </summary>
        /// <param name="s">The input string to check.</param>
        /// <returns>True if the string is a palindrome; otherwise, false.</returns>
        public static bool IsPalindrome(string s)
        {
            string cleanedString = new string(s.Where(char.IsLetterOrDigit).Select(char.ToLower).ToArray());

            int left = 0;
            int right = cleanedString.Length - 1;

            while (left < right)
            {
                if (cleanedString[left] != cleanedString[right])
                {
                    return false;
                }

                left++;
                right--;
            }

            return true;
        }
    }
}

