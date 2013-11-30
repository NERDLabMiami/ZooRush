using UnityEngine;
using System.Collections;

/** Keeps track of the current Level's score.
 * @author Ebtissam Wahman
 */ 
public class ScoreKeeper : MonoBehaviour
{

	private int redInfectionCount;
	private int yellowInfectionCount;
	private int greenInfectionCount;

	private int doctorVisits;

	private int waterBottleCount;
	private bool pillUsed;

	void Start ()
	{
		redInfectionCount = 0;
		yellowInfectionCount = 0;
		greenInfectionCount = 0;
		
		waterBottleCount = 0;
		pillUsed = false;
	}

	public void addToCount (GameObject obj)
	{
		if (obj.name.Contains ("Infection")) {
			if (obj.name.Contains ("Red")) {
				redInfectionCount += 1;
			} else {
				if (obj.name.Contains ("Yellow")) {
					yellowInfectionCount += 1;
				} else { // infection is green
					greenInfectionCount += 1;
				}
			}
			
		} else {
			if (obj.name.Contains ("Power Up")) {
				if (obj.name.Contains ("Water Bottle")) {
					waterBottleCount += 1;
				} else {
					pillUsed = true;
				}
			}
		}
	}

	public bool pillBottleUsed ()
	{
		return pillUsed;
	}

	public int[] getScore ()
	{
		int[] score = new int[] {
			redInfectionCount,
			yellowInfectionCount,
			greenInfectionCount,
			waterBottleCount,
			(pillUsed) ? 1 : 0
		};
		return score;
	}
}
