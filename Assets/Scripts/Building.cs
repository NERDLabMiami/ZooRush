using UnityEngine;
using System.Collections;


/** Script for character interactable buildings.
 * @author: Ebtissam Wahman
 * 
 */ 
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
			player.flash ();
			if (gameObject.name.Contains ("Doctor") || gameObject.name.Contains ("First Aid")) {
				GameObject.FindObjectOfType<AudioHandler> ().playSound ("DOCTOR", "SIGH", 0.5f);
			}
			GameObject.FindObjectOfType<PainBar> ().objectInteraction (gameObject);
		}
	}

	public void resetState ()
	{
		animate.StopPlayback ();
		animate.SetTrigger ("Idle");
	}
	
}
