using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

	public float Speed;

	void Start() {
		Speed = Random.Range(5f, 33f);
	}

	void Update () {
		transform.Rotate(Vector3.up, Speed * Time.deltaTime);
	}
}
