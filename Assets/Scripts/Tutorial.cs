// dnSpy decompiler from Assembly-CSharp.dll class: Tutorial
using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
	private void Start()
	{
		base.StartCoroutine(this.Timeline());
	}

	private void FixedUpdate()
	{
		if (this.move)
		{
			this.MoveStep();
		}
	}

	private void NextStep()
	{
		this.steps[this.count].gameObject.SetActive(true);
	}

	private void MoveStep()
	{
		Transform transform = this.steps[this.count].transform;
		transform.position = Vector3.Lerp(transform.position, this.destinations[this.count].position, Time.fixedDeltaTime * 3f);
		this.steps[this.count].fontSize = Mathf.Lerp(this.steps[this.count].fontSize, 28f, Time.fixedDeltaTime * 3f);
	}

	private void MoveHand()
	{
		this.hand.position = Vector3.Lerp(this.hand.position, this.dest.position, Time.deltaTime * 3f);
	}

	private void Touch()
	{
		this.hand.localRotation = Quaternion.Euler(0f, 0f, 50f);
		this.press.SetActive(true);
	}

	private void Release()
	{
		this.hand.localRotation = Quaternion.Euler(0f, 0f, 12f);
		this.press.SetActive(false);
	}

	private IEnumerator Timeline()
	{
		yield return new WaitForSeconds(2f);
		this.NextStep();
		yield return new WaitForSeconds(1f);
		this.move = true;
		yield return new WaitForSeconds(2f);
		this.move = false;
		this.Touch();
		this.count++;
		yield return new WaitForSeconds(1f);
		this.NextStep();
		yield return new WaitForSeconds(1f);
		this.move = true;
		yield return new WaitForSeconds(2f);
		this.move = false;
		this.Release();
		this.count++;
		yield return new WaitForSeconds(2f);
		this.NextStep();
		yield return new WaitForSeconds(1f);
		this.move = true;
		yield return new WaitForSeconds(2f);
		this.move = false;
		this.count++;
		yield return new WaitForSeconds(2f);
		this.NextStep();
		yield return new WaitForSeconds(1f);
		this.move = true;
		yield return new WaitForSeconds(2f);
		this.move = false;
		this.count++;
		yield break;
	}

	public Transform spawn;

	public TextMeshProUGUI[] steps;

	public Transform[] destinations;

	public Transform hand;

	public Transform dest;

	public GameObject press;

	private int count;

	private bool move;
}
