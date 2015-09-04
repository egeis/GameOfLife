using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Node : MonoBehaviour {
	public const int DEAD = 0;			// Cell Off	
	public const int ALIVE = 1;			// Cell On		

	private int _state = 0;				// Status of Cell
	public List<Node> adjacent = new List<Node>();	// To Trigger Update Events.

	//Extended RuleSet
	/*public const int MALE = 1;				
	public const int FEMALE = 2;

	private int _age = 0;				// New Born.
	private int	_gender = 0;			// Undefined Gender.

	private float _fertility = 0.0f;		// Chance to bare children.
	private float _fitness = 0.0f;		// Suvival Score.
	private float _courage = 0.0f;		// Used in making poor judgement.
	private float _speed = 0.0f;*/			// Used in calculating Movement.

	public int State {
		get { return this._state; }
		set { this._state = value; }
	}

	/*public int Age {
		get { return this._age; }
		set { this._age = value; }
	}*/

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