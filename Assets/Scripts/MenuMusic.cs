using UnityEngine;
using System.Collections;

public class MenuMusic : MonoBehaviour
{
	private static AudioSource source;
	private static MenuMusic instance = null;
	public static MenuMusic Instance {
		get { return instance; }
	}

	void Awake ()
	{
		if (instance != null && instance != this) {
			Destroy (this.gameObject);
			return;
		} else {
			instance = this;
		}
		DontDestroyOnLoad (gameObject);
		source = audio;
	}

	void Start ()
	{
		menuMusicUpdate ();
	}

	void OnLevelWasLoaded (int level)
	{
		menuMusicUpdate ();
	}

	public void menuMusicUpdate ()
	{
		switch (Application.loadedLevelName) {
		case "Credits":
		case "Instructions":
		case "Main Menu":
		case "Settings":
			if (PlayerPrefs.GetInt ("Music", 1) == 0) {
				source.mute = true;
			} else {
				source.enabled = true;
				source.mute = false;
			}
			break;
		default:
			source.enabled = false;
			break;
		}
	}
}
