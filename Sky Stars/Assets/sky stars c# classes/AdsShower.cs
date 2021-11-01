using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdsShower : MonoBehaviour {
    private InterstitialAd ad;
    private AdRequest request;
    private RewardBasedVideoAd videoAd;
	// Use this for initialization
	void Start () {
        //MobileAds.Initialize("ca-app-pub-8107532531338868~6854878153");
        this.videoAd = RewardBasedVideoAd.Instance;
        FullscreenAdLoad();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void FullscreenAdLoad()
    {
        ad = new InterstitialAd("ca-app-pub-3940256099942544/1033173712");
        request = new AdRequest.Builder().Build();
        ad.LoadAd(request);
    }
    public void OnFullscreenAdShow()
    {
        if (ad.IsLoaded())
        {
            ad.Show();
            Debug.Log("Ad is loaded");
        }
        else
        {
            Debug.Log("Not loaded");
        }
    }
    private void RequestRewardedVideo()
    {

        string adUnitId = "ca-app-pub-3940256099942544/5224354917";
        // Create an empty ad request.
        AdRequest request2 = new AdRequest.Builder().Build();
        // Load the rewarded video ad with the request.
        this.videoAd.LoadAd(request2, adUnitId);
    }
}
