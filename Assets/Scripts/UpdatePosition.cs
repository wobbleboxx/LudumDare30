using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpdatePosition : MonoBehaviour {

	public GameObject targetPlanet;
	public GameObject panel;
	public Text textUnits;
	private RectTransform rectTransform;

	void Start() {
		rectTransform = GetComponent<RectTransform>();
	}

	void Update () {
		if (targetPlanet.renderer.isVisible) {
			rectTransform.position = Camera.main.WorldToScreenPoint(targetPlanet.transform.position);
			textUnits.text = targetPlanet.GetComponent<PlanetProperties>().units.ToString();
			if (!panel.activeSelf) {
				panel.SetActive(true);
			}
		} else {
			if (panel.activeSelf) {
				panel.SetActive(false);
			}
		}
	}
}
