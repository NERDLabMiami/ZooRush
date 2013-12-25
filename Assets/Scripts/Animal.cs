using UnityEngine;
using System.Collections;

/** Data and functions specific to Animal objects.
 * 
 * @author Ebtissam Wahman
 */ 
public class Animal : MonoBehaviour
{
	private Animator animate;
	public bool caught;
	
	public GameObject net;
	private GameObject netBoundary;
	private BoxCollider2D[] netColliders;
	private Vector3 netSize;
	
	public Vector2 speed;
	
	private SceneManager sceneManager;
	private bool play;
	private bool prevPlay;
	
	private bool yVelocityChanged;
	private float upperBounds;
	private float lowerBounds;
	
	
	void Start ()
	{
		yVelocityChanged = false;
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
		netSize = new Vector3 (1f, 1f, 1f);
		upperBounds = GameObject.Find ("Upper Limit").transform.position.y;	
		lowerBounds = GameObject.Find ("Lower Limit").transform.position.y;
	}
	
	void Update ()
	{
		animate.SetFloat ("Speed", transform.parent.rigidbody2D.velocity.x);
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
				transform.parent.rigidbody2D.velocity = new Vector2 (0f, 0f);
			}
		}
		
		if (play) {
			if (yVelocityChanged) {
				Debug.Log ("Velocity Change Registered");
				StartCoroutine (waitToResume (1f));
				yVelocityChanged = false;
			} else {
				changeY ();
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
				if (gameObject.name.Equals ("Gorilla") || gameObject.name.Equals ("Rhino")) {
					other.gameObject.GetComponent<Animator> ().SetBool ("Big", true);
				}
				other.gameObject.GetComponent<Animator> ().SetTrigger ("Open");
				changeNetSize ();
				other.transform.localScale = netSize;
				if (/*other.rigidbody2D.velocity.x < 0.3f &&*/ !caught) {
					caught = true;
					GameObject.FindObjectOfType<ScoreKeeper> ().addToCount (gameObject);
				}
			}
		}
		
	}
	
	private void changeNetSize ()
	{
		switch (gameObject.name) {
		case"Tortoise": 
			netSize = new Vector3 (1f, 1f, 1f);
			break;
		case "Crocodile":
			netSize = new Vector3 (1.75f, 1.25f, 1f);
			break;
		case "Flamingo":
			netSize = new Vector3 (0.7f, 1.35f, 1f);
			break;
		case "Gorilla":
			netSize = new Vector3 (1f, 1f, 1f);
			break;
		case "Rhino":
			netSize = new Vector3 (1.6f, 1f, 1f);
			break;
		default:
			netSize = new Vector3 (1f, 1f, 1f);
			break;
		}
	}
	
	private void changeY ()
	{
		int moveChance = Random.Range (1, 101);
		if (moveChance % 50 == 0) {//2% chance
			float newYVelocity = Random.Range (-4.5f, 4.5f);
			Vector3 newSpeed = speed;
			newSpeed.y = newYVelocity;
			transform.parent.rigidbody2D.velocity = newSpeed;
			yVelocityChanged = true;
		} else {
			yVelocityChanged = false;
		}
		
	}
	private IEnumerator waitToResume (float time)
	{
		yield return new WaitForSeconds (time);
		transform.parent.rigidbody2D.velocity = speed;
	}
}
