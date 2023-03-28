// dnSpy decompiler from Assembly-CSharp.dll class: Privacy
using System;
using UnityEngine;
using UnityEngine.UI;

public class Privacy : MonoBehaviour
{
	private void Start()
	{
		this.gdpr = base.gameObject;
		this.startColor = this.adCs[0].color;
		this.secondScreen.SetActive(false);
		this.thirdScreen.SetActive(true);
		this.fourthScreen.SetActive(false);
		this.adsOn = (PlayerPrefs.GetInt("Ads") == 1);
		this.analyticsOn = (PlayerPrefs.GetInt("Analytics") == 1);
		MonoBehaviour.print(this.adsOn);
		MonoBehaviour.print(this.analyticsOn);
		if (!this.adsOn)
		{
			this.SwitchAds();
			this.SwitchAds();
		}
		if (!this.analyticsOn)
		{
			this.SwitchAnalytics();
			this.SwitchAnalytics();
		}
	}

	private void OnEnable()
	{
		this.secondScreen.SetActive(false);
		this.thirdScreen.SetActive(true);
		this.fourthScreen.SetActive(false);
	}

	private void Update()
	{
	}

	public void Awesome()
	{
		//MoPubAndroid.PartnerApi.GrantConsent();
		if (!this.adsOn)
		{
			this.SwitchAds();
		}
		if (!this.analyticsOn)
		{
			this.SwitchAnalytics();
		}
		this.adsOn = true;
		this.analyticsOn = true;
		TitleMain.adConsent = true;
		TitleMain.analyticsConsent = true;
		TitleMain.gdprFinished = true;
		PlayerPrefs.SetInt("GDPRDone", 1);
		PlayerPrefs.SetInt("Ads", 1);
		PlayerPrefs.SetInt("Analytics", 1);
		this.gdpr.SetActive(false);
	}

	public void ManageData()
	{
		this.firstScreen.SetActive(false);
		this.secondScreen.SetActive(true);
	}

	public void Next()
	{
		this.secondScreen.SetActive(false);
		this.thirdScreen.SetActive(true);
	}

	public void Back()
	{
		this.thirdScreen.SetActive(false);
		this.secondScreen.SetActive(true);
	}

	public void Accept()
	{
		if (!this.analyticsOn || !this.adsOn)
		{
			this.thirdScreen.SetActive(false);
			this.fourthScreen.SetActive(true);
		}
		else
		{
			this.Awesome();
		}
	}

	public void FixSettings()
	{
		this.fourthScreen.SetActive(false);
		this.thirdScreen.SetActive(true);
	}

	public void Understand()
	{
		if (!this.adsOn)
		{
			//MoPubAndroid.PartnerApi.RevokeConsent();
			TitleMain.adConsent = false;
			PlayerPrefs.SetInt("Ads", 0);
		}
		else if (this.adsOn)
		{
			//MoPubAndroid.PartnerApi.GrantConsent();
			TitleMain.adConsent = true;
			PlayerPrefs.SetInt("Ads", 1);
		}
		if (!this.analyticsOn)
		{
			TitleMain.analyticsConsent = false;
			PlayerPrefs.SetInt("Analytics", 0);
		}
		if (this.analyticsOn)
		{
			TitleMain.analyticsConsent = true;
			PlayerPrefs.SetInt("Analytics", 1);
		}
		TitleMain.gdprFinished = true;
		PlayerPrefs.SetInt("GDPRDone", 1);
		this.gdpr.SetActive(false);
	}

	public void FixIssue()
	{
		this.gdpr.SetActive(true);
		this.firstScreen.SetActive(false);
		this.secondScreen.SetActive(false);
		this.thirdScreen.SetActive(true);
		this.fourthScreen.SetActive(false);
	}

	public void ALPrivacy()
	{
		Application.OpenURL("https://www.applovin.com/privacy/");
	}

	public void MPPrivacy()
	{
		Application.OpenURL("https://www.mopub.com/legal/privacy/");
	}

	public void AMPrivacy()
	{
		Application.OpenURL("https://policies.google.com/privacy/update");
	}

	public void AFPrivacy()
	{
		Application.OpenURL("https://www.appsflyer.com/privacy-policy/");
	}

	public void SwitchAnalytics()
	{
		if (this.analyticsOn)
		{
			this.analyticsToggles[0].SetActive(false);
			this.analyticsToggles[1].SetActive(true);
			for (int i = 0; i < 3; i++)
			{
				this.ayCs[i].color = this.red;
			}
			this.analyticsOn = false;
		}
		else
		{
			this.analyticsToggles[0].SetActive(true);
			this.analyticsToggles[1].SetActive(false);
			for (int j = 0; j < 3; j++)
			{
				this.ayCs[j].color = this.startColor;
			}
			this.analyticsOn = true;
		}
	}

	public void SwitchAds()
	{
		if (this.adsOn)
		{
			this.adToggles[0].SetActive(false);
			this.adToggles[1].SetActive(true);
			for (int i = 0; i < 3; i++)
			{
				this.adCs[i].color = this.red;
			}
			this.adsOn = false;
		}
		else
		{
			this.adToggles[0].SetActive(true);
			this.adToggles[1].SetActive(false);
			for (int j = 0; j < 3; j++)
			{
				this.adCs[j].color = this.startColor;
			}
			this.adsOn = true;
		}
	}

	public GameObject firstScreen;

	public GameObject secondScreen;

	public GameObject thirdScreen;

	public GameObject fourthScreen;

	public GameObject[] analyticsToggles;

	public GameObject[] adToggles;

	public Image[] ayCs;

	public Image[] adCs;

	private GameObject gdpr;

	public Color red;

	private Color startColor;

	private bool analyticsOn = true;

	private bool adsOn = true;
}
