using UnityEngine;
using System.Collections;

public class CellBehaviour : MonoBehaviour
{
    private GlobalSettings _gs;
    private int state = 0;

    public int nextState = 0;
    public Color nextColor;

	// Use this for initialization
	void Start ()
    {
        _gs = GlobalSettings.Instance;
        state = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
	    //if(_gs.getCurrentGeneration() > generation)
        //{
            //generation++;
            _gs.currentStates.TryGetValue(new Vector2(transform.position.x, transform.position.y), out state);
            nextColor = _gs.Rules.getColorValue(state);

            StartCoroutine("CrossFade");
        //}
	}

    IEnumerator CrossFade()
    {
        Color current = GetComponent<SpriteRenderer>().color;
        for (float t = 0f; t < _gs.minSecondsBetweenGenerations / 2.0f; t+=Time.deltaTime)
        {
            Color newColor = new Color(
                Mathf.Lerp(lastColor.r, nextColor.r, t),
                Mathf.Lerp(lastColor.g, nextColor.g, t),
                Mathf.Lerp(lastColor.b, nextColor.b, t),
                Mathf.Lerp(lastColor.a, nextColor.a, t)
            );

            GetComponent<SpriteRenderer>().color = newColor;
            yield return null;
        }
    }
}
