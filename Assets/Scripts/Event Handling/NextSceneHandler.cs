using UnityEngine;
using System.Collections;

public class NextSceneHandler : MonoBehaviour
{
		public static void nextLevel (string levelName)
		{
				LoadLevel.levelToLoad = levelName;
				Application.LoadLevel ("Loading");
		}

		public static void loadGameLevelWithConditions (string levelName)
		{
				nextLevelStoryMode (levelName);
//		if (levelName.Equals ("End")) {
//			nextLevelStoryMode (levelName);
//		}
//
//		if (PlayerPrefs.GetInt (levelName + "Story", 0) == 0) {
//			nextLevelStoryMode (levelName);
//		} else {
//			nextLevel (levelName);
//		}
		}

		public static void nextLevelStoryMode (string levelName)
		{
				StoryModeHandler.NextSceneName = levelName;

				if (levelName.Equals ("Level1-Tutorial")) {
						Application.LoadLevel ("StoryMode");
				} else {
						Application.LoadLevel ("Story");
				}
		}
}
