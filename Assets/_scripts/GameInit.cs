using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using AssemblyCSharp;

public class GameInit : MonoBehaviour
{
	private GameSystemSettings _gss;
	private Graph _graph;
	private bool _loading = true;

	IWorld world;

	// Use this for initialization
	void Start () {
		_gss = GameSystemSettings.Instance;

		_graph = new Graph ();
		_PreInit ();

		world = new Grid(_gss);
		_graph = world.Create ();

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
	private void _PostInit() {
		Vector3 pos = new Vector3 (-20, (_gss.height / 2) * _gss._step, -20);
		Vector3 rot = new Vector3 (0, 45.0f, 0);

		Camera.main.transform.position = pos;
		Camera.main.transform.Rotate (rot);
	}

	void OnGUI()
	{
		if (_loading)
			GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), _gss.LoadingScreen, ScaleMode.ScaleAndCrop);
	}

	// Update is called once per frame
	void Update(){

		if(Application.isLoadingLevel)
		{
			_loading = true;
		} else {
			_loading = false;

			if (Input.GetMouseButtonDown (0)) {
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit;
				if (Physics.Raycast (ray, out hit)) {
					Debug.Log (hit.transform.gameObject.name);

					GameObject go1 = hit.transform.gameObject;
					List<GameObject> adj = _graph.getAdj (go1);

					Color c = new Color (Random.value, Random.value, Random.value, 0.25f);

					Renderer r = go1.GetComponent<Renderer> ();
					r.material.color = c;

					Debug.Log (adj.Count);

					foreach (GameObject g in adj) {
						r = g.GetComponent<Renderer> ();
						r.material.color = c;

						DebugTools.DrawLine (go1, c, g, c);
					}
				}
			}
		}
	}
}
