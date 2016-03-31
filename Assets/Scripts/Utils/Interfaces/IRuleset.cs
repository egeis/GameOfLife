using System;
using System.Collections.Generic;
using UnityEngine;
using UnityToolbag;

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

    Future<Dictionary<Vector2, int>> ComputeNextState(Dictionary<Vector2, int> lastState, Vector2 bounds);
}
