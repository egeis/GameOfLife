using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoutine : MonoBehaviour
{
    private Queue<Dictionary<Vector3, int>> FutureGenerations = new Queue<Dictionary<Vector3, int>>();
    private GlobalSettings _gs;

    private bool isWorking = false;

    void start()
    {
        _gs = GlobalSettings.Instance;
    }
}
