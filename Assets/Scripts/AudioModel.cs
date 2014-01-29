using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/** Game Audio Handler, plays appropriate sound effects during events and turns on or off sound based on settings.
 * @author: Ebtissam Wahman 
 */ 

public class AudioModel : MonoBehaviour
{
	private float rate; //number of seconds to take to fade in and out of audio
	private bool music;
	private bool sound;

	private bool sound1Paused;
	private bool sound2Paused;
	private bool musicPaused;

	private AudioClip levelMusic;
	private SceneManager sceneManager;
	
	private GameObject musicTrack;
	private GameObject soundTrack1;
	private GameObject soundTrack2;

	public static Dictionary<string,AudioClip> audioClips;

	void Start ()
	{
		rate = 3f;
		sceneManager = GameSetup.FindObjectOfType<SceneManager> ();

		musicTrack = GameObject.Find ("Music Track");
		soundTrack1 = GameObject.Find ("Sound Track 1");
		soundTrack2 = GameObject.Find ("Sound Track 2");

		if (!PlayerPrefs.HasKey ("Music")) {
			PlayerPrefs.SetString ("Music", "ON");
		}
		if (!PlayerPrefs.HasKey ("Sound")) {
			PlayerPrefs.SetString ("Sound", "ON");
		}

		music = (PlayerPrefs.GetString ("Music").Equals ("ON")) ? true : false;
		sound = (PlayerPrefs.GetString ("Sound").Equals ("ON")) ? true : false;

		sound1Paused = sound2Paused = musicPaused = false;

		//MUSIC

		//Zoo 
		audioClips.Add ("Zoo", Resources.Load ("Sounds/Zoo/Zoo", typeof(AudioClip)) as AudioClip);
//		audioClips.Add ("Zoo(doubleloop)", Resources.Load ("Sounds/Zoo/Zoo(doubleloop)", typeof(AudioClip)) as AudioClip);
//		audioClips.Add ("Zoo(singleloop)", Resources.Load ("Sounds/Zoo/Zoo(singleloop)", typeof(AudioClip)) as AudioClip);

		//Highway
		audioClips.Add ("Highway(SingleLoop)", Resources.Load ("Sounds/Highway/Highway(SingleLoop)", typeof(AudioClip)) as AudioClip);

	}

	void FixedUpdate ()
	{
		music = (PlayerPrefs.GetString ("Music").Equals ("ON")) ? true : false;
		sound = (PlayerPrefs.GetString ("Sound").Equals ("ON")) ? true : false;
	}

	void Update ()
	{
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
			if (soundTrack1.audio.volume != 1f) {
			
				soundFadeIn ();
			}
			if (soundTrack2.audio.volume != 1f) {
				sound2FadeIn ();
			}
			
		} else {
			soundTrack1.audio.volume = 0f;
			soundTrack1.audio.Stop ();
			soundTrack2.audio.volume = 0f;
			soundTrack2.audio.Stop ();
		}

		if (sceneManager.pauseAudio) {//Sound Paused
			if (sound) {
				if (soundTrack1.audio.isPlaying) {
					soundTrack1.audio.Pause ();
					sound1Paused = true;
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
				if (!soundTrack1.audio.isPlaying && sound1Paused) {
					soundTrack1.audio.Play ();
					sound1Paused = false;
				}
				if (soundTrack1.audio.volume != 1) {
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
		if (soundTrack1.audio.isPlaying) {
			if (soundTrack1.audio.clip != clip) {
				soundTrack1.audio.Pause ();
				soundTrack1.audio.clip = clip;
				soundTrack1.audio.Play ();
			}
		} else {
			soundTrack1.audio.clip = clip;
			soundTrack1.audio.Play ();
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
		soundTrack1.audio.volume = Mathf.Lerp (soundTrack1.audio.volume, 1f, rate * Time.deltaTime);
	}
	private void sound2FadeIn ()
	{
		soundTrack2.audio.volume = Mathf.Lerp (soundTrack2.audio.volume, 1f, rate * Time.deltaTime);
	}

	private void soundFadeOut ()
	{
		soundTrack1.audio.volume = Mathf.Lerp (soundTrack1.audio.volume, 0f, rate * Time.deltaTime);
		
	}
	private void sound2FadeOut ()
	{
		soundTrack2.audio.volume = Mathf.Lerp (soundTrack2.audio.volume, 0f, rate * Time.deltaTime);
		
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
