using UnityEngine;
using System.Collections;
using System.Globalization;

/** Handler for Game Options Menu.
 * @author: Ebtissam Wahman
 */ 

public class GameOptions : MonoBehaviour
{	
	private GameObject[] options;
	
	public GameObject MusicButton;
	public GameObject SoundButton;
	private GameObject Back;
	
	private SpriteRenderer charSelect;
	private string[] characterNames = {"David", "Lisa", "Christina","Zane"};
	public Sprite[] characters;
	private int charIndex;
	private int charMaxIndex = 0;
	
	private string musicValue;
	private string soundValue;
	
	
	void Start ()
	{
		Back = GameObject.Find ("Text - Back");
		charSelect = GameObject.Find ("Sprite - Character").GetComponent<SpriteRenderer> ();

		//Enable all Text Options
		TextOption[] textOptions = GameObject.FindObjectsOfType<TextOption> ();
		foreach (TextOption textOption in textOptions) {
			textOption.optionEnabled = true;
		}

		if (!PlayerPrefs.HasKey ("Music")) {
			PlayerPrefs.SetString ("Music", "ON");
		}
		if (!PlayerPrefs.HasKey ("Sound")) {
			PlayerPrefs.SetString ("Sound", "ON");
		}
		
		if (!PlayerPrefs.HasKey ("Character Selected")) { //Set up default character
			PlayerPrefs.SetString ("Character Selected", characterNames [0]);
		} 
		
		//Set the limit based on which characters are unlocked
		for (int i = 0; i < characterNames.Length; i++) {
			if (PlayerPrefs.HasKey (characterNames [i])) {
				if (PlayerPrefs.GetString (characterNames [i]).Equals ("true")) {
					charMaxIndex++;
				}
			} else {
				if (i > 1) {
					PlayerPrefs.SetString (characterNames [i], "false");
				} else {
					PlayerPrefs.SetString (characterNames [i], "true");
					charMaxIndex++;
				}
			}
		}
		
		musicValue = PlayerPrefs.GetString ("Music");
		soundValue = PlayerPrefs.GetString ("Sound");
		
		options = new GameObject[] {MusicButton, SoundButton, charSelect.gameObject, Back};
	}
	
	void Update ()
	{
		musicValue = PlayerPrefs.GetString ("Music");
		if (musicValue.Equals ("ON")) {
			MusicButton.GetComponent<ToggleButton> ().Activate ();
		} else {
			MusicButton.GetComponent<ToggleButton> ().Deactivate ();
		}
		
		soundValue = PlayerPrefs.GetString ("Sound");
		if (soundValue.Equals ("ON")) {
			SoundButton.GetComponent<ToggleButton> ().Activate ();
		} else {
			SoundButton.GetComponent<ToggleButton> ().Deactivate ();
		}

		for (int i = 0; i < charMaxIndex; i++) {
			if (PlayerPrefs.GetString ("Character Selected").Equals (characterNames [i])) {
				charSelect.sprite = characters [i];
				charIndex = i;
			}
		}
		
		foreach (GameObject option in options) {
			if (InputManager.touching && InputManager.pointerTouch.collider.name.Equals (option.name)) {
				if (InputManager.enter) {
					changeValue (option);
				}
			}
		}
		
	}
	
	void changeValue (GameObject option)
	{
		if (option.name.Contains ("Music")) {
			if (musicValue.Equals ("ON")) {
				PlayerPrefs.SetString ("Music", "OFF");
			} else {
				PlayerPrefs.SetString ("Music", "ON");
			}
		} else {
			if (option.name.Contains ("Sound")) {
				if (soundValue.Equals ("ON")) {
					PlayerPrefs.SetString ("Sound", "OFF");
				} else {
					PlayerPrefs.SetString ("Sound", "ON");
				}
			} else {
				if (option.name.Contains ("Character")) {
					if (charIndex == charMaxIndex - 1) {
						PlayerPrefs.SetString ("Character Selected", characterNames [0]);
					} else {
						PlayerPrefs.SetString ("Character Selected", characterNames [charIndex + 1]);
					}
				}
			}
		}
	}
}
