using UnityEngine;
using System.Collections;

/** Controls the sorting layer order of sprites in the play area.
 * @author: Ebtissam Wahman
 */ 
public class SortingLayerController : MonoBehaviour
{
	private SpriteRenderer spriteRenderer;

	void Awake ()
	{
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}
	
	void FixedUpdate ()
	{
		//values above the camera's center are more and more negative (behind). 
		//while values below the camera's center are more and more positve (in front)
		spriteRenderer.sortingOrder = Mathf.Abs ((int)(100f * transform.position.y));
	}
}
