// dnSpy decompiler from Assembly-CSharp.dll class: LionAdManager
using EasyMobile;
using System;
using UnityEngine;
public class LionAdManager
{
	public void Initialize()
	{
		//UnityEngine.Debug.Log("ADS: Initializing...");
		//AppLovin.SetSdkKey("hBDh6tzZrp-fWye63N4nhbgw8umnTzD99QsGIpq8bpo7lRDppHZVuEQ17Bpa80lIRaTlONt-Af6v5JiubGOUVp");
		//AppLovin.InitializeSdk();
		//AppLovinCrossPromo.Init();
		//Inneractive.InitializeSdk("106517");
		//MoPubAndroid.LoadBannerPluginsForAdUnits(LionAdManager.bannerAdUnits);
		//MoPubAndroid.LoadInterstitialPluginsForAdUnits(LionAdManager.interstitialAdUnits);
		//MoPubAndroid.LoadRewardedVideoPluginsForAdUnits(LionAdManager.rewardedVideoAdUnits);
		//MoPubManager.OnInterstitialLoadedEvent += this.OnInterstitialLoadedEvent;
		//MoPubManager.OnInterstitialFailedEvent += this.OnInterstitialFailedEvent;
		//MoPubManager.OnInterstitialDismissedEvent += this.OnInterstitialDismissedEvent;
		//MoPubManager.OnRewardedVideoLoadedEvent += this.OnRewardedVideoLoadedEvent;
		//MoPubManager.OnRewardedVideoFailedEvent += this.OnRewardedVideoFailedEvent;
		//MoPubManager.OnRewardedVideoFailedToPlayEvent += this.OnRewardedVideoFailedToPlayEvent;
		//MoPubManager.OnRewardedVideoClosedEvent += this.OnRewardedVideoClosedEvent;
		//this.EnsureAdsLoaded();
	}

	public void MarkInterstitialShown()
	{
		//this.interstitialShownAt = DateTime.Now;
	}

	public void SetInterstitialsDisabled(bool interstitialAdsDisabled)
	{
		//this.isInterstitialAdsDisabled = interstitialAdsDisabled;
	}

	public void ShowBanner()
	{
		UnityEngine.Debug.Log("ADS: Showing banner...");
        //MoPubAndroid.ShowBanner(LionAdManager.bannerAdUnits[0], true);
        Advertising.ShowBannerAd(BannerAdPosition.Bottom, BannerAdSize.Banner);
        Advertising.LoadInterstitialAd();
	}

	public void HideBanner()
	{
		//UnityEngine.Debug.Log("ADS: Hiding banner...");
		//MoPubAndroid.ShowBanner(LionAdManager.bannerAdUnits[0], false);
	}

	public void CreateBanner()
	{
		//MoPubAndroid.CreateBanner(LionAdManager.bannerAdUnits[0], MoPubBase.AdPosition.TopCenter);
	}

	public bool HasRewarded()
	{
		return this.isRewardedReady;
	}

	public void EnsureAdsLoaded()
	{
		this.MaybeLoadRewarded();
		this.MaybeLoadInterstitial();
	}

	public bool IsTimeToShowIntersitial()
	{
		if (this.isInterstitialAdsDisabled)
		{
			UnityEngine.Debug.Log("ADS: Not showing interstitial: ad temporary disabled");
			return false;
		}
		bool flag = (DateTime.Now - this.interstitialShownAt).TotalSeconds > 60.0;
		if (!flag)
		{
			UnityEngine.Debug.Log("ADS: Not showing interstitial: waiting from pervious ad");
		}
		return flag;
	}

	public void MaybeShowInterstitial()
	{
		if (this.isInterstitialReady)
		{
            //MoPubAndroid.ShowInterstitialAd(LionAdManager.interstitialAdUnits[0]);
            Advertising.ShowInterstitialAd();
            Advertising.LoadInterstitialAd();
		}
		else
		{
			UnityEngine.Debug.Log("ADS: Interstitial is not ready");
		}
	}

	public void MaybeShowRewarded()
	{
		if (this.isRewardedReady)
		{
			//MoPubAndroid.ShowRewardedVideo(LionAdManager.rewardedVideoAdUnits[0], null);
		}
		else
		{
			UnityEngine.Debug.Log("ADS: Rewarded is not ready");
		}
	}

