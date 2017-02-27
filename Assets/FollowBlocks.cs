using UnityEngine;
using System.Collections;

public class FollowBlocks : MonoBehaviour {
	public enum Movement {none, left, right, up};
	//determine 2 things - what block to follow, and is it starting block (jump up), or move to the right/left
	public Movement move = Movement.none;
	Vector3 movem, left;
	bool start = false;
	// Update is called once per frame
	void Update () {
		Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, transform.position.y, Camera.main.transform.position.z);
		//WIP, gonna be an animation and a courutine, for a nice effect.
		if (move != Movement.none) {
				switch (move) {
				case Movement.left:
					movem += new Vector3 (-1, 0, 0);
					break;
				case Movement.right:
					movem += new Vector3 (1, 0, 0);
					break;
				case Movement.up:
					movem += new Vector3 (0, 1, 0);


					break;
				}
			move = Movement.none;

			}
		if (movem != new Vector3 (0, 0, 0)) {
			//very poorly written, but works.
			transform.position += movem/10;
			movem -= movem/10;
		}
	}
}
