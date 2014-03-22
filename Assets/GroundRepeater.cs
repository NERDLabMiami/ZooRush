using UnityEngine;
using System.Collections;

public class GroundRepeater : MonoBehaviour
{

	private SpriteRenderer[] blocks;
	private CameraFollow cameraFollower;

	// Use this for initialization
	void Start ()
	{
		blocks = GetComponentsInChildren<SpriteRenderer> ();
		cameraFollower = GameObject.FindObjectOfType<CameraFollow> ();
	
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		if (cameraFollower.cameraSettled) {
			foreach (SpriteRenderer block in blocks) {
				if (block.bounds.max.x < Camera.main.transform.position.x && !inView (block)) { // if the block is not in view and to the left of the camera
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
		float farthestRight = blocks [0].bounds.max.x;
		SpriteRenderer farthestRightObj = blocks [0];
		foreach (SpriteRenderer blk in blocks) {
			if (blk.bounds.max.x > farthestRight) {
				farthestRight = blk.bounds.max.x;
				farthestRightObj = blk;
			}
		}
		obj.transform.position = new Vector3 (farthestRight + farthestRightObj.bounds.extents.x, obj.transform.position.y, obj.transform.position.z);
	}
}


