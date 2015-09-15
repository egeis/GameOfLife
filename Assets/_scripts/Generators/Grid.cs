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

		}

		public Graph Create ()
		{
			Debug.Log ("Creating World:");
			Debug.Log ("Using..." + _gss.prefab.name);

			//BroadcastMessage ("SetLoadingMessage", "Creating Cells", SendMessageOptions.DontRequireReceiver);
			for (float i = 0; i  < (_gss.width * _gss._step); i += _gss._step ) //X
			{
				for(float j = 0; j < (_gss.height * _gss._step); j += _gss._step) //Y
				{
					for(float k = 0; k < (_gss.depth * _gss._step); k += _gss._step) //Z
					{
						GameObject cell = UnityEngine.GameObject.Instantiate(_gss.prefab, new Vector3(i,j,k), Quaternion.identity) as GameObject;
						cell.name = "Cell_"+i+"_"+j+"_"+k;
						_graph.addVertex(cell);
					}
				}
			}

			float x_max = ((_gss.width  * _gss._step) - _gss._step);
			float y_max = ((_gss.height * _gss._step) - _gss._step);
			float z_max = ((_gss.depth  * _gss._step) - _gss._step);

			bool xn_wrap, xp_wrap, yn_wrap, yp_wrap, zn_wrap, zp_wrap;
			float xn, yn, zn, xp, yp, zp;

			Color ghost_blue = new Color (0.0f, 0.0f, 0.5f, 0.15f);
			Color ghost_green = new Color (0.0f, 0.5f, 0.0f, 0.15f);
			Color ghost_red = new Color (0.5f, 0.0f, 0.0f, 0.15f);
			Color ghost_purple = new Color (0.5f, 0.0f, 0.5f, 0.15f);

			//BroadcastMessage ("SetLoadingMessage", "Connecting Neigbors");
			for (float i = 0; i  < (_gss.width * _gss._step); i += _gss._step) 
			{ //X
				xn = ( ((i + _gss._step) <= x_max)?i + _gss._step:0.0f);
				xn_wrap = (xn == 0.0f)?!_gss.WRAP:false;

				xp = ( ((i - _gss._step) >= 0)?i - _gss._step:x_max);
				xp_wrap = (xp == x_max)?!_gss.WRAP:false;

				for (float j = 0; j < (_gss.height * _gss._step); j += _gss._step)
				{ //Y
					yn = ( ((j + _gss._step) <= y_max)?j + _gss._step:0.0f);
					yn_wrap = (yn == 0.0f)?!_gss.WRAP:false;

					yp = ( ((j - _gss._step) >= 0)?j - _gss._step:y_max);
					yp_wrap = (yp == y_max)?!_gss.WRAP:false;

					for (float k = 0; k < (_gss.depth * _gss._step); k += _gss._step)
					{ //Z
						GameObject go1, go2;

						//Find the First Cell
						go1 = GameObject.Find("Cell_"+i+"_"+j+"_"+k);

						zn = ( ((k + _gss._step) <= z_max)?k + _gss._step:0.0f);
						zn_wrap = (zn == 0.0f)?!_gss.WRAP:false;

						zp = ( ((k - _gss._step) >= 0)?k - _gss._step:z_max);
						zp_wrap = (zp == z_max)?!_gss.WRAP:false;

						if( !xn_wrap  ) 
						{
							go2 = GameObject.Find("Cell_"+xn+"_"+j+"_"+k);			//X+
							if(_gss.DEBUG_LINES_ENABLE && _gss.DEBUG_X) 
								DebugTools.DrawLine(go1, ghost_blue, go2, ghost_blue);

							_graph.addEdge(go1,go2);
						}

						if( !yn_wrap )
						{
							go2 = GameObject.Find("Cell_"+i+"_"+yn+"_"+k);  		//Y+
							if(_gss.DEBUG_LINES_ENABLE && _gss.DEBUG_Y) 
								DebugTools.DrawLine(go1, ghost_blue, go2, ghost_blue);

							_graph.addEdge(go1,go2);
						}

						if( !zn_wrap )
						{
							go2 = GameObject.Find("Cell_"+i+"_"+j+"_"+zn);			//Z+
							if(_gss.DEBUG_LINES_ENABLE && _gss.DEBUG_Z) 
								DebugTools.DrawLine(go1, ghost_blue, go2, ghost_blue);

							_graph.addEdge(go1,go2);
						}

						if( !xn_wrap && !zn_wrap )
						{
							go2 = GameObject.Find("Cell_"+xn+"_"+j+"_"+zn);			//X+,Z+
							if(_gss.DEBUG_LINES_ENABLE && _gss.DEBUG_X && _gss.DEBUG_Z) 
								DebugTools.DrawLine(go1, ghost_green, go2, ghost_green);

							_graph.addEdge(go1,go2);
						}


						if( !xn_wrap && !zp_wrap )
						{
							go2 = GameObject.Find("Cell_"+xn+"_"+j+"_"+zp);			//X-,Z+
							if(_gss.DEBUG_LINES_ENABLE && _gss.DEBUG_X && _gss.DEBUG_Z) 
								DebugTools.DrawLine(go1, ghost_green, go2, ghost_green);

							_graph.addEdge(go1,go2);
						}

						if( !xn_wrap && !yn_wrap )
						{
							go2 = GameObject.Find("Cell_"+xn+"_"+yn+"_"+k);			//X+,Y+
							if(_gss.DEBUG_LINES_ENABLE && _gss.DEBUG_X && _gss.DEBUG_Y) 
								DebugTools.DrawLine(go1, ghost_red, go2, ghost_red);

							_graph.addEdge(go1,go2);
						}

						if( !xn_wrap && !yn_wrap )
						{
							go2 = GameObject.Find("Cell_"+xp+"_"+yn+"_"+k);			//X-,Y+
							if(_gss.DEBUG_LINES_ENABLE && _gss.DEBUG_X && _gss.DEBUG_Y) 
								DebugTools.DrawLine(go1, ghost_red, go2, ghost_red);
							
							_graph.addEdge(go1,go2);
						}

						if(xn_wrap && yp_wrap )
						{
							go2 = GameObject.Find("Cell_"+xn+"_"+yp+"_"+k);			//X+,Y-
							if(_gss.DEBUG_LINES_ENABLE && _gss.DEBUG_X && _gss.DEBUG_Y) 
								DebugTools.DrawLine(go1, ghost_red, go2, ghost_red);

							_graph.addEdge(go1,go2);
						}

			

						if( !yn_wrap && !zn_wrap )
						{
						go2 = GameObject.Find("Cell_"+i+"_"+yn+"_"+zn);				//Y+,Z+
						if(_gss.DEBUG_LINES_ENABLE && _gss.DEBUG_Y && _gss.DEBUG_Z) 
								DebugTools.DrawLine(go1, ghost_purple, go2, ghost_purple);

							_graph.addEdge(go1,go2);
						}

						if( !yn_wrap && !zp_wrap )
						{
							go2 = GameObject.Find("Cell_"+i+"_"+yn+"_"+zp);			//Y+,Z-
							if(_gss.DEBUG_LINES_ENABLE && _gss.DEBUG_Y && _gss.DEBUG_Z) 
								DebugTools.DrawLine(go1, ghost_purple, go2, ghost_purple);

							_graph.addEdge(go1,go2);
						}

						if( !xn_wrap && !yn_wrap && !zp_wrap )
						{
							go2 = GameObject.Find("Cell_"+xn+"_"+yn+"_"+zp);			//X+,Y+,Z-
								if(_gss.DEBUG_LINES_ENABLE && _gss.DEBUG_X && _gss.DEBUG_Y && _gss.DEBUG_Z) 
								DebugTools.DrawLine(go1, ghost_purple, go2, ghost_purple);

							_graph.addEdge(go1,go2);
						}

						if( !xp_wrap && !yn_wrap && !zp_wrap )
						{
							go2 = GameObject.Find("Cell_"+xp+"_"+yn+"_"+zp);			//X-,Y+,Z-
							if(_gss.DEBUG_LINES_ENABLE && _gss.DEBUG_X && _gss.DEBUG_Y && _gss.DEBUG_Z) 
								DebugTools.DrawLine(go1, ghost_red, go2, ghost_red);

							_graph.addEdge(go1,go2);
						}

						if( !xn_wrap && !yn_wrap && !zn_wrap )
						{
							go2 = GameObject.Find("Cell_"+xn+"_"+yn+"_"+zn);			//X+,Y+,Z+
							if(_gss.DEBUG_LINES_ENABLE && _gss.DEBUG_X && _gss.DEBUG_Y && _gss.DEBUG_Z) 
								DebugTools.DrawLine(go1, ghost_purple, go2, ghost_purple);

							_graph.addEdge(go1,go2);
						}

						if( !xp_wrap && !yn_wrap && !zn_wrap )
						{
							go2 = GameObject.Find("Cell_"+xp+"_"+yn+"_"+zn);			//X-,Y+,Z+
							if(_gss.DEBUG_LINES_ENABLE && _gss.DEBUG_X && _gss.DEBUG_Y && _gss.DEBUG_Z) 
								DebugTools.DrawLine(go1, ghost_red, go2, ghost_red);

							_graph.addEdge(go1,go2);
						}
					}
				}
			}

			return _graph;
		}

	}
}

