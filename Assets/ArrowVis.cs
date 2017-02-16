using UnityEngine;
using System.Collections;

public class ArrowVis : MonoBehaviour {
	Color col;
	void Start() {
	}
	// Update is called once per frame
	void Update () {
		foreach(Transform child in transform) {
			col = child.GetComponent<SpriteRenderer> ().color;
			child.GetComponent<SpriteRenderer> ().color = new Vector4 (col.r, col.g, col.b, transform.parent.GetComponent<SpriteRenderer> ().color.a *2);
		}
	}
}
