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
	private int action;
	private bool firing;
	private bool animalCaught;
	private int throwCount;
	
	private Renderer[] throwAlert;
	
	void Start ()
	{
		launchEnabled = false;
		firing = false;
		speed = 20f;
		animalCaught = false;
		throwCount = 0;
		throwAlert = GameObject.Find ("Throw Alert").GetComponentsInChildren<Renderer> ();
	}
	
	void FixedUpdate ()
	{
		animalCaught = GameObject.FindObjectOfType<Animal> ().caught;
		if (launchEnabled) {
			foreach (Renderer renderer in throwAlert) {
				renderer.renderer.enabled = true;
			}
			if (throwCount >= 3) {
				GameObject.FindObjectOfType<PlayerControls> ().resetSpeed ();
				throwCount = 0;
			} else {
				if (GameObject.FindObjectOfType<AnimalTouched> ().touched /*|| Input.GetAxis ("Fire1") > 0*/) {
					action = 1;
				} else {
					action = 0;
				}
				
				if (action == 0) {
					firing = false;
				}
				if (action != 0 && !firing && net == null) {
					firing = true;
					net = fire ().gameObject;
				}
			}
		} else {
			foreach (Renderer renderer in throwAlert) {
				renderer.renderer.enabled = false;
			}
		}
		if (net != null && !animalCaught && net.rigidbody2D.velocity.x < 1f) {
			Destroy (net);
		}
	}
	
	public void resetNetLauncher ()
	{
		Destroy (net);
		launchEnabled = false;
		firing = false;
		animalCaught = false;
		throwCount = 0;
		foreach (Renderer renderer in throwAlert) {
			renderer.renderer.enabled = false;
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
