using UnityEngine;
using System.Collections;

public class LevelButton : MonoBehaviour
{

	public string SceneName;
	public bool storyMode;

	void OnMouseUp ()
	{
		if (storyMode) {
			StoryModeHandler.NextSceneName = SceneName;
			Application.LoadLevel ("IntroScene");
		} else {
			LoadLevel.levelToLoad = SceneName;
			Application.LoadLevel ("Loading");
		}
	}
}
