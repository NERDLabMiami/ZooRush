using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CloudGenerator : MonoBehaviour
{
	public bool enableCloudGeneration;
	public Sprite[] clouds; //cloud sprites
	private List<GameObject> cloudCollection;

	void Start ()
	{
		cloudCollection = new List<GameObject> ();
	}
	void FixedUpdate ()
	{
		if (enableCloudGeneration && GameStateMachine.currentState == (int)GameStateMachine.GameState.Play) {
			int randomNum = Random.Range (0, 10001);
			if (cloudCollection.Count < 20 && randomNum % 37 == 0) { //0.0001% chance per frame;
				cloudCollection.Add (createCloudObject ());
			}

			foreach (GameObject obj in cloudCollection) {
				if (obj.transform.position.x < Camera.main.transform.position.x - 25f) {
					cloudCollection.Remove (obj);
					Destroy (obj);
				}
			}
		}
	}

	private GameObject createCloudObject ()
	{
		int cloudIndex = Random.Range (0, clouds.Length);
		GameObject cloud = new GameObject ("Cloud" + cloudIndex, typeof(SpriteRenderer), typeof(Rigidbody2D));
		cloud.transform.position = transform.position;
		cloud.transform.position = new Vector3 (cloud.transform.position.x + Random.Range (0f, 5f), cloud.transform.position.y + Random.Range (-1f, 1f), cloud.transform.position.z);
		SpriteRenderer spriteRenderer = cloud.GetComponent<SpriteRenderer> ();
		spriteRenderer.sprite = clouds [cloudIndex];
		spriteRenderer.sortingLayerName = "Background";
		spriteRenderer.sortingOrder = 0;
		cloud.rigidbody2D.gravityScale = 0;
		cloud.rigidbody2D.velocity = new Vector2 (-1 * Random.Range (1f, 2f), 0);
		cloud.rigidbody2D.angularDrag = 0;
		cloud.rigidbody2D.angularVelocity = 0;
		cloud.rigidbody2D.fixedAngle = true;

		return cloud;
	}

}
