using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonInterstitialController : MonoBehaviour
{

    [SerializeField] AdManager _adManager;
    
    public TextMeshProUGUI coinText;

    bool _isRewardEarned;
    bool _canWatchRewarded = true;
    private void Awake()
    {
        _adManager.Init();
       
    }

    

    public void ShowInterstitialMenuPlayButton()
    {
        _adManager.InterstatialAdManager.ShowAd();
       

    }



    IEnumerator ResetInterstitial()
    {
        Debug.Log("----a-------");
        _adManager.InterstatialAdManager.LoadAds();
        yield return new WaitForSeconds(5f);
        _adManager.InterstatialAdManager.ShowAd();
        StartCoroutine(ResetInterstitial());


    }

   
    public void ShowRewardAd()
    {


        if (_adManager.RewardedAdManager.IsRewardedAdReady())
        {
            _adManager.RewardedAdManager.RegisterOnUserEarnedRewarededEvent(OnUserEarnedReward);
            _adManager.RewardedAdManager.RegisterOnAdClosedEvent(OnRewardedAdClosed);

            ShowRewardedAd();
        }

    }
    private void OnRewardedAdClosed(IronSourceAdInfo info)
    {

        _adManager.RewardedAdManager.UnRegisterOnUserEarnedRewarededEvent(OnUserEarnedReward);
        _adManager.RewardedAdManager.UnRegisterOnAdClosedEvent(OnRewardedAdClosed);

        if (_isRewardEarned)
        {
            Currency.coinAmount = GameState.Instance.AddCoins(150);
            this.coinText.text = Currency.coinAmount.ToString();
        }

        _isRewardEarned = false;
       


    }
    private void OnUserEarnedReward(IronSourcePlacement placement, IronSourceAdInfo info)
    {
        _isRewardEarned = true;
       
    }

    private void ShowRewardedAd()
    {

        _adManager.RewardedAdManager.ShowAd();
    }
}
