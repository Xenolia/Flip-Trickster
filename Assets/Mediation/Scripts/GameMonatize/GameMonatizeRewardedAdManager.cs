using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if GAME_MONOTIZE
public class GameMonatizeRewardedAdManager : IRewardedAdManager
{
    private Action<IronSourceAdInfo> OnRewardedAdOpened;
    private Action<IronSourceAdInfo> OnRewardedAdClosed;
    private Action<IronSourcePlacement, IronSourceAdInfo> OnUserEarnedReward;
    private float _lastTimeScale;

    private GameMonetize _instance;

    private static bool _isAddReady = true;

    private int counter = 0;
    public GameMonatizeRewardedAdManager()
    {
        _instance = GameMonetize.Instance;
    }

    public bool IsRewardedAdReady()
    {
       return _isAddReady;
    }

    public void LoadAds()
    {
        
    }

    public void RegisteOnAdAvailableEvent(Action<IronSourceAdInfo> method)
    {
     
    }

    public void RegisterIronSourceEvents()
    {
        GameMonetize.OnResumeGame += OnAdClosed;
        GameMonetize.OnPauseGame += OnAdOpened;
    
    }
    private void UnRegisterIronSourceEvents()
    {
        GameMonetize.OnResumeGame -= OnAdClosed;
        GameMonetize.OnPauseGame -= OnAdOpened;
    }

    public void RegisterOnAdClickedEvent(Action<IronSourcePlacement, IronSourceAdInfo> method)
    {
       
    }

    public void RegisterOnAdClosedEvent(Action<IronSourceAdInfo> method)
    {
        OnRewardedAdClosed += method;
    }

    public void RegisterOnAdLoadFailedEvent(Action<IronSourceError> method)
    {
       
    }

    public void RegisterOnAdOpenedEvent(Action<IronSourceAdInfo> method)
    {
        OnRewardedAdOpened += method;
    }

    public void RegisterOnAdReadyEvent(Action<IronSourceAdInfo> method)
    {
      
    }

    public void RegisterOnAdShowFailedEvent(Action<IronSourceError, IronSourceAdInfo> method)
    {
      
    }

    public void RegisterOnAdUnavailableEvent(Action method)
    {
      
    }

    public void RegisterOnUserEarnedRewarededEvent(Action<IronSourcePlacement, IronSourceAdInfo> method)
    {
        OnUserEarnedReward += method;
    }

    public void ShowAd()
    {
        _instance.ShowAd();
    }

    public void TerminateAd()
    {
        UnRegisterIronSourceEvents();
    }

    public void UnRegisteOnAdAvailableEvent(Action<IronSourceAdInfo> method)
    {
  
    }

    public void UnRegisterOnAdClickedEvent(Action<IronSourcePlacement, IronSourceAdInfo> method)
    {

    }

    public void UnRegisterOnAdClosedEvent(Action<IronSourceAdInfo> method)
    {
        OnRewardedAdClosed -= method;
    }

    public void UnRegisterOnAdLoadFailedEvent(Action<IronSourceError> method)
    {

    }

    public void UnRegisterOnAdOpenedEvent(Action<IronSourceAdInfo> method)
    {
        OnRewardedAdOpened -= method;
    }

    public void UnRegisterOnAdReadyEvent(Action<IronSourceAdInfo> method)
    {

    }

    public void UnRegisterOnAdShowFailedEvent(Action<IronSourceError, IronSourceAdInfo> method)
    {
     
    }

    public void UnRegisterOnAdUnavailableEvent(Action method)
    {
   
    }

    public void UnRegisterOnUserEarnedRewarededEvent(Action<IronSourcePlacement, IronSourceAdInfo> method)
    {
        OnUserEarnedReward -= method;
    }

    private void OnAdOpened()
    {
        _isAddReady = false;
        float x = 0;

        DOTween.To(() => x, newValue => x = newValue, 1f, 110f).SetUpdate(true).OnComplete(SetTrue);

        OnRewardedAdOpened?.Invoke(null);
        AudioListener.volume = 0;

        Time.timeScale = 0;

        void SetTrue()
        {
            _isAddReady = true;
        }
    }

    private void OnAdClosed()
    {
        Time.timeScale = 1;
        AudioListener.volume = 1;

        OnUserEarnedReward?.Invoke(null, null);
        OnRewardedAdClosed?.Invoke(null);
    }
}
#endif