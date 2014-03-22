using UnityEngine;
using System.Collections;

/**
 *  Swaps out and intializes Character Grpahics at the start of the level.
 */ 
public class Character : MonoBehaviour
{
	
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
		changeCharacter ();
	}

	public void changeCharacter ()
	{
		currentCharacter = PlayerPrefs.GetInt ("Character Selected", 0);

		Sprite[][] characterFaceIcons = {davidFaceSprites,lisaFaceSprites,christinaFaceSprites,zaneFaceSprites};
		
		gameObject.GetComponent<SpriteRenderer> ().sprite = baseSprites [currentCharacter];
		GetComponent<Animator> ().runtimeAnimatorController = animatorControllers [currentCharacter];
		playerControls = GetComponent<PlayerControls> ();
		if (playerControls != null) {
			playerControls.characterName = characters [currentCharacter];
			playerControls.faceIcons = characterFaceIcons [currentCharacter];
		}
	}
}
