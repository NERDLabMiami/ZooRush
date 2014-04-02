using UnityEngine;
using System.Collections;

public class ObstacleSpawner : MonoBehaviour
{
	public GameObject prefab;
	
	public bool receivedSignal;
	private bool instatiated;
	
	void Start ()
	{
		instatiated = false;
		receivedSignal = false;
	}
	
	void Update ()
	{
		if (receivedSignal) {
			if (!instatiated) {
				InstatiateObject ();
			}
		}
	}
	
	private void InstatiateObject ()
	{
		Instantiate (prefab, transform.position, Quaternion.Euler (new Vector3 (0, 0, 0)));
		instatiated = true;
	}
}
