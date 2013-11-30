using UnityEngine;
using System.Collections;

public class LevelMenu : MonoBehaviour
{

	private GameObject[] levels;
	private GameObject back;
	private string[] levelNames = {"Level 1", "Level 2", "Level 3"};
	private bool[] levelEnabled;

	private int levelsUnlocked;

	void Start ()
	{
		levels = GameObject.FindGameObjectsWithTag ("option");
		levelsUnlocked = 0;
		back = GameObject.Find ("Text - Back");
		back.GetComponent<TextOption> ().optionEnabled = true;
		levelEnabled = new bool[levelNames.Length];
		if (!PlayerPrefs.HasKey ("Level 1")) {
			PlayerPrefs.SetString ("Level 1", "true");
			levelsUnlocked++;
		}

		for (int i = 0; i < levelNames.Length; i++) {
			if (PlayerPrefs.HasKey (levelNames [i])) {
				levelEnabled [i] = PlayerPrefs.GetString (levelNames [i]).Equals ("true");
				if (levelEnabled [i]) {
					levelsUnlocked++;
				}
			} else {
				levelEnabled [i] = false;
			}
			foreach (GameObject level in levels) {
				if (level.name.Contains (levelNames [i])) {
					if (levelEnabled [i]) {
						enableLevel (level);
					} else {
						disableLevel (level);
					}
				}
			}
		}
		PlayerPrefs.SetInt ("Levels Unlocked", levelsUnlocked);
	}

	private void enableLevel (GameObject level)
	{
		level.GetComponent<SpriteRenderer> ().color = Color.white;
		level.GetComponentInChildren<TextMesh> ().color = Color.white;
		level.GetComponentInChildren<TextOption> ().optionEnabled = true;
	}

	private void disableLevel (GameObject level)
	{
		level.GetComponent<SpriteRenderer> ().color = Color.grey;
		level.GetComponentInChildren<TextMesh> ().color = Color.grey;
		level.GetComponentInChildren<TextOption> ().optionEnabled = false;
	}
}
