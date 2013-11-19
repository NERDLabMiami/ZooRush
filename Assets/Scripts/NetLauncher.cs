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
	public float speed = 20f;	
	private float action;
	private bool firing = false;
	
	void Start ()
	{
	}
	
	void FixedUpdate ()
	{
		if (launchEnabled) {
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
		
	private Rigidbody2D fire ()
	{
		Rigidbody2D netInstance = Instantiate (prefab, transform.position, Quaternion.Euler (new Vector3 (0, 0, 0))) as Rigidbody2D;
		netInstance.velocity = new Vector2 (speed, 15f);
		return netInstance;
	}
	
}
