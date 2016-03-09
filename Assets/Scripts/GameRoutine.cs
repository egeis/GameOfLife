using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoutine : MonoBehaviour
{
    private Queue<Dictionary<Vector3, int>> FutureGenerations = new Queue<Dictionary<Vector3, int>>();
    private GlobalSettings _gs;

    void start()
    {
        _gs = GlobalSettings.Instance;
    }

    void update()
    {

    }

    void load()
    {

    }

    IEnumerator generate()
    {
        //PreGeneration

        for (int i = 0; i < _gs.cellColumns; i++)
        {
            for (int j = 0; j < _gs.cellRows; j++)
            {

                yield return null;
            }
        }

        //PostGeneration
    }

    void save()
    {

    }
}
