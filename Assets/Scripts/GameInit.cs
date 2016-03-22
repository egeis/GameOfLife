using UnityEngine;
using UnityEngine.UI;
using SmartLocalization;
using System;
using System.Collections;
using System.Collections.Generic;

using UnityToolbag;

public class GameInit : MonoBehaviour
{
    private GlobalSettings _gs;

    public GameObject statusText;
    public GameObject loadingText;

    private LanguageManager languageManager;

    private bool loadingCorotineStarted = false;

    private Coroutine loader = null;
    private bool loaderStarted = false;

    private Queue<Dictionary<Vector3, int>> FutureGenerations = new Queue<Dictionary<Vector3, int>>();
    private Dictionary<Vector3, int> lastProcessed = new Dictionary<Vector3, int>();
    private Dictionary<Vector3, int> nextGeneration = new Dictionary<Vector3, int>();

    private bool renderingCells = false;

    private enum Status
    {
        LOADING,
        GENERATING,
        RUNNING,        //Running
        PAUSED,
        ERROR
    }

    private enum Errors
    {
        NONE,
        RULESET_INVALID,
        RULESET_NOT_SET
    }

    private Status _state = Status.LOADING;
    private Errors _error = Errors.NONE;

	void Start ()
    {
        _gs = GlobalSettings.Instance;
        _gs.loadingInterface.SetActive(true);
        _gs.gameboard = GameObject.Find(_gs.gameBoardName);

        languageManager = LanguageManager.Instance;        
    }

    private IEnumerator RenderNextGeneration()
    {
        while(true)
        {
            if (_state == Status.ERROR) break;

            yield return new WaitForSeconds(_gs.minSecondsBetweenGenerations);
            
            while (_state == Status.RUNNING)
            {
                if (!renderingCells && FutureGenerations.Count > 0)
                {
                    nextGeneration = FutureGenerations.Dequeue();
                    renderingCells = true;
                    break;
                }
                else
                    yield return null;
            }
        }
    }

    private IEnumerator RenderCells()
    {
        renderingCells = true;

#if (UNITY_EDITOR)
        System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
        sw.Start();
#endif

        for (int i = 0; i < _gs.cellColumns; i++)
        {
            for (int j = 0; j < _gs.cellRows; j++)
            {
                Vector3 position = new Vector3(i, j, 0);


            }

            //yield return null;
        }

#if (UNITY_EDITOR)
        sw.Stop();
        Debug.LogAssertion("Update() Took:" + sw.Elapsed);
#endif

        renderingCells = false;
        yield return null;
    }

    private IEnumerator GenerateFutures()
    {
        yield return null;
    }

    private IEnumerator CreateGame()
    {
        if (_gs.Rules == null)
        {
            _state = Status.ERROR;
            _error = Errors.RULESET_NOT_SET;

            Debug.LogError("Ruleset Invalid");
            yield break;
        }
    
        _state = Status.GENERATING;

        Dictionary<Vector3, int> currentStates = new Dictionary<Vector3, int>();

#if (UNITY_EDITOR)
        System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
        sw.Start();
#endif

        System.Diagnostics.Stopwatch swg = new System.Diagnostics.Stopwatch();
        swg.Start();
        
        //Generation
        for (int i = 0; i < _gs.cellColumns; i++)
        {
            for (int j = 0; j < _gs.cellRows; j++)
            {
                Vector3 position = new Vector3(i, j, 0);
                GameObject cell = Instantiate(_gs.prefab, Vector3.zero, Quaternion.identity) as GameObject;
                cell.transform.position = position;
                cell.transform.parent = _gs.gameboard.transform;

                //SET Cell 
                int state = _gs.Rules.getRandomCell();
                cell.name = "cell_" + position.x + "_" + position.y + "_" + position.z;
                currentStates.Add(position, state);
                //END SET Cell 

                cell.GetComponent<SpriteRenderer>().color = _gs.Rules.getColorValue(state);
            }

            swg.Stop();
            if (swg.ElapsedMilliseconds > 100)
            {
                yield return null;
                swg.Reset();
            }
            swg.Start();
        }

#if (UNITY_EDITOR)
        sw.Stop();
        Debug.LogAssertion("Generation Took:"+sw.Elapsed);
#endif

        _state = Status.RUNNING;
        _gs.loadingInterface.SetActive(false);
        yield return null;

        //Create Futures Coroutine;
        //StartCoroutine(RenderNextGeneration());
    }

    // Update is called once per frame
    void Update ()
    {
        if (_state != Status.RUNNING)
        {
            switch (_state)
            {
                case Status.LOADING:
                    if (loader == null && !loaderStarted)
                    {
                        loader = StartCoroutine(CreateGame());
                        loaderStarted = true;
                    }

                    loadingText.GetComponent<Text>().text = languageManager.GetTextValue("UI.Loading.Loading");
                    break;
                case Status.GENERATING:
                    statusText.GetComponent<Text>().text = languageManager.GetTextValue("UI.Loading.Generate.New");
                    break;
                case Status.ERROR:
                    statusText.GetComponent<Text>().text = languageManager.GetTextValue("UI.Loading.Generate.Error");

                    //TODO: Set Error message specific to error generated.
                    switch(_error)
                    {
                        case Errors.RULESET_INVALID:
                            loadingText.GetComponent<Text>().text = languageManager.GetTextValue("UI.Loading.Error.INVALID_RULESET");
                            break;
                        case Errors.RULESET_NOT_SET:
                            loadingText.GetComponent<Text>().text = languageManager.GetTextValue("UI.Loading.Error.RULESET_NOT_SET");
                            break;
                        default:
                            loadingText.GetComponent<Text>().text = languageManager.GetTextValue("UI.Loading.Error.UNKNOWN_ERROR");
                            break;
                    }
                    break;
            }

        }
        else
        {
            //Game Loop
        }
    }
}
