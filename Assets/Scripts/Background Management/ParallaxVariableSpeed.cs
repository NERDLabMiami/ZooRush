using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody2D))]
public class ParallaxVariableSpeed : MonoBehaviour
{
		public float offestFromCharacter;	
		public Rigidbody2D character;

		void Update ()
		{
				if (character.velocity.x <= offestFromCharacter) {
						rigidbody2D.velocity = Vector2.zero;
				} else {
						rigidbody2D.velocity = new Vector2 (character.velocity.x - offestFromCharacter, 0);
				}
		}

}
