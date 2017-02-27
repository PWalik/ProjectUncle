using UnityEngine;
using System.Collections;

public class FallingBlock : MonoBehaviour {
	Vector3 currPos, parPos;
	public float speed = 1f;
	public VectorManagement.Dir dir;
	bool fel = false;
	// Use this for initialization
	void Start () {
		parPos = transform.parent.localPosition;
		this.GetComponent<SpriteRenderer> ().color = new Color(Random.Range (0, 255)/255f, Random.Range (0,255)/255f, Random.Range (0, 255)/255f, 255/255f);
	}
	
	// Update is called once per frame
	void Update () {
		if (fel == false) {
			currPos = this.transform.localPosition;
			if (currPos.y > 0) {
				transform.position -= new Vector3 (0, speed);
			} else if (currPos.y <= 0) {
				transform.position = new Vector3 (parPos.x, parPos.y);
				fel = true;
			}
		}
	}
}
