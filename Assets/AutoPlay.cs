using UnityEngine;
using System.Collections;

public class AutoPlay : MonoBehaviour {

	VectorManagement manage;
	float timer = 0f;
	public float cd = 0.3f;
	float realcd;
	// Use this for initialization
	void Start () {
		manage = GameObject.FindWithTag("Control").GetComponent<VectorManagement> ();
		realcd = Random.Range (cd / 2, 1.5f * cd);
	}
	
	// Update is called once per frame
	void Update () {
		if (manage.isAutoOn) {
			timer += Time.deltaTime;
			if (timer > realcd) {
				timer = 0;
				realcd = Random.Range (0.8f *cd, 1.2f * cd);
				manage.currMove = manage.nodes [0];
			}
		} else
			timer = 0;
	}


}
