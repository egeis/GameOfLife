using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Node : MonoBehaviour {
	public const int DEAD = 0;			// Cell Off	
	public const int ALIVE = 1;			// Cell On		

	private int _state = 0;				// Status of Cell

	public int State {
		get { return this._state; }
		set { this._state = value; }
	}
	
	public float X {
		get { return this.transform.position.x; }
	}

	public float Y {
		get { return this.transform.position.y; }
	}

	public float Z {
		get { return this.transform.position.z; }
	}
	
}