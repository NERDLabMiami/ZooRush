using UnityEngine;
using System.Collections;

/** Data and functions specific to Animal objects.
 * 
 * @author Ebtissam Wahman
 */ 
public class Animal : MonoBehaviour
{
	private GameObject netBoundary;
	private BoxCollider2D[] netColliders;
	private Animator animate;
	public bool caught;
	
	public GameObject net;
	
	public Vector2 speed;
	
	private SceneManager sceneManager;
	private bool play;
	private bool prevPlay;
	
	void Start ()
	{
		sceneManager = FindObjectOfType<SceneManager> ();
		play = sceneManager.isPlaying;
		prevPlay = play;
		speed = new Vector2 (6.5f, 0f);
		caught = false;
		animate = GetComponent<Animator> ();
		netBoundary = transform.FindChild ("Net Boundary").gameObject;
		netColliders = netBoundary.GetComponents<BoxCollider2D> ();
		foreach (BoxCollider2D netCol in netColliders) {
			netCol.isTrigger = true;
		}
		transform.parent.rigidbody2D.velocity = speed;
	}
	
	void Update ()
	{
		prevPlay = play;
		if (sceneManager.isPlaying) {
			play = true;
		} else {// otherwise keep track that the input is not active
			play = false;
		}
		if (!prevPlay && play) { //our previous state is the paused state, we are now going into the play state
			StartCoroutine (waitToResume (0.1f));
		} else { // our previous state is the play state
			if (!play) {//we need to move into the paused state
				animate.SetTrigger ("Idle");
				transform.parent.rigidbody2D.velocity = new Vector2 (0f, 0f);
			}
		}
	}
	
	void OnTriggerEnter2D (Collider2D other)
	{
		if (!caught) {
			if (other.gameObject.name == "net(Clone)") {
				transform.parent.rigidbody2D.velocity = new Vector2 (0f, 0f);
				foreach (BoxCollider2D netCol in netColliders) {
					netCol.isTrigger = false;
				}
				other.gameObject.GetComponent<Animator> ().SetTrigger ("Open");
				if (/*other.rigidbody2D.velocity.x < 0.3f &&*/ !caught) {
					animate.SetTrigger ("Idle");
					caught = true;
				}
			}
		}
		
	}
	
	private IEnumerator waitToResume (float time)
	{
//		animate.SetTrigger ("Flash");
		yield return new WaitForSeconds (time);
		transform.parent.rigidbody2D.velocity = speed;
	}
}
