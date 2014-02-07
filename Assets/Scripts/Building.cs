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
	private SceneManager sceneManager;
	private PainIndicator painIndicator;
	private AudioController audioController;
	public AudioClip[] clips;
	public bool doc;

	void Start ()
	{
		animate = GetComponent<Animator> ();
		player = GameObject.FindObjectOfType<PlayerControls> ();
		sceneManager = GameObject.FindObjectOfType<SceneManager> ();
		painIndicator = GameObject.FindObjectOfType<PainIndicator> ();
		audioController = GameObject.FindObjectOfType<AudioController> ();
	}
	
	void OnTriggerEnter2D (Collider2D coll)
	{
		if (coll.gameObject.Equals (player.gameObject)) {
			animate.SetTrigger ("Open");
			player.flash ();
			//TODO Include Object Model classes in building objects
			if (doc) {
				if (GameObject.FindObjectOfType<StopwatchController> () != null) {
					GameObject.FindObjectOfType<StopwatchController> ().stopStopwatch ();
				}
				int pillCount = PlayerPrefs.GetInt ("PILLS");
				if (pillCount < 3) {
					pillCount = pillCount + 1;
					PlayerPrefs.SetInt ("PILLS", pillCount);
					sceneManager.updatePillCount ();
				}
				audioController.objectInteraction (clips, 0.5f);
				player.gameObject.rigidbody2D.AddForce (new Vector2 (0, -50f));
				player.resetSpeed ();
			}
			painIndicator.objectInteraction (gameObject);
		}
	}

	public void resetState ()
	{
		animate.StopPlayback ();
		animate.SetTrigger ("Idle");
	}
	
}
