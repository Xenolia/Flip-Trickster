// dnSpy decompiler from Assembly-CSharp.dll class: TitleMain
using System;
//using Facebook.Unity;
using UnityEngine;
using UnityEngine.UI;

public class TitleMain : MonoBehaviour
{
	private void Start()
	{
		this.startColor = this.ayCs[0].GetComponent<Image>().color;
		//FB.Init(null, null, null);
		float num = (float)Screen.width / (float)Screen.height;
		float num2 = 1.77777779f - num;
		if (num2 > 0.1f)
		{
			Camera.main.fieldOfView = 100f;
			LvlModels.onIpad = true;
		}
		if (num2 < -0.1f)
		{
			TitleMain.oniPhoneX = true;
		}
		this.ApplyShadowSettings();
	}

	private void Update()
	{
	}

	private void ApplyShadowSettings()
	{
		QualitySettings.shadows = ShadowQuality.Disable;
	}

	public void GDPR()
	{
		if (PlayerPrefs.GetInt("GDPRDone", 0) == 1)
		{
			UnityEngine.Debug.Log("GDPR Already completed");
			TitleMain.analyticsConsent = (PlayerPrefs.GetInt("Analytics") == 1);
			TitleMain.adConsent = (PlayerPrefs.GetInt("Ads") == 1);
			if (TitleMain.adConsent)
			{
				//MoPubAndroid.PartnerApi.GrantConsent();
			}
			else if (!TitleMain.adConsent)
			{
				//MoPubAndroid.PartnerApi.RevokeConsent();
			}
			TitleMain.gdprFinished = true;
			return;
		}
		UnityEngine.Debug.Log("Showing GDPR dialog...");
		this.gdpr.SetActive(true);
	}

	public void Awesome()
	{
		//MoPubAndroid.PartnerApi.GrantConsent();
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
		this.issueBanner.SetActive(true);
		this.gdpr.SetActive(false);
	}

	public void FixIssue()
	{
		this.gdpr.SetActive(true);
		this.firstScreen.SetActive(false);
		this.secondScreen.SetActive(false);
		this.thirdScreen.SetActive(true);
		this.fourthScreen.SetActive(false);
		this.issueBanner.SetActive(false);
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
			this.AdToggles[0].SetActive(false);
			this.AdToggles[1].SetActive(true);
			for (int i = 0; i < 3; i++)
			{
				this.adCs[i].color = this.red;
			}
			this.adsOn = false;
		}
		else
		{
			this.AdToggles[0].SetActive(true);
			this.AdToggles[1].SetActive(false);
			for (int j = 0; j < 3; j++)
			{
				this.adCs[j].color = this.startColor;
			}
			this.adsOn = true;
		}
	}

	public GameObject gdpr;

	public GameObject firstScreen;

	public GameObject secondScreen;

	public GameObject thirdScreen;

	public GameObject fourthScreen;

	public GameObject[] analyticsToggles;

	public GameObject[] AdToggles;

	public GameObject issueBanner;

	public Image[] ayCs;

	public Image[] adCs;

	public Color red;

	private Color startColor;

	public static bool oniPhoneX;

	public static bool adConsent;

	public static bool analyticsConsent;

	public static bool gdprFinished;

	private bool analyticsOn = true;

	private bool adsOn = true;
}
