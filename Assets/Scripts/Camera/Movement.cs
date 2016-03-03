using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

    private float minX;
    private float minY;
    private float maxX;
    private float maxY;

    private float sizeX;
    private float sizeY;

    private float vertExtent;
    private float horzExtent;

    private GlobalSettings _gs;

    private float _minZ = 10f;
    private float _maxZ;

    private GameObject _board;

    protected void CalculateMinMax()
    {
        vertExtent = Camera.main.orthographicSize;
        horzExtent = vertExtent * Screen.width / Screen.height;

        // Calculations assume map is position at the origin
        minX = 0 + horzExtent - 0.5f;
        maxX = sizeX - horzExtent - 0.5f;
        minY = 0 + vertExtent - 2.5f;
        maxY = sizeY - vertExtent - 0.5f;
    }


    void Start ()
    {
        _gs = GlobalSettings.Instance;
        _maxZ = (_gs.cellRows / 2.0f > _minZ) ? _gs.cellRows / 2.0f : _minZ;

        _board = GameObject.Find("GameBoard");
        
        sizeX = _gs.cellRows;
        sizeY = _gs.cellColumns;

        CalculateMinMax();

        Debug.logger.Log(minX + " " + maxX + " | " + minY + " " + maxY);

        Vector3 pos = new Vector3(
            Mathf.Clamp(maxX / 2.0f, minX, maxX), 
            Mathf.Clamp(maxY / 2.0f, minY, maxY), 
            this.transform.position.z);

        _gs.mainCamera.transform.position = (pos);
    }

    void Update()
    {
        float zoom = Input.GetAxis("Mouse ScrollWheel");
        float size = Camera.main.orthographicSize + (zoom * 10f);

        if (size != 0.0f)
        {
            Camera.main.orthographicSize = Mathf.Clamp(size, _minZ, _maxZ);
            CalculateMinMax();
        }
    }

    void LateUpdate()
    { 
        float xAxisValue = Input.GetAxis("Horizontal");
        float yAxisValue = Input.GetAxis("Vertical");

        Vector3 pos = new Vector3(xAxisValue, yAxisValue, this.transform.position.z);

        pos.x = Mathf.Clamp(pos.x + this.transform.position.x, minX , maxX );
        pos.y = Mathf.Clamp(pos.y + this.transform.position.y, minY , maxY );

        _gs.mainCamera.transform.position = (pos);
    }


}
