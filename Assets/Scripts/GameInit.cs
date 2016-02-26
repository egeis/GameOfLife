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

    public Dictionary<Vector3, GameObject> Cells = new Dictionary<Vector3, GameObject>();

	// Use this for initialization
	void Start ()
    {
        _gs = GlobalSettings.Instance;
        languageManager = LanguageManager.Instance;

        _gs.loadingSavedState = false;      // TODO: remove after creating loading / saving methods.
        loadingText.GetComponent<Text>().text = languageManager.GetTextValue("UI.Loading.Loading");


        if (_gs.loadingSavedState)
        {
            statusText.GetComponent<Text>().text = languageManager.GetTextValue("UI.Loading.Prev.State");
            generateWorld();
            loadPreviousCellStates();

            //Pregenerate World
            statusText.GetComponent<Text>().text = languageManager.GetTextValue("UI.Loading.Prev.Pregen");
        }
        else
        {
            // GENERATING NEW WORLD
            statusText.GetComponent<Text>().text = languageManager.GetTextValue("UI.Loading.New.State");
            generateWorld();

            //Pregenerate World
            statusText.GetComponent<Text>().text = languageManager.GetTextValue("UI.Loading.New.Pregen");
        }

        _gs.loadingInterface.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
	    
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

                Cells.Add(position, cell);

                //TODO: Create a Initial Cell Object
            }
        }
    }

    void loadPreviousCellStates()
    {
        for (int i = 0; i > _gs.cellRows; i++)
        {
            for (int j = 0; j > _gs.cellColumns; j++)
            {

            }
        }
    }
}
