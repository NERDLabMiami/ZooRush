using UnityEngine;
using System.Collections;


/**
 * 
 * 
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

		public abstract void getAway ();
		public abstract void setSpeed ();
}
