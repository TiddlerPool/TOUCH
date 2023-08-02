using System.Collections.Generic;

public class IntValueComparer : IComparer<AssaultData>
{
    public int Compare(AssaultData x, AssaultData y)
    {
        return y.weight.CompareTo(x.weight);
    }
}
