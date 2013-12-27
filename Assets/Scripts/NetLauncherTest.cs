using UnityEngine;
using System.Collections;

public class NetLauncherTest : MonoBehaviour
{
	public Rigidbody2D prefab;
	public float speed;	

	private GameObject net;
	private bool firing;

	void Start ()
	{
		firing = false;
	}

	void FixedUpdate ()
	{
		if (!InputManager.enter) {
			firing = false;
		}
		if (InputManager.enter && !firing) {
			firing = true;
			net = fire ().gameObject;
		}
//		if (net != null && net.rigidbody2D.velocity.x < 1f) {
//			Destroy (net);
//		}
	}

	public void resetNetLauncher ()
	{
		Destroy (net);
		firing = false;
	}

	private Rigidbody2D fire ()
	{
		Rigidbody2D netInstance = Instantiate (prefab, transform.position, Quaternion.Euler (new Vector3 (0, 0, 0))) as Rigidbody2D;
		netInstance.velocity = new Vector2 (speed, 15f);
		return netInstance;
	}

}