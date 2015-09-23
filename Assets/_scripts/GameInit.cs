using UnityEngine;
using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using AssemblyCSharp;

public class GameInit : MonoBehaviour
{
	private GameSystemSettings _gss;
	IWorld world;
	GenerationTask gt;
	Thread t;

	// Use this for initialization
	void Start () {
		_gss = GameSystemSettings.Instance;
		_gss.graph = new Graph ();
		_PreInit ();

		world = new Grid(_gss);
		_gss.graph = world.Create ();
		_gss.Ruleset = new Classic (_gss.graph);

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

	protected void Create()
	{

	}

	// Post-initialization
	private void _PostInit() {
		Vector3 pos = new Vector3 (-20, (_gss.height / 2) * _gss._step, -20);
		Vector3 rot = new Vector3 (0, 45.0f, 0);

		Camera.main.transform.position = pos;
		Camera.main.transform.Rotate (rot);

		gt = new GenerationTask (_gss.Generation);

		t = new Thread(new ThreadStart(gt.Start));
		t.Start ();
	}

	public void OnDestroy()
	{
		t.Abort ();
	}

	void OnGUI()
	{
		//if (_loading)
		//	GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), _gss.LoadingScreen, ScaleMode.ScaleAndCrop);
	}

	/** @TODO Create a Class / Interface for rulesets seperate as a coroutine or other async task **/
	private void Generation()
	{
		if (gt.Completed) {
			foreach (GameObject g in _gss.graph.GetElements) {
				g.BroadcastMessage ("TriggerNextGeneration");
			}

			gt = new GenerationTask(_gss.Generation++);
			t = new Thread(new ThreadStart(gt.Start));
			t.Start ();
		}
	}

	// Update is called once per frame
	void Update(){
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) {
				GameObject go1 = hit.transform.gameObject;
				List<GameObject> adj = _gss.graph.getAdj (go1);
				Color c = new Color (1.0f, 1.0f, 1.0f, 1.0f);

				int alive = 0;

				go1.GetComponent<Node>().State = Node.DEAD;

				foreach (GameObject g in adj) {
					Node n = g.GetComponent<Node>();
					if (n.State == Node.ALIVE) alive++;

					GameObject l = DebugTools.DrawLine (go1, c, g, c);

					l.AddComponent<LineHelper>();
					l.BroadcastMessage("TriggerFade");
				}

				Debug.Log (go1.name + " Status:"+go1.GetComponent<Node>().State);
				Debug.Log ("Adj: "+adj.Count+" LA:"+alive);
			}
		} 

	}
}
