using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
	
public class VectorManagement : MonoBehaviour {
	//this script is responsible for managing the vectors - which nodes are going to appear next on the game board.
	public enum Dir{none, left, right,up,down, COUNT}
	public int stackWidth = 10;
	public bool isAutoOn = false;
	Timer timer;
	public bool start = false;
	public GameObject slider;
	public string[] textz;
	public GameObject Best;
	public bool reload = false;
	public GameObject music, shatter;
	public GameObject text,high;
	public GameObject control;
	public int textSpeed;
	public GameObject clean;
	public GameObject fadeIn;
	public GameObject blink;
	public float gameOverWait;
	public GameObject orbs;
	public bool mixed = false;
	public Dir currMove = Dir.none;
	public GameObject orb;
	public List<Dir> nodes;
	public List<GameObject> nodes_go;
	public GameObject genericNode;
	public GameObject ComboImage;
	public int rowNr = 0;
	bool lost = false;

	public bool Lost {
		get {
			return lost;
		}
	}

	public GameObject comboText;
	int combo = 0;
	public int countCleared;
	void Start() {
		GameObject.FindWithTag ("Chara").GetComponent<FollowBlocks> ().move = determineMove ();
		comboText.GetComponent<Text> ().text = "0";
		combo = 0;
		if (PlayerPrefs.GetInt ("a") <= 0)
			PlayerPrefs.SetInt ("a", 0);
		Best.GetComponent<Text> ().text = "" + PlayerPrefs.GetInt ("a");
		timer = slider.GetComponent<Timer> ();
		AddRow ();
		AddRow ();
		Camera.main.transform.position = new Vector3 (stackWidth / 2 - 0.5f, rowNr + 10, Camera.main.transform.position.z);

		if (isAutoOn) {
			GameObject.FindWithTag ("Timer").transform.localScale = new Vector3 (0, 0, 0);
			comboText.transform.localScale = new Vector3 (0, 0, 0);
			ComboImage.transform.localScale = new Vector3 (0, 0, 0);
			ComboImage.transform.localScale = new Vector3 (0, 0, 0);
		} else {
			Best.transform.localScale = new Vector3 (0, 0, 0);
		}
		StartCoroutine (FadeIn ());
	}
	void Update () {
		//what happens if we get an input?
		if (currMove != Dir.none) {
			//if we get it right
			if (nodes != null)
				if(currMove == nodes [0]) { 
				ClearNode ();
				countCleared++;
				combo++;
				spawnOrb ();
				if(!isAutoOn)
				ComboImage.GetComponent<ComboImg> ().isPulse = true;
				comboText.GetComponent<Text> ().text = "" + combo;
				GameObject.FindWithTag ("Chara").GetComponent<FollowBlocks> ().move = determineMove ();
				if (timer.start != true && isAutoOn == false)
					timer.start = true;
				
				timer.reset = true;
				//or wrong
			} else {
				timer.timer=0;
				comboText.GetComponent<Text> ().text = "" + combo;
			}

			currMove = Dir.none;

		}
		//if we cleared the entire row, then get a new one
		if (countCleared == stackWidth) {
			countCleared = 0;
			AddRow ();
		}

		if (timer.timer <= 0 && lost == false) {
			lost = true;
			Lose ();
		}
	}
	//clear a single node
	void ClearNode() {
		nodes.RemoveAt (0);
		nodes_go [0].GetComponent<NodeController> ().isUsed = true;
		nodes_go.RemoveAt (0);
	}
	//clear entire row of nodes
	void ClearRow() {
		for (int i = 0; i < stackWidth; i++) {
			ClearNode ();
		}
	}
	//add a new row
	void AddRow() {
		
		for (int i = 0; i < stackWidth; i++) {
			Dir temp = RandomizeNode ();
			nodes.Add (temp);
			SpawnNode (temp, i, rowNr);


		}
		rowNr++;

	}
	//randomize the direction of the single node 
	Dir RandomizeNode() {
		return (Dir)Random.Range (1, (int) Dir.COUNT);
	}

	//spawn the GO that will show our node on the board
	void SpawnNode(Dir dir, int x, int y) {
		GameObject node_go;
		if(mixed && rowNr%2 != 0 && rowNr != 0)
			node_go = Instantiate (genericNode, new Vector2 (stackWidth - x -1, y), Quaternion.identity) as GameObject;
		else 
			node_go = Instantiate (genericNode, new Vector2 (x, y), Quaternion.identity) as GameObject;

		node_go.GetComponent<NodeController> ().dir = dir;
		node_go.GetComponent<NodeController> ().row = rowNr;
		node_go.transform.SetParent (this.transform);
		GameObject arrow = null;
		foreach (Transform child in node_go.transform) {
			//VEEEERY WIP, MAY CAUSE ISSUES!!!!
			if (child.name == "Arrow")
				arrow = child.gameObject;
		}
		//will be changed to different- looking gameOs
		if (dir == Dir.left)
			arrow.transform.rotation = new Quaternion (0,0,90, 90);
		else if (dir == Dir.right)
			arrow.transform.rotation = new Quaternion (0,0,-90, 90);
		else if (dir == Dir.down)
			arrow.transform.rotation = new Quaternion (0,0,180, 0);
		nodes_go.Add (node_go);
	}

	void ClearStack() {
		while (nodes.ToArray().Length > 0)
			ClearNode ();

		nodes = new List<Dir> ();
		nodes_go = new List<GameObject> ();
		countCleared = 0;
		rowNr = 0;
	}


