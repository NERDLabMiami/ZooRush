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

	public void objectInteraction (AudioClip[] clips)
	{
		audioModel.playSound (clips);
	}

}
