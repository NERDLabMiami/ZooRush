using UnityEngine;
using System.Collections;

/**
 *  Swaps out and intializes Character Grpahics at the start of the level.
 */ 
public class Character : MonoBehaviour
{
	public GameObject speechBubble;
	private TextMesh speechBubbleText;

	public Sprite[] baseSprites;
	public Sprite[] davidFaceSprites;
	public Sprite[] lisaFaceSprites;
	public Sprite[] christinaFaceSprites;
	public Sprite[] zaneFaceSprites;

	private int currentCharacter;
	private string[] characters = {"David", "Lisa", "Christina","Zane"};
	private PlayerControls playerControls;
	public RuntimeAnimatorController[] animatorControllers;

	void Awake ()
	{
		if (PlayerPrefs.HasKey ("Character Selected")) {
			currentCharacter = PlayerPrefs.GetInt ("Character Selected");
		} else {
			currentCharacter = 0;
		}
		Sprite[][] characterFaceIcons = {davidFaceSprites,lisaFaceSprites,christinaFaceSprites,zaneFaceSprites};

		gameObject.GetComponent<SpriteRenderer> ().sprite = baseSprites [currentCharacter];
		GetComponent<Animator> ().runtimeAnimatorController = animatorControllers [currentCharacter];
		playerControls = GetComponent<PlayerControls> ();
		playerControls.characterName = characters [currentCharacter];
		playerControls.faceIcons = characterFaceIcons [currentCharacter];
	}


	void Start ()
	{
		speechBubbleText = speechBubble.GetComponentInChildren<TextMesh> ();
		SpeechBubbleDisplay ("LOL");
//		speechBubble.SetActive (false);
	}

	public void SpeechBubbleDisplay (string text)
	{

		speechBubble.SetActive (true);
		speechBubbleText.text = text;
		speechBubble.GetComponent<Animator> ().SetTrigger ("Hide");
		StartCoroutine (waitForDisappear ());
	}

	private IEnumerator waitForDisappear ()
	{

		while (speechBubbleText.renderer.material.color.a > 0.1f) {
			yield return new WaitForSeconds (0.1f);
		}

		speechBubble.SetActive (false);


	}

}
