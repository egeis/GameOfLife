using UnityEngine;
using System.Collections;

public class controller_GUI : MonoBehaviour {

	private int _currentGenerationType = 1;
	private int _generationTabSelected = 1;
	private int _currentGameType = 0;
	private int _gameTabSelected = 0;

	private string _genLabelText;
	private int _generation = 1;
	private int _alive = 0;

	private string _savepath;

	// Use this for initialization
	void Start () {

	}

	void OnGUI() {
		string[] _gen_toolbar = {"Clear", "Random", "Acorn"};
		string[] _rules_toolbar = {"Original", "Mutation", "Resurrection"};

		//_generationTabSelected = GUI.Toolbar (new Rect (0, 0, (Screen.width/2), (Screen.height / 8)), 0, _gen_toolbar);
		//_gameTabSelected = GUI.Toolbar (new Rect ((Screen.width/2), 0, (Screen.width/2), (Screen.height / 8)), 0, _rules_toolbar)

		GUILayout.BeginArea( new Rect(0,0, (Screen.width), (Screen.height/4) ) );
		GUILayout.BeginHorizontal ();
		
		_generationTabSelected = GUILayout.Toolbar(_currentGenerationType, _gen_toolbar);
		_gameTabSelected = GUILayout.Toolbar(_currentGameType, _rules_toolbar);

		GUILayout.EndHorizontal ();
		GUILayout.EndArea ();

		_genLabelText = "Generation: " + this._generation.ToString () + " Cells Active: " + this._alive.ToString();

		GUI.Label( new Rect (0, Screen.height - 24, Screen.width / 2, 24 ), _genLabelText );
	}
	
	// Update is called once per frame
	void Update () {

		_genLabelText = "Generation: " + this._generation.ToString () + " Cells Active: " + this._alive.ToString();

		if (_currentGenerationType != _generationTabSelected) {
			_currentGenerationType = _generationTabSelected;

			BroadcastMessage("generate", (int) (_generationTabSelected+1));			
			BroadcastMessage("SetPause", true);
		}

		if (_currentGameType != _gameTabSelected) {
			_currentGameType = _gameTabSelected;
			Debug.Log(_gameTabSelected);

			BroadcastMessage("SetRules", (int) (_gameTabSelected+1));
			BroadcastMessage("SetPause", true);
		}

		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit();
		}
		
		if (Input.GetKeyDown(KeyCode.Space)) {
			BroadcastMessage("TogglePause");
		}
		
		if (Input.GetKeyDown(KeyCode.RightArrow)) {
			BroadcastMessage("OnRightArrow");
		}

		if (Input.GetKeyDown (KeyCode.F1)) {  //Quick Save
			BroadcastMessage("SaveGame");
		}

		if (Input.GetKeyDown (KeyCode.F2)) {  //Quick Load
			BroadcastMessage("LoadGame");
		}
		
		if (Input.GetMouseButton (0)) {			//Mouse Left Click
			if(GUIUtility.hotControl == 0) {
				BroadcastMessage("SetPause", true);
				BroadcastMessage("ChangeGrid", true);
			}
		} 

		if (Input.GetMouseButton (1)) {			//Mouse Right Click
			if (GUIUtility.hotControl == 0) {
				BroadcastMessage ("SetPause", true);
				BroadcastMessage ("ChangeGrid", false);
			}
		} 
	}

	public void UpdateGeneration(int generation) {
		this._generation = generation;
	}

	public void UpdateAlive(int alive) {
		this._alive = alive;
	}

}
