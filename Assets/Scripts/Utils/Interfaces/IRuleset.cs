using System;
using System.Collections.Generic;
using UnityEngine;

public interface IRuleset
{
    Dictionary<int, Func<int[], int, int>> getRuleset();
    String getStringValue(Enum value);
    Color getColorValue(Enum value);
    String UnlocalizedName { get; }
}
