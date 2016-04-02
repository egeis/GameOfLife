using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuUIScript : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject exitConfirm;
    public GameObject newConfirm;

    void Start ()
    {
        exitConfirm.SetActive(false);
        menuPanel.SetActive(false);
        this.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if(Input.GetKeyDown(KeyCode.Escape))
        {
            menuPanel.SetActive(false);
            this.gameObject.SetActive(false);
        }
	}

    public void onMenuButtonPressed()
    {
        menuPanel.SetActive(true);
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

    public void onNewConfirmedPress()
    {
        newConfirm.SetActive(true);
        menuPanel.SetActive(false);
    }

    public void onNewCanceledPressed()
    {
        newConfirm.SetActive(false);
        menuPanel.SetActive(true);
    }

    public void onNewButtonPressed()
    {
        SceneManager.UnloadScene(1);
        SceneManager.LoadScene("scene0");
    }
}
