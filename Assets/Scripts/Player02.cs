// dnSpy decompiler from Assembly-CSharp.dll class: Player02
using System;
using UnityEngine;

public class Player02 : MonoBehaviour
{
	private void Start()
	{
		this.rb = base.GetComponent<Rigidbody>();
		this.ani = base.GetComponentInChildren<Animator>();
		this.rbs = base.GetComponentsInChildren<Rigidbody>();
		for (int i = 0; i < this.rbs.Length; i++)
		{
			this.rbs[i].isKinematic = true;
		}
	}

	private void FixedUpdate()
	{
		if (!this.jumped && Input.GetMouseButtonDown(0))
		{
			this.Squat();
		}
		if (this.squatting && Input.GetMouseButtonUp(0))
		{
			this.ani.SetBool("Jump", true);
			base.Invoke("Jump", 0.04f);
			this.squatting = false;
		}
		if (this.jumped && Input.GetMouseButtonDown(0))
		{
			this.spin = true;
			this.ani.SetBool("Tuck", true);
			this.botCol.enabled = false;
		}
		if (this.spin && Input.GetMouseButtonUp(0))
		{
			this.ani.SetBool("Squat", false);
			this.ani.SetBool("Jump", false);
			this.ani.SetBool("Tuck", false);
			this.spinForce /= 3f;
			this.botCol.enabled = true;
		}
		if (this.spin)
		{
			this.Spin();
		}
		if (!this.squatting || !Input.GetMouseButtonUp(0))
		{
		}
		if (this.reorienting)
		{
			this.Reorient();
			if (Input.GetMouseButtonDown(0))
			{
				this.ReadyToJump();
			}
		}
	}

	private void Jump()
	{
		Vector3 vector = base.transform.right * -8f;
		if (this.count == 0)
		{
			this.spinForce = vector;
		}
		else
		{
			this.spinForce *= 3f;
		}
		this.count++;
		this.rb.isKinematic = false;
		this.botCol.enabled = false;
		this.rb.angularVelocity = Vector3.zero;
		this.rb.AddForce(base.transform.up * 3f, ForceMode.Impulse);
		this.rb.AddRelativeTorque(Vector3.left / 5f, ForceMode.Impulse);
		base.Invoke("EnableCol", 0.2f);
		this.jumped = true;
		this.squatting = false;
		this.sq = 0f;
	}

	private void Spin()
	{
		this.rb.angularVelocity = Vector3.Lerp(this.rb.angularVelocity, this.spinForce, Time.fixedDeltaTime * 15f);
	}

	private void Squat()
	{
		this.ani.SetBool("Squat", true);
		this.squatting = true;
		this.rb.isKinematic = false;
	}

	private void Charge()
	{
		this.sq *= 1.02f;
		base.transform.RotateAround(this.rotatePoint.position, base.transform.right, -this.sq * Time.fixedDeltaTime);
		if (this.sq > 110f)
		{
			this.Jump();
		}
	}

	private void EnableCol()
	{
		this.botCol.enabled = true;
	}

	private void OnCollisionEnter(Collision other)
	{
		if (this.jumped)
		{
			this.spin = false;
			float num = Vector3.Angle(base.transform.position - this.rotatePoint.position, Vector3.up);
			if (this.jumped && num < 45f)
			{
				this.rb.isKinematic = true;
				this.reorienting = true;
				float num2 = other.transform.lossyScale.y / 2f;
				float num3 = other.transform.position.y + num2;
				float d = this.rotatePoint.position.y - num3;
				base.transform.Translate(Vector3.down * d);
			}
			else if (this.jumped && num > 45f)
			{
				for (int i = 0; i < this.rbs.Length; i++)
				{
					this.rbs[i].isKinematic = false;
					this.rbs[i].useGravity = true;
				}
			}
			this.jumped = false;
		}
	}

	private void Reorient()
	{
		float num = Vector3.Angle(base.transform.position - this.rotatePoint.position, Vector3.up);
		MonoBehaviour.print(num);
		if (num > 45f || num < 3.5f)
		{
			this.ReadyToJump();
			return;
		}
		num = Mathf.Lerp(0f, num, Time.fixedDeltaTime * 5f);
		if (base.transform.rotation.eulerAngles.x > 180f)
		{
			num *= -1f;
		}
		base.transform.RotateAround(this.rotatePoint.position, -base.transform.right, num);
	}

	private void ReadyToJump()
	{
		this.rb.isKinematic = false;
		this.reorienting = false;
		this.jumped = false;
		this.squatting = false;
		this.spin = false;
	}

	private Rigidbody rb;

	private Rigidbody[] rbs;

	public BoxCollider botCol;

	private Animator ani;

	public Transform lowPolyMan;

	public Transform rotatePoint;

	private Vector3 spinForce;

	private int count;

	private float sq = 0.1f;

	private bool squatting;

	private bool jumped;

	private bool spin;

	private bool reorienting;
}
