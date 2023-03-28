// dnSpy decompiler from Assembly-CSharp.dll class: CarSpawner
using System;
using System.Collections;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
	private void Start()
	{
		base.StartCoroutine(this.Tick());
	}

	private void Update()
	{
	}

	private IEnumerator Tick()
	{
		for (;;)
		{
			float t = 2f;
			if (base.transform.position.x > -2f)
			{
				t = UnityEngine.Random.Range(2f, 3f);
			}
			else if (base.transform.position.x > -7f)
			{
				t = UnityEngine.Random.Range(1f, 2f);
			}
			else if (base.transform.position.x > -13f)
			{
				t = UnityEngine.Random.Range(1f, 2f);
			}
			else
			{
				t = UnityEngine.Random.Range(2f, 3f);
			}
			yield return new WaitForSeconds(t);
			GameObject copy = SimplePool.Spawn(this.car, base.transform.position, base.transform.rotation);
			MeshRenderer mr = copy.GetComponent<MeshRenderer>();
			mr.material = this.materials[UnityEngine.Random.Range(0, 4)];
		}
		yield break;
	}

	public Material[] materials;

	public GameObject car;
}
