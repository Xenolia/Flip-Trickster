// dnSpy decompiler from Assembly-CSharp.dll class: Challenges
using System;
using UnityEngine;

public class Challenges : MonoBehaviour
{
	private void Start()
	{
		if (LvlBtnHandler.activeLevel == 7 && LvlBtnHandler.activeStage == 5)
		{
			Challenges.bigtext = true;
		}
		if (LvlBtnHandler.activeLevel == 6 && LvlBtnHandler.activeStage == 5)
		{
			Challenges.bigtext = true;
		}
	}

	public static bool bigtext;

	public static int redZoneCount;

	public static int yellowZoneCount;

	public static int flipCount;

	public static int targetCount;

	public static int noTuckCount;

	public static bool[] gymSpecial = new bool[8];

	public static bool[] mountSpecial = new bool[8];

	public static bool[] gallerySpecial = new bool[8];

	public static bool[] citySpecial = new bool[8];

	public static bool[] houseSpecial = new bool[8];

	public static bool[] shipSpecial = new bool[8];

	public static bool[] islandSpecial = new bool[8];

	public static bool[] hauntedSpecial = new bool[8];

	public static bool[] ufoSpecial = new bool[8];
}
