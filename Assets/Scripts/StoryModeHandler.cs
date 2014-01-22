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
	private StreamReader fileInput;
	private AnimatedText textAnimator;

	void Start ()
	{
		slideText = new string[3];
		slideIndex = 0;
		currentSlide = Instantiate (slides [slideIndex], new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
		fileInput = new StreamReader (Application.dataPath + "/Resources/Dialog/" + Application.loadedLevelName + ".txt");
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
		if (!fileInput.EndOfStream) {
			numberOfLines = Int32.Parse (fileInput.ReadLine ());
			for (int i = 0; i < numberOfLines; i++) {
				slideText [i] = fileInput.ReadLine ();
			}
		}
		for (int i = 0; i < slideText.Length; i++) {
			Debug.Log (slideText [i]);
		}
		textAnimator.DisplayText (slideText);
	}
}
