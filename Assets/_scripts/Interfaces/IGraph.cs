using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AssemblyCSharp
{
	public interface IGraph
	{
		void addVertex(GameObject go);
		void removeVertex(GameObject go);
		void addEdge(GameObject go1, GameObject go2);
		void removeEdge(GameObject go1, GameObject go2);
		List<GameObject> getAdj (GameObject go);
		bool isConnected();
	}
}

