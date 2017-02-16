using UnityEngine;
using System.Collections;

public class CanvasScript : MonoBehaviour {
	bool OG = false;
	void Awake() {
		if (GameObject.FindGameObjectsWithTag ("Canvas").Length == 1)
			OG = true;

		if (OG != true)
			Destroy (this.gameObject);
		
		DontDestroyOnLoad (this);
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
