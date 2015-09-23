using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AssemblyCSharp
{
	public class Grid : IWorld
	{
		protected Graph _graph;

		private GameSystemSettings _gss;

		public Grid (GameSystemSettings gss)
		{
			_graph = new Graph ();
			_gss = gss;
		}

		public void Destroy ()
		{
			foreach (GameObject g in _graph.GetElements) {
				GameObject.Destroy(g);
			}

			_gss.graph = new Graph ();
		}

		public Graph Create ()
		{
			float x_max = ((_gss.width  * _gss._step) - _gss._step);
			float y_max = ((_gss.height * _gss._step) - _gss._step);
			float z_max = ((_gss.depth  * _gss._step) - _gss._step);
			
			bool xn_wrap, xp_wrap, yn_wrap, yp_wrap, zn_wrap, zp_wrap;
			float xn, yn, zn, xp, yp, zp;

			for (float i = 0; i  < (_gss.width * _gss._step); i += _gss._step ) //X
			{
				for(float j = 0; j < (_gss.height * _gss._step); j += _gss._step) //Y
				{
					for(float k = 0; k < (_gss.depth * _gss._step); k += _gss._step) //Z
					{
						GameObject cell = UnityEngine.GameObject.Instantiate(_gss.prefab, new Vector3(i,j,k), Quaternion.identity) as GameObject;
						cell.name = "Cell_"+i+"_"+j+"_"+k;

						Node n = cell.GetComponent<Node>();
						float v = UnityEngine.Random.value;
						Renderer r = cell.GetComponent<Renderer>();

						n.InitState = (v <= _gss.ActiveChance)? Node.ALIVE : Node.DEAD;
						Color c = r.material.color;
						c.a = (n.State == Node.ALIVE)?1.0f:0.0f;
						r.material.color = c;

						_graph.addVertex(cell);
					}
				}
			}

			for (float i = 0; i  < (_gss.width * _gss._step); i += _gss._step) 
			{ //X
				xn = ( ((i + _gss._step) <= x_max)?i + _gss._step:0.0f);
				xn_wrap = (xn == 0.0f)?true:false;

				xp = ( ((i - _gss._step) >= 0)?i - _gss._step:x_max);
				xp_wrap = (xp == x_max)?true:false;

				for (float j = 0; j < (_gss.height * _gss._step); j += _gss._step)
				{ //Y
					yn = ( ((j + _gss._step) <= y_max)?j + _gss._step:0.0f);
					yn_wrap = (yn == 0.0f)?true:false;

					yp = ( ((j - _gss._step) >= 0)?j - _gss._step:y_max);
					yp_wrap = (yp == y_max)?true:false;

					for (float k = 0; k < (_gss.depth * _gss._step); k += _gss._step)
					{ //Z
						GameObject go1, go2;

						//Find the First Cell
						go1 = GameObject.Find("Cell_"+i+"_"+j+"_"+k);

						zn = ( ((k + _gss._step) <= z_max)?k + _gss._step:0.0f);
						zn_wrap = (zn == 0.0f)?true:false;

						zp = ( ((k - _gss._step) >= 0)?k - _gss._step:z_max);
						zp_wrap = (zp == z_max)?true:false;

						if( (xn_wrap && _gss.WRAP) || !xn_wrap ) 
						{						
							go2 = GameObject.Find("Cell_"+xn+"_"+j+"_"+k);			//X+
							if(_gss.DEBUG_LINES_ENABLE) 
								DebugTools.DrawLine(go1, DebugTools.GhostBlue, go2, DebugTools.GhostBlue);

							_graph.addEdge(go1,go2);
						}

						if( (yn_wrap && _gss.WRAP) || !yn_wrap)
						{
							go2 = GameObject.Find("Cell_"+i+"_"+yn+"_"+k);  		//Y+
							if(_gss.DEBUG_LINES_ENABLE) 
								DebugTools.DrawLine(go1, DebugTools.GhostBlue, go2, DebugTools.GhostBlue);

							_graph.addEdge(go1,go2);
						}

						if( (zn_wrap && _gss.WRAP) || !zn_wrap)
						{
							go2 = GameObject.Find("Cell_"+i+"_"+j+"_"+zn);			//Z+
							if(_gss.DEBUG_LINES_ENABLE) 
								DebugTools.DrawLine(go1, DebugTools.GhostBlue, go2, DebugTools.GhostBlue);

							_graph.addEdge(go1,go2);
						}

						if( ((xn_wrap || zn_wrap )&&_gss.WRAP) || (!xn_wrap && !zn_wrap))
						{
							go2 = GameObject.Find("Cell_"+xn+"_"+j+"_"+zn);			//X+,Z+
							if(_gss.DEBUG_LINES_ENABLE) 
								DebugTools.DrawLine(go1, DebugTools.GhostGreen, go2, DebugTools.GhostGreen);

							_graph.addEdge(go1,go2);
						}


						if( ((xn_wrap || zp_wrap )&&_gss.WRAP) || ( !xn_wrap && !zp_wrap ))
						{
							go2 = GameObject.Find("Cell_"+xn+"_"+j+"_"+zp);			//X-,Z+
							if(_gss.DEBUG_LINES_ENABLE) 
								DebugTools.DrawLine(go1, DebugTools.GhostGreen, go2, DebugTools.GhostGreen);

							_graph.addEdge(go1,go2);
						}

						if( ((xn_wrap || yn_wrap )&&_gss.WRAP)||( !xn_wrap && !yn_wrap ))
						{
							go2 = GameObject.Find("Cell_"+xn+"_"+yn+"_"+k);			//X+,Y+
							if(_gss.DEBUG_LINES_ENABLE) 
								DebugTools.DrawLine(go1, DebugTools.GhostRed, go2, DebugTools.GhostRed);

							_graph.addEdge(go1,go2);
						}

						if( ((xp_wrap || yn_wrap )&&_gss.WRAP)||( !xp_wrap && !yn_wrap ))
						{
							go2 = GameObject.Find("Cell_"+xp+"_"+yn+"_"+k);			//X-,Y+
							if(_gss.DEBUG_LINES_ENABLE) 
								DebugTools.DrawLine(go1, DebugTools.GhostRed, go2, DebugTools.GhostRed);
							
							_graph.addEdge(go1,go2);
						}

						if( ((xn_wrap || yp_wrap )&&_gss.WRAP)||( !xn_wrap && !yp_wrap ))
						{
							go2 = GameObject.Find("Cell_"+xn+"_"+yp+"_"+k);			//X+,Y-
							if(_gss.DEBUG_LINES_ENABLE) 
								DebugTools.DrawLine(go1, DebugTools.GhostRed, go2, DebugTools.GhostRed);

							_graph.addEdge(go1,go2);
						}

						if( ((yn_wrap || zn_wrap )&&_gss.WRAP)||( !yn_wrap && !zn_wrap ))
						{
							go2 = GameObject.Find("Cell_"+i+"_"+yn+"_"+zn);				//Y+,Z+
							if(_gss.DEBUG_LINES_ENABLE) 
									DebugTools.DrawLine(go1, DebugTools.GhostPurple, go2, DebugTools.GhostPurple);

								_graph.addEdge(go1,go2);
						}

						if( ((yn_wrap || zp_wrap )&&_gss.WRAP)||( !yn_wrap && !zp_wrap ))
						{
							go2 = GameObject.Find("Cell_"+i+"_"+yn+"_"+zp);			//Y+,Z-
							if(_gss.DEBUG_LINES_ENABLE) 
								DebugTools.DrawLine(go1, DebugTools.GhostPurple, go2, DebugTools.GhostPurple);

							_graph.addEdge(go1,go2);
						}

						if( ((xn_wrap || yn_wrap || zp_wrap )&&_gss.WRAP) || ( !xn_wrap && !yn_wrap && !zp_wrap ))
						{
							go2 = GameObject.Find("Cell_"+xn+"_"+yn+"_"+zp);			//X+,Y+,Z-
								if(_gss.DEBUG_LINES_ENABLE) 
								DebugTools.DrawLine(go1, DebugTools.GhostPurple, go2, DebugTools.GhostPurple);

							_graph.addEdge(go1,go2);
						}

						if( ((xp_wrap || yn_wrap || zp_wrap )&&_gss.WRAP) || ( !xp_wrap && !yn_wrap && !zp_wrap ))
						{
							go2 = GameObject.Find("Cell_"+xp+"_"+yn+"_"+zp);			//X-,Y+,Z-
							if(_gss.DEBUG_LINES_ENABLE) 
								DebugTools.DrawLine(go1, DebugTools.GhostRed, go2, DebugTools.GhostRed);

							_graph.addEdge(go1,go2);
						}

						if( ((xn_wrap || yn_wrap || zn_wrap )&&_gss.WRAP) || ( !xn_wrap && !yn_wrap && !zn_wrap ))
						{
							go2 = GameObject.Find("Cell_"+xn+"_"+yn+"_"+zn);			//X+,Y+,Z+
							if(_gss.DEBUG_LINES_ENABLE) 
								DebugTools.DrawLine(go1, DebugTools.GhostPurple, go2, DebugTools.GhostPurple);

							_graph.addEdge(go1,go2);
						}

						if( ((xp_wrap || yn_wrap || zn_wrap )&&_gss.WRAP) || ( !xp_wrap && !yn_wrap && !zn_wrap ))
						{
							go2 = GameObject.Find("Cell_"+xp+"_"+yn+"_"+zn);			//X-,Y+,Z+
							if(_gss.DEBUG_LINES_ENABLE) 
								DebugTools.DrawLine(go1, DebugTools.GhostRed, go2, DebugTools.GhostRed);

							_graph.addEdge(go1,go2);
						}
					}
				}
			}

			return _graph;
		}

	}
}

