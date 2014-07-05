using UnityEngine;
using System.Collections;

public class HelpMenuPainKiller : OtherButtonClass
{
	public int pillCount; // current count of Pills
	public bool interacted; //if an interaction has been registered
	public AudioClip clip; //sound clip played when pill is taken
	public Animator animator; //Animator for +/- 1 effect when interecting with painkiller button & doctors
	public TextMesh animationText; //Text that is animated after interaction
	public TextMesh countText; //current pill count text

	public void activate ()
	{
		interacted = false;
		pillCount = 1;
		countText.text = string.Format ("x{0}", pillCount);
	}

	public override void otherButtonAction (Button thisButton)
	{
		if (!interacted && Input.GetMouseButtonUp (0)) {
			interacted = true;
			decrementPillCount ();
			StartCoroutine (waitToResetTouch ()); //resets the intereacted tracker after a small period of time
		}
	}

	private void decrementPillCount ()
	{
		if (pillCount > 0) {
			--pillCount;
			GameObject.FindObjectOfType<CharacterSpeech> ().SpeechBubbleDisplay ("What a Relief!");
			animationText.text = "-1";
			animator.SetTrigger ("Decrement");
		}
	}

	private IEnumerator waitToResetTouch ()
	{
		yield return new WaitForSeconds (0.15f);
		interacted = false;
	}

	public void decrement ()
	{
		GameObject.FindObjectOfType<AudioController> ().objectInteraction (clip);
		animator.SetTrigger ("Reset");
		countText.text = string.Format ("x{0}", pillCount);
	}
}
