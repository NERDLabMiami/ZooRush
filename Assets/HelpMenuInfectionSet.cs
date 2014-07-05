using UnityEngine;
using System.Collections;

public class HelpMenuInfectionSet : HelpMenuSet
{
	public Infection[] infections;
	private bool interacted;

	private Vector3[] positions;
	public GameObject[] infectionPrefabs;

	void Start ()
	{
		positions = new Vector3[infections.Length];
		for (int i = 0; i < infections.Length; i++) {
			positions [i] = infections [i].transform.localPosition;
		}
	}

	public override void activate ()
	{
		activated = true;
		interacted = false;
		transform.parent = Camera.main.transform;
		GameState.requestPause ();
		transform.localPosition = Vector3.zero;
		transform.parent = null;
		StartCoroutine ("resumeMovement");
	}

	public override void dismiss ()
	{
		activated = false;
		transform.position = originalPosition;
		GameObject.FindObjectOfType<CharacterSpeech> ().SpeechBubbleDisplay ("What would you\nlike help with?", true);

	}

	public override void reset ()
	{
		for (int j = 0; j < infections.Length; j++) {
			if (infections [j] == null) {
				GameObject newInfection = Instantiate (infectionPrefabs [Random.Range (0, 3)], positions [j], Quaternion.identity) as GameObject;
				newInfection.transform.parent = transform;
				newInfection.transform.localPosition = positions [j];
				infections [j] = newInfection.GetComponent<Infection> ();
			} else {
				infections [j].resetState ();
			}
		}
	}

	private IEnumerator resumeMovement ()
	{
		yield return new WaitForSeconds (1);
		GameState.requestPlay ();
	}

	private IEnumerator startDismissal ()
	{
		yield return new WaitForSeconds (1.2f);
		dismiss ();
	}

	void Update ()
	{
		if (activated && !interacted) {
			for (int i = 0; i < infections.Length; i++) {
				if (infections [i] == null) {
					interacted = true;
				} else {
					if (infections [i].touched ()) {
						interacted = true;
					}
				}
			}
			if (interacted) {
				StartCoroutine ("startDismissal");
			}
		}
	}
}

