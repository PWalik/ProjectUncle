using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchControls : MonoBehaviour {
	bool couldBeSwipe;
	Vector3 startPos;
	VectorManagement manage;
	float swipeStartTime;
	public float maxSwipeTime = 3f; 
	public float minSwipeDist = 2f;
	void Start(){
		StartCoroutine (checkHorizontalSwipes ());
		manage = GameObject.FindWithTag("Control").GetComponent<VectorManagement> ();
	}
	IEnumerator checkHorizontalSwipes () //Coroutine, wich gets Started in "Start()" and runs over the whole game to check for swipes
	{
		while (true) { //Loop. Otherwise we wouldnt check continoulsy ;-)
			foreach (Touch touch in Input.touches) { //For every touch in the Input.touches - array...
				switch (touch.phase) {
				case TouchPhase.Began: //The finger first touched the screen --> It could be(come) a swipe
					couldBeSwipe = true;
					startPos = touch.position;  //Position where the touch started
					swipeStartTime = Time.time; //The time it started
					if (manage.GetComponent<VectorManagement> ().Lost) {
						Application.LoadLevel (Application.loadedLevel);
					}
					break;
				}
				float swipeTime = Time.time - swipeStartTime; //Time the touch stayed at the screen till now.
				float swipeDistX = Mathf.Abs (touch.position.x - startPos.x); //Swipedistance
				float swipeDistY = Mathf.Abs (touch.position.y - startPos.y); //Swipedistance


				if (couldBeSwipe && swipeTime < maxSwipeTime && (swipeDistX > minSwipeDist || swipeDistY > minSwipeDist)) {
					// It's a swiiiiiiiiiiiipe!
					Debug.Log(swipeDistX);
					Debug.Log (swipeDistY);
					couldBeSwipe = false; //<-- Otherwise this part would be called over and over again.
					if (swipeDistX >= 1.5 * swipeDistY) {
						if (Mathf.Sign (touch.position.x - startPos.x) == 1f) { //Swipe-direction, either 1 or -1.

							manage.currMove = VectorManagement.Dir.right;
							Debug.Log ("Right");

						} else {

							manage.currMove = VectorManagement.Dir.left;
							Debug.Log ("Left");
						}
					} else if (swipeDistY >= 1.5 * swipeDistX) {
						if (Mathf.Sign (touch.position.y - startPos.y) == 1f) { //Swipe-direction, either 1 or -1.

							manage.currMove = VectorManagement.Dir.up;
							Debug.Log ("Up");

						} else {

							manage.currMove = VectorManagement.Dir.down;
							Debug.Log ("Down");
						}
					}
				}
			}
			yield return null;
		}
}
}
