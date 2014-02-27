using UnityEngine;
using System.Collections;

public class StopwatchController : MonoBehaviour
{
	private bool start;
	private float currentSpeed;

	void Start ()
	{
		start = false;
		currentSpeed = 1.0f;
		if (PlayerPrefs.GetInt ("Sound") == 0) {
			audio.mute = true;
		}
	}

	void FixedUpdate ()
	{
		switch (GameStateMachine.currentState) {
		case (int)GameStateMachine.GameState.Paused:
			pauseStopwatch ();
			break;
		case (int)GameStateMachine.GameState.PauseToPlay:
			resumeStopwatch ();
			break;
		default:
			break;
		}

	}

	public void receiveInteraction (string infectionType)
	{

		switch (infectionType) {
		case "Red":
			if (currentSpeed <= 2) {
				currentSpeed = 2f;
			}
			break;
		case "Yellow":
			if (currentSpeed <= 1.33) {
				currentSpeed = 1.33f;
			}
			break;
		default:
			if (currentSpeed <= 1.0) {
				currentSpeed = 1.0f;
			}
			break;
		}
		if (!start) {
			startStopwatch ();
		}
		GetComponent<Animator> ().speed = currentSpeed;
	}

	private void startStopwatch ()
	{
		if (!start) {
			start = true;
			GetComponent<Animator> ().SetTrigger ("Start");
		}
	}

	public void stopStopwatch ()
	{
		GetComponent<Animator> ().SetTrigger ("Reset");
		Destroy (gameObject);
	}

	public void pauseStopwatch ()
	{
		GetComponent<Animator> ().speed = 0;
	}

	public void resumeStopwatch ()
	{
		GetComponent<Animator> ().speed = currentSpeed;
	}

	public void timeEnded ()
	{
		NextSceneHandler.infected ();
	}
}
