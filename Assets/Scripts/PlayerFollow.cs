// dnSpy decompiler from Assembly-CSharp.dll class: PlayerFollow
using System;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
		if (this.player)
		{
			base.transform.position = Vector3.Lerp(base.transform.position, this.player.position, Time.deltaTime * 3f);
		}
	}

	public Transform player;

	public Transform camFollow;

	public Transform pointer;
}
