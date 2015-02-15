using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager instance;

	public int Points = 0;

	public PlayerController playerController;
	public CanvasController canvasController;
	public SoundManager soundManager;
	public ScoreManager scoreManager;
	public CanvasInfo canvasInfo;
	public Slider TimeScaleSlider;
	public GameObject PlanetPrefab;
	public GameObject PlanetCanvasContainer;
	public GameObject PlanetCanvasPrefab;
	public int maxPlanetNum = 10;
	public float minPlanetRadius = 100f;
	public float maxPlanetRadius = 500f;

	public GameObject SelectedPlanet;
	public GameObject HighlightPlanet;
	public GameObject LockedPlanet;

	public List<GameObject> PlanetChain;
	public GameObject Home;

	public bool isGameOver = false;

	void Start () {
		if (instance == null) {
			instance = this;
		}

		GeneratePlanets();
		PlanetChain.Add(Home);
		
		TimeScaleSlider.interactable = true;
	}

	void Update() {
		if (!isGameOver) {
			if (PlanetChain.Count > 0) {
				if (PlanetChain[0].GetComponent<PlanetProperties>().isDead) {
					PlanetChain[0].GetComponent<LineRenderer>().enabled = false;
					PlanetChain.RemoveAt(0);
				}
			} else {
				isGameOver = true;
				scoreManager.SaveHighScore();
				scoreManager.updateScoreList();
				canvasInfo.ShowGameOverScreen();
				Time.timeScale = 0f;
				TimeScaleSlider.interactable = false;
			}
		}
	}

	public void GeneratePlanets() {
		for (int i = 0; i < maxPlanetNum; i++) {
			GameObject go = (GameObject)Instantiate(PlanetPrefab, Random.onUnitSphere * Random.Range(minPlanetRadius, maxPlanetRadius), Random.rotation);
			PlanetProperties planetProperties = go.GetComponent<PlanetProperties>();
			planetProperties.planetName = GeneratePlanetName();
			planetProperties.incrementEverySeconds = Random.Range(1, 10);
			
			GameObject planetCanvas = (GameObject)Instantiate(PlanetCanvasPrefab, Camera.main.WorldToScreenPoint(go.transform.position), Quaternion.identity);
			planetCanvas.transform.parent = PlanetCanvasContainer.transform;
			planetCanvas.GetComponent<UpdatePosition>().targetPlanet = go;
		}
	}

	string GeneratePlanetName ()
	{
		string name = string.Empty;
		for (int i = 1; i <= 2; i++) {
			name += GetRandomChar();
		}
		name += "-";
		for (int i = 1; i <= 2; i++) {
			name += GetRanomDigit();
		}
		name += GetRandomChar().ToLower();
		return name;
	}
	string GetRandomChar() {
		return char.ConvertFromUtf32(Random.Range(65, 91));
	}
	string GetRanomDigit() {
		return char.ConvertFromUtf32(Random.Range(48, 58));
	}
}
