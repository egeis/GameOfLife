using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AssemblyCSharp
{
	public class LineHelper : MonoBehaviour
	{
		void TriggerFade()
		{
			StartCoroutine ("Fade");
		}

		IEnumerator Fade() {
			Color _color = this.GetComponent<Renderer>().material.color;
			float inc = -0.05f;

			while (_color.a > 0.0f)
			{
				_color.a += inc;

				this.GetComponent<Renderer>().material.SetColor("_Color",_color);
				yield return new WaitForSeconds(0.1f);
			}

			Destroy (this.gameObject);
		}
	}
}

