using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/** Game Audio Handler, plays appropriate sound effects during events and turns on or off sound based on settings.
 * @author: Ebtissam Wahman 
 */ 

public class AudioModel : MonoBehaviour
{
	private float rate; //number of seconds to take to fade in and out of audio
	private bool music; //indicates if music is enabled
	private bool sound; //indicates if sound is enabled

	private bool sound1Paused; //indicateds if sound track 1 is paused
	private bool sound2Paused; //indicateds if sound track 2 is paused
	private bool musicPaused; //indicateds if the music track is paused
	
	public GameObject musicTrack; 	//pointer to the music track object
	public GameObject soundTrack1;	//pointer to the sound track 1 object
	public GameObject soundTrack2; 	//pointer to the sound track 2 object
	
	void Start ()
	{
		rate = 3f;

		if (!PlayerPrefs.HasKey ("Music")) {
			PlayerPrefs.SetInt ("Music", 1);
		}
		if (!PlayerPrefs.HasKey ("Sound")) {
			PlayerPrefs.SetInt ("Sound", 1);
		}

		//Look at Player Prefs to determin if sound and music are enabled
		music = (PlayerPrefs.GetInt ("Music") == 1); 
		sound = (PlayerPrefs.GetInt ("Sound") == 1);

		if (!music) {
			turnOffMusic ();
		}
		if (!sound) {
			turnOffSound ();
		}
		resumeAudio ();
	}

	void FixedUpdate ()
	{
		//TODO: Consider removing due to  processing time
		music = (PlayerPrefs.GetString ("Music").Equals ("ON")) ? true : false;
		sound = (PlayerPrefs.GetString ("Sound").Equals ("ON")) ? true : false;
	}

	void Update ()
	{
		if (music) {
			if (musicTrack.audio.clip != null) {
				if (musicTrack.audio.volume != 1) {
					musicFadeIn ();
				}
			}
			if (!musicTrack.audio.isPlaying && !musicPaused) {
				musicTrack.audio.Play ();
				if (!musicPaused && musicTrack.audio.volume != 1) {
					musicFadeIn ();
				} else {
					if (musicPaused) {
						if (musicTrack.audio.volume != 0) {
							sound1FadeOut ();
						} else {
							if (musicTrack.audio.isPlaying && musicTrack.audio.volume == 0) {
								musicTrack.audio.Pause ();
							}
						}
					}
				}
			}
		}

		if (sound) {

			if (soundTrack1.audio.clip != null) {
				if (!sound1Paused && soundTrack1.audio.volume != 1) {
					sound1FadeIn ();
				} else {
					if (sound1Paused && soundTrack1.audio.volume != 0) {
						sound1FadeOut ();
					} else {
						if (soundTrack1.audio.isPlaying && soundTrack1.audio.volume == 0) {
							soundTrack1.audio.Pause ();
						}
					}
				}
			}
			if (soundTrack2.audio.clip != null) {
				if (!sound2Paused && soundTrack2.audio.volume != 1) {
					sound2FadeIn ();
				} else {
					if (sound2Paused && soundTrack2.audio.volume != 0) {
						sound2FadeOut ();
					} else {
						if (soundTrack2.audio.isPlaying && soundTrack2.audio.volume == 0) {
							soundTrack2.audio.Pause ();
						}
					}
				}
			}

			if (!soundTrack1.audio.isPlaying && !sound1Paused) {
				soundTrack1.audio.Play ();
			}
			if (!soundTrack2.audio.isPlaying && !sound2Paused) {
				soundTrack2.audio.Play ();
			}
		}
	}

	/** 
	 * Stops the music track audio.
	 */ 
	public void turnOffMusic ()
	{
		musicTrack.audio.volume = 0f;
		musicTrack.audio.Stop ();
	}

	/** 
	 * Stops the audio track 1 and 2 audio.
	 */ 
	public void turnOffSound ()
	{
		soundTrack1.audio.volume = 0f;
		soundTrack1.audio.Stop ();
		soundTrack2.audio.volume = 0f;
		soundTrack2.audio.Stop ();
	}

	/**
	 * Pauses audio on all tracks.
	 */ 
	public void pauseAudio ()
	{
		sound1Paused = sound2Paused = musicPaused = true;
	}

	/**
	 * Resumes audio on all tracks.
	 */ 
	public void resumeAudio ()
	{
		sound1Paused = sound2Paused = musicPaused = false;
	}

	/**
	 * Plays a level soundtrack.
	 * @param clip the level sound clip
	 */
	public void playMusic (AudioClip clip)
	{
		musicTrack.audio.clip = clip;
		musicTrack.audio.Play ();
	}

	/**
	 * Assigns and plays audio clip to sound track 1.
	 */ 
	public void playSound (AudioClip clip)
	{
		playOneSound (clip);
	}

	/**
	 * Assigns and plays audio clips to the sound tracks.
	 */ 
	public void playSound (AudioClip[] clips, float time = 0)
	{
		if (clips.Length > 1) {
			playTwoSounds (clips [0], clips [1], time);
		} else {
			playOneSound (clips [0]);
		}
	}

	private void playOneSound (AudioClip clip)
	{
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
	
	private void playTwoSounds (AudioClip soundEffect1, AudioClip soundEffect2, float delayTime)
	{
		Debug.Log ("PLAY SOUND EFFECT 2 CALLED");
		playOneSound (soundEffect1);
		StartCoroutine (waitToPlayTrack2 (soundEffect2, delayTime));
	}

	private void sound1FadeIn ()
	{
		soundTrack1.audio.volume = Mathf.Lerp (soundTrack1.audio.volume, 1f, rate * Time.deltaTime);
	}
	private void sound2FadeIn ()
	{
		soundTrack2.audio.volume = Mathf.Lerp (soundTrack2.audio.volume, 1f, rate * Time.deltaTime);
	}

	private void sound1FadeOut ()
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
