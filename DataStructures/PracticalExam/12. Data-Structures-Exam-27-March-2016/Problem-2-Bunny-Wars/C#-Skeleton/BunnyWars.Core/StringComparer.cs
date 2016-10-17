namespace BunnyWars.Core
{
    using System.Collections.Generic;
    using System.Linq;

    public class StringComparer : IComparer<string>
    {
        public int Compare(string str1, string str2)
        {
            char[] chArr1 = str1.ToCharArray();
            char[] chArr2 = str2.ToCharArray();

            int index1 = chArr1.Length - 1;
            int index2 = chArr2.Length - 1;
            while (index1 >= 0 && index2 >= 0)
            {
                if (chArr1[index1] != chArr2[index2])
                {
                    return chArr1[index1] - chArr2[index2];
                }

                index1--;
                index2--;
            }

            return index1 - index2;
        }
    }
}
