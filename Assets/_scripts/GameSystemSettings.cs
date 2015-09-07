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
	public string type = "TYPE_RANDOM";
	public GameObject prefab;
	
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
