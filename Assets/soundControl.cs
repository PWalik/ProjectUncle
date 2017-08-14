using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundControl : MonoBehaviour {
	void Awake() {
		if (PlayerPrefs.GetInt ("b", 1) == 0) {
			this.gameObject.SetActive (false);
		}
	}
}
