using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class GameOptions : MonoBehaviour
{
	public Rect MusicLabel;
	public Rect MusicOn;
	public Rect MusicOff;
	public Rect SoundLabel;
	public Rect SoundOn;
	public Rect SoundOff;
	public Rect VolumeLabel;
	public Rect VolumeSlider;
	public Rect CharacterSelect;
	private float volume = 1f;

	public GUIStyle LabelStyle = null;
	public GUIStyle ButtonStyle = null;

	void OnGUI ()
	{
		Debug.Log ("Screen Width " + Screen.width);
		Debug.Log ("Screen Height " + Screen.height);
		GUI.Label (MusicLabel, "Music", LabelStyle);
		{
			if (GUI.Button (MusicOn, "ON", ButtonStyle)) {
				PlayerPrefs.SetString ("Music", "ON");
			}
			if (GUI.Button (MusicOff, "OFF", ButtonStyle)) {
				PlayerPrefs.SetString ("Music", "OFF");
			}
		}

		GUI.Label (SoundLabel, "Sound", LabelStyle);
		{
			if (GUI.Button (SoundOn, "ON", ButtonStyle)) {
				PlayerPrefs.SetString ("Sound", "ON");
			}
			if (GUI.Button (SoundOff, "OFF", ButtonStyle)) {
				PlayerPrefs.SetString ("Sound", "OFF");
			}
		}

		GUI.Label (VolumeLabel, "Volume", LabelStyle);
		{
			volume = GUI.HorizontalSlider (VolumeSlider, volume * 100f, 0f, 100f) / 100f;
		}
		GUI.Label (CharacterSelect, "Character Select", LabelStyle);

	}
}
