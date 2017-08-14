using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMenu : MonoBehaviour {
	public GameObject check;
	public void recheckSound() {
		if (PlayerPrefs.GetInt ("b", 0) == 0) {
			PlayerPrefs.SetInt ("b", 1);
		} else {
			PlayerPrefs.SetInt ("b", 0);
		}
	}

	void Update() {
		if (PlayerPrefs.GetInt ("b", 0) == 0) {
			check.gameObject.SetActive (true);
		} else {
			check.gameObject.SetActive (false);
		}
	}
}
