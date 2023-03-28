// dnSpy decompiler from Assembly-CSharp.dll class: CollisionCheck
using System;
using UnityEngine;

public class CollisionCheck : MonoBehaviour
{
	private void Start()
	{
		this.pp = base.GetComponentInParent<PlayerBF>();
	}

	private void Update()
	{
	}

	private void OnCollisionEnter(Collision other)
	{
		if ((other.collider.tag == "Killer" && !PlayerBF.fallen) || (other.collider.tag == "Environment" && !PlayerBF.fallen))
		{
			if (other.gameObject.name == "Block")
			{
				PlayerBF.landedOnObs = true;
			}
			if (other.gameObject.name == "Wheel")
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
			if (other.gameObject.name == "Antenna")
			{
				PlayerBF.landedOnObs = true;
			}
			if (base.name == "shin_L" || base.name == "shin_R")
			{
				base.Invoke("CrashShin", 0.1f);
				this.shinCrash = true;
				return;
			}
			PlayerBF.land = true;
			PlayerBF.jumped = false;
			this.pp.CrashLanded();
		}
		else if (other.collider.tag == "Untagged" && other.gameObject.name == "Walkway")
		{
			PlayerBF.landedOnObs = true;
		}
	}

	private void CrashShin()
	{
		if (this.shinCrash)
		{
			return;
		}
		PlayerBF.land = true;
		PlayerBF.jumped = false;
		this.pp.CrashLanded();
	}

	private PlayerBF pp;

	private bool shinCrash;
}
