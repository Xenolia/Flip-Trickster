// dnSpy decompiler from Assembly-CSharp.dll class: Foot
using System;
using UnityEngine;

public class Foot : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	private void OnCollisionEnter(Collision other)
	{
		if (other.collider.tag == "Killer")
		{
			GameObject obj = UnityEngine.Object.Instantiate<GameObject>(this.thud, base.transform.position, Quaternion.identity);
			UnityEngine.Object.Destroy(obj, 1f);
			if (other.gameObject.name == "Block")
			{
				PlayerBF.landedOnObs = true;
			}
			if (other.gameObject.name == "Pole")
			{
				PlayerBF.landedOnObs = true;
			}
			if (other.gameObject.name == "Fence")
			{
				PlayerBF.landedOnObs = true;
			}
			if (other.gameObject.name == "Wheel")
			{
				PlayerBF.landedOnObs = true;
			}
			if (other.gameObject.name == "Antenna")
			{
				PlayerBF.landedOnObs = true;
			}
			base.GetComponentInParent<PlayerBF>().CrashLanded();
		}
		if (Score.scoringPoints & other.collider.tag == "Environment")
		{
			PlayerBF.land = true;
			PlayerBF.jumped = false;
			GameObject obj2 = UnityEngine.Object.Instantiate<GameObject>(this.thud, base.transform.position, Quaternion.identity);
			UnityEngine.Object.Destroy(obj2, 1f);
			if (other.gameObject.name == "Obstacle")
			{
				PlayerBF.landedOnObs = true;
			}
			if (other.gameObject.name == "Platform")
			{
				PlayerBF.landedOnObs = true;
			}
			if (other.gameObject.name == "Container")
			{
				PlayerBF.landedOnObs = true;
			}
			if (other.gameObject.name == "Road")
			{
				PlayerBF.landedOnObs = true;
			}
			if (other.gameObject.name == "Floor")
			{
				PlayerBF.landedOnObs = true;
			}
			if (other.gameObject.name == "Street" && LvlBtnHandler.activeLevel == 5)
			{
				PlayerBF.landedOnObs = true;
			}
			if (other.gameObject.name == "RedRoom")
			{
				PlayerBF.landedOnObs = true;
			}
			if (other.gameObject.name == "Lava")
			{
				Challenges.houseSpecial[7] = false;
			}
			if (other.gameObject.name == "ThirdStep")
			{
				PlayerBF.landedOnObs = true;
			}
			if (other.gameObject.name == "Bridge" && LvlBtnHandler.activeLevel == 1)
			{
				PlayerBF.landedOnObs = true;
			}
			if (other.gameObject.name == "Boat")
			{
				PlayerBF.landedOnObs = true;
			}
			if (other.gameObject.name == "Pumpkin")
			{
				PlayerBF.landedOnObs = true;
			}
			if (other.gameObject.name == "Grave")
			{
				PlayerBF.landedOnObs = true;
			}
			if (other.gameObject.name == "Balcony")
			{
				PlayerBF.landedOnObs = true;
			}
			if (other.gameObject.name == "2ndWindow")
			{
				PlayerBF.landedOnObs = true;
			}
			if (other.gameObject.name == "RocketWindow")
			{
				PlayerBF.landedOnObs = true;
			}
		}
		if (other.collider.tag == "Untagged" && other.collider.name == "Walkway")
		{
			PlayerBF.landedOnObs = true;
		}
	}

	public GameObject thud;
}
