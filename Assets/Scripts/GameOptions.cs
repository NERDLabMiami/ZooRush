using UnityEngine;
using System.Collections;
using System.Globalization;


public class GameOptions : MonoBehaviour
{	
	private GameObject[] options;
	
	private GameObject Music;
	private GameObject Sound;
	private GameObject Volume;
	private GameObject Back;
	
	private SpriteRenderer charSelect;
	private string[] characterNames = {"Main Boy", "Main Girl", "Hispanic Girl"};
	public Sprite[] characters;
	private int charIndex;
	
	private string musicValue;
	private string soundValue;
	
	
	void Start ()
	{
		Music = GameObject.Find ("Text - Music");
		Sound = GameObject.Find ("Text - Sound");
		Volume = GameObject.Find ("Text - Volume");
		Back = GameObject.Find ("Text - Back");
		charSelect = GameObject.FindObjectOfType<SpriteRenderer> ();
		
		if (!PlayerPrefs.HasKey ("Music")) {
			PlayerPrefs.SetString ("Music", "ON");
		}
		if (!PlayerPrefs.HasKey ("Sound")) {
			PlayerPrefs.SetString ("Sound", "ON");
		}
		
		if (!PlayerPrefs.HasKey ("Character Selected")) {
			PlayerPrefs.SetString ("Character Selected", "Main Boy");
		}
		
		musicValue = PlayerPrefs.GetString ("Music");
		soundValue = PlayerPrefs.GetString ("Sound");
		
		options = new GameObject[] {Music, Sound, Volume, charSelect.gameObject, Back};
	}
	
	void Update ()
	{
		musicValue = PlayerPrefs.GetString ("Music");
		Music.GetComponent<TextMesh> ().text = "Music:   " + musicValue.Substring (0, 1) + musicValue.Substring (1).ToLower ();
		
		soundValue = PlayerPrefs.GetString ("Sound");
		Sound.GetComponent<TextMesh> ().text = "Sound:   " + soundValue.Substring (0, 1) + soundValue.Substring (1).ToLower ();
		
		for (int i = 0; i < characterNames.Length; i++) {
			if (PlayerPrefs.GetString ("Character Selected").Equals (characterNames [i])) {
				charSelect.sprite = characters [i];
				charIndex = i;
			}
		}
		
		foreach (GameObject option in options) {
			if (InputManager.touching && InputManager.pointerTouch.collider.name.Equals (option.name)) {
				Debug.Log (option.name);
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
					if (charIndex == characterNames.Length - 1) {
						PlayerPrefs.SetString ("Character Selected", characterNames [0]);
					} else {
						PlayerPrefs.SetString ("Character Selected", characterNames [charIndex + 1]);
					}
				} else {
					if (option.name.Contains ("Back")) {
						Application.LoadLevel ("Splash");
					}
				}
			}
		}
	}
}
