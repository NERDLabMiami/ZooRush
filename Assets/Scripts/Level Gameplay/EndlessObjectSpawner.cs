using UnityEngine;
using System.Collections;

public class EndlessObjectSpawner : MonoBehaviour
{

		public float maxOffsetFromCenter; //We assume spawner object is the center
		public GameObject objectPrefab;
		public float minimumWaitTime;
		public float maximumWaitTime;

		private bool started;
		public GameObject parentObject; //Leave null if you want to be at the top of the hierachy

		void Start ()
		{
				started = false;
		}

		void OnEnable ()
		{
				GameState.StateChanged += OnStateChanged;
		}
	
		void OnDisable ()
		{
				GameState.StateChanged -= OnStateChanged;
		}

		void FixedUpdate ()
		{
				if (!started && (GameState.checkForState (GameState.States.Play) || GameState.checkForState (GameState.States.Launch))) {
						started = true;
						Debug.Log ("STARTED SET TO TRUE FROM FIXED UPDATE");
						startSpawnSequence ();
            
				}
		}
	
		private void OnStateChanged ()
		{
				if (GameState.checkForState (GameState.States.Play) || GameState.checkForState (GameState.States.Launch)) {
						if (!started) {
								started = true;
								startSpawnSequence ();
								Debug.Log ("STARTED SET TO TRUE FROM ON STATE CHANGED");
                
						}
				} else {
						started = false;
				}
		}
		
		private void startSpawnSequence ()
		{
				StartCoroutine (spawnTimer ());
		}

		private IEnumerator spawnTimer ()
		{
				float waitTime = Random.Range (minimumWaitTime, maximumWaitTime);
				yield return new WaitForSeconds (waitTime);
				if (started) {
						spawnObject ();
						StartCoroutine (spawnTimer ());
				} else {
						Debug.Log ("Object spawn sequence canceled");
				}
		}

		private GameObject spawnObject ()
		{
				if (started) {
						Debug.Log ("SPAWNING!");
						Vector3 objectPosition = transform.position;
						objectPosition.y = Random.Range (transform.localPosition.y - maxOffsetFromCenter, 
		                                 transform.localPosition.y + maxOffsetFromCenter);
						GameObject spawnedObject = GameObject.Instantiate (objectPrefab, objectPosition, Quaternion.identity) as GameObject;
						if (parentObject != null) {
								spawnedObject.transform.parent = parentObject.transform;
						}
				}
				return null;
		}
}
