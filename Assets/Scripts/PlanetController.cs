using UnityEngine;
using System.Collections;

public class PlanetController : MonoBehaviour {

	public Material HighlightMaterial;

	private Material NormalMaterial;
	private bool menuIsUp = false;

	void Start () {
		NormalMaterial = renderer.material;
	}

	void OnMouseEnter() {
		if (!GameManager.instance.isGameOver) {
			if (!gameObject.GetComponent<PlanetProperties>().isActive) {
				Material[] mats = renderer.materials;
				mats[1] = HighlightMaterial;
				renderer.materials = mats;
			}
			if (GameManager.instance.HighlightPlanet != gameObject) {
				GameManager.instance.HighlightPlanet = gameObject;
			}
			GameManager.instance.canvasController.ShowPlanetStats();
		}
	}

	void OnMouseExit() {
		if ((GameManager.instance.SelectedPlanet != gameObject)) {
			GameManager.instance.canvasController.HidePlanetStats();
			if (GameManager.instance.LockedPlanet != gameObject && (!gameObject.GetComponent<PlanetProperties>().isActive)) {
				ClearHighlight();
			}
		}
	}

	void OnMouseUp() {
		if ((GameManager.instance.LockedPlanet != gameObject) && (!gameObject.GetComponent<PlanetProperties>().isActive) && (!GameManager.instance.isGameOver)) {
			if (GameManager.instance.SelectedPlanet != gameObject) {
				if (GameManager.instance.SelectedPlanet != null) {
					GameManager.instance.SelectedPlanet.GetComponent<PlanetController>().ClearHighlight();
				}
				GameManager.instance.SelectedPlanet = gameObject;
				GameManager.instance.soundManager.PlayClip(GameManager.instance.soundManager.PlanetSelect);
			}
			GameManager.instance.canvasController.ShowPlanetMenu();
		}
	}

	public void ClearHighlight ()
	{
		Material[] mats = renderer.materials;
		mats[1] = NormalMaterial;
		renderer.materials = mats;
	}
}
