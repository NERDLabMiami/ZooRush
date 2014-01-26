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

	private bool soundPaused;
	private bool sound2Paused;
	private bool musicPaused;

	private AudioClip levelMusic;
	private SceneManager sceneManager;
	
	private GameObject musicTrack;
	private GameObject soundTrack;
	private GameObject soundTrack2;

	public static Dictionary<string,AudioClip> audioClips;

	void Start ()
	{
		sceneManager = GameSetup.FindObjectOfType<SceneManager> ();
		
		if (musicTrack == null) {
			musicTrack = new GameObject ("Music Track", typeof(AudioSource));
		}

		if (soundTrack == null) {
			soundTrack = new GameObject ("Sound Track", typeof(AudioSource));
		}
		
		if (soundTrack2 == null) {
			soundTrack2 = new GameObject ("Sound Track 2", typeof(AudioSource));
		}
		
		musicTrack.transform.parent = Camera.main.transform;
		musicTrack.audio.loop = true;
		musicTrack.audio.playOnAwake = false;

		soundTrack.transform.parent = Camera.main.transform;
		soundTrack.audio.loop = false;
		soundTrack.audio.playOnAwake = false;
		
		soundTrack2.transform.parent = Camera.main.transform;
		soundTrack2.audio.loop = false;
		soundTrack2.audio.playOnAwake = false;


		if (!PlayerPrefs.HasKey ("Music")) {
			PlayerPrefs.SetString ("Music", "ON");
		}
		if (!PlayerPrefs.HasKey ("Sound")) {
			PlayerPrefs.SetString ("Sound", "ON");
		}

		music = (PlayerPrefs.GetString ("Music").Equals ("ON")) ? true : false;
		sound = (PlayerPrefs.GetString ("Sound").Equals ("ON")) ? true : false;

		soundPaused = false;
		sound2Paused = false;
		musicPaused = false;

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
		audioClips.Add ("WATERDRINK", Resources.Load ("Sounds/SOUND_FXS/WATERDRINK", typeof(AudioClip)) as AudioClip);
		audioClips.Add ("SOFTSICKLOOP", Resources.Load ("Sounds/SOUND_FXS/SOFTSICKLOOP", typeof(AudioClip)) as AudioClip);
		audioClips.Add ("SIGH", Resources.Load ("Sounds/SOUND_FXS/SIGH", typeof(AudioClip)) as AudioClip); 
		audioClips.Add ("BALL", Resources.Load ("Sounds/SOUND_FXS/BALL", typeof(AudioClip)) as AudioClip); 
		

		//MUSIC

		//Zoo 
		audioClips.Add ("Zoo", Resources.Load ("Sounds/Zoo/Zoo", typeof(AudioClip)) as AudioClip);
//		audioClips.Add ("Zoo(doubleloop)", Resources.Load ("Sounds/Zoo/Zoo(doubleloop)", typeof(AudioClip)) as AudioClip);
//		audioClips.Add ("Zoo(singleloop)", Resources.Load ("Sounds/Zoo/Zoo(singleloop)", typeof(AudioClip)) as AudioClip);

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
		} else {
			musicTrack.audio.volume = 0f;
			musicTrack.audio.Stop ();
		}

		if (sound) {
			if (soundTrack.audio.volume != 1f) {
			
				soundFadeIn ();
			}
			if (soundTrack2.audio.volume != 1f) {
				sound2FadeIn ();
			}
			
		} else {
			soundTrack.audio.volume = 0f;
			soundTrack.audio.Stop ();
			soundTrack2.audio.volume = 0f;
			soundTrack2.audio.Stop ();
		}

		if (sceneManager.pauseAudio) {//Sound Paused
			if (sound) {
				if (soundTrack.audio.isPlaying) {
					soundTrack.audio.Pause ();
					soundPaused = true;
					soundFadeOut ();
				}
				if (soundTrack2.audio.isPlaying) {
					soundTrack2.audio.Pause ();
					sound2Paused = true;
					sound2FadeOut ();
				}
			}
			if (music) {
				if (musicTrack.audio.isPlaying) {
					musicTrack.audio.Pause ();
					musicPaused = true;
				}
			}

		} else {//Continue Sound Playing
			if (sound) {
				if (!soundTrack.audio.isPlaying && soundPaused) {
					soundTrack.audio.Play ();
					soundPaused = false;
				}
				if (soundTrack.audio.volume != 1) {
					soundFadeIn ();
				}
				if (!soundTrack2.audio.isPlaying && sound2Paused) {
					soundTrack2.audio.Play ();
					sound2Paused = false;
				}
				if (soundTrack2.audio.volume != 1) {
					sound2FadeIn ();
				}
			}
			if (music) {
				if (!musicTrack.audio.isPlaying && musicPaused) {
					musicTrack.audio.Play ();
					musicPaused = false;
				}
				if (musicTrack.audio.volume != 1) {
					musicFadeIn ();
				}
			}
		}
	}

	public void playSound (string soundEffect)
	{
		Debug.Log ("PLAY SOUND EFFECT 1 CALLED");
		AudioClip clip;
		audioClips.TryGetValue (soundEffect, out clip);
		if (soundTrack.audio.isPlaying) {
			if (soundTrack.audio.clip != clip) {
				soundTrack.audio.Pause ();
				soundTrack.audio.clip = clip;
				soundTrack.audio.Play ();
			}
		} else {
			soundTrack.audio.clip = clip;
			soundTrack.audio.Play ();
		}
		
	}
	
	private IEnumerator waitToPlayTrack2 (AudioClip clip2, float time)
	{
		yield return new WaitForSeconds (time);
		if (soundTrack2.audio.isPlaying) {
			if (soundTrack2.audio.clip != clip2) {
				soundTrack2.audio.Pause ();
				soundTrack2.audio.clip = clip2;
				soundTrack2.audio.Play ();
			}
		} else {
			soundTrack2.audio.clip = clip2;
			soundTrack2.audio.Play ();
		}
	}
	
	public void playSound (string soundEffect1, string soundEffect2, float delayTime)
	{
		Debug.Log ("PLAY SOUND EFFECT 2 CALLED");
		playSound (soundEffect1);
		AudioClip clip2;
		audioClips.TryGetValue (soundEffect2, out clip2);
		StartCoroutine (waitToPlayTrack2 (clip2, delayTime));
	}
	
	public void playMusic (string levelMusic)
	{
		AudioClip clip;
		audioClips.TryGetValue (levelMusic, out clip);
		musicTrack.audio.clip = clip;
	}

	private void soundFadeIn ()
	{
		soundTrack.audio.volume = Mathf.Lerp (soundTrack.audio.volume, 1f, rate * Time.deltaTime);
	}
	private void sound2FadeIn ()
	{
		soundTrack2.audio.volume = Mathf.Lerp (soundTrack.audio.volume, 1f, rate * Time.deltaTime);
	}

	private void soundFadeOut ()
	{
		soundTrack.audio.volume = Mathf.Lerp (soundTrack.audio.volume, 0f, rate * Time.deltaTime);
		
	}
	private void sound2FadeOut ()
	{
		soundTrack2.audio.volume = Mathf.Lerp (soundTrack.audio.volume, 0f, rate * Time.deltaTime);
		
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
