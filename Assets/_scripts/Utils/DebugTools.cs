//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using UnityEngine;

namespace AssemblyCSharp
{
	public static class DebugTools
	{
		public static Color GhostBlue {
			get { return new Color (0.0f, 0.0f, 0.5f, 0.25f); }
		}

		public static Color GhostGreen {
			get { return new Color (0.0f, 0.5f, 0.0f, 0.25f); }
		}

		public static Color ghost_red {
			get { return new Color (0.5f, 0.0f, 0.0f, 0.25f); }
		}

		public static Color ghost_purple {
			get { return new Color (0.5f, 0.0f, 0.5f, 0.25f); }
		}
		
		public static void DrawLine(GameObject go1, Color c1, GameObject go2,  Color c2)
		{
			GameObject line = UnityEngine.GameObject.Instantiate( GameSystemSettings.Instance.DEBUG_LINE_PERM, new Vector3(0,0,0), Quaternion.identity )  as GameObject;
			line.name = "Line_" + go1.transform.position.ToString () + "_" + go2.transform.position.ToString ();
			LineRenderer lineRenderer = line.GetComponent<LineRenderer>();
			lineRenderer.SetColors(c1,c2);
			lineRenderer.SetPosition(0, go1.transform.position);
			lineRenderer.SetPosition(1, go2.transform.position);
		}
	}
}