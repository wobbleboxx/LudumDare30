using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeScale : MonoBehaviour {

	public Text TimeScaleText;
	public Slider TimeScaleSlider;

	public void UpdateTimeScaleText(float value) {
		TimeScaleText.text = string.Format("{0}X", value);
		Time.timeScale = value;
	}

}
