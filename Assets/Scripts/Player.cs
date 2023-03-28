// dnSpy decompiler from Assembly-CSharp.dll class: Player
using System;
using UnityEngine;

public class Player : MonoBehaviour
{
	private void Start()
	{
	}

	private void FixedUpdate()
	{
		if (Input.GetMouseButton(0))
		{
			this.Bow();
		}
		if (Input.GetMouseButtonUp(0))
		{
			this.Jump();
		}
	}

	private void Jump()
	{
		JointLimits limits = this.topJoint.limits;
		JointLimits limits2 = this.midJoint.limits;
		limits.min = 0f;
		limits2.max = 0f;
		this.topJoint.limits = limits;
		this.midJoint.limits = limits2;
	}

	private void Bow()
	{
		JointLimits limits = this.topJoint.limits;
		JointLimits limits2 = this.midJoint.limits;
		limits.min = Mathf.Lerp(limits.min, 110f, Time.fixedDeltaTime * 2f);
		limits2.max = Mathf.Lerp(limits2.max, -70f, Time.fixedDeltaTime);
		this.topJoint.limits = limits;
		this.midJoint.limits = limits2;
	}

	public HingeJoint topJoint;

	public HingeJoint midJoint;
}
