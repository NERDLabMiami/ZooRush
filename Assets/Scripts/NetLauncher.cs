using UnityEngine;
using System.Collections;

/** Launches A net at the animal being captured.
 * 
 * @author Ebtissam Wahman
 */ 

public class NetLauncher : MonoBehaviour
{
	public Rigidbody2D prefab;
	public bool launchEnabled;
	private GameObject net;
	public float speed;	
	private float action;
	private bool firing;
	private bool animalCaught;
	private int throwCount;
	void Start ()
	{
		launchEnabled = false;
		firing = false;
		speed = 20f;
		animalCaught = false;
		throwCount = 0;
	}
	
	void FixedUpdate ()
	{
		animalCaught = GameObject.FindObjectOfType<Animal> ().caught;
		if (launchEnabled) {
			Debug.Log ("ENABLED!");
			//TODO Add Enabled Indicator
			if (throwCount >= 3) {
				GameObject.FindObjectOfType<PlayerControls> ().resetSpeed ();
				throwCount = 0;
			} else {
				action = Input.GetAxis ("Fire1");
				if (action == 0) {
					firing = false;
				}
				if (action != 0 && !firing && net == null) {
					firing = true;
					net = fire ().gameObject;
				}
			}

		}
		if (net != null && !animalCaught && net.rigidbody2D.velocity.x < 1f) {
			Destroy (net);
		}
	}
		
	private Rigidbody2D fire ()
	{
		Rigidbody2D netInstance = Instantiate (prefab, transform.position, Quaternion.Euler (new Vector3 (0, 0, 0))) as Rigidbody2D;
		netInstance.velocity = new Vector2 (speed, 15f);
		throwCount++;
		return netInstance;
	}
	
}
