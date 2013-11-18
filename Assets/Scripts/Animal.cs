using UnityEngine;
using System.Collections;

/** Data and functions specific to Animal objects.
 * 
 * @author Ebtissam Wahman
 */ 
public class Animal : MonoBehaviour
{
	private GameObject netBoundary;
	private BoxCollider2D[] netColliders;
	private Animator animate;
	public string type;
	private bool caught = false;
	
	public GameObject net;
	
	void Start ()
	{
		animate = GetComponent<Animator> ();
		netBoundary = transform.FindChild ("Net Boundary").gameObject;
		netColliders = netBoundary.GetComponents<BoxCollider2D> ();
		foreach (BoxCollider2D netCol in netColliders) {
			netCol.isTrigger = true;
		}
	}
	
	void Update ()
	{
		
	}
	
	void OnTriggerEnter2D (Collider2D other)
	{
		if (!caught) {
			if (other.gameObject.name == "net(Clone)") {
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
}
