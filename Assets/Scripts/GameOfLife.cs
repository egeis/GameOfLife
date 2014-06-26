using UnityEngine;
using System.Collections;

public class GameOfLife : MonoBehaviour {
	private int[,,] world;
	private int sx;
	private int sy;
	private Texture2D texture;
	private bool pause = false;
	private int seed = 2;

	//Public
	public float density = 0.5f;	//"f" forces 0.5 to be a single-percission float rather than a double.
	public bool step = false;

	void Init()
	{
		sx = (int) Screen.width/4;
		sy = (int) Screen.height/4;
		
		world = new int[sx, sy, 2];

		float height = (float) (Camera.main.orthographicSize * 2.0);
		float width = (float) (height * Screen.width / Screen.height);
		transform.localScale = new Vector3( (width/10), 1, (height/10) );

		texture = new Texture2D (sx, sy);
		renderer.material.mainTexture = texture;
	}

	/** 
	 * initialization
	 */ 
	void Start () {
		Init ();

		//Generate Random Start
		generate (seed);
		pause = true;
	}

	public void generate(int seed = 2) {
		if (Debug.isDebugBuild) Debug.Log ("Creating World: " + seed);

		this.seed = seed;

		//Clear World
		for (int x = 0; x < sx; x++) {
			for (int y = 0; y < sy; y++) {
				world[x,y,1] = 0;
				world[x,y,0] = 0;
			}
		}

		//(0,0) is top right...
		switch(seed) {
			case 1: //Clear World.
				//World is Already Cleared.
				break;
			case 2:	//Randomized
				for (int i = 0; i < sx * sy * density; i++) { 
					world[(int)(Random.value*sx), (int)(Random.value*sy), 1] = 1;
				}
				break;	
			case 3: //Acorn	
				int ax = (int) sx / 2;
				int ay = (int) sy / 2;

				world[ax,ay,1] = 1; 
				world[ax-1,ay-2,1] = 1;
				world[ax-1,ay,1] = 1;
				world[ax-3,ay-1,1] = 1; 
				world[ax-4,ay,1] = 1; 
				world[ax-5,ay,1] = 1; 
				world[ax-6,ay,1] = 1; 
				break;
		}

		draw ();
	}

	/**
	 * 
	 */ 
	void draw()
	{
		for (int x = 0; x < sx; x++) {
			for (int y = 0; y < sy; y++) {
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
		if (pause && step == false) {
			//DO Nothing
		} else {
			//Drawing and Updating Texture
			draw();
			nextGeneration();
		}

		step = false;
	}

	private void ChangeGrid(bool type = true) {
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		
		if(collider.Raycast(ray, out hit, Mathf.Infinity) ) {
			Vector2 pos;
			
			pos.x = Mathf.Round( (sx - 1) * ( (hit.point.x - hit.collider.bounds.min.x) / hit.collider.bounds.size.x) );
			pos.y = Mathf.Round( (sy - 1) * ( (hit.point.y - hit.collider.bounds.min.y) / hit.collider.bounds.size.y) );
			
			pos.x = Mathf.Abs( (sx - 1) - pos.x );
			pos.y = Mathf.Abs( (sy - 1) - pos.y );
			
			if(Debug.isDebugBuild) Debug.Log(pos);

			if(type) {
				if (world[(int) pos.x, (int) pos.y,0] == 0) {
					world[(int) pos.x, (int) pos.y,0] = 1;
					texture.SetPixel ((int) pos.x, (int) pos.y, Color.white);
				}
			} else {
				if (world[(int) pos.x, (int) pos.y,0] == 1) {
					world[(int) pos.x, (int) pos.y,0] = 0;
					texture.SetPixel ((int) pos.x, (int) pos.y, Color.black);
				}
			}
			
			texture.Apply ();
		}
	}

	/**
	 * Generates the NEXT generation
	 */
	private bool nextGeneration() {
		//Birth and Death of Generation Next.
		for (int x = 0; x < sx; x++) {
			for (int y = 0; y < sy; y++) {
				
				int adj = getAdjacent ((int)x, (int)y);

				if (adj == 3 && world [x, y, 0] == 0) {
					world [x, y, 1] = 1;
				} 
				
				if ((adj < 2 || adj > 3) && world [x, y, 0] == 1) {
					world [x, y, 1] = -1;
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
		return 	world [(x + 1) % sx, y, 0] + 
			world [(x + sx - 1) % sx, y, 0] +
			world [x, (y + 1) % sy, 0] +
			world [x, (y + sy - 1) % sy, 0] +
			world [(x + 1) % sx, (y + 1) % sy, 0] +
			world [(x + sx - 1) % sx, (y + 1) % sy, 0] +
			world [(x + 1) % sx, (y + sy - 1) % sy, 0] +
			world [(x + sx- 1) % sx, (y + sy - 1) % sy, 0];   
	}

	public void SetPause(bool a = true) {pause = a;}
	public void TogglePause() {pause = !pause;}
	public void OnRightArrow() {step = true;}
}
