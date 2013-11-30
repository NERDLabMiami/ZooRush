using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/** Game Audio Handler, plays appropriate sound effects during events and turns on or off sound based on settings.
 * @author: Ebtissam Wahman 
 */ 

public class AudioHandler : MonoBehaviour
{
	private bool music;
	private bool sound;

	private AudioClip levelMusic;
	private SceneManager sceneManager;

	public static Dictionary<string,AudioClip> audioClips;

	void Start ()
	{
		sceneManager = GameSetup.FindObjectOfType<SceneManager> ();
		if (!PlayerPrefs.HasKey ("Music")) {
			PlayerPrefs.SetString ("Music", "ON");
		}
		if (!PlayerPrefs.HasKey ("Sound")) {
			PlayerPrefs.SetString ("Sound", "ON");
		}

		music = (PlayerPrefs.GetString ("Music").Equals ("ON")) ? true : false;
		sound = (PlayerPrefs.GetString ("Sound").Equals ("ON")) ? true : false;

		//Set up for audioclip dictionary
		audioClips = new Dictionary<string,AudioClip> ();

		//SOUND EFFECTS
		audioClips.Add ("CAPTURED", Resources.Load ("Sounds/SOUND_FXS/CAPTURED", typeof(AudioClip)) as AudioClip);
		audioClips.Add ("DOCTOR", Resources.Load ("Sounds/SOUND_FXS/DOCTOR", typeof(AudioClip)) as AudioClip);
		audioClips.Add ("GAMEOVER", Resources.Load ("Sounds/SOUND_FXS/GAMEOVER", typeof(AudioClip)) as AudioClip);
		audioClips.Add ("HARDSICKLOOP", Resources.Load ("Sounds/SOUND_FXS/HARDSICKLOOP", typeof(AudioClip)) as AudioClip);
		audioClips.Add ("INFECTION", Resources.Load ("Sounds/SOUND_FXS/INFECTION", typeof(AudioClip)) as AudioClip);
		audioClips.Add ("JUMP", Resources.Load ("Sounds/SOUND_FXS/JUMP", typeof(AudioClip)) as AudioClip);
		audioClips.Add ("PILL", Resources.Load ("Sounds/SOUND_FXS/PILL", typeof(AudioClip)) as AudioClip);
		audioClips.Add ("SOFTSICKLOOP", Resources.Load ("Sounds/SOUND_FXS/SOFTSICKLOOP", typeof(AudioClip)) as AudioClip);

		//MUSIC

		//Zoo 
		audioClips.Add ("Zoo", Resources.Load ("Sounds/Zoo/Zoo", typeof(AudioClip)) as AudioClip);
		audioClips.Add ("Zoo(doubleloop)", Resources.Load ("Sounds/Zoo/Zoo(doubleloop)", typeof(AudioClip)) as AudioClip);
		audioClips.Add ("Zoo(singleloop)", Resources.Load ("Sounds/Zoo/Zoo(singleloop)", typeof(AudioClip)) as AudioClip);

		//Highway
		audioClips.Add ("Highway(SingleLoop)", Resources.Load ("Sounds/Highway/Highway(SingleLoop)", typeof(AudioClip)) as AudioClip);

	}
	
	void Update ()
	{
		music = (PlayerPrefs.GetString ("Music").Equals ("ON")) ? true : false;
		sound = (PlayerPrefs.GetString ("Sound").Equals ("ON")) ? true : false;

		if (!sceneManager.isPlaying) {//Scene Paused

		}
	}

	public void playSound (string soundEffect)
	{

	}

	public void playMusic (string environment)
	{

	}

	private void soundFadeIn ()
	{
		
	}

	private void soundFadeOut ()
	{

	}

	private void musicFadeIn ()
	{
		
	}

	private void musicFadeOut ()
	{
		
	}
}
