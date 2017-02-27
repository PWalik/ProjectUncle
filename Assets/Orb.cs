using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour {
	int phase = 0;
	public Vector3 orbDir;
	public float speed = 1f;
	float timer;
	public float decSpeed = 1f;
	public float liveTime = 3f;
	// Use this for initialization
	// Update is called once per frame
	void Update () {
		if (phase == 0) {
			GetComponent<SpriteRenderer> ().color += new Color (0, 0, 0, 1 / 255f * decSpeed);
			if (GetComponent<SpriteRenderer> ().color.a >= 200 / 255f) {
				phase++;
			}
		}
		if (phase == 1) {
			timer += Time.deltaTime;
			transform.Translate (orbDir / 10f * speed);
			if (timer >= liveTime) {
				GetComponent<SpriteRenderer> ().color -= new Color (0, 0, 0, 1 / 255f * decSpeed);
			}
			if (GetComponent<SpriteRenderer> ().color.a <= 5 / 255f) {
				Destroy (gameObject);
			}
		}
	}
}
