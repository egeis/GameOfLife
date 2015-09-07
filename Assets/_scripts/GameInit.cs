using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using AssemblyCSharp;

public class GameInit : MonoBehaviour
{
	private GameSystemSettings _gss;
	IWorld world;

	// Use this for initialization
	void Start () {
		_gss = GameSystemSettings.Instance;
		_PreInit ();

		world = new Grid(_gss);
		world.Create ();

		_PostInit ();
	}

	// Pre-initialization
	private void _PreInit()
	{
		if (!System.IO.Directory.Exists (_gss.SavePath)) {
			Debug.Log("Creating Save Directory at: "+Application.dataPath + "/../saves/");
			System.IO.Directory.CreateDirectory(Application.dataPath + "/../saves/");
		}
	}

	// Post-initialization
	private void _PostInit() {}

	// Update is called once per frame
	void Update () {
	
	}
}
