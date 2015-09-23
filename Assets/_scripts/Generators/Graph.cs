/**
 * Undirected Graph
 */
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AssemblyCSharp
{
	public class Graph : IGraph
	{
		protected List<GameObject> _graph = new List<GameObject> ();
		protected Dictionary<GameObject, List<GameObject>> _adj = new Dictionary<GameObject, List<GameObject>>();

		public Graph() {}

		public void addVertex (GameObject go)
		{
			_graph.Add(go);
			_adj.Add(go,new List<GameObject>());
		}

		public void removeVertex (GameObject go)
		{
			List<GameObject> _nodes = this.getAdj (go);

			foreach (GameObject g in _nodes) {
				this.removeEdge(g, go);
			}

			_adj.Remove(go);
			_graph.Remove (go);
		}

		public void addEdge (GameObject go1, GameObject go2)
		{
			if (!_graph.Contains (go1))
				throw new ArgumentException ("Game Object has not been added to the graph.","go1");

			if (!_graph.Contains (go2))
				throw new ArgumentException ("Game Object has not been added to the graph.","go2");

			_adj [go1].Add (go2);
			_adj [go2].Add (go1);
		}

		public void removeEdge (GameObject go1, GameObject go2)
		{
			if (!_graph.Contains (go1))
				throw new ArgumentException ("Game Object has not been added to the graph.","go1");
			
			if (!_graph.Contains (go2))
				throw new ArgumentException ("Game Object has not been added to the graph.","go2");

			_adj [go1].Remove (go2);
			_adj [go2].Remove (go1);
		}

		public List<GameObject> getAdj (GameObject go)
		{
			return _adj [go];
		}

		public int Size {
			get { return _graph.Count; }
		}

		public bool isEmpty	{
			get { return (_graph.Count == 0); }
		}

		public bool isConnected()
		{
			throw new NotImplementedException ();
		}

		public override string ToString ()
		{
			return string.Format ("[Graph: Size={0}, isEmpty={1}]", Size, isEmpty);
		}

		public List<GameObject> GetElements {
			get { return _graph; }
		}
	}
}

