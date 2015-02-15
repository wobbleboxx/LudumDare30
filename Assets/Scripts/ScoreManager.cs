using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	const string PREF_HIGHSCORES = "Highscores";

	public Text TextScores;
	public List<string> ScoreList;

	void Start() {

		ScoreList = new List<string>(PlayerPrefs.GetString(PREF_HIGHSCORES, "").Split(new char[] { ';' }, System.StringSplitOptions.RemoveEmptyEntries));

	}

	public void SaveHighScore() {
		ScoreList.Add(GameManager.instance.Points.ToString());
		ScoreList.Sort(delegate(string x, string y) {
			if (int.Parse(x) > int.Parse(y)) {
				return -1;
			} else if (int.Parse(x) < int.Parse(y)) {
				return 1;
			} else {
				return 0;
			}
		});

		PlayerPrefs.SetString(PREF_HIGHSCORES, string.Join(";", ScoreList.ToArray()));

		PlayerPrefs.Save();

	}

	public void ClearHighScores() {

		ScoreList.Clear();

		updateScoreList();

		PlayerPrefs.DeleteAll();
		PlayerPrefs.Save();

	}

	public void updateScoreList() {

		TextScores.text = string.Join(System.Environment.NewLine, ScoreList.GetRange(0, Mathf.Min(3, ScoreList.Count)).ToArray());

	}

}
