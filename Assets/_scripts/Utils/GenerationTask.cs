using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AssemblyCSharp
{
	public class GenerationTask
	{
		private int _id;
		private GameSystemSettings _gss;
		public bool Completed = false;

		public GenerationTask (int id)
		{
			_id = id;
			_gss = GameSystemSettings.Instance;
		}

		public int ID {
			get { return _id; }
		}

		public void Start()
		{
			foreach (GameObject g in _gss.graph.GetElements)
			{
				_gss.Ruleset.Process(g.GetComponent<Node>());
			}

			Completed = true;
		}
	}
}

