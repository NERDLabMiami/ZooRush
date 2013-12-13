using UnityEngine;
using System.Collections;

/** Spawns a random moving object (with and without collisions) depending on the level being played.
 * @author: Ebtissam Wahman
 */ 
public class MovingObjectSpawner : MonoBehaviour
{
	public LayerMask layerMask;

	private SceneManager sceneManager;
	
	void Start ()
	{
		sceneManager = GameObject.FindObjectOfType<SceneManager> ();
	}
	
	void Update ()
	{
		
		RaycastHit2D detected = Physics2D.Raycast (transform.position, Vector2.up, 250f, layerMask);
		if (detected.collider != null) {
			detected.collider.gameObject.GetComponent<ObstacleSpawner> ().receivedSignal = true;
		}
		
	}
}
