using UnityEngine;
using System.Collections;
//keyboard controller, done in a way that it can be easily swapped for touch controls (throw out this script and get a new one with touch
public class KeyboardControl : MonoBehaviour {
	VectorManagement manage;
	string input;
	// Use this for initialization
	void Start () {
		manage = GameObject.FindWithTag("Control").GetComponent<VectorManagement> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.A) || Input.GetKeyDown (KeyCode.D) || Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown (KeyCode.S) && manage.start == true) {
			if (manage.Lost == true || manage.isAutoOn) {
				StartCoroutine (manage.ReloadLevel (1));
			}
			else {
				switch (Input.inputString) {
				case "a":
					manage.currMove = VectorManagement.Dir.left;
					break;
				case "d":
					manage.currMove = VectorManagement.Dir.right;
					break;
				case "w":
					manage.currMove = VectorManagement.Dir.up;
					break;
				case "s":
					manage.currMove = VectorManagement.Dir.down;
					break;

				}
			}
		}
	}
}
