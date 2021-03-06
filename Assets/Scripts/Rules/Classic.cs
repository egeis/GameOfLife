﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityToolbag;

public class Classic : IRuleset
{
    public string UnlocalizedName { get { return "UI.Rules.Classic"; } }

    private float cumValueLast;

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

        cumValueLast = cumValues[cumValues.Length - 1];

#if (UNITY_EDITOR)
       Debug.LogAssertion("Number of Cumulative Values for [CLASSIC]:" + cumValues.Length);
#endif
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

    public static float mod(float a, float b)
    {
        return (a % b + b) % b;
    }

    public Future<Dictionary<Vector2, int>> ComputeNextState(Dictionary<Vector2, int> lastState, Vector2 bounds)
    {
        Future<Dictionary<Vector2, int>> future = new Future<Dictionary<Vector2, int>>();

        future.Process(() =>
        {
            Dictionary<Vector2, int> states = new Dictionary<Vector2, int>();

            foreach (Vector2 key in lastState.Keys)
            {
                int value = 0;
                lastState.TryGetValue(key, out value);
                int[] count = new int[Enum.GetNames(typeof(States)).Length];

                float[] xs = new float[]
                {
                    mod(key.x + 1, bounds.x),
                    mod(key.x + 1, bounds.x),
                    key.x,
                    key.x,
                    mod(key.x + 1, bounds.x),
                    mod(key.x - 1, bounds.x),
                    mod(key.x - 1, bounds.x),
                    mod(key.x - 1, bounds.x)
                };

                float[] ys = new float[]
                {
                    key.y,
                    mod(key.y + 1, bounds.y),
                    mod(key.y + 1, bounds.y),
                    mod(key.y - 1, bounds.y),
                    mod(key.y - 1, bounds.y),
                    key.y,
                    mod(key.y + 1, bounds.y),
                    mod(key.y - 1, bounds.y)
                };

                for (int i = 0; i < 8; i++)
                {
                    Vector2 v = new Vector2(xs[i], ys[i]);
                    int a = (int) States.Dead;

                    bool success = lastState.TryGetValue(v, out a);

                    if(success)
                        count[a] += 1;
                }

                if(value == (int) States.Alive)
                    value = CheckRuleDeath(count, value);
                else
                    value = CheckRuleBirth(count, value);

                //Debug.LogAssertion(key + ":" + value 
                //    + "\ncount[alive]:"+count[(int) States.Alive] + " \tcount[Dead]:" + count[(int) States.Dead]);

                states.Add(key, value);
            }

            //Debug.LogAssertion("Ending Future:");

            return states;
        });

        return future;
    }

    public int getRandomCell()
    {
        float value = UnityEngine.Random.value * cumValueLast;

        int index = Array.BinarySearch(cumValues, value);

        if (index <= 0)
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
            if (count[(int) States.Alive] < 2 || count[(int) States.Alive] > 3)
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