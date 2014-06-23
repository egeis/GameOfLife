using UnityEngine;
using System.Collections;

public class GameOfLife : MonoBehaviour {
	private int[,,] world;
	private int board_size_x;
	private int board_size_y;
	private Texture2D texture;
	private int last_seed;
	private float timer = 30.0f;

	//Public
	public int height = 32;
	public int width = 32;
	public float density = 0.5f;	//"f" forces 0.5 to be a single-percission float rather than a double.
	public bool pause = false;
	public bool step = false;
	public int seed = 0;			//Predefined: 1 = Acorn.
	
	/** 
	 * initialization
	 */ 
	void Start () {
		if (Debug.isDebugBuild) Debug.Log ("Starting: " + gameObject.name);

		world = new int[width, height, 3];	//0 = Previous Generation, 1 = Current Generation (drawn), 2 = next generation
		board_size_x = width;
		board_size_y = height;

		texture = new Texture2D (width, height);
		renderer.material.mainTexture = texture;

		//Generate Random Start
		generate ();
		draw ();

		pause = true;
	}
	
	public void generate() {
		last_seed = seed;
		for (int x = 0; x < board_size_x; x++) {
			for (int y = 0; y < board_size_y; y++) {
				world[x,y,2] = 0;
				world[x,y,1] = 0;
				world[x,y,0] = 0;
			}
		}

		//(0,0) is top right...
		switch(seed) {
			case 0:	//Randomized
			for (int i = 0; i < board_size_x * board_size_y * density; i++) {
				int x = (int)(Random.value*board_size_x);
				int y = (int)(Random.value*board_size_y);
				world[x, y, 1] = 1;
			}
			break;	
			case 1: //Acorn	
			int sx = (int) width / 2;
			int sy = (int) height / 2;

			world[sx,sy,1] = 1; 
			world[sx-1,sy-2,1] = 1;
			world[sx-1,sy,1] = 1;
			world[sx-3,sy-1,1] = 1; 
			world[sx-4,sy,1] = 1; 
			world[sx-5,sy,1] = 1; 
			world[sx-6,sy,1] = 1; 
			break;
		}
	}

	/**
	 * 
	 */ 
	void draw()
	{
		for (int x = 0; x < board_size_x; x++) {
			for (int y = 0; y < board_size_y; y++) {
				texture.SetPixel (x, y, Color.black);
				
				if ((world [x, y, 1] == 1) || (world [x, y, 1] == 0 && world [x, y, 0] == 1)) {
					world [x, y, 0] = 1;
					texture.SetPixel (x, y, Color.white);
				} 

				if (world [x, y, 1] == -1) {
					world [x, y, 0] = 0;
				}
				
				world [x, y, 1] = 0;
			}
		}
		
		texture.Apply ();
	}
	
	/**
	 * Update is called once per frame
	 */
	void Update () {
		if (pause) {

		}

		if(Input.GetMouseButton(0) ) {
			pause = true;
			
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			
			if(collider.Raycast(ray, out hit, Mathf.Infinity) ) {
				Vector2 pos;
				
				pos.x = Mathf.Round( (width - 1) * ( (hit.point.x - hit.collider.bounds.min.x) / hit.collider.bounds.size.x) );
				pos.y = Mathf.Round( (height - 1) * ( (hit.point.y - hit.collider.bounds.min.y) / hit.collider.bounds.size.y) );

				pos.x = Mathf.Abs( (width - 1) - pos.x );
				pos.y = Mathf.Abs( (height - 1) - pos.y );

				if(Debug.isDebugBuild) Debug.Log(pos);
				
				world[(int) pos.x, (int) pos.y,0] = 1;
				texture.SetPixel ((int) pos.x, (int) pos.y, Color.white);
				texture.Apply ();
			}
		} else {
			if (pause && step == false) {
				//TODO: Toggle Paused Warning


			} else {
				//Regeneration during play.
				if(last_seed != seed) {
					generate();
					pause = true;
				}

				//Drawing and Updating Texture
				draw();
				nextGeneration();
			}

			step = false;
		}
	}

	/**
	 *  OnMouseDown EVENT using gameObject.collider
	 */ 
	/*void OnMouseDown() {
		pause = true;

		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;

		if(collider.Raycast(ray, out hit, Mathf.Infinity) ) {
			Vector2 pos;

			pos.x = Mathf.Round( (width - 1) * ( (hit.point.x - hit.collider.bounds.min.x) / hit.collider.bounds.size.x) );
			pos.y = Mathf.Round( (height - 1) * ( (hit.point.y - hit.collider.bounds.min.y) / hit.collider.bounds.size.y) );

			Debug.Log(pos);

			world[(int) pos.x, (int) pos.y,0] = 1;
			texture.SetPixel ((int) pos.x, (int) pos.y, Color.white);
			texture.Apply ();
		}
	}*/

	/**
	 * Generates the NEXT generation
	 */
	private bool nextGeneration() {
		//Birth and Death of Generation Next.
		for (int x = 0; x < board_size_x; x++) {
			for (int y = 0; y < board_size_y; y++) {
				
				int adj = getAdjacent ((int)x, (int)y);
				//world [x, y, 1] = world [x, y, 0];

				if (adj == 3 && world [x, y, 0] == 0) {
					world [x, y, 1] = 1;
				} 
				
				if ((adj < 2 || adj > 3) && world [x, y, 0] == 1) {
					world [x, y, 1] = -1;
					//world [x, y, 1] = 0;
				}
			}
		}

		return false;
	}

	/** Gets the Count of Adjacent Cells
	 * 
	 * @param int x x-coordinate
	 * @param int y y-coordinate
	 */
	int getAdjacent(int x, int y) {
		return 	world [(x + 1) % board_size_x, y, 0] +
				world [(x + height - 1) % board_size_x, y, 0] +
				world [x, (y + 1) % board_size_y, 0] +
				world [x, (y + height- 1) % board_size_y, 0] +
				world [(x + 1) % board_size_x, (y + 1) % board_size_y, 0] +
				world [(x + height - 1) % board_size_x, (y + 1) % board_size_y, 0] +
				world [(x + 1) % board_size_x, (y + height - 1) % board_size_y, 0] +
				world [(x + height- 1) % board_size_x, (y + height - 1) % board_size_y, 0];   
	}
}