	public void MaybeLoadRewarded()
	{
		if (!this.isRewardedReady && !this.isRewardedLoading)
		{
			UnityEngine.Debug.Log("ADS: Requesting new rewarded ad...");
			this.isRewardedLoading = true;
			//MoPubAndroid.RequestRewardedVideo(LionAdManager.rewardedVideoAdUnits[0], null, null, null, 99999.0, 99999.0, null);
			if (TitleMain.analyticsConsent)
			{
				////AppsFlyer.trackEvent("request_rewarded", string.Empty);
			}
		}
		else
		{
			UnityEngine.Debug.Log(string.Concat(new object[]
			{
				"ADS: Rewarded ready: ",
				this.isRewardedReady,
				", loading: ",
				this.isRewardedLoading
			}));
		}
	}

	private void MaybeLoadInterstitial()
	{
		if (!this.isInterstitialReady && !this.isInterstitialLoading)
		{
			UnityEngine.Debug.Log("ADS: Requesting new intersitital ad...");
			this.isInterstitialReady = true;
			//MoPubAndroid.RequestInterstitialAd(LionAdManager.interstitialAdUnits[0], string.Empty, string.Empty);
			if (TitleMain.analyticsConsent)
			{
				//AppsFlyer.trackEvent("request_interstitial", string.Empty);
			}
		}
		else
		{
			UnityEngine.Debug.Log(string.Concat(new object[]
			{
				"ADS: Interstitial ready: ",
				this.isRewardedReady,
				", loading: ",
				this.isRewardedLoading
			}));
		}
	}

	private void OnInterstitialLoadedEvent(string adUnitId)
	{
		UnityEngine.Debug.Log("ADS: Intersitital loaded");
		this.isInterstitialReady = true;
		this.isInterstitialLoading = false;
	}

	private void OnInterstitialFailedEvent(string adUnitId, string error)
	{
		UnityEngine.Debug.Log("ADS: Intersitital failed: " + error);
		this.isInterstitialReady = false;
		this.isInterstitialLoading = false;
	}

	private void OnInterstitialDismissedEvent(string adUnitId)
	{
		UnityEngine.Debug.Log("ADS: Intersitital hidden");
		this.isInterstitialReady = false;
		this.isInterstitialLoading = false;
		this.MarkInterstitialShown();
		this.MaybeLoadInterstitial();
	}

	private void OnRewardedVideoLoadedEvent(string adUnitId)
	{
		UnityEngine.Debug.Log("ADS: Rewarded loaded");
		this.isRewardedReady = true;
		this.isRewardedLoading = false;
	}

	private void OnRewardedVideoFailedEvent(string adUnitId, string error)
	{
		UnityEngine.Debug.Log("ADS: Rewarded failed: " + error);
		this.isRewardedReady = false;
		this.isRewardedLoading = false;
	}

	private void OnRewardedVideoClosedEvent(string adUnitId)
	{
		UnityEngine.Debug.Log("ADS: Rewarded closed");
		this.isRewardedLoading = false;
		this.isRewardedReady = false;
		this.MaybeLoadRewarded();
	}

	private void OnRewardedVideoFailedToPlayEvent(string adUnitId, string error)
	{
		UnityEngine.Debug.Log("ADS: Rewarded failed to play: " + error);
	}

	private const int INTERSTITIAL_AD_INTERVAL_SEC = 60;

	public static string[] bannerAdUnits = new string[]
	{
		"443306862fb24129bbbe61719814eeed"
	};

	public static string[] interstitialAdUnits = new string[]
	{
		"3aa1720af676444baccfe4f13642e89a"
	};

	public static string[] rewardedVideoAdUnits = new string[]
	{
		"b1dd31da73084ad0a4145d6e9092df77"
	};

	public static LionAdManager Instance = new LionAdManager();

	private bool isInterstitialReady;

	private bool isInterstitialLoading;

	private bool isInterstitialAdsDisabled;

	private DateTime interstitialShownAt;

	private bool isRewardedReady;

	private bool isRewardedLoading;
}
