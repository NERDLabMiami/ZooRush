using UnityEngine;
using System.Collections;

public class StopwatchController : MonoBehaviour
{
	private bool start;
	private float currentSpeed;
	private Animator animator;

	void Start ()
	{
		start = false;
		currentSpeed = 1.0f;
		animator = GetComponent<Animator> ();
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
		gameObject.GetComponent<Animator> ().speed = currentSpeed;
	}

	private void startStopwatch ()
	{
		if (!start) {
			start = true;
			gameObject.GetComponent<Animator> ().SetTrigger ("Start");
		}
	}

	public void stopStopwatch ()
	{
		if (start) {
			gameObject.GetComponent<Animator> ().SetTrigger ("Reset");
			Destroy (gameObject);
		}
	}

	public void timeEnded ()
	{
		NextSceneHandler.infected ();
	}
}
