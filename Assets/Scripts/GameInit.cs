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

    private enum Status
    {
        LOADING,
        GENERATING,
        READY,          //Paused
        RUNNING,        //Running
        ERROR
    }

    private Status _state;

    void awake()
    {
        _gs = GlobalSettings.Instance;
        _gs.loadingInterface.SetActive(true);
        _gs.gameboard = GameObject.Find(_gs.gameBoardName);
        _state = Status.LOADING;

        loadingText.GetComponent<Text>().text = languageManager.GetTextValue("UI.Loading.Loading");  
    }

	void Start ()
    {
        languageManager = LanguageManager.Instance;
        rulesManager = RulesManager.Instance;

        //_state = Status.GENERATING;
        //statusText.GetComponent<Text>().text = languageManager.GetTextValue("UI.Loading.Generate.New");

        StartCoroutine("GenerateWorldCoroutines");
    }

    // Update is called once per frame
    void Update ()
    {

    }

    IEnumerator GenerateWorldCoroutines()
    {
        /*for (int i = 0; i < _gs.cellColumns; i++)
        {
            for (int j = 0; j < _gs.cellRows; j++)
            {
                Vector3 position = new Vector3(i, j, 0);
                GameObject cell = Instantiate(_gs.prefab, Vector3.zero, Quaternion.identity) as GameObject;
                cell.transform.position = position;
                cell.transform.parent = _gs.gameboard.transform;

                if (UnityEngine.Random.value < 0.2f)
                {
                    cell.GetComponent<SpriteRenderer>().color = new Color(0.8f, 0.8f, 0.8f, 1f);
                }
                else
                {
                    cell.GetComponent<SpriteRenderer>().color = new Color(0.2f, 0.2f, 0.2f, 1f);
                }

                _gs.Cells.Add(position, cell);
            }

            yield return null;
        }
        _state = Status.READY;
        _gs.loadingInterface.SetActive(false);*/

        yield return null;
    }

  
}
