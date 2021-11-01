using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;
using System;

public class RemoveAds : MonoBehaviour {

    private RewardBasedVideoAd videoAd;
    public GameObject removeAdsPanel;
    public float horizontalFrac;
    public float heightWidthFrac;
    public Text yesText;
    public Text noText;
    public GameObject usefulGO;
    private usefulClassForAll u;
    public Text panelText;
    public GameObject videoNotLoadedYetPanel;
    //private bool videoAdLoaded = false;
    // Use this for initialization
    void Start () {
        u = usefulGO.GetComponent<usefulClassForAll>();
        this.videoAd = RewardBasedVideoAd.Instance;
        videoAd.OnAdFailedToLoad += onVideoFailedToLoad;
        videoAd.OnAdRewarded += onVideoRewarded;
        RequestRewardedVideo();
    }
	
    private void RequestRewardedVideo()
    {

        string adUnitId = "ca-app-pub-3940256099942544/5224354917";
        // Create an empty ad request.
        AdRequest request2 = new AdRequest.Builder().Build();
        // Load the rewarded video ad with the request.
        this.videoAd.LoadAd(request2, adUnitId);
    }
    // Update is called once per frame
    void Update()
    {
        yesText.fontSize = noText.cachedTextGenerator.fontSizeUsedForBestFit;
        
    }
    public void offerRemoveAds()
    {
        u.SendMessageToAll("onMenu2Disable");
        RectTransform rect = removeAdsPanel.GetComponent<RectTransform>();
        float frac = horizontalFrac * heightWidthFrac;
        //Debug.Log("FRACCCCCCC222222");
        float frac2 = frac * ((float)Screen.width / Screen.height);
        rect.anchorMin = new Vector2(0.5f - horizontalFrac / 2f, 0.5f - frac2 / 2f);
        rect.anchorMax = new Vector2(0.5f + horizontalFrac / 2f, 0.5f + frac2 / 2f);
    }
    public void yesBtnClick()
    {
        if (this.videoAd.IsLoaded() && !(Application.internetReachability == NetworkReachability.NotReachable))
        {
            Debug.Log("AD LOADED(LOL)");
            onRemoveAdsPanelHide();
            this.videoAd.Show();
            RequestRewardedVideo();
        }
        else StartCoroutine(onVideoNotLoadedYetPanel());
    }
    public void noBtnClick()
    {
        onRemoveAdsPanelHide();
    }
    public void onRemoveAdsPanelHide()
    {
        u.SendMessageToAll("onMenu2Enable");
        RectTransform rect = removeAdsPanel.GetComponent<RectTransform>();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.zero;
    }
    private IEnumerator onVideoNotLoadedYetPanel()
    {
        RequestRewardedVideo();
        u.videoAdErrorPanelIsShowed = true;
        RectTransform rect = videoNotLoadedYetPanel.GetComponent<RectTransform>();
        float frac = horizontalFrac * heightWidthFrac;
        //Debug.Log("FRACCCCCCC222222");
        float frac2 = frac * ((float)Screen.width / Screen.height);
        rect.anchorMin = new Vector2(0.5f - horizontalFrac / 2f, 0.5f - frac2 / 2f);
        rect.anchorMax = new Vector2(0.5f + horizontalFrac / 2f, 0.5f + frac2 / 2f);
        yield return new WaitForSeconds(5f);
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.zero;
        onRemoveAdsPanelHide();
        u.videoAdErrorPanelIsShowed = false;
    }
    public void onVideoNotLoadedYetPanelHide()
    {
        RectTransform rect = videoNotLoadedYetPanel.GetComponent<RectTransform>();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.zero;
    }
    private void onVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        RequestRewardedVideo();
    }
    public void onVideoRewarded(object sender, Reward args)
    {
        u.LoadData();
        u.timeAdsRemoved = DateTime.Now;
        u.Save(u.highscore, u.lastscore);
        onRemoveAdsPanelHide();
    }

}
