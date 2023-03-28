// dnSpy decompiler from Assembly-CSharp.dll class: Flips
using System;
using UnityEngine;

public class Flips : MonoBehaviour
{
	private void Start()
	{
	}

	public float Flip(int count)
	{
		float result = 0f;
		if (this.backflip)
		{
			result = this.BackflipSquat(count);
		}
		if (this.gainer)
		{
			result = this.GainerSquat(count);
		}
		return result;
	}

	private float BackflipSquat(int count)
	{
		float[] array = new float[]
		{
			70f,
			-80f,
			20f,
			-40f
		};
		return array[count];
	}

	private float GainerSquat(int count)
	{
		float[] array = new float[]
		{
			110f,
			-75f,
			20f,
			-40f
		};
		return array[count];
	}

	private float ulr;

	private float dlr;

	private float fr;

	private float sr;

	private float ull;

	private float dll;

	private float fl;

	private float sl;

	public bool backflip;

	public bool gainer;
}
