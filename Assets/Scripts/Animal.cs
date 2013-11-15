using UnityEngine;
using System.Collections;

public class Animal : MonoBehaviour
{
	private GameObject netBoundary;
	private BoxCollider2D[] netColliders;
	private Animator animate;
	public string type	;
	private bool caught = false;
	
	// Use this for initialization
	void Start ()
	{
		animate = GetComponent<Animator> ();
		netBoundary = transform.FindChild ("Net Boundary").gameObject;
		netColliders = netBoundary.GetComponents<BoxCollider2D> ();
		foreach (BoxCollider2D netCol in netColliders) {
			netCol.isTrigger = true;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
	
	void OnTriggerEnter2D (Collider2D other)
	{
		if (!caught) {
			foreach (BoxCollider2D netCol in netColliders) {
				netCol.isTrigger = false;
			}
			other.gameObject.GetComponent<Animator> ().SetTrigger ("Open");
			if (other.rigidbody2D.velocity.x < 0.3f && !caught) {
				animate.SetTrigger ("Idle");
				caught = true;
			}
		}
		
	}
}
