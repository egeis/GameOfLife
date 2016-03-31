using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InfobarBehavior : MonoBehaviour {

    public Text GenerationCounter;
    public Text PreGeneratedGenerationsCounter;

    private GlobalSettings _gs;

    void Start()
    {
        _gs = GlobalSettings.Instance;

        if (GenerationCounter == null)
            throw new System.NullReferenceException("[Generation Counter] cannot be null, please set a UI text element in the Editor.");

        if (PreGeneratedGenerationsCounter == null)
            throw new System.NullReferenceException("[Pre Generated Generations Counter] cannot be null, please set a UI text element in the Editor.");
    }

	void Update ()
    {
        GenerationCounter.text = _gs.getCurrentGeneration().ToString();
        PreGeneratedGenerationsCounter.text = _gs.FutureGenerations.Count.ToString();
    }

  
}
