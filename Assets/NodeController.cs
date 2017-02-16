using UnityEngine;
using System.Collections;

public class NodeController : MonoBehaviour {
	public bool isUsed = false;
	public GameObject fall;
	public float appearSpeed = 0.1f;
	public VectorManagement.Dir dir;
	public int row;
	public int destroyAfterHowMany = 10;
	int count = 0;
	Color col;
	void Start () {
		col = GetComponent<SpriteRenderer> ().color;
		if(row < 2)
		GetComponent<SpriteRenderer> ().color = new Vector4 (col.r, col.g, col.b,col.a -0.5f);
		else 
			GetComponent<SpriteRenderer> ().color = new Vector4 (col.r, col.g, col.b,col.a -1.0f);
	}

	void Update () {
		if (row >= 2){
			GetComponent<SpriteRenderer> ().color = new Vector4 (col.r, col.g, col.b,col.a - 1.0f + (appearSpeed*count) / 10);
		count++;
	}
		if(count >= 5/ appearSpeed)
			GetComponent<SpriteRenderer> ().color = new Vector4 (col.r, col.g, col.b,col.a -0.5f);
		//On optimalisation you might want to check it only when the row increases
		if (row < GameObject.FindWithTag("Control").GetComponent<VectorManagement> ().rowNr - destroyAfterHowMany) {
			Destroy (this.gameObject);
		}

		if (isUsed) {
			GameObject obj = Instantiate (fall, new Vector3 (transform.position.x ,Camera.main.ViewportToWorldPoint(new Vector3(1,1)).y), Quaternion.identity) as GameObject;;
			obj.GetComponent<FallingBlock> ().dir = dir;
			obj.transform.SetParent (this.transform);
			isUsed = false;
		}
	}
}
