using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetButton : MonoBehaviour {
	public GameObject managem, info;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void StartGame() {
			StartCoroutine (managem.GetComponent<VectorManagement> ().ReloadLevel (1));
	}
}
