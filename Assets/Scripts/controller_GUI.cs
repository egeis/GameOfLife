using UnityEngine;
using System.Collections;

public class controller_GUI : MonoBehaviour {

	private int _currentGenerationType = 1;
	private int _generationTabSelected = 1;
	private int _currentGameType = 1;
	private int _gameTabSelected = 1;

	private string _genLabelText;
	private int _generation = 1;
	private int _alive = 0;

	// Use this for initialization
	void Start () {
	
	}

	void OnGUI() {
		string[] _gen_toolbar = {"Clear", "Random", "Acorn"};
		string[] _rules_toolbar = {"Original", "Set #2", "Set #3"};
		
		GUILayout.BeginArea( new Rect(0,0, (Screen.width), (Screen.height/4) ) );
		GUILayout.BeginHorizontal ();
		
		_generationTabSelected = GUILayout.Toolbar(_generationTabSelected, _gen_toolbar);
		_currentGameType = GUILayout.Toolbar(_gameTabSelected, _rules_toolbar);

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
			switch(_generationTabSelected) {
			case 0:
				BroadcastMessage("generate", 1);			
				BroadcastMessage("SetPause", true);
				break;
			case 1:
				BroadcastMessage("generate", 2);
				BroadcastMessage("SetPause", true);
				break;
			case 2:
				BroadcastMessage("generate", 3);
				BroadcastMessage("SetPause", true);
				break;
			}
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
			//TODO: Write Save.
		}

		if (Input.GetKeyDown (KeyCode.F2)) {  //Quick Load
			//TODO: Write Load.
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
