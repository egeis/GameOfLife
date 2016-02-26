using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

    private float minX;
    private float minY;
    private float maxX;
    private float maxY;

    private float vertExtent;
    private float horzExtent;

    private GlobalSettings _gs;

    void Start ()
    {
        _gs = GlobalSettings.Instance;

        vertExtent = Camera.main.orthographicSize;
        horzExtent = vertExtent * Screen.width / Screen.height;

        minX = horzExtent - _gs.cellRows / 2.0f;
        maxX = _gs.cellRows / 2.0f - horzExtent;

        minY = vertExtent - _gs.cellColumns / 2.0f - 0.5f;
        maxY = _gs.cellColumns / 2.0f - vertExtent + 0.5f;

        Debug.logger.Log(minX + " " + maxX + " " + minY + " " + maxY);
    }
	
	void Update ()
    {
        float xAxisValue = Input.GetAxis("Horizontal");
        float yAxisValue = Input.GetAxis("Vertical");

        Vector3 pos = new Vector3(xAxisValue, yAxisValue, this.transform.position.z);

        pos.x = Mathf.Clamp(pos.x + this.transform.position.x, minX - 1, maxX + 1);
        pos.y = Mathf.Clamp(pos.y + this.transform.position.y, minY - 1, maxY + 1);

        _gs.mainCamera.transform.position = (pos);
    }
}
