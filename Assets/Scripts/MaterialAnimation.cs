using UnityEngine;
using System.Collections;

public class MaterialAnimation : MonoBehaviour {

	public float Speed = .1f;

	private LineRenderer lineRenderer;

	void Start() {
		lineRenderer = GetComponent<LineRenderer>();
	}

	void Update () {
		if (lineRenderer.enabled) {
			float newValue = lineRenderer.material.mainTextureOffset.x + Speed * Time.deltaTime;
			if (newValue < 1f) {
				newValue += 1f;
			}
			lineRenderer.material.mainTextureOffset = new Vector2(newValue, lineRenderer.material.mainTextureOffset.y);
		}
	}
}
