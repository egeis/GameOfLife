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

    private float _minZ = 1f;
    private float _maxZ = 1f;

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
        _maxZ = (Screen.height *1f / Screen.width * 1f ) * (_gs.cellColumns / 2f); //Size = (Units in width / 2) * (height / width)
        Debug.logger.Log(_maxZ);

        _maxZ = (_maxZ > _minZ) ? _maxZ : _minZ;

        _board = GameObject.Find("GameBoard");
        
        sizeX = _gs.cellColumns;
        sizeY = _gs.cellRows;

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
        float size = Camera.main.orthographicSize + (zoom * 1.5f);

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
