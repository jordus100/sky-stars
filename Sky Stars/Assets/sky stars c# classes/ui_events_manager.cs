using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
public class ui_events_manager : MonoBehaviour
{
    public Text debug;
    public string leaderboard = "CgkIlfrF2rsHEAIQAA";
    void Start()
    {
        // recommended for debugging:
        PlayGamesPlatform.DebugLogEnabled = true;

        // Activate the Google Play Games platform
        PlayGamesPlatform.Activate();
        //LogIn();
        //OnShowLeaderBoard();

    }

    /// <summary>
    /// Login In Into Your Google+ Account
    /// </summary>
    public void LogIn()
    {
        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                debug.text = "Login Sucess";
            }
            else
            {
                debug.text = "Login failed";
            }
        });
    }
    /// <summary>
    /// Shows All Available Leaderborad
    /// </summary>
    public void OnShowLeaderBoard()
    {
        debug.text = "showing leaderboard";
        //Social.ShowLeaderboardUI (); // Show all leaderboard
        ((PlayGamesPlatform)Social.Active).ShowLeaderboardUI("CgkIlfrF2rsHEAIQAA"); // Show current (Active) leaderboard
        //PlayGamesPlatform.Instance.ShowLeaderboardUI(leaderboard);
        //((PlayGamesPlatform)Social.Active).ShowLeaderboardUI(leaderboard);
    }
    /// <summary>
    /// Adds Score To leader board
    /// </summary>
    public void OnAddScoreToLeaderBorad()
    {
                Social.ReportScore(100, "CgkIlfrF2rsHEAIQAA", (bool success2) =>
                {
                    if (success2)
                    {
                        Debug.Log("Update Score Success");

                    }
                    else
                    {
                        Debug.Log("Update Score Fail");
                    }
                });
            }

        
            
    /// <summary>
    /// On Logout of your Google+ Account
    /// </summary>
    public void OnLogOut()
    {
        ((PlayGamesPlatform)Social.Active).SignOut();
    }





    /*
    public void OnPlayBntClick()
    {
        Object[] objects = FindObjectsOfType(typeof(GameObject));
        foreach (GameObject go in objects)
        {
            go.SendMessage("OnMenuHide", SendMessageOptions.DontRequireReceiver);
        }
    }
    */
}

