using UnityEngine;
using System.Collections;

public class controller_GUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void OnGUI() {
		string[] toolbar = {"Clear", "Random", "Acorn"};
		
		GUILayout.BeginArea( new Rect(0,0, (Screen.width), (Screen.height/4) ) );
		GUILayout.BeginHorizontal ();
		
		GUILayout.Toolbar(1, toolbar);
		
		GUILayout.EndHorizontal ();
		GUILayout.EndArea ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
