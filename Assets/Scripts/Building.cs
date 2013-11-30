using UnityEngine;
using System.Collections;

public class Building : MonoBehaviour
{
	private Animator animate;
	private PlayerControls player;
	
	void Start ()
	{
		animate = GetComponent<Animator> ();
		player = GameObject.FindObjectOfType<PlayerControls> ();
	}
	
	void OnTriggerEnter2D (Collider2D coll)
	{
		if (coll.gameObject.Equals (player.gameObject)) {
			animate.SetTrigger ("Open");
			if (gameObject.name.Contains ("Doctor")) {
				GameObject.FindObjectOfType<AudioHandler> ().playSound ("DOCTOR");
			}
		}
	}

	public void resetState ()
	{
		Debug.Log ("RESETTING");
		animate.StopPlayback ();
		animate.SetTrigger ("Idle");
	}
	
}
