using UnityEngine;
using System.Collections;

/** Handles options for the level Menu.
 * @author: Ebtissam Wahman
 */ 
public class LevelMenu : MonoBehaviour
{
	private LevelOption[] levels;
	private GameObject back;
	private bool[] levelEnabled;

	private int levelsUnlocked;
	private int currentFocused;
	private int previousLevel;
	
	void Start ()
	{
		levels = new LevelOption[] {
			GameObject.Find ("Level - Tortoise").GetComponent<LevelOption> (),
			GameObject.Find ("Level - Crocodile").GetComponent<LevelOption> (),
			GameObject.Find ("Level - Flamingo").GetComponent<LevelOption> (),
			GameObject.Find ("Level - Gorilla").GetComponent<LevelOption> ()
		};
		
		levelsUnlocked = 0;
		back = GameObject.Find ("Text - Back");
		back.GetComponent<TextOption> ().optionEnabled = true;
		levelEnabled = new bool[levels.Length];
		
		foreach (LevelOption level in levels) {
			if (PlayerPrefs.HasKey (level.SceneName)) {
				if (PlayerPrefs.GetInt (level.SceneName) > 0) {
					levelsUnlocked++;
				}
			}
		}
		
		if (PlayerPrefs.HasKey ("Previous Level Played")) {
			previousLevel = PlayerPrefs.GetInt ("Previous Level Played");
		} else {
			previousLevel = 0;
			PlayerPrefs.SetInt ("Previous Level Played", previousLevel);
		}
		
		currentFocused = previousLevel;
	}
	
	void Update ()
	{	
		if (InputManager.left) {
			if (currentFocused > 0) {
				currentFocused--;
			}
		} else { 
			if (InputManager.right) {
				if (currentFocused < levelsUnlocked) {
					currentFocused++;
				}
			}
		}
		Camera.main.transform.position = Vector3.Lerp (Camera.main.transform.position,
		                                        new Vector3 (levels [currentFocused].transform.localPosition.x, 
		             										Camera.main.transform.position.y, 
		             										Camera.main.transform.position.z),
		                                        3f * Time.deltaTime);
	}
}
