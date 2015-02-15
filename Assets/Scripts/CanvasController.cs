using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour {

	public RectTransform PlanetStatsPanel;
	public bool showPlanetStats = false;
	public Text NameText;
	public Text UnitsText;
	public Text IncrementText;
	public Text ActiveText;
	public Text Deadtext;

	public RectTransform PlanetMenuPanel;
	public bool showPlanetMenu = false;

	public Material LockedMaterial;
	public Material LockedConnectionMaterial;

	void Update () {
		
		if (showPlanetStats && (GameManager.instance.HighlightPlanet != null) && GameManager.instance.HighlightPlanet.renderer.isVisible) {
			if (!PlanetStatsPanel.gameObject.activeSelf) {
				PlanetStatsPanel.gameObject.SetActive(true);
			}
			PlanetStatsPanel.position = Camera.main.WorldToScreenPoint(GameManager.instance.HighlightPlanet.transform.position);
			UpdateContent();
		} else {
			if (PlanetStatsPanel.gameObject.activeSelf) {
				PlanetStatsPanel.gameObject.SetActive(false);
			}
		}
		
		if (showPlanetMenu && (GameManager.instance.SelectedPlanet != null) && GameManager.instance.SelectedPlanet.renderer.isVisible) {
			if (!PlanetMenuPanel.gameObject.activeSelf) {
				PlanetMenuPanel.gameObject.SetActive(true);
			}
			PlanetMenuPanel.position = Camera.main.WorldToScreenPoint(GameManager.instance.SelectedPlanet.transform.position);
		} else {
			if (PlanetMenuPanel.gameObject.activeSelf) {
				PlanetMenuPanel.gameObject.SetActive(false);
			}
		}

	}
	
	public void UpdateContent() {
		PlanetProperties planetProps = GameManager.instance.HighlightPlanet.GetComponent<PlanetProperties>();
		NameText.text = string.Format("World: {0}", planetProps.planetName);
		UnitsText.text = string.Format("Available resources: {0}", planetProps.units);
		IncrementText.text = string.Format("Decreases every {0}second{1}", (planetProps.incrementEverySeconds > 1) ? planetProps.incrementEverySeconds.ToString() + " " : "", (planetProps.incrementEverySeconds > 1) ? "s" : "");
		ActiveText.text = string.Format("Used: {0}", (planetProps.isActive) ? "Yes" : "No");
		Deadtext.text = string.Format("Resources left: {0}", (planetProps.isDead) ? "No" : "Yes");
	} 

	public void ShowPlanetMenu() {
		GameManager.instance.canvasController.showPlanetMenu = true;
		
		ConnectPlanet();
	}
	public void HidePlanetMenu() {
		GameManager.instance.canvasController.showPlanetMenu = false;
		GameManager.instance.SelectedPlanet.GetComponent<PlanetController>().ClearHighlight();
		GameManager.instance.SelectedPlanet = null;
		GameManager.instance.HighlightPlanet = null;
		GameManager.instance.LockedPlanet.GetComponent<LineRenderer>().enabled = false;
	}
	public void ShowPlanetStats() {
		GameManager.instance.canvasController.showPlanetStats = true;
	}
	public void HidePlanetStats() {
		if (showPlanetMenu) {
			GameManager.instance.HighlightPlanet = GameManager.instance.SelectedPlanet;
		} else {
			GameManager.instance.HighlightPlanet = null;
			GameManager.instance.canvasController.showPlanetStats = false;
		}
	}
	
	public void ConnectPlanet() {
		LineRenderer lr = GameManager.instance.LockedPlanet.GetComponent<LineRenderer>();
		lr.SetPosition(0, GameManager.instance.LockedPlanet.transform.position);
		lr.SetPosition(1, GameManager.instance.SelectedPlanet.transform.position);
		Material lineMat = lr.material;
		lineMat.mainTextureScale = new Vector2(Vector3.Distance(GameManager.instance.LockedPlanet.transform.position, GameManager.instance.SelectedPlanet.transform.position) * .3f, 1);
		lr.material = lineMat;
		lr.enabled = true;
	}
	public void LockPlanet() {

		LineRenderer lr = GameManager.instance.LockedPlanet.GetComponent<LineRenderer>();
		lr.material.CopyPropertiesFromMaterial(LockedConnectionMaterial);
		lr.material.mainTextureScale = new Vector2(Vector3.Distance(GameManager.instance.LockedPlanet.transform.position, GameManager.instance.SelectedPlanet.transform.position) * .3f, 1);
		//lr.material = LockedConnectionMaterial;

		GameManager.instance.LockedPlanet = GameManager.instance.SelectedPlanet;
		PlanetProperties planetProps = GameManager.instance.LockedPlanet.GetComponent<PlanetProperties>();
		planetProps.isActive = true;
		planetProps.ParticlesActive.SetActive(true);
		planetProps.ParticlesInActive.particleSystem.enableEmission = false;
		GameManager.instance.LockedPlanet.light.color = planetProps.ActiveColor;

		Material[] mats = GameManager.instance.LockedPlanet.renderer.materials;
		mats[1] = LockedMaterial;
		GameManager.instance.LockedPlanet.renderer.materials = mats;

		GameManager.instance.SelectedPlanet = null;

		GameManager.instance.canvasController.HidePlanetStats();

		GameManager.instance.PlanetChain.Add(GameManager.instance.LockedPlanet);
	}
}
