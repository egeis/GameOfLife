using System;
using System.Collections.Generic;
using UnityEngine;

public interface IRuleset
{
    Dictionary<int, Func<int[], int, int>> getRuleset();

    String getStringValue(int value);
    String getStringValue(Enum value);  //DEPRICATED

    Color getColorValue(int value);
    Color getColorValue(Enum value);    //DEPRICATED

    float getFloatValue(int value);
    float getFloatValue(Enum value);    //DEPRICATED

    String UnlocalizedName { get; }
    int getRandomCell();
}
