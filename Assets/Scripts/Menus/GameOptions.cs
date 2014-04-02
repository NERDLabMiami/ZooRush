using UnityEngine;
using System.Collections;
using System.Globalization;

/** Handler for Game Options Menu.
 * @author: Ebtissam Wahman
 */ 

public class GameOptions : OtherButtonClass
{	
	private GameObject[] options;

	public Transform characterCamera;
	public Transform character;
	public GameObject MusicButton;
	public GameObject SoundButton;
	public GameObject leftChar;
	public GameObject rightChar;

	public Slider sensitivityBar;

	private GameObject Back;
	
	private SpriteRenderer charSelect;
	public TextMesh charSelectName;
	private string[] characterNames = {"David", "Lisa", "Christina","Zane"};
	public Sprite[] characters;
	private int charIndex;
	private int charMaxIndex;
	
	private bool musicValue;
	private bool soundValue;
	private float sensitivity;

	public bool touching;
	private Ray origin;
	public RaycastHit pointerTouch;

	void Awake ()
	{
		sensitivity = PlayerPrefs.GetFloat ("Sensitivity", 1);

	}
	
	void Start ()
	{
		GameState.currentState = GameState.States.Play;

		Back = GameObject.Find ("Text - Back");
		charSelect = GameObject.Find ("Sprite - Character").GetComponent<SpriteRenderer> ();

		if (!PlayerPrefs.HasKey ("Music")) {
			PlayerPrefs.SetInt ("Music", 1);
		}
		if (!PlayerPrefs.HasKey ("Sound")) {
			PlayerPrefs.SetInt ("Sound", 1);
		}
		if (!PlayerPrefs.HasKey ("Sensitivity")) {
			PlayerPrefs.SetFloat ("Sensitivity", 1);
		}
		
		if (!PlayerPrefs.HasKey ("Character Selected")) { //Set up default character
			PlayerPrefs.SetInt ("Character Selected", 0);
		}

		charMaxIndex = 1; // 0 for david, 1 for lisa
		//Set Characters as available in player prefs
		PlayerPrefs.SetInt (characterNames [0], 1); 
		PlayerPrefs.SetInt (characterNames [1], 1);

		//Set the limit based on which characters are unlocked
		for (int i = 2; i < characterNames.Length; i++) {
			if (PlayerPrefs.HasKey (characterNames [i])) {
				if (PlayerPrefs.GetInt (characterNames [i], 0) > 0) {
					charMaxIndex++;
				}
			}
		}
		
		musicValue = (PlayerPrefs.GetInt ("Music") == 1);
		soundValue = (PlayerPrefs.GetInt ("Sound") == 1);



		if (musicValue) {
			MusicButton.GetComponent<ToggleButtonOld> ().Activate ();
		} else {
			MusicButton.GetComponent<ToggleButtonOld> ().Deactivate ();
		}
		
		if (soundValue) {
			SoundButton.GetComponent<ToggleButtonOld> ().Activate ();
		} else {
			SoundButton.GetComponent<ToggleButtonOld> ().Deactivate ();
		}
		
		options = new GameObject[] {MusicButton, SoundButton, leftChar, rightChar, Back};
	}

	void FixedUpdate ()
	{
		if (PlayerPrefs.GetInt ("Music") != 0) {
			audio.mute = false;
		} else {
			audio.mute = true;
		}
		musicValue = (PlayerPrefs.GetInt ("Music") == 1);
		soundValue = (PlayerPrefs.GetInt ("Sound") == 1);
		sensitivity = sensitivityBar.GetSliderPercent ();

		PlayerPrefs.SetInt ("Music", MusicButton.GetComponent<ToggleButtonOld> ().activated ? 1 : 0);
		PlayerPrefs.SetInt ("Sound", SoundButton.GetComponent<ToggleButtonOld> ().activated ? 1 : 0);
		PlayerPrefs.SetFloat ("Sensitivity", sensitivity);

	}

	void Update ()
	{
		if (Input.touchCount > 0) {
			origin = camera.ScreenPointToRay (Input.GetTouch (0).position);
		} else {
			origin = camera.ScreenPointToRay (Input.mousePosition);
		}
		
		touching = Physics.Raycast (origin, out pointerTouch);


		for (int i = 0; i <= charMaxIndex; i++) {
			if (PlayerPrefs.GetInt ("Character Selected", 0) == i) {
				charSelect.sprite = characters [i];
				charSelectName.text = characterNames [i];
				charIndex = i;
			}
		}
		
		foreach (GameObject option in options) {
			if (touching && pointerTouch.collider.gameObject == option) {
				if (Input.GetMouseButtonUp (0)) {
					changeValue (option);
				}
			}
		}

		characterCamera.position = new Vector3 (character.position.x, characterCamera.position.y, characterCamera.position.z);
	}
	
	void changeValue (GameObject option)
	{
		if (option == leftChar) {
			if (charIndex > 0) {
				PlayerPrefs.SetInt ("Character Selected", charIndex - 1);
				GameObject.FindObjectOfType<Character> ().changeCharacter ();
			}
			return;
		}

		if (option == rightChar) {
			if (charIndex < charMaxIndex) {
				PlayerPrefs.SetInt ("Character Selected", charIndex + 1);
				GameObject.FindObjectOfType<Character> ().changeCharacter ();
			}
			return;
		}
	}

	public override void otherButtonAction (Button thisButton)
	{
		throw new System.NotImplementedException ();
	}
}
