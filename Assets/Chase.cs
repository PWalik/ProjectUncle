using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MonoBehaviour {
	public float decSpeed = 1f;
	public int baseFlickerSpeed = 1;
	public int baseExpandSpeed = 1;
	public float rotateSpeed = 2;
	// Use this for initialization
	// Update is called once per frame
	void Start() {
		StartCoroutine ("Appear");
		StartCoroutine ("Expand");
	}





	IEnumerator Appear() {
		while (GetComponent<SpriteRenderer> ().color.a < 200 / 255f) {
			GetComponent<SpriteRenderer> ().color += new Color (0, 0, 0, decSpeed / 255f);
			yield return null;
			}

		StartCoroutine ("Flicker");
		}

	IEnumerator Expand() {
		while (true) {
			int expand = Random.Range (-100, 100);
			if (expand != 0) {
				for (int j = 0; j < 2; j++) {
					for (int i = 0; i < Mathf.Abs (expand); i += baseExpandSpeed) {
						if (expand > 0)
							gameObject.transform.localScale += new Vector3 (baseExpandSpeed / 1000f, baseExpandSpeed / 1000f, 0);
						else
							gameObject.transform.localScale -= new Vector3 (baseExpandSpeed / 1000f, baseExpandSpeed / 1000f, 0);


						yield return null;
					}
					expand = -expand;
				}
				expand = 0;
			}
			yield return null;
		}
		}

IEnumerator Flicker() {
		while (true) {
			int flicker = Random.Range (0, 100);
			if (flicker != 0) {
				Color c = transform.GetComponent<SpriteRenderer> ().color;
				for (int j = 0; j < 2; j++) {
					for (int i = 0; i < Mathf.Abs (flicker); i += baseFlickerSpeed) {
						if(flicker>0)
						c -= new Color (0, 0, 0, baseFlickerSpeed / 255f/2);
						else
						c += new Color (0, 0, 0, baseFlickerSpeed / 255f/2);
						
						transform.GetComponent<SpriteRenderer> ().color = c;
						yield return null;
					}
					flicker = -flicker;
				}
				flicker = 0;
			}

			yield return null;
		}
}
}