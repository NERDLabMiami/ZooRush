using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
	private GameObject character;

	void Start ()
	{
		character = GameObject.FindGameObjectWithTag ("character");
	}
	
	void Update ()
	{
		transform.localPosition = new Vector3 (character.transform.localPosition.x + 5f, transform.localPosition.y, transform.localPosition.z);
	}
}
