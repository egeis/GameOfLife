using System;
using System.Collections;
using System.Collections.Generic;

namespace AssemblyCSharp
{
	public abstract class ARules : IRuleset
	{
		protected List<IRules> ruleset = new List<IRules>(); 
		protected Graph _graph;

		public ARules(Graph _g)
		{
			_graph = _g;
		}

		public bool Process(Node n)
		{
			bool result = false;

			foreach(IRules r in ruleset) 
			{
				result = r.Check(n);
				if (result) break;
			}

			return result;
		}
	}
}

