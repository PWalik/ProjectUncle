using UnityEngine;
using System.Collections;

public class Pavement : MonoBehaviour {
	public int destroyAtCameraPoint = 10;
	
	// Update is called once per frame
	void Update () {
		if (Camera.main.transform.position.y > destroyAtCameraPoint) {
			Destroy (this);
		}
	}
}
