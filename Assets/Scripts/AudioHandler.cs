using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/** Game Audio Handler, plays appropriate sound effects during events and turns on or off sound based on settings.
 * @author: Ebtissam Wahman 
 */ 

public class AudioHandler : MonoBehaviour
{
	public float rate; //TODO make default 3f
	private bool music;
	private bool sound;

	private AudioClip levelMusic;
	private SceneManager sceneManager;
	
	private GameObject musicTrack;
	private GameObject soundTrack;

	public static Dictionary<string,AudioClip> audioClips;

	void Start ()
	{
		sceneManager = GameSetup.FindObjectOfType<SceneManager> ();
		
		musicTrack = GameObject.Find ("Music Track");
		soundTrack = GameObject.Find ("Sound Track");
		
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
		if (music) {
			if (musicTrack.audio.clip == null) {
				switch (sceneManager.levelNumber) {
				case 1:
					playMusic ("Zoo");
					musicTrack.audio.volume = 0f;
					musicTrack.audio.Play ();
					break;
				default:
					break;
				}
			}
			if (musicTrack.audio.volume != 1) {
				musicFadeIn ();
			}
			
		}
		
	
		
		if (!sceneManager.isPlaying) {//Scene Paused
			if (music) {
				if (!musicTrack.audio.isPlaying) {
					musicTrack.audio.Play ();
				}
				
			}
		}
	}

	public void playSound (string soundEffect)
	{
		AudioClip clip;
		audioClips.TryGetValue (soundEffect, out clip);
		soundTrack.audio.clip = clip;
	}

	public void playMusic (string levelMusic)
	{
		AudioClip clip;
		audioClips.TryGetValue (levelMusic, out clip);
		musicTrack.audio.clip = clip;
	}

	private void soundFadeIn ()
	{
		soundTrack.audio.volume = Mathf.Lerp (musicTrack.audio.volume, 1f, rate * Time.deltaTime);
	}

	private void soundFadeOut ()
	{
		soundTrack.audio.volume = Mathf.Lerp (musicTrack.audio.volume, 0f, rate * Time.deltaTime);
	}

	private void musicFadeIn ()
	{
		musicTrack.audio.volume = Mathf.Lerp (musicTrack.audio.volume, 1f, rate * Time.deltaTime);
	}

	private void musicFadeOut ()
	{
		musicTrack.audio.volume = Mathf.Lerp (musicTrack.audio.volume, 0f, rate * Time.deltaTime);
	}
}
