using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

    private float minX;
    private float minY;
    private float maxX;
    private float maxY;

    private bool zooming = false;
    public float zoomSpeed = 1.0f;
    public float zoomSmoothSpeed = 2.0f;
    private float orthoSize;

    private float sizeX;
    private float sizeY;

    private float vertExtent;
    private float horzExtent;

    private GlobalSettings _gs;

    private float _minZ = 1f;
    private float _maxZ = 1f;

    private GameObject _board;

    protected void CalculateMinMaxBounds()
    {
        vertExtent = Camera.main.orthographicSize;
        horzExtent = vertExtent * Screen.width / Screen.height;

        // Calculations assume map is position at the origin, the origin is also the the bottom left corner of the gameboard.
        minX = 0 + horzExtent - 0.5f;
        maxX = sizeX - horzExtent - 0.5f;
        minY = 0 + vertExtent - 2.5f;
        maxY = sizeY - vertExtent - 0.5f;
    }


    void Start ()
    {
        _gs = GlobalSettings.Instance;
        _board = GameObject.Find("GameBoard");

        _maxZ = (Screen.height *1f / Screen.width * 1f ) * (_gs.cellColumns / 2f); //Size = (Units in width / 2) * (height / width)
        _maxZ = (_maxZ > _minZ) ? _maxZ : _minZ;

        sizeX = _gs.cellColumns;
        sizeY = _gs.cellRows;
        orthoSize = Camera.main.orthographicSize;

        CalculateMinMaxBounds();

        //Center the Camera to the Board
        Vector3 pos = new Vector3(
            Mathf.Clamp(maxX / 2.0f, minX, maxX), 
            Mathf.Clamp(maxY / 2.0f, minY, maxY), 
            this.transform.position.z);

        _gs.mainCamera.transform.position = (pos);
    }

    void Update()
    {
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel"); 
         
        if(scrollWheel != 0.0f)
        {
            orthoSize -= scrollWheel * zoomSpeed;
            orthoSize = Mathf.Clamp(orthoSize, _minZ, _maxZ);
        }

        Camera.main.orthographicSize = Mathf.MoveTowards(Camera.main.orthographicSize, orthoSize, Time.deltaTime * zoomSmoothSpeed);
        CalculateMinMaxBounds();
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
