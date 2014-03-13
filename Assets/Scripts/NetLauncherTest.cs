using UnityEngine;
using System.Collections;

//public class NetLauncherTest : MonoBehaviour
//{
//	public bool launchEnabled;
//	
//	public Rigidbody2D prefab;
//	public float speed;	
//
//	private bool firing;
//	private int throwCount; //number of tries that have been made 
//	
//
//	void Start ()
//	{
//		firing = false;
//		throwCount = 0;
//	}
//
//	void FixedUpdate ()
//	{
//		if (!InputManager.enter) {
//			firing = false;
//		}
//		if (InputManager.enter && !firing && throwCount < 3) {
//			firing = true;
//			fire ();
//		} else {
//			if (throwCount >= 3) {
//				Debug.Log ("RESET");
//				throwCount = 0;
//			}
//		}
//	}
//
//	private void fire ()
//	{
//		Rigidbody2D netInstance = Instantiate (prefab, transform.position, prefab.transform.rotation) as Rigidbody2D;
//		netInstance.velocity = new Vector2 (speed, 0f);
//		throwCount += 1;
//		
//	}
//}