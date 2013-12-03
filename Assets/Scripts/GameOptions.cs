using UnityEngine;
using System.Collections;
using System.Globalization;


public class GameOptions : MonoBehaviour
{	
	private GameObject[] options;
	
	private GameObject Music;
	private GameObject Sound;
	private GameObject Back;
	
	private SpriteRenderer charSelect;
	private string[] characterNames = {"Main Boy", "Main Girl", "Hispanic Girl"};
	public Sprite[] characters;
	private int charIndex;
	private int charMaxIndex = 0;
	
	private string musicValue;
	private string soundValue;
	
	
	void Start ()
	{
		Music = GameObject.Find ("Text - Music");
		Sound = GameObject.Find ("Text - Sound");
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
		
		options = new GameObject[] {Music, Sound,/* VolumeSlider,*/ charSelect.gameObject, Back};
	}
	
	void Update ()
	{
		musicValue = PlayerPrefs.GetString ("Music");
		Music.GetComponent<TextMesh> ().text = "Music:   " + musicValue.Substring (0, 1) + musicValue.Substring (1).ToLower ();
		
		soundValue = PlayerPrefs.GetString ("Sound");
		Sound.GetComponent<TextMesh> ().text = "Sound:   " + soundValue.Substring (0, 1) + soundValue.Substring (1).ToLower ();
		
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
				} else {
//					if (option.name.Contains ("Slider")) {
//					}
				}
			}
		}
	}
}
