// dnSpy decompiler from Assembly-CSharp.dll class: Car
using System;
using UnityEngine;

public class Car : MonoBehaviour
{
	private void Start()
	{
		if (base.transform.position.x > -2f)
		{
			this.speed = 5f;
		}
		else if (base.transform.position.x > -7f)
		{
			this.speed = 8f;
		}
		else if (base.transform.position.x > -13f)
		{
			this.speed = 8f;
		}
		else
		{
			this.speed = 5f;
		}
	}

	private void FixedUpdate()
	{
		if (base.transform.position.z > 100f || base.transform.position.z < -110f)
		{
			SimplePool.Despawn(base.gameObject);
		}
		else
		{
			base.transform.position += base.transform.right * Time.fixedDeltaTime * this.speed;
		}
	}

	private float speed;
}
