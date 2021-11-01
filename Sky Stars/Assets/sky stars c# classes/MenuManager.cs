using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;
using System;

public class MenuManager : MonoBehaviour{
    public GameObject scorePanel;
    public GameObject usefulGO;
    public Button[] btns = new Button[3];
    public GameObject mainMenuPanel;
    public GameObject menuCover;
    public GameObject column, column_2, column_3, column_4;
    public Text highscoreValue;
    public Text lastscoreValue;
    public Text bestscoreValue;
    public Text bestscoreText;
    public Text lastscoreText;
    public Text highscoreText;
    public int numberGameRunNow = 0;
    private InterstitialAd ad;
    private AdRequest request;
    private RectTransform rect;
    private float screen_width_in_world_units, screenHeightInUnits, screenWidthInUnits;
    private usefulClassForAll u;
    public GameObject leaderboardManager;
    private LeaderboardManager leadManager;
    public GameObject SoundManager;
    private AudioSource playSound;
    public GameObject backgroundMusicManager;
    private AudioSource backMusic;
    //private float screenHeightInUnits;
    //private float screenWidthInUnits;
    // Use this for initialization
    private void Awake()
    {
        //MobileAds.Initialize("ca-app-pub-8107532531338868~6854878153");
        Debug.Log(Application.persistentDataPath);
        u = usefulGO.GetComponent<usefulClassForAll>();
        playSound = SoundManager.GetComponent<AudioSource>();
        backMusic = backgroundMusicManager.GetComponent<AudioSource>();
        leadManager = leaderboardManager.GetComponent<LeaderboardManager>();
        screenHeightInUnits = Camera.main.orthographicSize * 2;

        screenWidthInUnits = screenHeightInUnits * Screen.width / Screen.height;
        if (((float)Screen.width / Screen.height) > (2.0f / 2.3f))
        {
            float distance_from_middle = Camera.main.orthographicSize * 2 / 2.3f;
            screen_width_in_world_units = distance_from_middle * 2.0f;
        }
        else
        {
            screen_width_in_world_units = screenWidthInUnits;
        }
    }
    void Start () {
        
        
        ad = new InterstitialAd("ca-app-pub-3940256099942544/1033173712");
        ad.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        request = new AdRequest.Builder().Build();
        ad.LoadAd(request);
        u.SendMessageToAll("onMenuShow");
        //u.SendMessageToAll("onGamePause");
        //u.SendMessageToAll("onGameReset");
        //SendMessageToAll("offerRateApp");
    }


    // Update is called once per frame
    void Update () {
        highscoreText.fontSize = lastscoreText.cachedTextGenerator.fontSizeUsedForBestFit;
	}
    public void onMenuShow()
    {
        backMusic.Stop();
        usefulClassForAll.gameOn = false;
        u.SendMessageToAll("onGamePause");
        u.SendMessageToAll("onGameReset");
        column.transform.localScale = new Vector3(1f, screenHeightInUnits);
        column.transform.position = new Vector3(screenWidthInUnits / 2f + column.transform.localScale.x / 2f, 0f);
        column_2.transform.localScale = new Vector3(1f, screenHeightInUnits);
        column_2.transform.position = new Vector3(screenWidthInUnits / 2f * -1f - column_2.transform.localScale.x / 2f, 0f);
        column_3.transform.localScale = new Vector3(screenWidthInUnits, 1f);
        column_3.transform.position = new Vector3(0f, screenHeightInUnits / 2f + column_3.transform.localScale.y / 2f);
        column_4.transform.localScale = new Vector3(screenWidthInUnits, 1f);
        column_4.transform.position = new Vector3(0f, screenHeightInUnits / 2f * -1f - column_4.transform.localScale.y / 2f);
        rect = scorePanel.GetComponent<RectTransform>();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.zero;
        if (PlayerPrefs.GetString("FirstRun") == "")
        {
            PlayerPrefs.SetString("FirstRun", "yes");
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }
        u.LoadData();
        if (u.highscore >= u.lastscore)
        {
            highscoreText.text = "HIGHSCORE";
            lastscoreText.text = "LAST SCORE";
            highscoreValue.text = u.highscore.ToString();
            lastscoreValue.text = u.lastscore.ToString();
            bestscoreText.text = "";
            bestscoreValue.text = "";
        }
        else
        {
            u.Save(u.lastscore, u.lastscore);
            bestscoreText.text = "NEW BEST SCORE!!!";
            bestscoreValue.text = u.lastscore.ToString();
            highscoreText.text = "";
            highscoreValue.text = "";
            lastscoreText.text = "";
            lastscoreValue.text = "";
        }
        u.LoadData();
        leadManager.addScoreToLeaderboard(u.highscore);
        onMenuEnable();
        rect = mainMenuPanel.GetComponent<RectTransform>();
        rect.anchorMin = new Vector2(0f, 0f);
        rect.anchorMax = new Vector2(1f, 1f);
        if (PlayerPrefs.GetInt("numberGameRun") > 35)
        {
            u.SendMessageToAll("offerRateApp");
        }
        if(ad.IsLoaded() && usefulClassForAll.afterGame && ((DateTime.Now.Hour - u.timeAdsRemoved.AddHours(4D).Hour>0)|| DateTime.Now.Day - u.timeAdsRemoved.AddHours(4D).Day>0 || DateTime.Now.Year - u.timeAdsRemoved.AddHours(4D).Year>0))
        {
            //highscoreText.text = (DateTime.Now.Hour - u.timeAdsRemoved.AddHours(4D).Hour).ToString();
            usefulClassForAll.afterGame = false;
            ad.Show();
            ad = new InterstitialAd("ca-app-pub-3940256099942544/1033173712");
            ad.OnAdFailedToLoad += HandleOnAdFailedToLoad;
            request = new AdRequest.Builder().Build();
            ad.LoadAd(request);
        }
        
    }
    public void onMenuHide()
    {
        usefulClassForAll.gameOn = true;
        rect = mainMenuPanel.GetComponent<RectTransform>();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.zero;
        backMusic.Play();
    }
    public void onMenuDisable()
    {
        for (int i = 0; i < btns.Length; i++)
        {
            btns[i].interactable = false;
        }
        rect = menuCover.GetComponent<RectTransform>();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = new Vector2(1f, 1f);
    }
    public void onMenuEnable()
    {
        for (int i = 0; i < btns.Length; i++)
        {
            btns[i].interactable = true;
        }
        rect = menuCover.GetComponent<RectTransform>();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.zero;
    }
    public void PlayBtnClick()
    {
        //playSound.Play();
        u.SendMessageToAll("onMenuHide");
        u.SendMessageToAll("onGameReset");
    }
    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        request = new AdRequest.Builder().Build();
        ad.LoadAd(request);
    }
    public void MoreBtnClick()
    {
        u.SendMessageToAll("onMenu2Show");
    }
}

