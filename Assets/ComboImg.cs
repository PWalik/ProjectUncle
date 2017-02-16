using UnityEngine;
using System.Collections;

public class ComboImg : MonoBehaviour {
	public bool isPulse = false;
	bool pulsing = false;
	bool up = true;
	public float sizeSpeed = 2f;
	Vector3 size;
	// Use this for initialization
	void Start () {
		size = this.transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
		if (isPulse) {
			isPulse = false;
			pulsing = true;
			up = true;
			this.transform.localScale = size;
		}
		if (pulsing) {
			if (up) {
				this.transform.localScale += new Vector3(sizeSpeed / 5, sizeSpeed / 5);
				if (this.transform.localScale.x >= size.x * 3 / 2) {
					this.transform.localScale = size * 3 / 2;
					up = false;
				}

			} else {
				this.transform.localScale -= new Vector3(sizeSpeed/5, sizeSpeed/5);
					if(this.transform.localScale.x <= size.x) {
						up = true;
						pulsing = false;
						this.transform.localScale = size;
					}
			}


	
		}


	}
}
