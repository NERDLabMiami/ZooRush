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
	public AudioClip[] clips;
	
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
			//TODO Include Object Model classes in building objects
			if (gameObject.name.Contains ("Doctor") || gameObject.name.Contains ("First Aid")) {
				int pillCount = PlayerPrefs.GetInt ("PILLS");
				if (pillCount < 3) {
					pillCount = pillCount + 1;
					PlayerPrefs.SetInt ("PILLS", pillCount);
					GameObject.FindObjectOfType<SceneManager> ().updatePillCount ();
				}
				GameObject.FindObjectOfType<AudioController> ().objectInteraction (clips, 0.5f);
			}
			GameObject.FindObjectOfType<PainIndicator> ().objectInteraction (gameObject);
		}
	}

	public void resetState ()
	{
		animate.StopPlayback ();
		animate.SetTrigger ("Idle");
	}
	
}
