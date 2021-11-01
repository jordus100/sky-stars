using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rate_app_script : MonoBehaviour {

    public GameObject rateAppPanel;
    public float horizontalFrac;
    public float heightWidthFrac;
    public Text yesText;
    public Text noText;
    public GameObject usefulGO;
    private usefulClassForAll u;
	// Use this for initialization
	void Start () {
        u = usefulGO.GetComponent<usefulClassForAll>();
	}
	
	// Update is called once per frame
	void Update () {
        yesText.fontSize = noText.cachedTextGenerator.fontSizeUsedForBestFit;
	}
    public void offerRateApp()
    {
        u.SendMessageToAll("onMenuDisable");
        RectTransform rect = rateAppPanel.GetComponent<RectTransform>();
        float frac = horizontalFrac * heightWidthFrac;
        //Debug.Log("FRACCCCCCC222222");
        float frac2 = frac * ((float)Screen.width / Screen.height);
        rect.anchorMin = new Vector2(0.5f - horizontalFrac / 2f, 0.5f - frac2 / 2f);
        rect.anchorMax = new Vector2(0.5f + horizontalFrac / 2f, 0.5f + frac2 / 2f);
    }
    public void yesBtnClick()
    {
        PlayerPrefs.SetInt("numberGameRun", 0);
        onRateAppPanelHide();
        u.SendMessageToAll("onMenuEnable");
        Application.OpenURL("http://play.google.com/store/apps/details?id=" + Application.identifier);
    }
    public void noBtnClick()
    {
        PlayerPrefs.SetInt("numberGameRun", 0);
        onRateAppPanelHide();
        u.SendMessageToAll("onMenuEnable");
    }
    private void onRateAppPanelHide()
    {
        RectTransform rect = rateAppPanel.GetComponent<RectTransform>();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.zero;
    }
}
