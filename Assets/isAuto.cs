using UnityEngine;
using System.Collections;

public class isAuto : MonoBehaviour {
	public bool isAutoOn = true;
	void Awake() {
		DontDestroyOnLoad (this);
	}
}
