using UnityEngine;
using System.Collections;

/** Controls user interaction with the splash screen.
 *	@author Ebtissam Wahman
 */
public class StartMenu : MonoBehaviour
{
	public DirectToScene playButton;
	
	void Start ()
	{
		if (PlayerPrefs.GetInt ("Levels Unlocked") > 1) {
			playButton.sceneName = "AllLevels";
		} else {
			StoryModeHandler.NextSceneName = "Level1-Tutorial";
			playButton.sceneName = "Story";
		}
	}
}
