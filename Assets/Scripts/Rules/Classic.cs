using System;
using System.Collections.Generic;

public class Classic : IRuleset
{
    private Dictionary<int, Func<int[], int, int>> Rules = new Dictionary<int, Func<int[], int, int>>
    {
        {0, CheckRuleBirth},
        {0, CheckRuleDeath}
    };

    public enum States
    {
        [FloatValue(0.35f)]
        [ColorValue(0.8f, 0.8f, 0.8f, 1.0f)]
        Alive,

        [FloatValue(0.65f)]
        [ColorValue(0.2f, 0.2f, 0.2f, 1.0f)]
        Dead
    }

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

    public Dictionary<int, Func<int[], int, int>> getRuleset() { return Rules; }
}