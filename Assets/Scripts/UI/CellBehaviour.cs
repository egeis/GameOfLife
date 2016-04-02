using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CellBehaviour : MonoBehaviour
{
    private GlobalSettings _gs;
    private Color _nextColor;
    private int generation = 0;

	// Use this for initialization
	void Start ()
    {
        _gs = GlobalSettings.Instance;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (_gs.getCurrentGeneration() > generation)
        {
            generation++;
            StartCoroutine("CrossFade");
        }
	}

    IEnumerator CrossFade()
    {
        Color current = GetComponent<SpriteRenderer>().color;

        int n = -1;
        _gs.currentStates.TryGetValue(new Vector2(transform.position.x, transform.position.y), out n);
        Color next = _gs.Rules.getColorValue(n);

        if (current.Equals(next)) yield break;

        for (float t = 0f; t < 1.0f; t += Time.deltaTime / 1.0f)
        {
            Color nc = new Color(
                Mathf.Lerp(current.r, next.r, t),
                Mathf.Lerp(current.g, next.g, t),
                Mathf.Lerp(current.b, next.b, t),
                Mathf.Lerp(current.a, next.a, t)
            );

            GetComponent<SpriteRenderer>().color = nc;
            yield return null;
        }
    }
}
