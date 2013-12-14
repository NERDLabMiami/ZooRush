using UnityEngine;
using System.Collections;

public class Clickable : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	void OnMouseUp ()
	{
		if (gameObject.name.Contains ("Pill")) {
			Debug.Log ("CLUCK CLUCK");
			GameObject.FindObjectOfType<PainBar> ().objectInteraction (gameObject);
		}
	}
}