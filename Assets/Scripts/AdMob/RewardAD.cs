using System;
using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;

public class RewardAD : MonoBehaviour
{
    private RewardedAd _rewardedAd;
    private const string adUnitId = "ca-app-pub-3940256099942544/5224354917";
    
    public delegate void RewardADHandler();
    public event RewardADHandler Notify;
    public void Start()
    {
        _rewardedAd = new RewardedAd(adUnitId);
        
        // Called when an ad is shown.
        _rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        // Called when an ad request failed to show.
        _rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        // Called when the user should be rewarded for interacting with the ad.
        _rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        // Called when the ad is closed.
        _rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        _rewardedAd.LoadAd(request);
    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        print("HandleRewardedAdOpening event received");
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        print(
            "HandleRewardedAdFailedToShow event received with message: "
                             + args.Message);
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        print("HandleRewardedAdClosed event received");
        Notify?.Invoke();
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {

    }

    public void Show()
    {
        if (_rewardedAd.IsLoaded())
            _rewardedAd.Show();
    }
    private void OnDisable()
    {
        _rewardedAd.OnAdOpening -= HandleRewardedAdOpening;
        _rewardedAd.OnAdFailedToShow -= HandleRewardedAdFailedToShow;
        _rewardedAd.OnUserEarnedReward -= HandleUserEarnedReward;
        _rewardedAd.OnAdClosed -= HandleRewardedAdClosed;
    }
    
}
