using System.Collections;
using System.Collections.Generic;


namespace ExtraString
{
    public static class CropString
    {
        static string newString = null;

        public static string CropBetween(string s, string a, string b)
        {
            newString = null;

            int start = s.IndexOf(a);
            int end = s.IndexOf(b);
            

            for (int i = start+1; i < end; ++i)
            {
                newString += s[i];
            }

            return newString;
        }
    }
}
