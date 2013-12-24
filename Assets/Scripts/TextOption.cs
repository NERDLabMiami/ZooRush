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
	public string levelName;
	public int levelNum;
	void Start ()
	{
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
				if (gameObject.name.Contains ("Retry")) {
					Application.LoadLevel (Application.loadedLevelName);
				} else {
					if (gameObject.name.Contains ("Quit") || gameObject.name.Contains ("Main")) {
						Application.LoadLevel ("Splash");
					} else {
						if (gameObject.name.Contains ("Next")) {
							Application.LoadLevel (levelName);
						} 
					}
				}
				
			} else {
				if (gameObject.name.Contains ("Play")) {
					if (PlayerPrefs.HasKey ("Levels Unlocked")) {
						if (PlayerPrefs.GetInt ("Levels Unlocked") > 1) {
							Application.LoadLevel ("Level Select");
						} else {
							StoryModeHandler.NextSceneName = "LevelFrame";
							Application.LoadLevel ("IntroScene");
						}
					} else {
						StoryModeHandler.NextSceneName = "LevelFrame";
						Application.LoadLevel ("IntroScene");
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
