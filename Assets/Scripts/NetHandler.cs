using UnityEngine;
using System.Collections;

public class NetHandler : MonoBehaviour
{
	
	public Sprite[] sprites;

	void Start ()
	{
	
	}
	
	void Update ()
	{
		if (rigidbody2D.velocity.y < 0f) {
			Debug.Log ("DOWN!!!");
			GetComponent<SpriteRenderer> ().sprite = sprites [1];
		}
	}
	
	void OnTriggerEnter2D (Collider2D other)
	{
//		Debug.Log (other.gameObject.name);
		
	}
	
	void OnCollisionEnter2D (Collision2D collision)
	{
		Debug.Log (collision.gameObject.name);
//		if (collision.gameObject.name.Contains ("Animal") != null) {
//			Debug.Log ("AMINAL!");
//		}
	}
	
}
