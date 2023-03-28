// dnSpy decompiler from Assembly-CSharp.dll class: Spinner
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Spinner : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
{
	private void Start()
	{
		Spinner.activeHatNum = PlayerPrefs.GetInt("ActiveHatNum", -1);
		for (int i = 0; i < this.owned.Length; i++)
		{
			this.owned[i] = GameState.Instance.HasHat(i);
		}
		this.coinText.text = Currency.coinAmount.ToString();
		this.pointSpinner = new Transform[6];
		Transform[] componentsInChildren = this.spinner.GetComponentsInChildren<Transform>();
		this.pointSpinner[0] = componentsInChildren[1];
		this.pointSpinner[1] = componentsInChildren[3];
		this.pointSpinner[2] = componentsInChildren[5];
		this.pointSpinner[3] = componentsInChildren[7];
		this.pointSpinner[4] = componentsInChildren[9];
		this.pointSpinner[5] = componentsInChildren[11];
		this.spinner.localRotation = Quaternion.Euler(0f, (float)(Spinner.activeHatNum * 60), 0f);
		if (Spinner.activeHatNum != -1)
		{
			this.hatsBeingWorn[Spinner.activeHatNum].SetActive(true);
		}
		if (Spinner.activeHatNum == -1)
		{
			this.buyButton.SetActive(false);
		}
		this.hatNum = Spinner.activeHatNum;
		if (this.spinner.localRotation.eulerAngles.y > 330f || this.spinner.localRotation.eulerAngles.y < 31f)
		{
			this.lastNum = 0;
		}
		else if (this.spinner.localRotation.eulerAngles.y > 270f)
		{
			this.lastNum = -1;
		}
		else if (this.spinner.localRotation.eulerAngles.y > 210f)
		{
			this.lastNum = 4;
		}
		else if (this.spinner.localRotation.eulerAngles.y > 150f)
		{
			this.lastNum = 3;
		}
		else if (this.spinner.localRotation.eulerAngles.y > 90f)
		{
			this.lastNum = 2;
		}
		else if (this.spinner.localRotation.eulerAngles.y > 30f)
		{
			this.lastNum = 1;
		}
		if (Spinner.activeHatNum < 0)
		{
			Spinner.spinCount = -1;
		}
		else if (Spinner.activeHatNum < 2)
		{
			Spinner.spinCount = 0;
		}
		else if (Spinner.activeHatNum < 4)
		{
			Spinner.spinCount = 1;
		}
		else if (Spinner.activeHatNum < 6)
		{
			Spinner.spinCount = 2;
		}
		else if (Spinner.activeHatNum < 8)
		{
			Spinner.spinCount = 3;
		}
		else if (Spinner.activeHatNum < 10)
		{
			Spinner.spinCount = 4;
		}
		else if (Spinner.activeHatNum < 12)
		{
			Spinner.spinCount = 5;
		}
	}

	private void Update()
	{
		if (Input.GetMouseButtonUp(0))
		{
			this.swiping = false;
		}
		if (this.swiping)
		{
			this.current = Camera.main.ScreenToViewportPoint(UnityEngine.Input.mousePosition).x;
		}
		this.SpinnerCheck();
		this.HatSelected();
		this.Spin();
	}

	private void SpinnerCheck()
	{
		if (this.spinner.localRotation.eulerAngles.y >= 240f)
		{
			if (this.right)
			{
				Spinner.spinCount--;
				this.right = false;
			}
			if (this.back)
			{
				Spinner.spinCount++;
				this.back = false;
			}
			this.left = true;
		}
		else if (this.spinner.localRotation.eulerAngles.y >= 120f)
		{
			if (this.right)
			{
				Spinner.spinCount++;
				this.right = false;
			}
			else if (this.left)
			{
				Spinner.spinCount--;
				this.left = false;
			}
			this.back = true;
		}
		else if (this.spinner.localRotation.eulerAngles.y >= 0f)
		{
			if (this.left)
			{
				Spinner.spinCount++;
				this.left = false;
			}
			if (this.back)
			{
				Spinner.spinCount--;
				this.back = false;
			}
			this.right = true;
		}
	}

	private void HatSelected()
	{
		if (this.spinner.localRotation.eulerAngles.y > 330f || this.spinner.localRotation.eulerAngles.y < 31f)
		{
			this.SelectHat(0);
		}
		else if (this.spinner.localRotation.eulerAngles.y > 270f)
		{
			this.SelectHat(-1);
		}
		else if (this.spinner.localRotation.eulerAngles.y > 210f)
		{
			this.SelectHat(4);
		}
		else if (this.spinner.localRotation.eulerAngles.y > 150f)
		{
			this.SelectHat(3);
		}
		else if (this.spinner.localRotation.eulerAngles.y > 90f)
		{
			this.SelectHat(2);
		}
		else if (this.spinner.localRotation.eulerAngles.y > 30f)
		{
			this.SelectHat(1);
		}
	}

	private void SelectHat(int num)
	{
		MonoBehaviour.print(Spinner.activeHatNum);
		if (num != this.lastNum)
		{
			if (num == this.lastNum + 1)
			{
				this.hatNum++;
			}
			else if (num == this.lastNum - 1)
			{
				this.hatNum--;
			}
			else if (num > this.lastNum)
			{
				this.hatNum--;
			}
			else
			{
				this.hatNum++;
			}
			this.lastNum = num;
		}
		if (this.hatNum <= -2)
		{
			return;
		}
		if (this.hatNum >= this.hatsInSpinner.Length - 1)
		{
			return;
		}
		if (this.hatNum == -1)
		{
			this.buyButton.SetActive(false);
			this.lockBtn.SetActive(false);
			if (Spinner.activeHatNum != -1)
			{
				this.hatsBeingWorn[Spinner.activeHatNum].SetActive(false);
			}
			Spinner.activeHatNum = this.hatNum;
			PlayerPrefs.SetInt("ActiveHatNum", Spinner.activeHatNum);
			return;
		}
		if (this.hatNum == -1 && Spinner.activeHatNum == -1)
		{
			return;
		}
		if (this.hatNum == 0 && !this.owned[this.hatNum])
		{
			this.lockBtn.SetActive(true);
		}
		else
		{
			this.lockBtn.SetActive(false);
		}
		if (this.owned[this.hatNum])
		{
			this.buyButton.SetActive(false);
		}
		else
		{
			this.buyButton.SetActive(true);
		}
		this.costText.text = this.hatCosts[this.hatNum].ToString();
		if (this.hatCosts[this.hatNum] >= 1000)
		{
			this.costText.transform.localPosition = new Vector3(10f, -10f);
		}
		else
		{
			this.costText.transform.localPosition = new Vector3(5f, -10f);
		}
		if (Spinner.activeHatNum == this.hatNum)
		{
			return;
		}
		if (!this.owned[this.hatNum])
		{
			return;
		}
		if (Spinner.activeHatNum != -1)
		{
			this.hatsBeingWorn[Spinner.activeHatNum].SetActive(false);
		}
		if (!this.hatsBeingWorn[this.hatNum].activeInHierarchy)
		{
			this.hatsBeingWorn[this.hatNum].SetActive(true);
		}
		Spinner.activeHatNum = this.hatNum;
		PlayerPrefs.SetInt("ActiveHatNum", Spinner.activeHatNum);
	}

	public void BuyHat()
	{
		if (this.hatCosts[this.hatNum] > Currency.coinAmount)
		{
			GameObject.Find("MAIN").GetComponent<Customization>().OpenShop();
			return;
		}
		Currency.coinAmount = GameState.Instance.RemoveCoins(this.hatCosts[this.hatNum]);
		this.coinText.text = Currency.coinAmount.ToString();
		this.owned[this.hatNum] = true;
		GameState.Instance.AwardHat(this.hatNum);
		GameState.Instance.Syncronize();
		if (TitleMain.analyticsConsent)
		{
			Dictionary<string, string> eventValues = new Dictionary<string, string>();
			//AppsFlyer.trackRichEvent("purchased_hat", eventValues);
			//AppsFlyer.trackEvent("purchased_hat", string.Empty);
		}
		if (TitleMain.analyticsConsent && this.hatNum == 11)
		{
			Dictionary<string, string> eventValues2 = new Dictionary<string, string>();
			//AppsFlyer.trackRichEvent("purchased_viking_hat", eventValues2);
			//AppsFlyer.trackEvent("purchased_viking_hat", string.Empty);
		}
	}

	private void Spin()
	{
		if (!this.swiping)
		{
			float y;
			if (this.spinner.localRotation.eulerAngles.y > 330f)
			{
				y = 360f;
			}
			else if (this.spinner.localRotation.eulerAngles.y > 270f)
			{
				y = 300f;
			}
			else if (this.spinner.localRotation.eulerAngles.y > 210f)
			{
				y = 240f;
			}
			else if (this.spinner.localRotation.eulerAngles.y > 150f)
			{
				y = 180f;
			}
			else if (this.spinner.localRotation.eulerAngles.y > 90f)
			{
				y = 120f;
			}
			else if (this.spinner.localRotation.eulerAngles.y > 30f)
			{
				y = 60f;
			}
			else
			{
				y = 0f;
			}
			if (Spinner.spinCount < 0 && this.spinner.localRotation.eulerAngles.y < 300f && this.spinner.localRotation.eulerAngles.y > 100f)
			{
				y = 300f;
			}
			else if (Spinner.spinCount > this.hatsInSpinner.Length / 3)
			{
				y = 300f;
			}
			Quaternion b = Quaternion.Euler(0f, y, 0f);
			this.spinner.localRotation = Quaternion.Lerp(this.spinner.localRotation, b, Time.deltaTime * 12f);
			return;
		}
		float num = 400f;
		if (Spinner.spinCount < 0 && this.spinner.localRotation.eulerAngles.y < 300f && !this.outOfBounds && this.spinner.localRotation.eulerAngles.y > 100f)
		{
			this.start = this.current;
			this.spinnerStart = this.spinner.localRotation.eulerAngles.y;
			this.outOfBounds = true;
		}
		else if (Spinner.spinCount < 0 && this.spinner.localRotation.eulerAngles.y >= 300f && this.outOfBounds)
		{
			this.start = this.current;
			this.spinnerStart = this.spinner.localRotation.eulerAngles.y;
			this.outOfBounds = false;
		}
		if (this.outOfBounds)
		{
			num = 50f;
		}
		if (Spinner.spinCount > this.hatsInSpinner.Length / 3 && !this.outOfBounds && this.spinner.localRotation.eulerAngles.y > 300f)
		{
			MonoBehaviour.print(this.spinner.localRotation.eulerAngles.y);
			this.start = this.current;
			this.spinnerStart = this.spinner.localRotation.eulerAngles.y;
			this.outOfBounds = true;
		}
		else if (Spinner.spinCount <= this.hatsInSpinner.Length / 3 && Spinner.spinCount > 1 && this.outOfBounds)
		{
			this.start = this.current;
			this.spinnerStart = this.spinner.localRotation.eulerAngles.y;
			this.outOfBounds = false;
		}
		if (this.outOfBounds)
		{
			num = 50f;
		}
		float num2 = (this.start - this.current) * num;
		Quaternion b2 = Quaternion.Euler(0f, this.spinnerStart + num2, 0f);
		this.spinner.localRotation = Quaternion.Lerp(this.spinner.localRotation, b2, Time.deltaTime * 12f);
	}

	private void PointHeight()
	{
		for (int i = 0; i < 6; i++)
		{
			float num = (float)(i - 180) + this.pointSpinner[i].rotation.eulerAngles.y;
			num /= 300f;
			this.pointSpinner[i].localPosition = new Vector3(this.pointSpinner[i].localPosition.x, num, this.pointSpinner[i].localPosition.z);
		}
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		this.swiping = true;
		this.outOfBounds = false;
		this.start = Camera.main.ScreenToViewportPoint(UnityEngine.Input.mousePosition).x;
		this.spinnerStart = this.spinner.localRotation.eulerAngles.y;
	}

	public GameObject lockBtn;

	public Transform spinner;

	private Transform[] pointSpinner;

	public GameObject[] hatsBeingWorn;

	public GameObject[] hatsInSpinner;

	private bool[] owned = new bool[13];

	public static int activeHatNum;

	private int hatNum;

	private int lastNum;

	public GameObject buyButton;

	public TextMeshProUGUI coinText;

	public TextMeshProUGUI costText;

	private int[] hatCosts = new int[]
	{
		0,
		250,
		250,
		250,
		250,
		250,
		250,
		250,
		250,
		1000,
		1500,
		2000,
		0
	};

	private float start;

	private float current;

	private float spinnerStart;

	private bool swiping;

	private bool outOfBounds;

	private bool right;

	private bool back;

	private bool left;

	public static int spinCount;
}
