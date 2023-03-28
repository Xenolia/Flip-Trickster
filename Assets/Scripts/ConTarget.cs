// dnSpy decompiler from Assembly-CSharp.dll class: ConTarget
using System;
using UnityEngine;

[ExecuteInEditMode]
public class ConTarget : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
		Vector3 localPosition = this.midLeft.localPosition;
		Vector3 localPosition2 = this.midRight.localPosition;
		Vector3 localPosition3 = this.midUp.localPosition;
		Vector3 localPosition4 = this.midDown.localPosition;
		this.midLeft.localPosition = new Vector3(-localPosition2.x, localPosition.y, localPosition.z);
		this.midUp.localPosition = new Vector3(localPosition3.x, localPosition3.y, -localPosition4.z);
		this.midUp.localScale = new Vector3(-localPosition2.x * 2f + this.c, this.c, this.c);
		this.midDown.localScale = new Vector3(-localPosition2.x * 2f + this.c, this.c, this.c);
		this.midRight.localScale = new Vector3(-localPosition4.z * 2f + this.c, this.c, this.c);
		this.midLeft.localScale = new Vector3(-localPosition4.z * 2f + this.c, this.c, this.c);
	}

	public Transform midLeft;

	public Transform midRight;

	public Transform midUp;

	public Transform midDown;

	private float c = 0.1f;
}
