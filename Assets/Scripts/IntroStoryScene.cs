using UnityEngine;
using System.Collections;

/** Handles text and sequencing for the introduction story mode.
 * @author: Ebtissam Wahman
 */ 

public class IntroStoryScene : MonoBehaviour
{
	private TextMesh dialogText;
	private GameObject[] slides;
	private int dialogIndex;
	private int currentSlide;

	void Start ()
	{
		dialogText = GameObject.FindObjectOfType<TextMesh> ();
		slides = GameObject.FindGameObjectsWithTag ("storyboard");
		currentSlide = 0;
		dialogIndex = 0;
		dialog = new string[][] {slide0Dialog,slide1Dialog,slide2Dialog,slide3Dialog,slide4Dialog};
		dialogText.text = dialog [currentSlide] [dialogIndex];
		foreach (GameObject slide in slides) {
			if (slide.name.Contains ("" + currentSlide)) {
				slide.SetActive (true);
			} else {
				slide.SetActive (false);
			}
		}
	}
	
	void FixedUpdate ()
	{
		Debug.Log (dialogIndex);
		Debug.Log (currentSlide);
		Debug.Log (slides.Length);
		if (InputManager.enter) {
			goNext ();
		}

		foreach (GameObject slide in slides) {
			if (slide.name.Contains ("" + currentSlide)) {
				slide.SetActive (true);
			} else {
				slide.SetActive (false);
			}
		}
	}

	private string[] slide0Dialog = { 
		"Today is a big day.\nI get to start my dream job!"
	};
	private string[] slide1Dialog = { 
		"I may have struggled all my life with sickle-cell\nanemia, but by paying attention to my body,\nI have learned to live with it...", "And I certainly won’t let it slow me down today!"
	};
	private string[] slide2Dialog = { 
		"..."
	};
	private string[] slide3Dialog = { 
		"Help! Help!\nAll the animals have escaped their pens.\nThey’re getting away!"
	};
	private string[] slide4Dialog = { 
		"The first day of work is always the hardest."
	};
	private string[][] dialog;

	private void goNext ()
	{
		if (dialogIndex < dialog [currentSlide].Length - 1) {
			dialogIndex++;
		} else {
			if (currentSlide < slides.Length - 1) {
				currentSlide++;
				dialogIndex = 0;
			} else {
				reachedEndOfScene ();
			}
		}

		dialogText.text = dialog [currentSlide] [dialogIndex];

	}

	private void reachedEndOfScene ()
	{
		//TODO Make scene and level dependent
		PlayerPrefs.SetString ("Intro Scene", "true");
		LoadLevel.levelToLoad = "LevelFrame";
		Application.LoadLevel ("Loading");
	}
}
