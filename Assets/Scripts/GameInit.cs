using UnityEngine;
using UnityEngine.UI;
using SmartLocalization;
using System;
using System.Collections.Generic;

public class GameInit : MonoBehaviour
{
    private GlobalSettings _gs;

    public GameObject statusText;
    public GameObject loadingText;

    private LanguageManager languageManager;
    private RulesManager rulesManager;

    public Dictionary<string, IRuleset> Rulesets = new Dictionary<string, IRuleset>();

	// Use this for initialization
	void Start ()
    {
        _gs = GlobalSettings.Instance;
        languageManager = LanguageManager.Instance;
        rulesManager = RulesManager.Instance;

        loadingText.GetComponent<Text>().text = languageManager.GetTextValue("UI.Loading.Loading");
        registerDefault();


        {   // GENERATING NEW WORLD
            statusText.GetComponent<Text>().text = languageManager.GetTextValue("UI.Loading.New.State");
            //generateWorld();

            //Pregenerate World
            statusText.GetComponent<Text>().text = languageManager.GetTextValue("UI.Loading.New.Pregen");
        }

        _gs.loadingInterface.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    void registerDefault()
    {
        //rulesManager.register()
    }


    void generateWorld()
    {
        for (int i = 0; i < _gs.cellRows; i++)
        {
            for (int j = 0; j < _gs.cellColumns; j++)
            {
                Vector3 position = new Vector3(i - _gs.cellRows / 2.0f, j - _gs.cellColumns / 2.0f, 0);
                GameObject cell = Instantiate(_gs.prefab, Vector3.zero, Quaternion.identity) as GameObject;
                cell.transform.position = position;
                cell.transform.parent = GameObject.Find("GameBoard").transform;

                if (UnityEngine.Random.value < 0.2f)
                {
                    cell.GetComponent<SpriteRenderer>().color = new Color(0.8f, 0.8f, 0.8f, 1f);
                }
                else
                {
                    cell.GetComponent<SpriteRenderer>().color = new Color(0.2f, 0.2f, 0.2f, 1f);
                }

                _gs.Cells.Add(position, cell);

                //TODO: Create a Initial Cell Object
            }
        }
    }

  
}
