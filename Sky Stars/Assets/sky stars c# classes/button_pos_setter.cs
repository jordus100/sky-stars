using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class button_pos_setter : MonoBehaviour {

    private float[,] menuBtnsAnchors = new float[4, 2];
    private int PlayBtnIndex = 0;
    private int MoreBtnIndex = 0;
    private int LeadBtnIndex = 0;
    private int backBtn2Index = 0;
    private RectTransform rect;
    public GameObject aboutTitleText;
    private float screen_width_in_world_units, screenHeightInUnits, screenWidthInUnits;
    // Use this for initialization

    private void Awake()
    {
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
        
        setAnchorValues();
        Button[] btns = FindObjectsOfType<Button>();
        for(int i=0; i<btns.Length; i++)
        {
            switch (btns[i].name)
            {
                case "PlayBtn":
                    {
                        float[] anchors = getAnchorsForSquare("horizontal", new float[] { menuBtnsAnchors[0, 0], menuBtnsAnchors[0, 1] }, 0.25f);
                        btns[i].GetComponent<RectTransform>().anchorMin = new Vector2(anchors[0], anchors[1]);
                        btns[i].GetComponent<RectTransform>().anchorMax = new Vector2(anchors[2], anchors[3]);
                        PlayBtnIndex = i;
                    }
                    break;
                case "LeadBtn":
                    {
                        float[] anchors = getAnchorsForSquare("horizontal", new float[] { menuBtnsAnchors[1, 0], menuBtnsAnchors[1, 1] }, 0.15f);
                        btns[i].GetComponent<RectTransform>().anchorMin = new Vector2(anchors[0], anchors[1]);
                        btns[i].GetComponent<RectTransform>().anchorMax = new Vector2(anchors[2], anchors[3]);
                        LeadBtnIndex = i;
                    }
                    break;
                case "MoreBtn":
                    {
                        float[] anchors = getAnchorsForSquare("horizontal", new float[] { menuBtnsAnchors[2, 0], menuBtnsAnchors[2, 1] }, 0.15f);
                        btns[i].GetComponent<RectTransform>().anchorMin = new Vector2(anchors[0], anchors[1]);
                        btns[i].GetComponent<RectTransform>().anchorMax = new Vector2(anchors[2], anchors[3]);
                        MoreBtnIndex = i;

                    }
                    break;
                case "BackBtn":
                    {
                        float[] anchors = getAnchorsForSquare("horizontal", new float[] { menuBtnsAnchors[3, 0], menuBtnsAnchors[3, 1] }, 0.1f);
                        btns[i].GetComponent<RectTransform>().anchorMin = new Vector2(anchors[0], anchors[1]);
                        btns[i].GetComponent<RectTransform>().anchorMax = new Vector2(anchors[2], anchors[3]);
                        RectTransform rect = btns[i].GetComponent<RectTransform>();
                        rect.anchorMax += new Vector2(0f - rect.anchorMin.x, 0f);
                        rect.anchorMin += new Vector2(0f - rect.anchorMin.x, 0f);

                    }
                    break;
                case "BackBtn2":
                    {
                        float[] anchors = getAnchorsForSquare("horizontal", new float[] { menuBtnsAnchors[3, 0], menuBtnsAnchors[3, 1] }, 0.1f);
                        btns[i].GetComponent<RectTransform>().anchorMin = new Vector2(anchors[0], anchors[1]);
                        btns[i].GetComponent<RectTransform>().anchorMax = new Vector2(anchors[2], anchors[3]);
                        rect = btns[i].GetComponent<RectTransform>();
                        rect.anchorMax += new Vector2(0f - rect.anchorMin.x, 0f);
                        rect.anchorMin += new Vector2(0f - rect.anchorMin.x, 0f);
                        backBtn2Index = i;
                    }
                    break;

            }
        }
        rect = btns[backBtn2Index].GetComponent<RectTransform>();
        if (rect.anchorMax.x >= aboutTitleText.GetComponent<RectTransform>().anchorMin.x)
        aboutTitleText.GetComponent<RectTransform>().anchorMin = new Vector2(rect.anchorMax.x + 0.01f, aboutTitleText.GetComponent<RectTransform>().anchorMin.y);

        /*
        while (btns[PlayBtnIndex].GetComponent<RectTransform>().anchorMax.x > btns[MoreBtnIndex].GetComponent<RectTransform>().anchorMin.x)
        {
            RectTransform rect = btns[PlayBtnIndex].GetComponent<RectTransform>();
            float frac = screenWidthInUnits / screenHeightInUnits;
            rect.anchorMax = new Vector2(rect.anchorMax.x - 0.01f, rect.anchorMax.y-(0.01f * frac));
            rect.anchorMin = new Vector2(rect.anchorMin.x + 0.01f, rect.anchorMin.y+(0.01f * frac));
            rect = btns[MoreBtnIndex].GetComponent<RectTransform>();
            rect.anchorMax = new Vector2(rect.anchorMax.x - 0.01f, rect.anchorMax.y - (0.01f * frac));
            rect.anchorMin = new Vector2(rect.anchorMin.x + 0.01f, rect.anchorMin.y + (0.01f * frac));
            rect = btns[LeadBtnIndex].GetComponent<RectTransform>();
            rect.anchorMax = new Vector2(rect.anchorMax.x - 0.01f, rect.anchorMax.y - (0.01f * frac));
            rect.anchorMin = new Vector2(rect.anchorMin.x + 0.01f, rect.anchorMin.y + (0.01f * frac));
            Debug.Log("Improving button positions");
        }
        */


    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private float[] getAnchorsForSquare(string scaleRelation, float[] centerRelPos, float screenFrac)
    {
        float[] anchors = new float[4];
        if (scaleRelation == "vertical")
        {
            Debug.Log("SCREENfRACiNuNITS:");
            //Debug.Log(centerRelPos[0]);
            //Debug.Log(centerRelPos[1]);
            //Debug.Log("Anchors 1 3");
            anchors[0] = centerRelPos[0] - screenFrac / 2f;
            anchors[2] = centerRelPos[0] + screenFrac / 2f;
            float screenFracInUnits = screenFrac * screenWidthInUnits;
            //Debug.Log(screenFrac);
            //Debug.Log(screenWidthInUnits);
            //Debug.Log(screenFracInUnits);
            float heightRel = screenFracInUnits / screenHeightInUnits;
            
            anchors[1] = centerRelPos[1] - heightRel / 2f;
            anchors[3] = centerRelPos[1] + heightRel / 2f;
            //Debug.Log(anchors[1]);
            //Debug.Log(anchors[3]);
            return anchors;
        }
        else
        {
            anchors[1] = centerRelPos[1] - screenFrac / 2f;
            anchors[3] = centerRelPos[1] + screenFrac / 2f;
            float screenFracInUnits = screenFrac * screenHeightInUnits;
            float heightRel = screenFracInUnits / screenWidthInUnits;
            anchors[0] = centerRelPos[0] - heightRel / 2f;
            anchors[2] = centerRelPos[0] + heightRel / 2f;
            return anchors;
        }
    }
    private void setAnchorValues()
    {
        float[,] v = new float[4, 2];
        for (int i = 0; i < v.GetLength(0); i++)
        {
            for (int e = 0; e < v.GetLength(1); e++)
            {
                v[i,e] = 0f;
            }
        }
        v[2, 0] = 0.8f;
        v[2, 1] = 0.1f;
        v[1, 0] = 0.2f;
        v[1, 1] = 0.1f;
        v[0, 0] = 0.5f;
        v[0, 1] = 0.32f;
        v[3, 0] = 0.05f;
        v[3, 1] = 0.95f;
        for (int i = 0; i < v.GetLength(0); i++)
        {
            for (int e = 0; e < v.GetLength(1); e++)
            {
                menuBtnsAnchors[i, e] = v[i, e];
            }
        }
    }
}
