using UnityEngine;
using System.Collections;
using System.IO;

public class GameInit : MonoBehaviour {
	
	// Pre-initialization
	private void PreInit()
	{
		if (!System.IO.Directory.Exists (SavePath)) {
			System.IO.Directory.CreateDirectory(Application.dataPath + "/../saves/");
		}
	}

	// Post-initialization
	private void PostInit()
	{

	}

	// Use this for initialization
	void Start () {
		PreInit ();

		//Code Goes Here

		PostInit ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public string SavePath {
		get { return Application.dataPath + "/../saves/";}
	}
}
