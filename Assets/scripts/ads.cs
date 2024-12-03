using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AdiveryUnity;
using System;
using UnityEngine.SceneManagement;
public class ads : MonoBehaviour
{
    //private string sample = "38b301f2-5e0c-4776-b671-c6b04a612311";
    private string APP_ID = "80b12fc0-38af-4e3e-a320-d5081a7a4373";
    private string bannerPlacement = "40b471a7-8818-4077-8708-15cd2fe8f9dd";
    private string rewarded_ID = "8c46b0d3-30c8-4e19-aa63-c93a6bb7321f";
    AdiveryListener listener;
    AdiveryListener rewarded_listener;
    BannerAd bannerAd;
    // testing
    // Start is called before the first frame update
    void Start()
    {
        // config adivery 
        Adivery.Configure(APP_ID);
        // set the inter ads
        listener = new AdiveryListener();
        listener.OnError += OnError;
        listener.OnInterstitialAdLoaded += OnInterstitialAdLoaded;
        Adivery.AddListener(listener);
        // set the baner ads 
        bannerAd = new BannerAd(bannerPlacement, BannerAd.TYPE_BANNER, BannerAd.POSITION_BOTTOM);
        bannerAd.OnAdLoaded += OnBannerAdLoaded;
        bannerAd.LoadAd();
        // set the rewarded ad 
        Adivery.PrepareRewardedAd(rewarded_ID);
        rewarded_listener = new AdiveryListener();
        rewarded_listener.OnError += OnError;
        rewarded_listener.OnRewardedAdLoaded += OnRewardedLoaded;
        rewarded_listener.OnRewardedAdClosed += OnRewardedClosed;
        Adivery.AddListener(rewarded_listener);


    }

    // Update is called once per frame
    void Update()
    {
        //text_number.text = number.ToString();
    }
    public void OnInterstitialAdLoaded(object caller, string placementId)
    {
        // Interstitial ad loaded
    }

    public void OnError(object caller, AdiveryError error)
    {
        Debug.Log("placement: " + error.PlacementId + " error: " + error.Reason);
    }
    public void OnBannerAdLoaded(object caller, EventArgs args)
    {
        bannerAd.Show();
        // bannerAd.Hide(); می‌توانید در هر زمانی نمایش تبلیغ بنری را غیرفعال کنید.
    }
    public void OnRewardedLoaded(object caller, string placementId)
    {
        // Rewarded ad loaded
    }

    public void OnRewardedClosed(object caller, AdiveryReward reward)
    {
        // Check if User should receive the reward
        if (reward.IsRewarded)
        {
            PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level") + 1);
            SceneManager.LoadScene(0);
            //getRewardAmount(reward.PlacementId); // Implrement getRewardAmount yourself
        }
    }
    public bool is_loaded_ad()
    {
        if (Adivery.IsLoaded(rewarded_ID))
        {
            return true;
            
        }
        return false;
    }
    public void show_ad()
    {
        Adivery.Show(rewarded_ID);
    }
}
