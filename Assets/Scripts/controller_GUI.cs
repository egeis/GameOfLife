using UnityEngine;
using System.Collections;

public class controller_GUI : MonoBehaviour {

	private int _currentGenerationType = 1;
	private int _generationTabSelected = 1;
	private int _currentGameType = 1;
	private int _gameTabSelected = 1;

	// Use this for initialization
	void Start () {
	
	}

	void OnGUI() {
		string[] toolbar = {"Clear", "Random", "Acorn"};
		
		GUILayout.BeginArea( new Rect(0,0, (Screen.width), (Screen.height/4) ) );
		GUILayout.BeginHorizontal ();
		
		_generationTabSelected = GUILayout.Toolbar(_generationTabSelected, toolbar);
		
		GUILayout.EndHorizontal ();
		GUILayout.EndArea ();
	}
	
	// Update is called once per frame
	void Update () {

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
}
