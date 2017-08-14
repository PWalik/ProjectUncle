using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class infoButton : MonoBehaviour {
	bool clicked = false;
	public GameObject button, fade;
	public void click() {
		clicked = !clicked;
			button.SetActive (clicked);
			fade.SetActive (clicked);

	}
}
