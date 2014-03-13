using UnityEngine;
using System.Collections;

public class CharacterSelect : MonoBehaviour
{
	private GameObject[] options;
	public GameObject leftChar;
	public GameObject rightChar;

	private SpriteRenderer charSelect;
	public TextMesh charSelectName;
	private string[] characterNames = {"David", "Lisa", "Christina","Zane"};
	private int charMaxIndex;
	public Sprite[] characters;
	private int charIndex;

	public bool touching;
	private Ray origin;
	public RaycastHit pointerTouch;

	void Start ()
	{
		Input.simulateMouseWithTouches = true;
		options = new GameObject[]{leftChar,rightChar};
		charSelect = GameObject.Find ("Sprite - Character").GetComponent<SpriteRenderer> ();
		charMaxIndex = 1; // 0 for david, 1 for lisa
		charIndex = 0;

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
	}
	
	// Update is called once per frame
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
	}

	void changeValue (GameObject option)
	{
		if (option == leftChar) {
			if (charIndex > 0) {
				PlayerPrefs.SetInt ("Character Selected", charIndex - 1);
			}
			return;
		}
		
		if (option == rightChar) {
			if (charIndex < charMaxIndex) {
				PlayerPrefs.SetInt ("Character Selected", charIndex + 1);
			}
			return;
		}
	}
}
