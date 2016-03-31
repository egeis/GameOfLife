﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

public class GlobalSettings : MonoBehaviour
{
    public static GlobalSettings Instance
    {
        get { return _instance; }
    }

    private static GlobalSettings _instance;
    private int current_generation = 0;

    public IRuleset Rules;

    public GameObject prefab;
    public GameObject loadingInterface;
    public GameObject gameboard;

    [Range(1, 60)]
    public int minSecondsBetweenGenerations = 2;

    [Range(1, 100)]
    public int maxPregenCells = 2;

    [Range(100, 500)]
    public int cellRows = 100;

    [Range(100, 500)]
    public int cellColumns = 100;

    public Camera mainCamera;

    [HideInInspector]
    public Dictionary<Vector3, GameObject> Cells;

    [HideInInspector]
    public Dictionary<Vector2, int> currentStates = new Dictionary<Vector2, int>();

    [HideInInspector]
    public int activeCells = 0;

    [HideInInspector]
    public int totalCells = 0;

    [HideInInspector]
    public readonly string gameBoardName = "GameBoard";

    [HideInInspector]
    public string SelectedRules = "";

    [HideInInspector]
    public Queue<Dictionary<Vector2, int>> FutureGenerations = new Queue<Dictionary<Vector2, int>>();

    private int maxCount;
    private int count;

    public int getCurrentGeneration()
    {
        return Interlocked.CompareExchange(ref current_generation, 0, 0);
    }

    public int incrementCurrentGeneration()
    {
        return Interlocked.Increment(ref current_generation);
    }

    public int getRenderCount()
    {
        return count;
    }

    public void incrementRenderCount()
    {
        count++;
    }

    public void resetRenderCount()
    {
        count = 0;
    }

    void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        Rules = new Classic();
        Cells = new Dictionary<Vector3, GameObject>();
        count = 0;
        maxCount = this.cellColumns * this.cellRows;
    }

    void Update()
    {

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