	public void Reset(bool isChangeControl) {
		if (isChangeControl) {
			if (isAutoOn)
				Application.LoadLevel (1);
			else
				Application.LoadLevel (0);
		}
	}
	

	public void Lose()
	{
		shatter.GetComponent<AudioSource> ().Play ();
		Destroy (music);
		if (combo >= PlayerPrefs.GetInt ("a")) {
			PlayerPrefs.SetInt ("a", combo);
			GameObject texts = Instantiate (high, new Vector3 (0, 80, 0), Quaternion.identity);
			texts.transform.SetParent (GameObject.FindWithTag ("Canvas").transform,false);
		}

		GameObject.FindWithTag ("Chara").SetActive (false);
		slider.SetActive (false);
		foreach(Transform child in transform) {
			child.GetComponent<Rigidbody2D> ().simulated = true;
			float forcex = child.transform.localPosition.x > 1 ? 1 : -1;
			child.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (Random.Range(forcex, forcex*100), Random.Range (1, 2)));
		}
		foreach (Transform childs in orbs.transform) {
			childs.GetComponent<Orb> ().liveTime = 0;
		}
		StartCoroutine ("spawnEndScreen");
	}

	FollowBlocks.Movement determineMove() {
		int rowClearing = rowNr -2;
		if (countCleared == 0 || countCleared == stackWidth)
				return FollowBlocks.Movement.up;
			else if (mixed == false || rowClearing % 2 == 0)
				return FollowBlocks.Movement.right;
			else
				return FollowBlocks.Movement.left;
		}

	void spawnOrb() {
		int rand;
		int x = ((rand = Random.Range (0, 3))== 0 ? -1 : rand == 1 ? 0 : 1);
		int y = x== 1? ((rand =Random.Range (0, 2)) == 0 ? -1 : 1): ((rand = Random.Range (0, 3)) == 0 ? -1 : rand == 1 ? 0 : 1);
		Vector3 orbPos = Camera.main.ViewportToWorldPoint(new Vector3(x==-1? -0.1f: (x==0? 0.5f:1.1f),y==-1? -0.1f: (y==0? 0.5f:1.1f), 0));
		GameObject orbz= Instantiate (orb, new Vector3(orbPos.x, orbPos.y, 0), Quaternion.identity);
		orbz.transform.SetParent (orbs.transform);
		orb.GetComponent<SpriteRenderer> ().color = new Color (Random.Range (0, 255) / 255f, Random.Range (0, 255) / 255f, Random.Range (0, 255) / 255f, 1/ 255f);
		orbz.GetComponent<Orb> ().orbDir = -new Vector3 (x, y);

	}
	//Couritine for spawning the test + light ball
	IEnumerator spawnEndScreen() {
		//Creating the text "Don't Give Up!"
		float timez = 0f;
		while (timez <= gameOverWait) {
			timez += Time.deltaTime;
			yield return null;
		}

		GameObject texts = Instantiate (text, new Vector3 (0, 90, 0), Quaternion.identity);
		texts.GetComponent<Text> ().text = textz [Random.Range (0, textz.Length)];
		Color c = texts.GetComponent<Text> ().color;
		texts.transform.SetParent (GameObject.FindWithTag ("Canvas").transform,false);
		texts.GetComponent<Text> ().color = new Color (c.r, c.g, c.b, 0f);
		//Slowly showing it on the screen
		for (int i = 0; i < 510; i += textSpeed) {
			c = new Color (c.r, c.g, c.b, c.a + textSpeed / 255f/2);
			texts.GetComponent<Text> ().color = c;
			yield return null;
		}
		//creating the blinking light (all of the functions on the object itself)
		reload = true;
			GameObject blinkz = Instantiate (blink, Camera.main.ViewportToWorldPoint (new Vector3 (0.5f, 0.5f, 0)), Quaternion.identity);
			blinkz.transform.position = new Vector3 (blinkz.transform.position.x, blinkz.transform.position.y, 0);
	}

	public IEnumerator ReloadLevel(int lvl) {
		StartCoroutine (SpawnBall (true,lvl));
		float time = 0f;
		while (time < 0.5) {
			time += Time.deltaTime;
			yield return null;
		}
		StartCoroutine (SpawnBall (false,lvl));
		time = 0f;
		while (time < 0.5) {
			time += Time.deltaTime;
			yield return null;
		}
		SceneManager.LoadScene ("gaaame");
	}
	IEnumerator SpawnBall(bool white,int lvl) {
		GameObject blinkz = Instantiate (clean, Camera.main.ViewportToWorldPoint (new Vector3 (0.5f, 0.5f, 0)), Quaternion.identity);
		if (!white)
			blinkz.GetComponent<SpriteRenderer> ().color = Color.black;
		blinkz.transform.position += new Vector3 (0,0, 5);
		while(blinkz.transform.localScale.x < 25) {
			blinkz.transform.localScale+= new Vector3(0.2f,0.2f,0.2f);
			yield return null;
		}

	}
	IEnumerator FadeIn() {
		Color c = fadeIn.GetComponent<SpriteRenderer> ().color;
		while (c.a > 0) {
			c -= new Color (0, 0, 0, 5 / 255f);
			fadeIn.GetComponent<SpriteRenderer> ().color = c;
			yield return null;
		}
		start = true;
		Destroy (fadeIn);
	}
}
