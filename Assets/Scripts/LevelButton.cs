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
			Application.LoadLevel ("Story");
		} else {
			LoadLevel.levelToLoad = SceneName;
			Application.LoadLevel ("Loading");
		}
	}
}
