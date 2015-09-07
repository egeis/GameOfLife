using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AssemblyCSharp
{
	public class Grid : IWorld
	{
		protected float _step;
		protected Graph _graph;

		private GameSystemSettings _gss;

		public Grid (GameSystemSettings gss)
		{
			_graph = new Graph ();
			_gss = gss;
			_step = 2.0f;
		}

		public void Destroy ()
		{

		}

		public void Create ()
		{
			Debug.Log ("Creating World:");
			Debug.Log ("Using..." + _gss.prefab.name);

			for (float i = 0; i  < (_gss.width * _step); i += _step )
			{
				for(float j = 0; j < (_gss.depth * _step); j += _step)
				{
					for(float k = 0; k < (_gss.height * _step); k += _step)
					{
						GameObject cell = UnityEngine.GameObject.Instantiate(_gss.prefab, new Vector3(i,j,k), Quaternion.identity) as GameObject;
						cell.name = "Cell_"+i+"_"+j+"_"+k;

					}
				}
			}


		}

		public float Step {
			get { return _step; }
			set { this._step = value; }
		}
	}
}

