using UnityEngine;
using System.Collections;

public class MainMenuClouds : MonoBehaviour
{
	public SpriteRenderer[] clouds; //cloud sprites

	void Start ()
	{
		clouds = GetComponentsInChildren<SpriteRenderer> ();
		rigidbody2D.velocity = new Vector2 (-0.5f, 0);
	}
	
	void FixedUpdate ()
	{
		foreach (SpriteRenderer obj in clouds) {
			if (obj.transform.position.x < Camera.main.transform.position.x && !inView (obj)) {
				recycleCloud (obj.gameObject);
			}
		}
	}

	private bool inView (Renderer obj)
	{
		Plane[] planes = GeometryUtility.CalculateFrustumPlanes (Camera.main);
		return GeometryUtility.TestPlanesAABB (planes, obj.bounds);
	}

	private void recycleCloud (GameObject cloud)
	{
		cloud.transform.position = new Vector3 (cloud.transform.position.x + Camera.main.transform.position.x + Random.Range (25f, 27f), cloud.transform.position.y, cloud.transform.position.z);
	}

}
