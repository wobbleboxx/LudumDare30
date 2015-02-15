using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	public AudioClip PlanetDies;
	public AudioClip PlanetSelect;

	public void PlayClip(AudioClip audioClip) {
		audio.clip = audioClip;
		audio.Play();
	}
}
