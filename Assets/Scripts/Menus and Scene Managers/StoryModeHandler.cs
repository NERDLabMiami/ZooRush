using UnityEngine;
using System.Collections;

public class StoryModeHandler : OtherButtonClass
{
	enum sceneValues
	{
		level1 = -1,
		level2,
		level3,
		level4,
		level5,
		level6,
		level7,
		level8,
		level9,
		level10,
		end,
		COUNT
	}
	;

	public static string NextSceneName;
	public StoryModePanel[] storyPanels;
	private StoryModePanel currentStory;
	private sceneValues nextLevel;

	void Start ()
	{
		GameState.currentState = GameState.States.Play;
		if (PlayerPrefs.GetInt ("Music") != 0) {
			audio.mute = false;
		} else {
			audio.mute = true;
		}
		
		switch (NextSceneName) {
		case "Level1-Tutorial":
			nextLevel = sceneValues.level1;
			break;
		case "Level2-Zoo":
			nextLevel = sceneValues.level2;
			break;
		case "Level3-Suburbs":
			nextLevel = sceneValues.level3;
			break;
		case "Level4-Suburbs":
			nextLevel = sceneValues.level4;
			break;
		case "Level5-Suburbs":
			nextLevel = sceneValues.level5;
			break;
		case "Level6-Beach":
			nextLevel = sceneValues.level6;
			break;
		case "Level7-Beach":
			nextLevel = sceneValues.level7;
			break;
		case "Level8-Downtown":
			nextLevel = sceneValues.level8;
			break;
		case "Level9-Downtown":
			nextLevel = sceneValues.level9;
			break;
		case "Level10-Downtown":
			nextLevel = sceneValues.level10;
			break;
		case "End":
			nextLevel = sceneValues.end;
			NextSceneName = "Credits";
			break;
		default:
			nextLevel = sceneValues.end;
			NextSceneName = "Credits";
			break;
		}
		currentStory = storyPanels [(int)nextLevel];
		currentStory.next ();
	}

	public override void otherButtonAction (Button thisButton)
	{
		if (!currentStory.next ()) {
			LoadLevel.levelToLoad = NextSceneName;
//			PlayerPrefs.SetInt (NextSceneName + "Story", 1);
			Application.LoadLevel ("Loading");
		}
//		StartCoroutine (waitToResetTouch ());
	}
	
}
