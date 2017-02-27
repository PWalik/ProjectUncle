using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Timer : MonoBehaviour {
	public float timeLeft = 4f;
	public float timer;
	public bool start = false;
	public int changeEveryXTries = 4;
	public float increase = 0.1f;
	float starts = 1f;
	int tries;
	public bool reset = false;
	// Use this for initialization
	void Awake() {
		timer = timeLeft;
		GetComponent<Slider> ().maxValue = timeLeft;
	}
	// Update is called once per frame
	void Update () {
		if (start) {
			timer -= (Time.fixedDeltaTime * starts);
			GetComponent<Slider> ().value = timer;
			//now it resets, maybe just add a little bit to it, instead of filling it completely
			if (reset) {
				reset = false;
				tries++;
				if (tries >= changeEveryXTries && timeLeft > 1f) {
					tries = 0;
					starts += increase;
				}
				timer += GetComponent<Slider>().maxValue * 1 / 5f;
				if (timer > timeLeft)
					timer = timeLeft;
			}


			if (timer <= 0)
				GameOver ();
		}
	}

	void GameOver() {
		
	}
}
