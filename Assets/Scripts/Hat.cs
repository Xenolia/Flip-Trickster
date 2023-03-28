// dnSpy decompiler from Assembly-CSharp.dll class: Hat
using System;
using UnityEngine;

public class Hat : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
		if (this.head == null)
		{
			this.head = GameObject.Find("head").transform;
		}
		base.transform.position = this.head.position;
		base.transform.localScale = this.head.lossyScale;
		base.transform.rotation = this.head.rotation;
	}

	public void Fix()
	{
		if (this.head == null)
		{
			this.head = GameObject.Find("head").transform;
		}
		base.transform.position = this.head.position;
		base.transform.localScale = this.head.lossyScale;
		base.transform.rotation = this.head.rotation;
	}

	private Transform head;
}
