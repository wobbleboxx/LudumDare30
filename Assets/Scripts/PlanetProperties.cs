using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlanetProperties : MonoBehaviour {

	public string planetName = "";
	public int units = 0;
	public float incrementEverySeconds = 1f;
	public bool isActive = false;
	public bool isDead = false;
	public float addPointsEverySeconds = 1f;

	public Material DeadPlanet;
	
	public GameObject ParticlesActive;
	public GameObject ParticlesInActive;

	public Color ActiveColor;

	private float lastIncrement = 0f;
	private float lastPoints = 0f;

	void Update() {
		if (!isDead) {
			if (Time.time > (lastIncrement + incrementEverySeconds)) {
				lastIncrement = Time.time;
				
				if (isActive) {
					if (units > 0) {
						units--;
					} else {
						isDead = true;
						Material[] mats = renderer.materials;
						mats[1] = DeadPlanet;
						renderer.materials = mats;
						ParticlesActive.SetActive(false);
						ParticlesInActive.SetActive(false);
						light.enabled = false;
						
						GameManager.instance.soundManager.PlayClip(GameManager.instance.soundManager.PlanetDies);
					}
				}
			}
			
			if (Time.time > (lastPoints + addPointsEverySeconds)) {
				lastPoints = Time.time;
				if (isActive) {
					GameManager.instance.Points += GameManager.instance.PlanetChain.Count;
				}
				if (!isActive) {
					units++;
				}
			}
		}
	}

}
