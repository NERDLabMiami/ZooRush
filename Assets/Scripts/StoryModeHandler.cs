using UnityEngine;
using System;
using System.IO;
using System.Collections;

public class StoryModeHandler : Button
{
	public static string NextSceneName;
	public GameObject[] introSlides;
	public GameObject[] level2Slides;
	public GameObject[] level3Slides;
	public GameObject[] level4Slides;
	public GameObject[] level5Slides;
	public GameObject[] level6Slides;
	public GameObject[] level7Slides;
	public GameObject[] level8Slides;
	public GameObject[] level9Slides;

	private int nextLevel;
	private GameObject[][] slides;
	private GameObject currentSlide;
	private int slideIndex;
	private string[] slideText;
	private StringReader fileInput;
	private AnimatedText textAnimator;


	new void Start ()
	{
		base.Start ();
		slides = new GameObject[][] {
			introSlides,
			level2Slides,
			level3Slides,
			level4Slides,
			level5Slides,
			level6Slides,
			level7Slides,
			level8Slides,
			level9Slides
		};
		string[] sceneTexts = {introScene,level2,level3,level4};
		nextLevel = 1;
		
		switch (NextSceneName) {
		case "Level1-Tutorial":
			nextLevel = 1;
			break;
		case "Level2-Zoo":
			nextLevel = 2;
			break;
		case "Level3-Suburbs":
			nextLevel = 3;
			break;
		case "Level4-Suburbs":
			nextLevel = 4;
			break;
		case "Level5-Suburbs":
			nextLevel = 5;
			break;
		case "Level6-Beach":
			nextLevel = 6;
			break;
		case "Level7-Beach":
			nextLevel = 7;
			break;
		case "Level8-Downtown":
			nextLevel = 8;
			break;
		case "Level9-Downtown":
			nextLevel = 9;
			break;
		default:
			nextLevel = 4;
			break;
		}

		slideText = new string[3];
		slideIndex = 0;

		currentSlide = Instantiate (slides [nextLevel - 1] [slideIndex], new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;

		fileInput = new StringReader (sceneTexts [nextLevel - 1]);
		textAnimator = GameObject.FindObjectOfType<AnimatedText> ();
		readSlideText ();
	}

	protected override void action ()
	{
		if (slideIndex < slides [nextLevel - 1].Length - 1) {
			readSlideText ();
			slideIndex++;
			Destroy (currentSlide);
			currentSlide = Instantiate (slides [nextLevel - 1] [slideIndex], new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
		} else {
			LoadLevel.levelToLoad = NextSceneName;
			Application.LoadLevel ("Loading");
		}
		clicked = false;
	}

	private void readSlideText ()
	{
		//Makes all dialog lines blank first
		for (int i = 0; i < slideText.Length; i++) {
			slideText [i] = "";
		}
		int numberOfLines;
		numberOfLines = Int32.Parse (fileInput.ReadLine ());
		for (int i = 0; i < numberOfLines; i++) {
			slideText [i] = fileInput.ReadLine ();
		}
		textAnimator.DisplayText (slideText);
	}

	public void stopSpeechSprites ()
	{
		Animator[] animators = GameObject.FindObjectsOfType<Animator> ();
		foreach (Animator animator in animators) {
			animator.SetTrigger ("Stop");
		}
	}

	private string introScene = "2\n" +
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

	private string level2 = "1\n" +
		"Good job with the tortoise, but there’s no time to rest.\n" +
		"2\n" +
		"It looks like a crocodile is on the loose inside the Zoo.\n" +
		"We need you to track it down before it hurts anyone!\n" +
		"1\n" +
		"Let's Do It!";

	private string level3 = "1\n" +
		"Good job with the crocodile, but there’s no time to rest.\n" +
		"2\n" + 
		"It looks like a flamingo has escaped into the suburbs.\n" + 
		"We need you to track it down before it hurts itself!\n" +
		"1\n" +
		"Let's Do It!";

	private string level4 = "1\n" +
		"Good job with the flamingo, but there’s not time to rest.\n" +
		"2\n" +
		"There’s a rhino running wild in the park.\n" +
		"Capture it before it’s overrun by squirrels!\n" +
		"1\n" +
		"Let's Do It!";
}
