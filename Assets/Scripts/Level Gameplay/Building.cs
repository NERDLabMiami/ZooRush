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
		private PainIndicator painIndicator;
		private AudioController audioController;

		private Doctor doctor;

		public AudioClip[] clips;
		public bool doc;

		void Start ()
		{
				animate = GetComponent<Animator> ();
				player = GameObject.FindObjectOfType<PlayerControls> ();
				painIndicator = GameObject.FindObjectOfType<PainIndicator> ();
				audioController = GameObject.FindObjectOfType<AudioController> ();
				if (doc) {
						doctor = GetComponentInChildren<Doctor> ();
				}
		}
	
		void OnTriggerEnter2D (Collider2D coll)
		{
				if (coll.transform.parent != null && coll.transform.parent.gameObject.Equals (player.gameObject)) {
						animate.SetTrigger ("Open");
						player.flash ();

						//TODO Include Object Model classes in building objects
						if (doc) {
								doctor.react ();
								if (GameObject.FindObjectOfType<StopwatchController> () != null) {
										GameObject.FindObjectOfType<StopwatchController> ().stopStopwatch ();
								}
								if (GameObject.FindObjectOfType<EndlessSceneManager> () != null) {
										GameObject.FindObjectOfType<EndlessModePainKiller> ().incrementPillCount ();
								} else {
										GameObject.FindObjectOfType<PainKiller> ().incrementPillCount ();
								}
								audioController.objectInteraction (clips, 0.5f);
						}
						if (GameObject.FindObjectOfType<EndlessSceneManager> () != null) {
								GameObject.FindObjectOfType<EndlessPainIndicator> ().objectInteraction (gameObject);
						} else {
								painIndicator.objectInteraction (gameObject);
						}
				}
		}

		public void resetState ()
		{
				animate.StopPlayback ();
				animate.SetTrigger ("Idle");
				if (doc) {
						doctor.reset ();
				}
		}
	
}
