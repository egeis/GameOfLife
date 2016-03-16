using System;
using System.Collections.Generic;
using UnityEngine;

public class Classic : MonoBehaviour, IRuleset
{
    public readonly static string unlocalizedName = "UI.Rules.Classic"; //TEMP: Used as a default, must remain public and static.

    public string UnlocalizedName { get { return unlocalizedName; } }

    public Classic()
    {
        WeightedStates lws = null;

        int index = 0;
        cumValues = new float[Enum.GetNames(typeof(States)).Length];

        foreach (States val in Enum.GetValues(typeof(States)))
        {
            WeightedStates ws = new WeightedStates();
            ws.Id = (int) val;
            ws.Weight = getFloatValue((int)val);

            if (lws == null)
                ws.Cumulative = ws.Weight;
            else
                ws.Cumulative = ws.Weight + lws.Cumulative;

            weights.Add(ws);
            lws = ws;
            cumValues[index++] = ws.Cumulative;
        }
    }

    private Dictionary<int, Func<int[], int, int>> Rules = new Dictionary<int, Func<int[], int, int>>
    {
        {0, CheckRuleBirth},
        {1, CheckRuleDeath}
    };

    public Dictionary<int, Func<int[], int, int>> getRuleset() { return Rules; }

    private List<WeightedStates> weights = new List<WeightedStates>();
    private float[] cumValues;

    public Dictionary<int, Color> StateColors = new Dictionary<int, Color>();

    public enum States
    {
        [FloatValue(0.35f)]
        [ColorValue(0.8f, 0.8f, 0.8f, 1.0f)]
        Alive,

        [FloatValue(0.65f)]
        [ColorValue(0.2f, 0.2f, 0.2f, 1.0f)]
        Dead
    }

    public int getRandomCell()
    {
        float value = UnityEngine.Random.Range(0.0f, cumValues[cumValues.Length-1]);
        int index = Array.BinarySearch(cumValues, value);

        if (index >= 0)
            index = ~index;

        return weights[index].Id;
    }

    //Rule 1
    public static int CheckRuleBirth(int[] count, int current_state)
    {
        int next_state = current_state;

        if (current_state == (int)States.Dead)
        {
            if (count[(int)States.Alive] == 3)
                next_state = (int)States.Alive;
        }

        return next_state;
    }

    //Rule 2 & 3
    public static int CheckRuleDeath(int[] count, int current_state)
    {
        int next_state = current_state;

        if(current_state == (int)States.Alive)
        {
            if (count[(int)States.Alive] < 2 || count[(int)States.Alive] > 3)
                next_state = (int)States.Dead;
        }

        return next_state;
    }

    public float getFloatValue(int value)
    {
        return FloatEnum.getFloatValue((States) value);
    }

    [Obsolete("use getFloatValue(int value)", false)]
    public float getFloatValue(Enum value)
    {
        return FloatEnum.getFloatValue(value);
    }

    [Obsolete("use getStringValue(int value)", false)]
    public string getStringValue(Enum value)
    {
        return StringEnum.getStringValue(value);
    }

    public string getStringValue(int value)
    {
        return StringEnum.getStringValue((States)value);
    }

    public Color getColorValue(int value)
    {
       return ColorEnum.getColorValue((States)value);
    }

    [Obsolete("use getColorValue(int value)", false)]
    public Color getColorValue(Enum value)
    {
        return ColorEnum.getColorValue(value);
    }
}

class WeightedStates
{
    public int Id { get; set; }
    public float Weight { get; set; }
    public float Cumulative { get; set; }
}