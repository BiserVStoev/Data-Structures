namespace BunnyWars.Core
{
    using System;
    using System.Collections.Generic;

    public class SuffixComparer : Comparer<Bunny>
    {
        public override int Compare(Bunny x, Bunny y)
        {
            var firstNameReversed = new char[x.Name.Length];
            var secondNameReversed = new char[y.Name.Length];
            for (int i = firstNameReversed.Length - 1; i >= 0; i--)
            {
                firstNameReversed[firstNameReversed.Length - 1 - i] = x.Name[i];
            }

            for (int i = secondNameReversed.Length - 1; i >= 0; i--)
            {
                secondNameReversed[secondNameReversed.Length - 1 - i] = y.Name[i];
            }

            var minLength = Math.Min(firstNameReversed.Length, secondNameReversed.Length);

            for (int i = 0; i < minLength; i++)
            {
                if (firstNameReversed[i] > secondNameReversed[i])
                {
                    return 1;
                }

                if (firstNameReversed[i] < secondNameReversed[i])
                {
                    return -1;
                }
            }

            if (firstNameReversed.Length > secondNameReversed.Length)
            {
                return 1;
            }

            if (firstNameReversed.Length < secondNameReversed.Length)
            {
                return -1;
            }

            return 0;
        }
    }
}