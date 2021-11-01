using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class score_counting : MonoBehaviour {
    private int crosses_order_number = 0;
    public GameObject[] crosses = new GameObject[2];
    public Sprite redcross;
    private bool endgame = false;
    public Text score;
    public Sprite greycross;
    public Sprite emptySprite;
    public GameObject usefulGO;
    private usefulClassForAll u;
    public GameObject SoundManager;
    private AudioSource loseSound;
    public GameObject backgroundMusicManager;
    private AudioSource backMusic;
    private IEnumerator wait_2_seconds_for_the_end()
    {
        u.SendMessageToAll("onGamePause");
        yield return new WaitForSeconds(0.55f);
        for (int i = 0; i < crosses.Length; i++)
        {
            crosses[i].GetComponent<Image>().sprite = emptySprite;
        }
        yield return new WaitForSeconds(0.55f);
        for (int i = 0; i < crosses.Length; i++)
        {
            crosses[i].GetComponent<Image>().sprite = redcross;
        }
        yield return new WaitForSeconds(0.55f);
        for (int i = 0; i < crosses.Length; i++)
        {
            crosses[i].GetComponent<Image>().sprite = emptySprite;
        }
        yield return new WaitForSeconds(0.55f);
        for (int i = 0; i < crosses.Length; i++)
        {
            crosses[i].GetComponent<Image>().sprite = redcross;
        }
        yield return new WaitForSeconds(0.55f);
        for (int i = 0; i < crosses.Length; i++)
        {
            crosses[i].GetComponent<Image>().sprite = emptySprite;
        }
        yield return new WaitForSeconds(0.55f);
        for (int i = 0; i < crosses.Length; i++)
        {
            crosses[i].GetComponent<Image>().sprite = redcross;
        }
        yield return new WaitForSeconds(0.55f);
        for (int i = 0; i < crosses.Length; i++)
        {
            crosses[i].GetComponent<Image>().sprite = emptySprite;
        }
        backMusic.Stop();
        u.SendMessageToAll("onMenuShow");
        //Debug.Log("GAME OVER!!!");

    }
    public void OnStarMiss()
    {
        if (!endgame)
        {
            //Debug.Log(crosses_order_number);
            crosses[crosses_order_number].GetComponent<Image>().sprite = redcross;
            Color previous_color = crosses[crosses_order_number].GetComponent<Image>().color;
            crosses[crosses_order_number].GetComponent<Image>().color = new Color(previous_color.r, previous_color.g, previous_color.b, 0.7f);


            if (crosses_order_number == 1 && endgame == false)
            {
                endgame = true;
                u.LoadData();
                int Highscore = u.highscore;
                u.Save(Highscore, Int32.Parse(score.text));
                int numGameRun = PlayerPrefs.GetInt("numberGameRun");
                PlayerPrefs.SetInt("numberGameRun", numGameRun + 1);
                usefulClassForAll.afterGame = true;
                StartCoroutine(wait_2_seconds_for_the_end());
                
            }
            else
            {
                //Debug.Log("powiekszanie_numeru!");
                crosses_order_number += 1;
            }
        }
    }
    public void OnScoreIncrease()
    {
        int score = Int32.Parse(this.score.text);
        int score2 = score + 1;
        this.score.text = score2.ToString();
    }
    
    public void onGameReset()
    {
        for (int i = 0; i < crosses.Length; i++)
        {
            crosses[i].GetComponent<Image>().sprite = greycross;
            Color col = crosses[i].GetComponent<Image>().color;
            crosses[i].GetComponent<Image>().color = new Color(col.r, col.g, col.b, 0.392f);
        }
        endgame = false;
        crosses_order_number = 0;
        score.text = "0";
    }

    private void Awake()
    {
        u = usefulGO.GetComponent<usefulClassForAll>();
        loseSound = SoundManager.GetComponent<AudioSource>();
        backMusic = backgroundMusicManager.GetComponent<AudioSource>();
    }
}
