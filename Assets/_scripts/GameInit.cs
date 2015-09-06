using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class GameInit : MonoBehaviour
{
	private GameSystemSettings gss;
	private List<GameObject> _cells = new List<GameObject>();
	
	// Use this for initialization
	void Start () {
		gss = GameSystemSettings.Instance;
	}

	// Update is called once per frame
	void Update () {
	
	}
}
