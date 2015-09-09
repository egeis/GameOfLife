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

		protected void DrawLine(GameObject go1, Color c1, GameObject go2,  Color c2)
		{
			GameObject line = UnityEngine.GameObject.Instantiate( _gss.DEBUG_LINE_PERM, new Vector3(0,0,0), Quaternion.identity )  as GameObject;
			LineRenderer lineRenderer = line.GetComponent<LineRenderer>();
			lineRenderer.SetColors(c1,c2);
			lineRenderer.SetPosition(0, go1.transform.position);
			lineRenderer.SetPosition(1, go2.transform.position);
		}

		public void Destroy ()
		{

		}

		public void Create ()
		{
			Debug.Log ("Creating World:");
			Debug.Log ("Using..." + _gss.prefab.name);

			//BroadcastMessage ("SetLoadingMessage", "Creating Cells", SendMessageOptions.DontRequireReceiver);
			for (float i = 0; i  < (_gss.width * _step); i += _step ) //X
			{
				for(float j = 0; j < (_gss.height * _step); j += _step) //Y
				{
					for(float k = 0; k < (_gss.depth * _step); k += _step) //Z
					{
						GameObject cell = UnityEngine.GameObject.Instantiate(_gss.prefab, new Vector3(i,j,k), Quaternion.identity) as GameObject;
						cell.name = "Cell_"+i+"_"+j+"_"+k;

						/*Vector3 c = cell.transform.position;
						if( i > 0) {
							GameObject go2 = GameObject.Find("Cell_"+(i-_step)+"_"+j+"_"+k);

							if(_gss.DEBUG_LINES_ENABLE) {
								GameObject line = UnityEngine.GameObject.Instantiate( _gss.DEBUG_LINE_PERM, new Vector3(0,0,0), Quaternion.identity )  as GameObject;
								LineRenderer lineRenderer = line.GetComponent<LineRenderer>();
								lineRenderer.SetColors(Color.black,Color.black);
								lineRenderer.SetPosition(0,c);
								lineRenderer.SetPosition(1, go2.transform.position);
							}
						}
						
						if( j > 0) {
							GameObject go2 = GameObject.Find("Cell_"+i+"_"+(j-_step)+"_"+k);
							
							if(_gss.DEBUG_LINES_ENABLE) {
								GameObject line = UnityEngine.GameObject.Instantiate( _gss.DEBUG_LINE_PERM, new Vector3(0,0,0), Quaternion.identity )  as GameObject;
								LineRenderer lineRenderer = line.GetComponent<LineRenderer>();
								lineRenderer.SetColors(Color.black,Color.black);
								lineRenderer.SetPosition(0,c);
								lineRenderer.SetPosition(1, go2.transform.position);
							}
						}
						if( k > 0) {
							GameObject go2 = GameObject.Find("Cell_"+i+"_"+j+"_"+(k-_step));
							
							if(_gss.DEBUG_LINES_ENABLE) {
								GameObject line = UnityEngine.GameObject.Instantiate( _gss.DEBUG_LINE_PERM, new Vector3(0,0,0), Quaternion.identity )  as GameObject;
								LineRenderer lineRenderer = line.GetComponent<LineRenderer>();
								lineRenderer.SetColors(Color.black,Color.black);
								lineRenderer.SetPosition(0,c);
								lineRenderer.SetPosition(1, go2.transform.position);
							}
						}

						if( (i > 0) && ( j > 0)) {
							GameObject go2 = GameObject.Find("Cell_"+(i-_step)+"_"+(j-_step)+"_"+k);
							
							if(_gss.DEBUG_LINES_ENABLE) {
								GameObject line = UnityEngine.GameObject.Instantiate( _gss.DEBUG_LINE_PERM, new Vector3(0,0,0), Quaternion.identity )  as GameObject;
								LineRenderer lineRenderer = line.GetComponent<LineRenderer>();
								lineRenderer.SetColors(Color.black,Color.black);
								lineRenderer.SetPosition(0,c);
								lineRenderer.SetPosition(1, go2.transform.position);
							}
						}

						if( (i > 0) && ( k > 0)) {
							GameObject go2 = GameObject.Find("Cell_"+(i-_step)+"_"+j+"_"+(k-_step));

							if(_gss.DEBUG_LINES_ENABLE) {
								GameObject line = UnityEngine.GameObject.Instantiate( _gss.DEBUG_LINE_PERM, new Vector3(0,0,0), Quaternion.identity )  as GameObject;
								LineRenderer lineRenderer = line.GetComponent<LineRenderer>();
								lineRenderer.SetColors(Color.black,Color.black);
								lineRenderer.SetPosition(0,c);
								lineRenderer.SetPosition(1, go2.transform.position);
							}
						}

						if( (k > 0) && ( j > 0)) {
							GameObject go2 = GameObject.Find("Cell_"+i+"_"+(j-_step)+"_"+(k-_step));
							
							if(_gss.DEBUG_LINES_ENABLE) {
								GameObject line = UnityEngine.GameObject.Instantiate( _gss.DEBUG_LINE_PERM, new Vector3(0,0,0), Quaternion.identity )  as GameObject;
								LineRenderer lineRenderer = line.GetComponent<LineRenderer>();
								lineRenderer.SetColors(Color.black,Color.black);
								lineRenderer.SetPosition(0,c);
								lineRenderer.SetPosition(1, go2.transform.position);
							}
						}
							
						if( (i > 0) && ( j > 0) && (k > 0)) {
							GameObject go2 = GameObject.Find("Cell_"+(i-_step)+"_"+(j-_step)+"_"+(k-_step));
							
							if(_gss.DEBUG_LINES_ENABLE) {
								GameObject line = UnityEngine.GameObject.Instantiate( _gss.DEBUG_LINE_PERM, new Vector3(0,0,0), Quaternion.identity )  as GameObject;
								LineRenderer lineRenderer = line.GetComponent<LineRenderer>();
								lineRenderer.SetColors(Color.blue,Color.blue);
								lineRenderer.SetPosition(0,c);
								lineRenderer.SetPosition(1, go2.transform.position);
							}
						}	

						if( (i > 0)&&(k < (_gss.depth * _step) - _step)) {
							GameObject go2 = GameObject.Find("Cell_"+(i-_step)+"_"+j+"_"+(k+_step));

							if(_gss.DEBUG_LINES_ENABLE) {
								GameObject line = UnityEngine.GameObject.Instantiate( _gss.DEBUG_LINE_PERM, new Vector3(0,0,0), Quaternion.identity )  as GameObject;
								LineRenderer lineRenderer = line.GetComponent<LineRenderer>();
								lineRenderer.SetColors(Color.black,Color.black);
								lineRenderer.SetPosition(0,c);
								lineRenderer.SetPosition(1, go2.transform.position);
							}
						}

						if( (i > 0)&&(j < (_gss.height * _step) - _step)) {
							GameObject go2 = GameObject.Find("Cell_"+(i-_step)+"_"+(j+_step)+"_"+k);

							if(_gss.DEBUG_LINES_ENABLE) {
								GameObject line = UnityEngine.GameObject.Instantiate( _gss.DEBUG_LINE_PERM, new Vector3(0,0,0), Quaternion.identity )  as GameObject;
								LineRenderer lineRenderer = line.GetComponent<LineRenderer>();
								lineRenderer.SetColors(Color.black,Color.black);
								lineRenderer.SetPosition(0,c);
								lineRenderer.SetPosition(1, go2.transform.position);
							}
						}

						if( (j > 0) && (k < (_gss.depth * _step) - _step)) 
						{
							Debug.Log(i + " " + j + " " + k);
							//Debug.Log(("Cell_"+(i)+"_"+(j-_step)+"_"+(k+_step)));

							GameObject go2 = GameObject.Find("Cell_"+(i)+"_"+(j-_step)+"_"+(k+_step));
							
							if(_gss.DEBUG_LINES_ENABLE) {
								GameObject line = UnityEngine.GameObject.Instantiate( _gss.DEBUG_LINE_PERM, new Vector3(0,0,0), Quaternion.identity )  as GameObject;
								LineRenderer lineRenderer = line.GetComponent<LineRenderer>();
								lineRenderer.SetColors(Color.black,Color.black);
								lineRenderer.SetPosition(0,c);
								lineRenderer.SetPosition(1, go2.transform.position);
							}
						}

						if( (i > 0) && (j < (_gss.height * _step) - _step) && (k < (_gss.depth * _step) - _step)) 
						{
							GameObject go2 = GameObject.Find("Cell_"+(i-_step)+"_"+(j+_step)+"_"+(k+_step));
							Debug.Log(i + " " + j + " " + k);
							Debug.Log("Cell_"+(i+_step)+"_"+(j-_step)+"_"+(k-_step));

							if(_gss.DEBUG_LINES_ENABLE) {
								GameObject line = UnityEngine.GameObject.Instantiate( _gss.DEBUG_LINE_PERM, new Vector3(0,0,0), Quaternion.identity )  as GameObject;
								LineRenderer lineRenderer = line.GetComponent<LineRenderer>();
								lineRenderer.SetColors(Color.red,Color.red);
								lineRenderer.SetPosition(0,c);
								lineRenderer.SetPosition(1, go2.transform.position);
							}
						}

						if( (i > 0) && ( j > 0) && (k < (_gss.depth * _step) - _step)) 
						{
							GameObject go2 = GameObject.Find("Cell_"+(i-_step)+"_"+(j-_step)+"_"+(k+_step));
							
							if(_gss.DEBUG_LINES_ENABLE) {
								GameObject line = UnityEngine.GameObject.Instantiate( _gss.DEBUG_LINE_PERM, new Vector3(0,0,0), Quaternion.identity )  as GameObject;
								LineRenderer lineRenderer = line.GetComponent<LineRenderer>();
								lineRenderer.SetColors(Color.blue,Color.blue);
								lineRenderer.SetPosition(0,c);
								lineRenderer.SetPosition(1, go2.transform.position);
							}
						}

						if( (i > 0) && ( k > 0) && (j < (_gss.height * _step) - _step)) 
						{
							GameObject go2 = GameObject.Find("Cell_"+(i-_step)+"_"+(j+_step)+"_"+(k-_step));
							
							if(_gss.DEBUG_LINES_ENABLE) {
								GameObject line = UnityEngine.GameObject.Instantiate( _gss.DEBUG_LINE_PERM, new Vector3(0,0,0), Quaternion.identity )  as GameObject;
								LineRenderer lineRenderer = line.GetComponent<LineRenderer>();
								lineRenderer.SetColors(Color.blue,Color.blue);
								lineRenderer.SetPosition(0,c);
								lineRenderer.SetPosition(1, go2.transform.position);
							}
						}*/

					}
				}
			}

			float x_max = ((_gss.width  * _step) - _step);
			float y_max = ((_gss.height * _step) - _step);
			float z_max = ((_gss.depth  * _step) - _step);

			//BroadcastMessage ("SetLoadingMessage", "Connecting Neigbors");
			for (float i = 0; i  < (_gss.width * _step); i += _step) { 			//X
				for (float j = 0; j < (_gss.height * _step); j += _step) { 		//Y
					for (float k = 0; k < (_gss.depth * _step); k += _step) { 	//Z

						GameObject go1, go2;

						//Find the First Cell
						go1 = GameObject.Find("Cell_"+i+"_"+j+"_"+k);

						// Find the Adjacent Cells (9)
						//1  Z+
						if( k < ((_gss.depth * _step) - _step) ) 
						{
							go2 = GameObject.Find("Cell_"+i+"_"+j+"_"+(k+_step));
						
							if(_gss.DEBUG_LINES_ENABLE) 
								this.DrawLine(go1, Color.blue, go2, Color.blue);

						} else {
							go2 = GameObject.Find("Cell_"+i+"_"+j+"_"+0);

							if(_gss.DEBUG_WRAP_ENABLE) 
								DrawLine(go1, new Color(0.5f,0.5f,0.5f,0.5f), go2, new Color(0.5f,0.5f,0.5f,0.5f));
						}

						//2  X+
						if( i < ((_gss.width * _step) - _step) ) 
						{
							go2 = GameObject.Find("Cell_"+(i+_step)+"_"+j+"_"+k);

							if(_gss.DEBUG_LINES_ENABLE) 
								DrawLine(go1, Color.blue, go2, Color.blue);
						} else {
							go2 = GameObject.Find("Cell_"+0+"_"+j+"_"+k);

							if(_gss.DEBUG_WRAP_ENABLE) 
								DrawLine(go1, new Color(0.5f,0.5f,0.5f,0.5f), go2, new Color(0.5f,0.5f,0.5f,0.5f));
						}

						//3  X+Z+
						if( i < ((_gss.width * _step) - _step) && k < ((_gss.depth * _step) - _step) ) 
						{
							go2 = GameObject.Find("Cell_"+(i+_step)+"_"+j+"_"+(k+_step));
							
							if(_gss.DEBUG_LINES_ENABLE) 
								DrawLine(go1, Color.red, go2, Color.red);
						} else {
							if(_gss.WRAP_XZ)
							{
								if(i < ((_gss.width * _step) - _step))
								{
									go2 = GameObject.Find("Cell_"+(i+_step)+"_"+j+"_"+0);
								} else if(k < ((_gss.depth * _step) - _step)) {
									go2 = GameObject.Find("Cell_"+0+"_"+j+"_"+(k+_step));
								} else {
									go2 = GameObject.Find("Cell_"+0+"_"+j+"_"+0);
								}

								if(_gss.DEBUG_WRAP_ENABLE) 
									DrawLine(go1, new Color(0.5f,0.5f,0.5f,0.5f), go2, new Color(0.5f,0.5f,0.5f,0.5f));

							}
						}

						//4  X+Z-
						if( i < ((_gss.width * _step) - _step) && (k > 0) ) 
						{
							go2 = GameObject.Find("Cell_"+(i+_step)+"_"+j+"_"+(k-_step));
							
							if(_gss.DEBUG_LINES_ENABLE) 
								DrawLine(go1, Color.red, go2, Color.red);
						} else {

							if(_gss.WRAP_XZ)
							{
								if(i < ((_gss.width * _step) - _step))
								{
									go2 = GameObject.Find("Cell_"+(i+_step)+"_"+j+"_"+z_max);
								} else if(k > 0) {
									go2 = GameObject.Find("Cell_"+0+"_"+j+"_"+(k-_step));
								} else {
									go2 = GameObject.Find("Cell_"+0+"_"+j+"_"+z_max);
								}

								if(_gss.DEBUG_WRAP_ENABLE) 
									DrawLine(go1, new Color(0.5f,0.5f,0.5f,0.5f), go2, new Color(0.5f,0.5f,0.5f,0.5f));
							}

						}

						//5  Y+
						if( j < ((_gss.height * _step) - _step) ) 
						{
							go2 = GameObject.Find("Cell_"+i+"_"+(j+_step)+"_"+k);
							
							if(_gss.DEBUG_LINES_ENABLE) 
								DrawLine(go1, Color.blue, go2, Color.blue);
						} else {
							go2 = GameObject.Find("Cell_"+i+"_"+0+"_"+k);

							if(_gss.DEBUG_WRAP_ENABLE) 
									DrawLine(go1, new Color(0.5f,0.5f,0.5f,0.5f), go2, new Color(0.5f,0.5f,0.5f,0.5f));
						}

						//6  Y+Z+


						//7  Y+Z-


						//8  X+Y+


						//8  X+Y+Z+


						//9  X-Y+Z-

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

