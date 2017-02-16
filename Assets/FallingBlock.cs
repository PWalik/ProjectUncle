using UnityEngine;
using System.Collections;

public class FallingBlock : MonoBehaviour {
	Vector3 currPos, parPos;
	public float speed = 1f;
	public Sprite right,left,up,down;
	public VectorManagement.Dir dir;
	// Use this for initialization
	void Start () {
		parPos = transform.parent.localPosition;
		if (dir == VectorManagement.Dir.left)
			this.transform.GetComponent<SpriteRenderer> ().sprite = left;
		else if (dir == VectorManagement.Dir.left)
			this.transform.GetComponent<SpriteRenderer> ().sprite = right;
		else if (dir == VectorManagement.Dir.up)
			this.transform.GetComponent<SpriteRenderer> ().sprite = up;
		else
			this.transform.GetComponent<SpriteRenderer> ().sprite = down;
	}
	
	// Update is called once per frame
	void Update () {
		currPos = this.transform.localPosition;
		if (currPos.y > 0) {
			transform.position -= new Vector3 (0, speed);
		} else if (currPos.y < 0) {
			transform.position = new Vector3 (parPos.x, parPos.y);
		}
	}
}
