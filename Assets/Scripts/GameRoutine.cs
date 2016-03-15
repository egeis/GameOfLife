using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoutine : MonoBehaviour
{
    private Queue<Dictionary<Vector3, int>> FutureGenerations = new Queue<Dictionary<Vector3, int>>();
    private GlobalSettings _gs;
    private IRuleset _ruleset;

    public IRuleset Rule
    {
        get { return _ruleset; }
        set { _ruleset = value; }
    }

    void awake()
    {
        _gs = GlobalSettings.Instance;
    }

    void load()
    {
        if(true)    //New Generation
        {

        }
        else
        {

        }
    }

    IEnumerator generate(System.Action<int> callback)
    {
        if (_ruleset == null)
        {
            callback(0);
            yield return null;
        }
        
        //PreGeneration

        for (int i = 0; i < _gs.cellColumns; i++)
        {
            for (int j = 0; j < _gs.cellRows; j++)
            {

                yield return null;
            }
        }

        //PostGeneration

        callback(1);
    }

    void save()
    {

    }
}
