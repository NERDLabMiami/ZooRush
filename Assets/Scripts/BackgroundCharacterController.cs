using UnityEngine;
using System.Collections;

public class BackgroundCharacterController : MonoBehaviour
{

	public GameObject prefab;
	public float minWait;
	public float maxWait;
	private GameObject instance;
	
	void Update ()
	{
		if (instance == null) {
			StartCoroutine (delayGeneration ());
		}
	}

	IEnumerator delayGeneration ()
	{
		yield return new WaitForSeconds (Random.Range (minWait, maxWait));

		if (instance == null) {
			transform.localPosition = new Vector3 (transform.localPosition.x, Random.Range (-0.95f, -4.8f), transform.localPosition.z);
			instance = Instantiate (prefab, transform.position, Quaternion.identity) as GameObject;
		}
	}
}
