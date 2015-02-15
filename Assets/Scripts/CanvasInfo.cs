using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CanvasInfo : MonoBehaviour {

	public Text PointsText;
	public Text MultiplierText;
	public GameObject GameOverScreen;

	void Update () {
		PointsText.text = string.Format("Resources: {0}", GameManager.instance.Points);
		MultiplierText.text = string.Format("Connected worlds: {0}", GameManager.instance.PlanetChain.Count);
	}

	public void ShowGameOverScreen() {
		GameOverScreen.SetActive(true);
	}

	public void RestartGame() {
		Application.LoadLevel(Application.loadedLevelName);
		Time.timeScale = 1f;
	}
		
}
