using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;

public class Node : MonoBehaviour,
					INode 
{
	public const int DEAD = 0;			// Cell Off	
	public const int ALIVE = 1;			// Cell On		

	protected int _state = 0;				// Status of Cell
	protected int _next_state = 0;
	
	public void TriggerNextGeneration()
	{
		_state = _next_state;
		_next_state = 0;
	}

	public int State {
		get { return this._state; }
		set { this._next_state = value; }
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