using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float touchSpeed = 1f;

	void Update () {
		if (!GameManager.instance.isGameOver) {
			transform.RotateAround(Vector3.zero, Camera.main.transform.up, Input.GetAxis("Horizontal"));
			transform.RotateAround(Vector3.zero, -Camera.main.transform.right, Input.GetAxis("Vertical"));
			transform.RotateAround(Vector3.zero, -Camera.main.transform.forward, Input.GetAxis("Roll"));

			if (Input.touches.Length > 0) {
				if (Input.touches[0].phase == TouchPhase.Moved) {
					transform.RotateAround(Vector3.zero, -Camera.main.transform.up, Input.touches[0].deltaPosition.x * touchSpeed * Time.deltaTime);
					transform.RotateAround(Vector3.zero, Camera.main.transform.right, Input.touches[0].deltaPosition.y * touchSpeed * Time.deltaTime);
				}
			}
		}
	}

}
