// dnSpy decompiler from Assembly-CSharp.dll class: HatInSpinner
using System;
using UnityEngine;

public class HatInSpinner : MonoBehaviour
{
	private void Start()
	{
		this.mr = base.GetComponentInChildren<MeshRenderer>();
		for (int i = 0; i < this.names.Length; i++)
		{
			if (!(base.name != this.names[i]))
			{
				if (i < 2)
				{
					this.visibleSpin = 0;
				}
				else if (i < 4)
				{
					this.visibleSpin = 1;
				}
				else if (i < 6)
				{
					this.visibleSpin = 2;
				}
				else if (i < 8)
				{
					this.visibleSpin = 3;
				}
				else if (i < 10)
				{
					this.visibleSpin = 4;
				}
				else if (i < 12)
				{
					this.visibleSpin = 5;
				}
				else if (i < 14)
				{
					this.visibleSpin = 6;
				}
			}
		}
	}

	private void Update()
	{
		base.transform.position = this.point.position;
		this.VisibilityCheck();
	}

	private void VisibilityCheck()
	{
		float x = base.transform.localPosition.x;
		float z = base.transform.localPosition.z;
		if (Spinner.spinCount < this.visibleSpin + 1)
		{
			if (Spinner.spinCount < this.visibleSpin - 2)
			{
				this.mr.enabled = false;
				return;
			}
			if (Spinner.spinCount > this.visibleSpin - 2)
			{
				this.mr.enabled = true;
				return;
			}
			if (x > -6.15f && z < 9f)
			{
				if (Spinner.spinCount <= this.visibleSpin - 2)
				{
					this.mr.enabled = false;
				}
			}
			else if (Spinner.spinCount >= this.visibleSpin - 2)
			{
				this.mr.enabled = true;
			}
		}
		else
		{
			if (Spinner.spinCount > this.visibleSpin + 1)
			{
				this.mr.enabled = false;
				return;
			}
			if (Spinner.spinCount < this.visibleSpin)
			{
				this.mr.enabled = true;
				return;
			}
			if (x < -6.15f && z < 9f)
			{
				if (Spinner.spinCount >= this.visibleSpin + 1)
				{
					this.mr.enabled = false;
				}
			}
			else if (Spinner.spinCount <= this.visibleSpin + 1)
			{
				this.mr.enabled = true;
			}
		}
	}

	public Transform point;

	private MeshRenderer mr;

	private string[] names = new string[]
	{
		"NoHat",
		"NoobHat",
		"Beanie",
		"Beanie2",
		"Beanie3",
		"Beanie4",
		"Cap",
		"Cap2",
		"Cap3",
		"Cap4",
		"Helmet",
		"TopHat",
		"Viking"
	};

	private int visibleSpin;
}
