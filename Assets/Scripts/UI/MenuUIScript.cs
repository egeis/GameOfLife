using UnityEngine;
using System.Collections;

public class MenuUIScript : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject exitConfirm;

    void Start ()
    {
        this.gameObject.SetActive(false);
        exitConfirm.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if(Input.GetKeyDown(KeyCode.Escape))
        {
            this.gameObject.SetActive(false);
        }
	}

    public void onMenuButtonPressed()
    {
        this.gameObject.SetActive(true);
    }

    public void onExitButtonPressed()
    {
        exitConfirm.SetActive(true);
        menuPanel.SetActive(false);
    }

    public void onExitConfirmPressed()
    {
        Application.Quit();
    }

    public void onExitCanceledPressed()
    {
        exitConfirm.SetActive(false);
        menuPanel.SetActive(true);
    }
}
