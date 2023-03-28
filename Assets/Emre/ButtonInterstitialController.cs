using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInterstitialController : MonoBehaviour
{

    [SerializeField] AdManager _adManager;

    private void Awake()
    {
        _adManager.Init();
    }

    

    public void ShowInterstitialMenuPlayButton()
    {
        _adManager.InterstatialAdManager.ShowAd();
        StartCoroutine(ResetInterstitial());

    }



    IEnumerator ResetInterstitial()
    {
        Debug.Log("----a-------");
        _adManager.InterstatialAdManager.LoadAds();
        yield return new WaitForSeconds(5f);
        _adManager.InterstatialAdManager.ShowAd();
        StartCoroutine(ResetInterstitial());


    }

}
