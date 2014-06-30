using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;

public class GameOfLife : MonoBehaviour {
	private int[,,] world;
	private int sx;
	private int sy;
	private Texture2D texture;
	private bool pause = false;
	private int seed = 2;

	private int _generation;
	private int _rules = 1;	//1 = Original
	private int _alive;
	private string _savepath;
	private bool step = false;
    
    //Public
	public float density = 0.5f;	//"f" forces 0.5 to be a single-percission float rather than a double.

	void Init()
	{
		sx = (int) Screen.width/4;
		sy = (int) Screen.height/4;

		if (Debug.isDebugBuild) Debug.Log ("Size: " + sx.ToString() + " by " + sy.ToString());

		world = new int[sx+1, sy+1, 2];

		float height = (float) (Camera.main.orthographicSize * 2.0);
		float width = (float) (height * Screen.width / Screen.height);

		/**
		 * Dividing the width and height both by 10 before setting the transform vector fits
		 * the plane perfectly to the window.  This could have something to do with the OrthographSize..
		 * TODO: Research why this is working...
		 */
		transform.localScale = new Vector3 ((width / 11), 1, (height / 11));

		texture = new Texture2D (sx, sy);
		renderer.material.mainTexture = texture;
	}

	/** 
	 * initialization
	 */ 
	void Start () {
		_savepath = Application.dataPath + "/save.txt";
		if (Debug.isDebugBuild) Debug.Log ("Path: " + _savepath);
        
        Init ();

		//Generate Random Start
		generate (seed);
		BroadcastMessage("SetPause", true);
	}

	/**
	 * Generates Game Board
	 * 
	 * @param int seed, seed to be used.
	 */ 
	public void generate(int seed = 2) {
		if (Debug.isDebugBuild) Debug.Log ("Creating World: " + seed);

		this.seed = seed;
		this._generation = 1;

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

				if( _rules > 2) {
					for (int i = 0; i < (int) (sx * sy * density / 10); i++) { 
						world[(int)(Random.value*sx), (int)(Random.value*sy), 1] = 2;
					}
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
	 *  Draws the the current generation and clears the array for the next generation.
	 */ 
	void draw()
	{
		_alive = 0;
		for (int x = 0; x < sx; x++) {
			for (int y = 0; y < sy; y++) {
				texture.SetPixel (x, y, Color.black);
				
				if ((world [x, y, 1] == 1) || (world [x, y, 1] == 0 && world [x, y, 0] == 1)) {
					world [x, y, 0] = 1;
					texture.SetPixel (x, y, Color.white);
					_alive++;
				} 

				if(_rules > 1)
				{
					if ((world [x, y, 1] == 2) || (world [x, y, 1] == 0 && world [x, y, 0] == 2)) {
						world [x, y, 0] = 2;
						texture.SetPixel (x, y, Color.red);
					}
				}

				if (world [x, y, 1] == -1) {
					world [x, y, 0] = 0;
				}
				
				world [x, y, 1] = 0;
			}
		}

		this.texture.Apply ();
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

			this._generation++;		

			SendMessageUpwards("UpdateGeneration", this._generation);
			SendMessageUpwards("UpdateAlive", this._alive);

			nextGeneration();
		}

		this.step = false;
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




		for (int x = 0; x < sx; x++) {
			for (int y = 0; y < sy; y++) {

				int adj = getAdjacent ((int)x, (int)y);

				switch(_rules) {
				case 1:
					//Birth
					if (adj == 3 && world [x, y, 0] == 0) {
						world [x, y, 1] = 1;
					} 
						
					//Death
					if ((adj < 2 || adj > 3) && world [x, y, 0] == 1) {
						world [x, y, 1] = -1;
					}
					break;
				case 2:
					int pAdj = getAdjacent((int) x, (int) y, 2);

					if(world [x, y, 0] == 0) {
						if ( (adj >= 2 && adj <= 3) ) world [x, y, 1] = 1;
					} else if(world [x, y, 0] == 1) {
						if( pAdj >=1 || adj > 7) {
							world [x, y, 1] = 2;
						} else if ( (adj < 2 || adj > 3) ) { 
							world [x, y, 1] = -1;
						}

					} else if (world [x, y, 0] == 2) {
						if ( adj < 2 || pAdj > 1) world [x, y, 1] = -1;
					}

					break;
				case 3:
					int virus = getAdjacent((int) x, (int) y, 2);
					
					if ( (adj == 0 || virus > 3)  && world [x, y, 0] == 2) {	//Burn-out
						world [x, y, 1] = -1;
					}
					
					if( virus > 1 && world [x, y, 0] == 1) {	//Infect
						world [x, y, 1] = 2;
					}
					
					// Original Cells
					if (adj == 3 && world [x, y, 0] == 0) {		//Birth
						world [x, y, 1] = 1;
					} 
					
					if ( adj < 2 && world [x, y, 0] == 1) {		//Death
						world [x, y, 1] = -1;
					}
					
					if (adj > 3 && world [x, y, 0] == 1) { 		//Mutate [Chance]
						world [x, y, 1] = (int) Mathf.Floor(Random.Range(0,2.05f));
						if(world [x, y, 1] == 0) world [x, y, 1] = -1;
					}
					break;
				}
			}
		}
		return false;
	}

	/** Gets the Count of Adjacent Cells
	 * 
	 * @param int x x-coordinate
	 * @param int y y-coordinate
	 * @param int target (Optional) Target adjacent cell state.
	 */
	int getAdjacent(int x, int y, int target = 1) {
		return 	((world [(x + 1) % sx, y, 0] == target) ? 1 : 0) + 
				((world [(x + sx - 1) % sx, y, 0] == target) ? 1 : 0 ) +
				((world [x, (y + 1) % sy, 0] == target) ? 1 : 0 ) +
				((world [x, (y + sy - 1) % sy, 0] == target) ? 1 : 0 ) +
				((world [(x + 1) % sx, (y + 1) % sy, 0] == target) ? 1 : 0 ) +
				((world [(x + sx - 1) % sx, (y + 1) % sy, 0] == target) ? 1 : 0 ) +
				((world [(x + 1) % sx, (y + sy - 1) % sy, 0] == target) ? 1 : 0 ) +
				((world [(x + sx- 1) % sx, (y + sy - 1) % sy, 0] == target) ? 1 : 0 );   
	}

	public void SetPause(bool a = true) {pause = a;}
	public void TogglePause() {pause = !pause;}
	public void OnRightArrow() {step = true;}

	public void SetRules(int a) {
		_rules = a;
		generate (this.seed);
	}

	public void SaveGame() {
		StringBuilder sb = new StringBuilder ();
		BroadcastMessage("SetPause", true);

		for (int x = 0; x < sx; x++) {
			for (int y = 0; y < sy; y++) {
				if (world [x, y, 0] == 1) {
					string temp = x.ToString()+","+y.ToString();
					sb.AppendLine( temp );
				}
			}
		}

		StreamWriter sw = File.CreateText (this._savepath);
		sw.Write (sb.ToString ());
		sw.Close ();

    }

	public void LoadGame() {
		BroadcastMessage("SetPause", true);
		StreamReader sr = new StreamReader (this._savepath);

		generate (1);	//Clears the Array
		while (!sr.EndOfStream) {
			string line = sr.ReadLine();
			string[] values = line.Split(',');
			world[int.Parse(values[0]),int.Parse (values[1]),1] = 1; 
		}
		
		draw ();
	}
}
