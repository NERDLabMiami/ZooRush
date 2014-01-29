using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class AudioController : MonoBehaviour
{

	public string location; //The level's current location, can be the Zoo, Suburbs, City, etc.
	private AudioModel audioModel;
	private static Dictionary<string,string> soundDirectories;

	void Awake ()
	{

		soundDirectories = new Dictionary<string,string> ();

		//ANIMAL SOUNDS
		soundDirectories.Add ("CHEETAH", "Sounds/Cheetah");
		soundDirectories.Add ("CROCODILE", "Sounds/Crocodile");
		soundDirectories.Add ("ELEPHANT", "Sounds/Elephant");
		soundDirectories.Add ("FLAMINGO", "Sounds/Flamingo");
		soundDirectories.Add ("GORILLA", "Sounds/Gorilla");
		soundDirectories.Add ("OSTRITCH", "Sounds/Ostritch");
		soundDirectories.Add ("PENGUIN", "Sounds/Penguin");
		soundDirectories.Add ("RHINO", "Sounds/Rhino");



		//SOUND EFFECTS
		soundDirectories.Add ("CAPTURED", "Sounds/SOUND_FXS/CAPTURED");
		soundDirectories.Add ("DOCTOR", "Sounds/SOUND_FXS/DOCTOR");
		soundDirectories.Add ("GAMEOVER", "Sounds/SOUND_FXS/GAMEOVER");
		soundDirectories.Add ("HARDSICKLOOP", "Sounds/SOUND_FXS/HARDSICKLOOP");
		soundDirectories.Add ("INFECTION", "Sounds/SOUND_FXS/INFECTION");
		soundDirectories.Add ("JUMP", "Sounds/SOUND_FXS/JUMP");
		soundDirectories.Add ("PILL", "Sounds/SOUND_FXS/PILL");
		soundDirectories.Add ("WATERDRINK", "Sounds/SOUND_FXS/WATERDRINK");
		soundDirectories.Add ("SOFTSICKLOOP", "Sounds/SOUND_FXS/SOFTSICKLOOP");
		soundDirectories.Add ("SIGH", "Sounds/SOUND_FXS/SIGH"); 
		soundDirectories.Add ("BALL", "Sounds/SOUND_FXS/BALL"); 
		
		//MUSIC
		soundDirectories.Add ("Zoo", "Sounds/Zoo/Zoo");
		soundDirectories.Add ("Highway(SingleLoop)", "Sounds/Highway/Highway(SingleLoop)");

	}

	void Start ()
	{
		audioModel = FindObjectOfType<AudioModel> ();


	}
	
	void Update ()
	{
	
	}

	private AudioClip loadAudioClipToMemory (string soundName)
	{
		string directory;
		soundDirectories.TryGetValue (soundName, out directory);
		return Resources.Load (directory, typeof(AudioClip)) as AudioClip;
	}

	public void objectInteraction (GameObject obj)
	{

	}

}
