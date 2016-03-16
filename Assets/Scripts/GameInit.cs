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
    private RulesManager rulesManager;
    private IRuleset _ruleset;

    private bool loadingCorotineStarted = false;

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
        RULESET_INVALID
    }

    private Status _state = Status.LOADING;
    private Errors _error = Errors.NONE;

    void awake()
    {
        _gs = GlobalSettings.Instance;
        _gs.loadingInterface.SetActive(true);
        _gs.gameboard = GameObject.Find(_gs.gameBoardName);
    }

	void Start ()
    {
        languageManager = LanguageManager.Instance;
        rulesManager = RulesManager.Instance;
    }

    IEnumerator CreateGame()
    {
        Debug.LogAssertion(rulesManager.Count());
        bool success = rulesManager.getRules(_gs.SelectedRules, ref _ruleset);

        if (!success)
        {
            _state = Status.ERROR;
            _error = Errors.RULESET_INVALID;

            Debug.LogError("Ruleset Invalid");
        }

        _state = Status.GENERATING;
        //Generation
        for (int i = 0; i < _gs.cellColumns; i++)
        {
            for (int j = 0; j < _gs.cellRows; j++)
            {
                Vector3 position = new Vector3(i - _gs.cellRows / 2.0f, j - _gs.cellColumns / 2.0f, 0);
                GameObject cell = Instantiate(_gs.prefab, Vector3.zero, Quaternion.identity) as GameObject;
                cell.transform.position = position;
                cell.transform.parent = _gs.gameboard.transform;

                //SET Cell 
                int state = _ruleset.getRandomCell();

                //END SET Cell 

                cell.GetComponent<SpriteRenderer>().color = _ruleset.getColorValue(state);
            }

            yield return null;
        }
    }

    // Update is called once per frame
    void Update ()
    {
        if (!loadingCorotineStarted)
        {
            loadingCorotineStarted = true;
            StartCoroutine(CreateGame());
        }

        if (_state != Status.RUNNING)
        {
            switch (_state)
            {
                case Status.LOADING:
                    loadingText.GetComponent<Text>().text = languageManager.GetTextValue("UI.Loading.Loading");
                    break;
                case Status.GENERATING:
                    statusText.GetComponent<Text>().text = languageManager.GetTextValue("UI.Loading.Generate.New");
                    break;
                case Status.ERROR:
                    statusText.GetComponent<Text>().text = languageManager.GetTextValue("UI.Loading.Generate.Error");

                    //TODO: Set Error message specific to error generated.

                    break;
            }

        }
        else
        {
            _gs.loadingInterface.active = false;
        }
    }
}
