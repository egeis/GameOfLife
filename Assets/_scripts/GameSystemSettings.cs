using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class GameSystemSettings : MonoBehaviour
{
	public static GameSystemSettings Instance { 
		get { return _instance; } 
	}

	private static GameSystemSettings _instance;

	public int width = 100;
	public int height = 1;
	public int depth = 100;
	public float _step = 2.0f;

	public string type = "TYPE_RANDOM";

	public GameObject prefab;

	public bool WRAP = true;

	public GameObject DEBUG_LINE_PERM;

	public Texture LoadingScreen;

	public bool DEBUG_LINES_ENABLE = true;

	public bool DEBUG_X   = true;
	public bool DEBUG_Y   = true;
	public bool DEBUG_Z   = true;

	void Awake() {
		_instance = this;
	}

	void OnDestroy()
	{
		_instance = null;
	}

	public string SavePath
	{
		get { return Application.dataPath + "/../saves/";}
	}
	
	public string Presets
	{
		get { return Application.dataPath + "/presets/";}
	}


}
