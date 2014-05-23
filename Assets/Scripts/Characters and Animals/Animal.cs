using UnityEngine;
using System.Collections;

/** Data and functions specific to Animal objects.
 * 
 * @author Ebtissam Wahman
 */ 
public abstract class Animal : MonoBehaviour
{
		public enum AnimalValues
		{
				Bear = 0,
				Cheetah,
				Crocodile,
				Elephant,
				Flamingo,
				Gorilla,
				Ostrich,
				Penguin,
				Rhino,
				Tortoise,
				COUNT
	}
		;//Animal values set as enums for better readability
	
		public bool caught; //Indicator for whehter the Animal has been caught by the player
		public Vector2 speed; //Current speed of the animal object
		public AudioClip audioClip; // Animal audio sound clip
		public Button touchZone;

		public Animator animator; //Animator for the animal's running sprites
		protected AudioSource audioSource; //Audio Source that plays sound clip

//		public virtual void otherButtonAction (Button thisButton)
//		{
//				GameObject.FindObjectOfType<NetLauncher> ().throwNet ();
//		}
		
		public abstract void getAway ();

}
