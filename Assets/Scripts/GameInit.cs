using UnityEngine;
using UnityEngine.UI;
using SmartLocalization;
using System;
using System.Collections;
using System.Collections.Generic;

public class GameInit : MonoBehaviour
{
    private GlobalSettings _gs;

    public GameObject statusText;
    public GameObject loadingText;

    private LanguageManager languageManager;

    private bool loadingCorotineStarted = false;

    private Coroutine loader = null;
    private bool loaderStarted = false;

    //private Queue<Dictionary<Vector3, int>> FutureGenerations = new Queue<Dictionary<Vector3, int>>();

    private enum Status
    {
        LOADING,
        GENERATING,
        RUNNING,        //Running
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

    IEnumerator CreateGame()
    {
        if (_gs.Rules == null)
        {
            _state = Status.ERROR;
            _error = Errors.RULESET_NOT_SET;

            Debug.LogError("Ruleset Invalid");
            yield break;
        }
    
        _state = Status.GENERATING;
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

                //END SET Cell 

                cell.GetComponent<SpriteRenderer>().color = _gs.Rules.getColorValue(state);
            }

            yield return null;
        }

        _state = Status.RUNNING;
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
            _gs.loadingInterface.SetActive(false);
        }
    }
}
