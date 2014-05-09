using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody2D))]
public class GenericRepeater : MonoBehaviour
{
	public Vector2 driftSpeed;
	private Vector2 stopped = Vector2.zero;
	private SpriteRenderer[] sprites;
	private Plane[] planes;
	private CameraFollow cameraFollower;
	private SpriteRenderer farthestRightObj;
	private bool endlessMode;

	void Start ()
	{
		sprites = GetComponentsInChildren<SpriteRenderer> ();
		cameraFollower = GameObject.FindObjectOfType<CameraFollow> ();
		endlessMode = GameObject.FindObjectOfType<EndlessSceneManager> () != null;
	}
	
	void FixedUpdate ()
	{
		speedUpdate ();
		if (endlessMode || cameraFollower.cameraSettled) {
			foreach (SpriteRenderer sprite in sprites) {
				if (sprite.bounds.max.x < Camera.main.transform.position.x - 10f && !inView (sprite)) { // if the block is not in view and to the left of the camera
					moveToEnd (sprite);
				}
			}
		} 
	}

	private bool inView (SpriteRenderer obj)
	{
		planes = GeometryUtility.CalculateFrustumPlanes (Camera.main);
		return GeometryUtility.TestPlanesAABB (planes, obj.bounds);
	}

	private void moveToEnd (SpriteRenderer obj)
	{
		Debug.Log ("MOVE TO END CALLED");
		float farthestRight = sprites [0].bounds.max.x;
		farthestRightObj = sprites [0];
		foreach (SpriteRenderer blk in sprites) {
			if (blk.bounds.max.x > farthestRight) {
				farthestRight = blk.bounds.max.x;
				farthestRightObj = blk;
			}
		}
		obj.transform.position = new Vector3 (farthestRight + farthestRightObj.bounds.extents.x, obj.transform.position.y, obj.transform.position.z);
	}

	private void speedUpdate ()
	{
		if (GameState.checkForState (GameState.States.Play) || GameState.checkForState (GameState.States.Launch)) {
			if (rigidbody2D.velocity != driftSpeed) {
				rigidbody2D.velocity = driftSpeed;
			}
		} else {
			if (rigidbody2D.velocity != stopped) {
				rigidbody2D.velocity = stopped;
			}
		}
	}
}
