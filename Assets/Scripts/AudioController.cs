using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class AudioController : MonoBehaviour
{
	public AudioClip levelMusic;
	public bool audioPaused;
	private AudioModel audioModel;

	void Start ()
	{
		audioModel = FindObjectOfType<AudioModel> ();
		if (levelMusic != null) {
			audioModel.playMusic (levelMusic);
		}

	}

	public void pauseAudio ()
	{
		if (!audioPaused) {
			audioModel.pauseAudio ();
			audioPaused = true;
		}
	}

	public void objectInteraction (AudioClip clip)
	{
		if (clip != null) {
			audioModel.playSound (clip);
		}
	}

	public void objectInteraction (AudioClip[] clips, float time = 0)
	{
		if (clips != null && clips.Length > 1) {
			audioModel.playSound (clips, time);
		}

	}

}
