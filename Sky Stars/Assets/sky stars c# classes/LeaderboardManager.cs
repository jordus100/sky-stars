using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class LeaderboardManager : MonoBehaviour {

    private bool success2 = false;
    public GameObject usefulGO;
    private usefulClassForAll u;
	// Use this for initialization
	void Start () {
        u = usefulGO.GetComponent<usefulClassForAll>();
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        if (Application.internetReachability != NetworkReachability.NotReachable)
        LogIn();
    }
    private void Update()
    {
        if ((!usefulClassForAll.gameOn) && Application.internetReachability != NetworkReachability.NotReachable && (!success2)) LogIn();
    }
    private bool LogIn()
    {
        success2 = false;
        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                success2 = true;
            }
            else
            {
                success2 = false;
            }
        });
        return success2;
    }
    public void onShowLeaderboard()
    {
        ((PlayGamesPlatform)Social.Active).ShowLeaderboardUI("CgkIlfrF2rsHEAIQAA");
    }
    public void addScoreToLeaderboard(int score)
    {
        Social.ReportScore(score, "CgkIlfrF2rsHEAIQAA", (bool success3) =>
        {
            if (success3)
            {
                Debug.Log("Update Score Success");

            }
            else
            {
                Debug.Log("Update Score Fail");
            }
        });
    }
    
}
