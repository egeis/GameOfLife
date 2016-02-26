using UnityEngine;
using UnityEngine.UI;
using SmartLocalization;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private LanguageManager languageManager;
    private bool loadScene = false;

    [SerializeField]
    private int scene = 1;

    public GameObject statusText;
    public GameObject loadingText;

    // Use this for initialization
    void Start()
    {
        languageManager = LanguageManager.Instance;
        loadingText.GetComponent<Text>().text = languageManager.GetTextValue("UI.Loading.Ready");
        statusText.GetComponent<Text>().text = languageManager.GetTextValue("UI.Loading.Waiting");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space) && loadScene == false)
        {
            loadScene = true;

            loadingText.GetComponent<Text>().text = languageManager.GetTextValue("UI.Loading.Loading");
            statusText.GetComponent<Text>().text = languageManager.GetTextValue("UI.Loading.Scene");

            StartCoroutine(LoadNewScene(scene));
        }
    }

    IEnumerator LoadNewScene(int scene)
    {
        yield return new WaitForSeconds(2); //For Demo & debug Purposes only, not needed in production versions.

        AsyncOperation async = SceneManager.LoadSceneAsync(scene);

        while (!async.isDone)
        {
            yield return null;
        }
    }
}