using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour {
	public Vector3 orbDir;
	public float speed = 1f;
	float timer;
	bool startT = false;
	public float decSpeed = 1f;
	public float liveTime = 3f;
	// Use this for initialization
	// Update is called once per frame

	void Start() {
		StartCoroutine (OrbAppear ());
		StartCoroutine (OrbMove ());
	}

	void Update() {
		if (startT == true) {
			timer += Time.deltaTime;
		}
		if (timer >= liveTime)
			StartCoroutine (OrbDestroy ());

	}


	IEnumerator OrbAppear() {
		
		while (GetComponent<SpriteRenderer> ().color.a < 200 / 255f) {
			GetComponent<SpriteRenderer> ().color += new Color (0, 0, 0, 1 / 255f * decSpeed);
			yield return null;
		}
		startT = true;
	}
			

	IEnumerator OrbMove() {
		while(true) {
			transform.Translate (orbDir / 10f * speed);
			yield return null;
		}

	}

	IEnumerator OrbDestroy() {
		while (GetComponent <SpriteRenderer> ().color.a > 5 / 255f) {
			GetComponent<SpriteRenderer> ().color -= new Color (0, 0, 0, 1 / 255f * decSpeed);
			yield return null;
		}
		Destroy (gameObject);
	}
			
}
