using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;

public class Node : MonoBehaviour,
					INode 
{
	private GameSystemSettings _gss;

	public const int DEAD = 0;			// Cell Off	
	public const int ALIVE = 1;			// Cell On

	public static int generation = 0;

	protected int _state = 0;			// Status of Cell
	protected int _next_state = 0;		
	protected int _last_state = 0;		
	
	public Node()
	{
		_gss = GameSystemSettings.Instance;
	}

	public int[] AdjacentStates {
		get { return CalculateAdjStates(); }
	}

	public  int[] CalculateAdjStates() {
		int[] _adjStates = new int[2];

		foreach(GameObject sg in _gss.graph.getAdj(this.gameObject) )
		{
			switch(sg.GetComponent<Node>().State)
			{
				case Node.ALIVE:
					_adjStates[1] += 1;
					break;
				case Node.DEAD:
					_adjStates[0] += 1;
					break;
			}
		}

		return _adjStates;
	}

	public void TriggerNextGeneration()
	{
		_last_state = _state;
		_state = _next_state;
		_next_state = 0;

		if (_last_state != _state) {
			if(_state == ALIVE)
			{
				StopCoroutine("Fade");
				StartCoroutine("Fade",1);
			}
			else if (_state == DEAD)
			{
				StopCoroutine("Fade");
				StartCoroutine("Fade",0);
			}
		}
	}

	public int InitState {
		set { this._state = value; }
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

	IEnumerator Fade(int dir) {
		Color color = this.GetComponent<Renderer>().material.color;
		float inc = (dir == 0) ? -0.1f : 0.1f;

		for( int i = 0; i < 10; i++ )
		{
			float t = color.a + inc;

			t = (t < 0.0f)?0.0f:t;	//Min
			t = (t > 1.0f)?1.0f:t;	//Max

			color.a = t;

			this.GetComponent<Renderer>().material.color = color;
			yield return new WaitForSeconds(0.1f);
		}
	}

}