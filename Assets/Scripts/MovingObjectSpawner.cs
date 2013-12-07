using UnityEngine;
using System.Collections;

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
			Debug.Log ("BLAH!");
			detected.collider.gameObject.GetComponent<ObstacleSpawner> ().receivedSignal = true;
		}
		
	}
}
