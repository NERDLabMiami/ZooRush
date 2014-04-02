using UnityEngine;
using System;
using System.IO;
using System.Collections;

public class StoryModeHandler : ButtonOld
{
	public TextMesh[] textMeshes;

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
	public GameObject[] level10Slides;
	public GameObject[] endSlides;

	private int nextLevel;
	private GameObject[][] slides;

	private GameObject[] thisStory;
	private GameObject currentSlide;
	private int slideIndex;
	private string[] slideText;
	private StringReader fileInput;

	new void Start ()
	{
		GameStateMachine.currentState = (int)GameStateMachine.GameState.Play;
		if (PlayerPrefs.GetInt ("Music") != 0) {
			audio.mute = false;
		} else {
			audio.mute = true;
		}
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
			level9Slides,
			level10Slides,
			endSlides
		};
		string[] sceneTexts = {introScene,level2,level3,level4,level5,level6,level7,level8,level9,level10,end};
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
		case "Level10-Downtown":
			nextLevel = 10;
			break;
		case "End":
			nextLevel = 11;
			NextSceneName = "Credits";
			break;
		default:
			nextLevel = 11;
			NextSceneName = "Credits";
			break;
		}

		slideText = new string[3];
		slideIndex = 0;

		thisStory = new GameObject[slides [nextLevel - 1].Length];

		for (int i = 0; i < slides[nextLevel-1].Length; i++) {
			thisStory [i] = Instantiate (slides [nextLevel - 1] [i], new Vector3 (0, 0, i), Quaternion.identity) as GameObject;
			if (i != 0) {
				thisStory [i].SetActive (false);
			}
		}
	
		currentSlide = thisStory [slideIndex];

		fileInput = new StringReader (sceneTexts [nextLevel - 1]);
		readSlideText ();
	}

	protected override void action ()
	{
		if (slideIndex < slides [nextLevel - 1].Length - 1) {
			readSlideText ();
			slideIndex++;
			currentSlide.SetActive (false);
			currentSlide = thisStory [slideIndex];
			currentSlide.SetActive (true);
		} else {
			LoadLevel.levelToLoad = NextSceneName;
			PlayerPrefs.SetInt (NextSceneName + "Story", 1);
			GameStateMachine.currentState = (int)GameStateMachine.GameState.Intro;
			Application.LoadLevel ("Loading");
		}
		StartCoroutine (waitToResetTouch ());
	}

	private void readSlideText ()
	{
		//Makes all dialog lines blank first
		for (int i = 0; i < slideText.Length; i++) {
			textMeshes [i].text = "";
		}
		int numberOfLines;
		numberOfLines = Int32.Parse (fileInput.ReadLine ());
		for (int i = 0; i < numberOfLines; i++) {
			textMeshes [i].text = fileInput.ReadLine ();
		}
	}

	private string introScene = "2\n" +
		"Today is a big day.\n" +
		"I get to start my dream job working in a zoo!\n" +
		"3\n" +
		"I may have struggled all my life with sickle-cell anemia,\n" +
		"but by paying attention to my body,\n" +
		"I have learned to live with it...\n" +
		"1\n" +
		"And I certainly won't let it slow me down today!\n" +
		"1\n" +
		"(gasp!)\n" +
		"3\n" +
		"Help! Help! \n" +
		"All the animals have escaped their pens. \n" +
		"They're getting away!\n" +
		"1\n" +
		"The first day of work is always the hardest.\n";

	private string level2 = "1\n" +
		"Good job with the tortoise, but there's no time to rest.\n" +
		"2\n" +
		"A crocodile is on the loose inside the zoo.\n" +
		"We need you to track it down before it hurts anyone!\n" +
		"1\n" +
		"Let's Do It!\n";

	private string level3 = "1\n" +
		"Good job with the crocodile, but there's no time to rest.\n" +
		"1\n" + 
		"A flamingo was reported in the suburbs, find it fast!\n" + 
		"1\n" +
		"Let's Do It!\n";

	private string level4 = "1\n" +
		"Good job with the flamingo, but there's not time to rest.\n" +
		"3\n" +
		"We got another report of a rhino in the suburbs.\n" +
		"Let's get it back to the zoo!\n" +
		"And watch out for lawnmowers.\n" +
		"1\n" +
		"Let's Do It!\n";

	private string level5 = "2\n" +
		"What a week!\n" + 
		"Half the animals are safely back in their pens.\n" +
		"2\n" +
		"There's still a penguin out in the suburbs.\n" +
		"Let's get him back to a cooler climate.\n" +
		"1\n" +
		"Let's Do It!\n";

	private string level6 = "2\n" +
		"There's a gorilla at the beach giving sunbathers grief,\n" +
		"let's get over there and catch him before he gets sunburned!\n" +
		"1\n" +
		"Let's Do It!\n";

	private string level7 = "2\n" +
		"Looks like one of our ostriches likes running on the beach.\n" +
		"You know what to do!\n" +
		"1\n" +
		"Let's Do It!\n";

	private string level8 = "3\n" +
		"An elephant has been spotted downtown.\n" +
		"Track it down before it causes too much havoc.\n" +
		"And watch out for cars!\n" +
		"1\n" +
		"Let's Do It!\n";

	private string level9 = "2\n" +
		"We've got a report of a cheetah downtown.\n" +
		"You know the drill!\n" +
		"1\n" +
		"Let's Do It!\n";

	private string level10 = "3\n" +
		"Last one!\n" +
		"It's a bear of, yeah, it's a bear!\n" +
		"Let's get him!\n" +
		"1\n" +
		"Let's Do It!\n";

	private string end = "2\n" +
		"Congratulations!\n" +
		"All the animals have been returned to the zoo safe and sound.\n" +
		"3\n" +
		"By paying attention to my body's needs,\n" +
		"I was able to pursue my dreams and succeed.\n" +
		"I'll never let sickle-cell keep me down.\n";


}
