// dnSpy decompiler from Assembly-CSharp.dll class: AFtitle
using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AFtitle : MonoBehaviour
{
	private void Start()
	{
		UnityEngine.Debug.Log("Staring AFTtile..");
		Skipper.skipAmount = PlayerPrefs.GetInt("Skips", 0);
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		AFtitle.noAds = GameState.Instance.HasAdsDisabled();
		this.targetPlayTime = PlayerPrefs.GetFloat("TargetTime", 300f);
		this.playTime = PlayerPrefs.GetFloat("PlayTime", 0f);
		AFtitle.rvmodalTimer = 90f;
		base.StartCoroutine(this.SetUpMopub());
	}

	private void FixedUpdate()
	{
		this.playTime += Time.fixedDeltaTime;
		PlayerPrefs.SetFloat("PlayTime", this.playTime);
		if (this.playTime > this.targetPlayTime)
		{
			if (this.targetPlayTime < 310f)
			{
				if (TitleMain.analyticsConsent)
				{
					//AppsFlyer.trackEvent("5_minutes_played", string.Empty);
				}
				this.targetPlayTime = 1800f;
				PlayerPrefs.SetFloat("TargetTime", this.targetPlayTime);
			}
			else if (this.targetPlayTime < 1810f)
			{
				if (TitleMain.analyticsConsent)
				{
					//AppsFlyer.trackEvent("30_minutes_played", string.Empty);
				}
				this.targetPlayTime = 3600f;
				PlayerPrefs.SetFloat("TargetTime", this.targetPlayTime);
			}
			else if (this.targetPlayTime < 3610f)
			{
				if (TitleMain.analyticsConsent)
				{
					//AppsFlyer.trackEvent("60_minutes_played", string.Empty);
				}
				this.targetPlayTime = 10800f;
				PlayerPrefs.SetFloat("TargetTime", this.targetPlayTime);
			}
			else if (this.targetPlayTime < 10810f)
			{
				if (TitleMain.analyticsConsent)
				{
					//AppsFlyer.trackEvent("180_minutes_played", string.Empty);
				}
				this.targetPlayTime = 20000f;
				PlayerPrefs.SetFloat("TargetTime", this.targetPlayTime);
			}
		}
	}

	private IEnumerator SetUpMopub()
	{
		//MoPubBase.SdkConfiguration sdkConfig = new MoPubBase.SdkConfiguration
		//{
		//	AdUnitId = LionAdManager.bannerAdUnits[0]
		//};
		//MoPubAndroid.InitializeSdk(sdkConfig);
		//if (AFtitle._003C_003Ef__mg_0024cache0 == null)
		//{
		//	//AFtitle._003C_003Ef__mg_0024cache0 = new Func<bool>(MoPubAndroid.get_IsSdkInitialized);
		//}
		//yield return new WaitUntil(AFtitle._003C_003Ef__mg_0024cache0);
		//yield return new WaitForSeconds(1f);
		//this.playButton.SetActive(true);
		//UnityEngine.Debug.Log(string.Concat(new object[]
		//{
		//	"GDPR Applicable: ",
		//	MoPubAndroid.IsGdprApplicable,
		//	", MoPub Initialized: ",
		//	MoPubAndroid.IsSdkInitialized
		//}));
		//if (MoPubAndroid.IsGdprApplicable == true)
		//{
		//	GameObject.Find("MAIN").GetComponent<TitleMain>().GDPR();
		//	yield return new WaitUntil(() => TitleMain.gdprFinished);
		//}
		 if (PlayerPrefs.GetInt("GDPRDone", 0) == 0)
		{
			TitleMain.adConsent = true;
			TitleMain.analyticsConsent = true;
			PlayerPrefs.SetInt("GDPRDone", 1);
			PlayerPrefs.SetInt("Ads", 1);
			PlayerPrefs.SetInt("Analytics", 1);
		}
		else
		{
			TitleMain.adConsent = (PlayerPrefs.GetInt("Ads") == 1);
			TitleMain.analyticsConsent = (PlayerPrefs.GetInt("Analytics") == 1);
		}
		LionAdManager.Instance.Initialize();
		if (!AFtitle.noAds)
		{
			//AppLovinCrossPromo.Instance().ShowMRec(3f, 56f, 38f, 50f, -15f);
		}
		if (TitleMain.analyticsConsent)
		{
			//AppsFlyer.setAppsFlyerKey("7yZY6gN88m48knvBDVDCyB");
			//AppsFlyer.setAppID("com.revolverolver.fliptrickster");
			//AppsFlyer.init("7yZY6gN88m48knvBDVDCyB", "AppsFlyerTrackerCallbacks");
			this.CheckRetention();
		}
		yield break;
	}

	private void CheckRetention()
	{
		string text = PlayerPrefs.GetString("LaunchDate", string.Empty);
		if (text == string.Empty)
		{
			text = DateTime.Now.ToString();
			PlayerPrefs.SetString("LaunchDate", text);
		}
		DateTime d;
		DateTime.TryParse(text, out d);
		DateTime now = DateTime.Now;
		long num = (long)(now - d).Days;
		if (num == 1L)
		{
			//AppsFlyer.trackEvent("day1_retained", string.Empty);
		}
		else if (num == 3L)
		{
			//AppsFlyer.trackEvent("day3_retained", string.Empty);
		}
		else if (num == 7L)
		{
			//AppsFlyer.trackEvent("day7_retained", string.Empty);
		}
	}

	public static bool noAds;

	public GameObject playButton;

	public static float rvmodalTimer;

	public static bool watchedReward;

	private float targetPlayTime;

	private float playTime;

	[CompilerGenerated]
	private static Func<bool> _003C_003Ef__mg_0024cache0;
}
