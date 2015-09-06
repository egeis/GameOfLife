using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class GameSystemSettings : MonoBehaviour
{
	public static GameSystemSettings Instance { 
		get { if(_instance == null) _instance = new GameSystemSettings();
			return _instance; 
		} 
	}

	private static GameSystemSettings _instance;

	public int width = 100;
	public int height = 1;
	public int depth = 100;
	
	public const int TYPE_RANDOM = 2;
	public const int TYPE_PRESET = 1;
	
	private int _type = 0;
	private string _prefab;
	private List<string> _prefabs;

	void Start()
	{
		_PreInit ();



		_PostInit ();
	}

	void Awake() {
		_instance = this;
	}

	void OnDestroy()
	{
		_instance = null;
	}

	// Pre-initialization
	private void _PreInit()
	{
		_prefabs = new List<string> {"_pcube"};
		_prefab = _prefabs[0];
		
		if (!System.IO.Directory.Exists (SavePath)) {
			Debug.Log("Creating Save Directory at: "+Application.dataPath + "/../saves/");
			System.IO.Directory.CreateDirectory(Application.dataPath + "/../saves/");
		}
	}

	// Post-initialization
	private void _PostInit() {}
	
	public int Type
	{
		get { 
			return _type;
		}
		set {
			switch (value) {
				case TYPE_RANDOM:
					_type = value;
					break;
				case TYPE_PRESET:
					_type = value;
					break;
				default:
					_type = 0;
					break;
			}
		}
	}

	public string Prefab
	{
		get { 
			return _prefab; 
		}
		set { 
			if(_prefabs.Contains(value)) 
				_prefab = value; 
		}
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
