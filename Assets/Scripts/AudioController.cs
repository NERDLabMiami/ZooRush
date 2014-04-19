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
	void OnEnable ()
	{
		GameState.StateChanged += OnStateChanged;
	}
	
	void OnDisable ()
	{
		GameState.StateChanged -= OnStateChanged;
	}
	
	private void OnStateChanged ()
	{
		switch (GameState.currentState) {
		case GameState.States.Pause:
			OnPause ();
			break;
		case GameState.States.Play:
			OnPlay ();
			break;
		case GameState.States.Dialog:
			break;
		case GameState.States.Intro:
			break;
		case GameState.States.Transition:
			break;
		case GameState.States.Win:
			break;
		case GameState.States.Lose:
			break;
		default:
			break;
		}
	}

	void OnPlay ()
	{
		resumeAudio ();
	}

	void OnPause ()
	{
		pauseAudio ();
	}

	public void resumeAudio ()
	{
		if (audioPaused) {
			audioModel.resumeAudio ();
			audioPaused = false;
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
