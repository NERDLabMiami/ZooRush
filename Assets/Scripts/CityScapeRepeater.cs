using UnityEngine;
using System.Collections;

public class CityScapeRepeater : MonoBehaviour
{

	private SpriteRenderer[] cityblocks;
	private CameraFollow cameraFollower;
	private Vector2 moving = new Vector2 (-0.5f, 0);
	private Vector2 stopped = Vector2.zero;

	void Start ()
	{
		cameraFollower = GameObject.FindObjectOfType<CameraFollow> ();
		cityblocks = GetComponentsInChildren<SpriteRenderer> ();
	}

	void FixedUpdate ()
	{
		speedUpdate ();
		if (cameraFollower.cameraSettled) {
			foreach (SpriteRenderer block in cityblocks) {
				if (block.bounds.max.x < Camera.main.transform.position.x - 50f && !inView (block)) { // if the block is not in view and to the left of the camera
					moveToEnd (block);
				}
			}
		} 
	}

	private bool inView (SpriteRenderer obj)
	{
		Plane[] planes = GeometryUtility.CalculateFrustumPlanes (Camera.main);
		return GeometryUtility.TestPlanesAABB (planes, obj.bounds);
	}

	private void moveToEnd (SpriteRenderer obj)
	{
		float farthestRight = cityblocks [0].bounds.max.x;
		SpriteRenderer farthestRightObj = cityblocks [0];
		foreach (SpriteRenderer blk in cityblocks) {
			if (blk.bounds.max.x > farthestRight) {
				farthestRight = blk.bounds.max.x;
				farthestRightObj = blk;
			}
		}
		obj.transform.position = new Vector3 (farthestRight + farthestRightObj.bounds.extents.x, obj.transform.position.y, obj.transform.position.z);
	}

	private void speedUpdate ()
	{
		if (GameStateMachine.currentState == (int)GameStateMachine.GameState.Play) {
			if (rigidbody2D.velocity != moving) {
				rigidbody2D.velocity = moving;
			}
		} else {
			if (rigidbody2D.velocity != stopped) {
				rigidbody2D.velocity = stopped;
			}
		}
	}
}
