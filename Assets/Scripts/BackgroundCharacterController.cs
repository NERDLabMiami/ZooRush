using UnityEngine;
using System.Collections;

public class BackgroundCharacterController : MonoBehaviour
{

	public bool OneShot;
	public GameObject prefab;
	public float minWait;
	public float maxWait;
	private GameObject instance;
	private bool waiting;

	void Start ()
	{
		waiting = false;
	}
	
	void Update ()
	{
		if (!waiting && !OneShot) {
			StartCoroutine (delayGeneration ());
		}
	}

	public void createInstance ()
	{
		instance = Instantiate (prefab, transform.position, Quaternion.identity) as GameObject;
	}

	IEnumerator delayGeneration ()
	{
		waiting = true;
		yield return new WaitForSeconds (Random.Range (minWait, maxWait));

		if (instance == null) {
			transform.localPosition = new Vector3 (transform.localPosition.x, Random.Range (-0.95f, -4.8f), transform.localPosition.z);
			instance = Instantiate (prefab, transform.position, Quaternion.identity) as GameObject;
		}
		waiting = false;
	}
}
