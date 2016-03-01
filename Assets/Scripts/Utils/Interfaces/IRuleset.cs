using System;
using System.Collections.Generic;

public interface IRuleset
{
    Dictionary<int, Func<int[], int, int>> getRuleset();
}
