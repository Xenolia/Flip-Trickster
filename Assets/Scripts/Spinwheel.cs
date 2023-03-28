// dnSpy decompiler from Assembly-CSharp.dll class: Spinwheel
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spinwheel : MonoBehaviour
{
	private void Start()
	{
	}

	private void OnEnable()
	{
		if (TitleMain.analyticsConsent)
		{
			Dictionary<string, string> eventValues = new Dictionary<string, string>();
			//AppsFlyer.trackRichEvent("spinwheel", eventValues);
			//AppsFlyer.trackEvent("spinwheel", string.Empty);
		}
	}

	private void Update()
	{
		if (this.speed >= 0f)
		{
			this.Spin();
		}
		else if (this.speed <= 0f && this.firstStop != 0f)
		{
			float z = this.spinner.localRotation.eulerAngles.z;
			if (z > 308.6f)
			{
				this.WinPrize(0);
			}
			else if (z > 257.1f)
			{
				this.WinPrize(1);
			}
			else if (z > 205.7f)
			{
				this.WinPrize(2);
			}
			else if (z > 154.3f)
			{
				this.WinPrize(3);
			}
			else if (z > 102.9f)
			{
				this.WinPrize(4);
			}
			else if (z > 51.4f)
			{
				this.WinPrize(5);
			}
			else
			{
				this.WinPrize(6);
			}
		}
	}

	private void WinPrize(int prizeNum)
	{
		this.num = prizeNum;
		if (this.secondStop != 0f)
		{
			this.texts[prizeNum].parent = base.transform.parent;
			this.secondStop = 0f;
			int num = int.Parse(this.texts[prizeNum].GetComponent<TextMeshProUGUI>().text);
			Currency.coinAmount += num;
			GameObject.Find("Coins").GetComponent<TextMeshProUGUI>().text = Currency.coinAmount.ToString();
			PlayerPrefs.SetInt("Coins", Currency.coinAmount);
		}
		Transform transform = this.texts[prizeNum];
		transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.up * 20f, Time.deltaTime * 4f);
		transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.identity, Time.deltaTime * 4f);
		float num2 = Mathf.Sin(Time.time * 12f + 5f) * 1f + 2f;
		transform.localScale = Vector3.one * (1.2f + num2 / 3f);
		this.wholeThing.localScale = Vector3.Lerp(this.wholeThing.localScale, Vector3.zero, Time.deltaTime * 6f);
		if (this.wholeThing.localScale.x > 0.3f)
		{
			return;
		}
		this.star.localScale = Vector3.Lerp(this.star.localScale, Vector3.one, Time.deltaTime * 6f);
		this.awesome.gameObject.SetActive(true);
	}

	private void Spin()
	{
		this.spinner.Rotate(0f, 0f, this.speed);
		this.speed -= ((this.speed >= this.firstStop) ? 0.06f : 0.03f);
		if (this.speed < this.secondStop)
		{
			this.speed += 0.022f;
		}
	}

	public void ClickToSpin()
	{
		this.button.SetActive(false);
		this.speed = UnityEngine.Random.Range(16f, 24f);
		this.firstStop = UnityEngine.Random.Range(5f, 11f);
		this.secondStop = UnityEngine.Random.Range(1.2f, 2.8f);
	}

	public void Awesome()
	{
 		if (SceneManager.GetActiveScene().name == "NewLvlSelect")
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
		if (SceneManager.GetActiveScene().name != "NewLvlSelect")
		{
			GameObject.Find("MAIN").GetComponent<Main>().AfterModal();
		}
		this.texts[this.num].gameObject.SetActive(false);
		GameObject.Find("FreePrize").SetActive(false);
		base.gameObject.SetActive(false);
	}

	public Transform star;

	public Transform wholeThing;

	public Transform spinner;

	public GameObject button;

	public Transform awesome;

	public Transform[] texts;

	private float speed;

	private float firstStop;

	private float secondStop;

	private int num;
}
