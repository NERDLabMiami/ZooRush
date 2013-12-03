using UnityEngine;
using System.Collections;

/** Handles visuals for text in options.
 * @author: Ebtissam Wahman
 */ 
public class TextOption : MonoBehaviour
{
	private Color optionUnselected = Color.white;
	private Color optionSelected = Color.yellow;
	public bool optionEnabled;
	public bool isLevelOption;
	public int levelNum;

	void Start ()
	{
		enabled = false;
	}

	void OnMouseEnter ()
	{	
		if (optionEnabled) {
			GetComponent<TextMesh> ().color = optionSelected;
		}

	}

	void OnMouseExit ()
	{
		if (optionEnabled) {
			GetComponent<TextMesh> ().color = optionUnselected;
		}
	}

	void OnMouseUp ()
	{
		if (optionEnabled) {
			if (isLevelOption) {
				switch (levelNum) {
				case 1:
					Application.LoadLevel ("LevelFrame");
					break;
				default:
					break;
				}
			} else {
				if (gameObject.name.Contains ("Play")) {
					if (PlayerPrefs.HasKey ("Levels Unlocked")) {
						if (PlayerPrefs.GetInt ("Levels Unlocked") > 1) {
							Application.LoadLevel ("Level Select");
						} else {
							if (!PlayerPrefs.HasKey ("Intro Scene")) {
								PlayerPrefs.SetString ("Intro Scene", "false");
							}
							if (PlayerPrefs.GetString ("Intro Scene").Equals ("false")) {
								Application.LoadLevel ("IntroScene");
							} else {
								Application.LoadLevel ("LevelFrame"); //TODO Must change this to level 1
							}
						}
					} else {
						PlayerPrefs.SetInt ("Levels Unlocked", 1);
						Application.LoadLevel ("LevelFrame"); //TODO Must change this to level 1
					}
				}

				if (gameObject.name.Contains ("Options")) {
					PlayerPrefs.SetString ("Last Scene", Application.loadedLevelName);
					Application.LoadLevel ("Game Options");
				}

				if (gameObject.name.Contains ("Back")) {
					if (PlayerPrefs.HasKey ("Last Scene") && !PlayerPrefs.GetString ("Last Scene").Equals (Application.loadedLevelName)) {
						Application.LoadLevel (PlayerPrefs.GetString ("Last Scene"));
					} else {
						Application.LoadLevel ("Splash");
					}
				}
			}
		}

	}
	
}
