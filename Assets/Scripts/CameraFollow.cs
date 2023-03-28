// dnSpy decompiler from Assembly-CSharp.dll class: CameraFollow
using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	private void Start()
	{
		if (this.followPoint)
		{
			this.start = this.followPoint.position;
		}
	}

	private void Update()
	{
		base.transform.position = Vector3.Lerp(this.start, this.followPoint.position, 1f);
		base.transform.LookAt(Vector3.Lerp(this.player.position, this.pointer.position, 0.3f));
	}

	public Transform followPoint;

	public Transform pointer;

	public Transform player;

	private Vector3 start;
}
