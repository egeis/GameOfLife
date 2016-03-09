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

    public Dictionary<string, IRuleset> Rulesets = new Dictionary<string, IRuleset>();

    private System.Diagnostics.Stopwatch _sw = new System.Diagnostics.Stopwatch();

    private enum Status
    {
        LOADING,
        GENERATING,
        READY,          //Paused
        RUNNING,        //Running
        ERROR
    }

    private Status _state;

	// Use this for initialization
	void Start ()
    {
        _gs = GlobalSettings.Instance;
        _gs.loadingInterface.SetActive(true);
        _gs.gameboard = GameObject.Find(_gs.gameBoardName);
        _state = Status.LOADING;

        languageManager = LanguageManager.Instance;

        loadingText.GetComponent<Text>().text = languageManager.GetTextValue("UI.Loading.Loading");
        registerDefault();

        _state = Status.GENERATING;

        statusText.GetComponent<Text>().text = languageManager.GetTextValue("UI.Loading.New.State");

        _sw.Start();
        StartCoroutine("GenerateWorld");
    }

    // Update is called once per frame
    void Update ()
    {

    }

    //TEMP
    IEnumerator GenerateWorld()
    {
        for (int i = 0; i < _gs.cellColumns; i++)
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
        _sw.Stop();
        Debug.logger.Log(LogType.Log, "Generation Completed in " + _sw.ElapsedMilliseconds+"ms.");
        _state = Status.READY;
        _gs.loadingInterface.SetActive(false);
    }

  
}
