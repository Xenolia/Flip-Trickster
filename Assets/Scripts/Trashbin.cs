// dnSpy decompiler from Assembly-CSharp.dll class: Trashbin
using System;
using UnityEngine;

public class Trashbin : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name == "head")
		{
			Challenges.citySpecial[0] = true;
		}
	}
}
