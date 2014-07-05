using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/**
 * Adds an outline around Objects with a Text Mesh. Obtained from: 
 * http://answers.unity3d.com/questions/542646/3d-text-strokeoutline.html
 * @author TheGering from unityAnswers , answers.unity3d.com
 */ 
public class TextOutline : MonoBehaviour
{
	public Camera parentCamera;
	public float pixelSize = 1;
	public Color outlineColor = Color.black;
	private TextMesh textMesh;
	private List<TextMesh> outlineText;

	void Start ()
	{
		textMesh = GetComponent<TextMesh> (); 
		outlineText = new List<TextMesh> ();
		MeshRenderer meshRenderer = GetComponent<MeshRenderer> ();
		
		for (int i = 0; i < 8; i++) {
			GameObject outline = new GameObject ("outline", typeof(TextMesh));
			outlineText.Add (outline.GetComponent<TextMesh> ());
			outline.transform.parent = transform;
			outline.transform.localScale = new Vector3 (1, 1, 1);
			
			MeshRenderer otherMeshRenderer = outline.GetComponent<MeshRenderer> ();
			otherMeshRenderer.material = new Material (meshRenderer.material);
			otherMeshRenderer.castShadows = false;
			otherMeshRenderer.receiveShadows = false;
			otherMeshRenderer.renderer.sortingLayerName = renderer.sortingLayerName;
			otherMeshRenderer.renderer.sortingOrder = renderer.sortingOrder;
		}
		GameObject currentParent = gameObject;

		if (parentCamera == null) {
			parentCamera = currentParent.camera;
			while (parentCamera == null) {
				currentParent = currentParent.transform.parent.gameObject;
				parentCamera = currentParent.camera;
			}
		}

//				Debug.Log ("CURRENT PARENT CAMERA IS AT " + parentCamera.name);
	}
	
	void LateUpdate ()
	{
		if (parentCamera.rect.xMax > 1.0 || parentCamera.rect.xMin < 0) {
			return;
		}
		Vector3 screenPoint = parentCamera.WorldToScreenPoint (transform.position);
		
//		outlineColor.a = textMesh.renderer.material.color.a;
		
		// copy attributes
		for (int i = 0; i < outlineText.Count; i++) {
			
			TextMesh other = outlineText [i];
			other.color = outlineColor;
			other.text = textMesh.text;
			other.alignment = textMesh.alignment;
			other.anchor = textMesh.anchor;
			other.characterSize = textMesh.characterSize;
			other.font = textMesh.font;
			other.fontSize = textMesh.fontSize;
			other.fontStyle = textMesh.fontStyle;
			other.richText = textMesh.richText;
			other.tabSize = textMesh.tabSize;
			other.lineSpacing = textMesh.lineSpacing;
			other.offsetZ = textMesh.offsetZ;
			other.renderer.sortingLayerName = renderer.sortingLayerName;
			other.renderer.sortingOrder = renderer.sortingOrder - 1;
			
			Vector3 pixelOffset = GetOffset (i) * pixelSize;
			Vector3 worldPoint = parentCamera.ScreenToWorldPoint (screenPoint + pixelOffset);
			other.transform.position = worldPoint;
		}
	}
	
	Vector3 GetOffset (int i)
	{
		switch (i % 8) {
		case 0:
			return new Vector3 (0, 1, 0);
		case 1:
			return new Vector3 (1, 1, 0);
		case 2:
			return new Vector3 (1, 0, 0);
		case 3:
			return new Vector3 (1, -1, 0);
		case 4:
			return new Vector3 (0, -1, 0);
		case 5:
			return new Vector3 (-1, -1, 0);
		case 6:
			return new Vector3 (-1, 0, 0);
		case 7:
			return new Vector3 (-1, 1, 0);
		default:
			return Vector3.zero;
		}
	}
}