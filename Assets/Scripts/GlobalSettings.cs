using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

public class GlobalSettings : MonoBehaviour
{
    public static GlobalSettings Instance
    {
        get { return _instance; }
    }

    public enum Rules
    {
        [StringValue("Rules.Classic")]
        Classic,
        [StringValue("Rules.Preditors")]
        Preditors,
        [StringValue("Rules.ViralInfection")] 
        ViralInfection
    }

    private static GlobalSettings _instance;
    private int current_generation = 0;

    public GameObject prefab;
    public GameObject loadingInterface;
    public GameObject gameboard;

    public Rules RuleSet = Rules.Classic;

    [Range(1, 10)]
    public int maxGenerationsPerSecond = 2;

    [Range(1, 100)]
    public int maxPregenCells = 2;

    [Range(100, 500)]
    public int cellRows = 100;

    [Range(100, 500)]
    public int cellColumns = 100;

    public bool showAllGenerations= true;
    public bool animatedStateChanges = true;

    public Camera mainCamera;

    [HideInInspector]
    public Dictionary<Vector3, GameObject> Cells = new Dictionary<Vector3, GameObject>();

    [HideInInspector]
    public Dictionary<Vector4, int> FutureCellStates = new Dictionary<Vector4, int>();

    [HideInInspector]
    public int activeCells = 0;

    [HideInInspector]
    public int totalCells = 0;

    public int getCurrentGeneration()
    {
        return Interlocked.CompareExchange(ref current_generation, 0, 0);
    }

    public int incrementCurrentGeneration()
    {
        return Interlocked.Increment(ref current_generation);
    }

    void Awake()
    {
        _instance = this;
    }

    void OnDestroy()
    {
        _instance = null;
    }

    public string SavePath
    {
        get { return Application.dataPath + "/../saves/"; }
    }

    public string Presets
    {
        get { return Application.dataPath + "/presets/"; }
    }
}
