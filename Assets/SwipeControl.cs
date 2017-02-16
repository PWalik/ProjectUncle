using UnityEngine;
using System.Collections;
//keyboard controller, done in a way that it can be easily swapped for touch controls (throw out this script and get a new one with touch
public class SwipeControl : MonoBehaviour {
	// Values to set:
	public float comfortZone = 70.0f;
	public float minSwipeDist = 14.0f;
	public float maxSwipeTime = 0.5f;
	VectorManagement manage;
	private float startTime;
	private Vector2 startPos;
	private bool couldBeSwipe;
	public VectorManagement.Dir lastSwipe = VectorManagement.Dir.none;
	public float lastSwipeTime;

	void Start () {
		manage = GameObject.FindWithTag("Control").GetComponent<VectorManagement> ();
	}
	void  Update()
	{
		if (Input.touchCount > 0)
		{
			Touch touch = Input.touches[0];

			switch (touch.phase)
			{
			case TouchPhase.Began:
				lastSwipe = VectorManagement.Dir.none;
				lastSwipeTime = 0;
				couldBeSwipe = true;
				startPos = touch.position;
				startTime = Time.time;
				break;

			case TouchPhase.Moved:
				if (Mathf.Abs(touch.position.x - startPos.x) > comfortZone || Mathf.Abs(touch.position.y - startPos.y) > comfortZone)
				{
					Debug.Log("Not a swipe. Swipe strayed " + (int)Mathf.Abs(touch.position.x - startPos.x) +
						"px which is " + (int)(Mathf.Abs(touch.position.x - startPos.x) - comfortZone) +
						"px outside the comfort zone.");
					couldBeSwipe = false;
				}
				break;
			case TouchPhase.Ended:
				if (couldBeSwipe) {
					float swipeTime = Time.time - startTime;
					float swipeDistX = (new Vector3 (0, touch.position.x, 0) - new Vector3 (0, startPos.x, 0)).magnitude;
					float swipeDistY = (new Vector3 (0, touch.position.y, 0) - new Vector3 (0, startPos.y, 0)).magnitude;
					if (swipeDistX > swipeDistY) {
						//Swipe Down or Up
						if (swipeTime < maxSwipeTime && swipeDistX > minSwipeDist) {
							// It's a swiiiiiiiiiiiipe!
							float swipeValueX = Mathf.Sign (touch.position.y - startPos.y);
							float swipeValueY = Mathf.Sign (touch.position.x - startPos.x);
							// If the swipe direction is positive, it was an upward swipe.
							// If the swipe direction is negative, it was a downward swipe.
							if (swipeValueX > 0)
								manage.currMove = VectorManagement.Dir.up;
							else if (swipeValueX < 0)
								manage.currMove = VectorManagement.Dir.down;
							
						} else {
							//Swipe Right or Left
							if (swipeTime < maxSwipeTime && swipeDistY > minSwipeDist) {
								// It's a swiiiiiiiiiiiipe!
								float swipeValueX = Mathf.Sign (touch.position.y - startPos.y);
								float swipeValueY = Mathf.Sign (touch.position.x - startPos.x);
								// If the swipe direction is positive, it was an upward swipe.
								// If the swipe direction is negative, it was a downward swipe.
								if (swipeValueX > 0)
									manage.currMove = VectorManagement.Dir.left;
								else if (swipeValueX < 0)
									manage.currMove = VectorManagement.Dir.right;
							}
						}
					}
				}
					break;
					}
				}
		}
	}
