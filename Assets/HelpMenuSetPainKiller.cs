using UnityEngine;
using System.Collections;

public class HelpMenuSetPainKiller : HelpMenuSet
{
	public HelpMenuPainKiller painKillerTapHandler;
	private bool interacted;

	public override void activate ()
	{
		painKillerTapHandler.activate ();
		activated = true;
		interacted = false;
		transform.parent = Camera.main.transform;
		transform.localPosition = Vector3.zero;
	}
	
	public override void dismiss ()
	{
		activated = false;
		transform.parent = null;
		transform.position = originalPosition;
		GameObject.FindObjectOfType<CharacterSpeech> ().SpeechBubbleDisplay ("What would you\nlike help with?", true);
	}

	public override void reset ()
	{
		painKillerTapHandler.pillCount = 1;
	}

	void Update ()
	{
		if (activated && painKillerTapHandler.interacted && !interacted) {
			interacted = false;
			StartCoroutine ("startDismissal");
		}
	}

	private IEnumerator startDismissal ()
	{
		yield return new WaitForSeconds (4);
		dismiss ();
	}

}
