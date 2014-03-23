using UnityEngine;
using System.Collections;

public class LevelFailModel : MonoBehaviour
{

	public static string failReason;
	public static string levelFailed;
	public TextMesh title;
	public TextMesh dialog;
	public DirectToScene retry;

	private string[] titleOptions = {	
		"Sickle Cell Crisis", 
		"Infection!",
		"It Got Away!",
		"Ouch! Watch Out Next Time!"
	};
	private string[] dialogText = {
		"Drink water to avoid\na sickle cell crisis",
		"Infections can only be\ncured by visiting the\nhospital",
		"Failed!\nIt's time to stop\nchasing this one, it's long\ngone by now.",
		"Chin up, you'll probably\ncatch it next time!"
	};
	public Sprite[] timeOutSprites;

	void Start ()
	{

		switch (failReason) {
		case "Fainted":
			title.text = titleOptions [0];
			dialog.text = dialogText [0];
			break;
		case "Infected":
			title.text = titleOptions [1];
			dialog.text = dialogText [1];
			break;
		case "TimeOut":
			title.text = titleOptions [2];
			dialog.text = dialogText [2];
			GameObject.FindObjectOfType<StoryModeSpriteHandler> ().sprites = timeOutSprites;
			GameObject.FindObjectOfType<StoryModeSpriteHandler> ().changeSprites ();
			break;
		default:
			title.text = titleOptions [3];
			dialog.text = dialogText [3];
			break;
		}
		LoadLevel.levelToLoad = levelFailed;
		retry.sceneName = "Loading";
	}
}
