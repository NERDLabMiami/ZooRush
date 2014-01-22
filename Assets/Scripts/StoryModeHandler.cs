using UnityEngine;
using System;
using System.IO;
using System.Collections;

public class StoryModeHandler : MonoBehaviour
{
	public static string NextSceneName;
	public GameObject[] slides;
	private GameObject currentSlide;
	private int slideIndex;
	private string[] slideText;
	private StringReader fileInput;
	private AnimatedText textAnimator;

	void Start ()
	{
		slideText = new string[3];
		slideIndex = 0;
		currentSlide = Instantiate (slides [slideIndex], new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;

		fileInput = new StringReader (textStuff);
		textAnimator = GameObject.FindObjectOfType<AnimatedText> ();
		readSlideText ();
	}
	
	void Update ()
	{
		if (InputManager.enter) {
			if (slideIndex < slides.Length - 1) {
				readSlideText ();
				slideIndex++;
				Destroy (currentSlide);
				currentSlide = Instantiate (slides [slideIndex], new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
			} else {
				LoadLevel.levelToLoad = NextSceneName;
				Application.LoadLevel ("Loading");
			}
		}
	}

	private void readSlideText ()
	{
		for (int i = 0; i < slideText.Length; i++) {
			slideText [i] = "";
		}
		int numberOfLines;
//		if (!fileInput.EndOfStream) {
		numberOfLines = Int32.Parse (fileInput.ReadLine ());
		for (int i = 0; i < numberOfLines; i++) {
			slideText [i] = fileInput.ReadLine ();
		}
//		}
		for (int i = 0; i < slideText.Length; i++) {
			Debug.Log (slideText [i]);
		}
		textAnimator.DisplayText (slideText);
	}


	private string textStuff = "2\n" +
		"Today is a big day.\n" +
		"I get to start my dream job as a Zoo Keeper!\n" +
		"3\n" +
		"I may have struggled all my life with sickle-cell anemia,\n" +
		"but by paying attention to my body,\n" +
		"I have learned to live with it...\n" +
		"1\n" +
		"And I certainly won't let it slow me down today!\n" +
		"1\n" +
		"...\n" +
		"3\n" +
		"Help! Help! \n" +
		"All the animals have escaped their pens. \n" +
		"They’re getting away!\n" +
		"1\n" +
		"The first day of work is always the hardest.\n";

}
