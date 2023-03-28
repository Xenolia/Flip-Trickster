// dnSpy decompiler from Assembly-CSharp.dll class: LongSleeve
using System;
using System.Collections.Generic;
using UnityEngine;

public class LongSleeve : MonoBehaviour
{
	private void Start()
	{
		this.Rename(base.transform);
		this.SetTransforms();
	}

	private void Update()
	{
		this.SetTransforms();
	}

	private void SetTransforms()
	{
		Transform[] array = new Transform[]
		{
			this.hips,
			this.spine,
			this.chest,
			this.neck,
			this.shoulder_L,
			this.upper_arm_L,
			this.forearm_L,
			this.shoulder_R,
			this.upper_arm_R,
			this.forearm_R,
			this.thigh_L,
			this.thigh_R
		};
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] != null)
			{
				array[i].SetParent(GameObject.Find(this.names[i]).transform);
				array[i].localPosition = Vector3.zero;
				array[i].localRotation = Quaternion.identity;
			}
		}
	}

	private void Rename(Transform parent)
	{
		this.count++;
		for (int i = 0; i < parent.childCount; i++)
		{
			if (this.count > 1)
			{
				this.names.Add(parent.GetChild(i).name);
			}
			parent.GetChild(i).name = parent.GetChild(i).name + "2";
			if (parent.GetChild(i).childCount > 0)
			{
				this.Rename(parent.GetChild(i));
			}
		}
	}

	public Transform hips;

	public Transform spine;

	public Transform chest;

	public Transform neck;

	public Transform shoulder_L;

	public Transform shoulder_R;

	public Transform upper_arm_L;

	public Transform upper_arm_R;

	public Transform forearm_L;

	public Transform forearm_R;

	public Transform thigh_L;

	public Transform thigh_R;

	private List<string> names = new List<string>();

	private int count;
}
