using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;

public class Menu2Manager : MonoBehaviour {

    public GameObject menuPanel, menuCover;
    public Button[] btns = new Button[3];
    private RectTransform rect;
    public Text aboutText;
    public GameObject aboutPanel;
    public Button backBtn2;
    public ScrollRect aboutScrollRect;
    public GameObject menu2Cover;
    public GameObject usefulGO;
    private usefulClassForAll u;

    private void Start()
    {
        u = usefulGO.GetComponent<usefulClassForAll>();
        aboutText.fontSize = (int)(((float)(Screen.height) * 0.85f) / 17f);
        if (aboutText.fontSize < 30) aboutText.fontSize = 30;
    }
    public void onMenu2Show()
    {
        onMenu2Enable();
        rect = menuPanel.GetComponent<RectTransform>();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = new Vector2(1f, 1f);
    }
    public void onMenu2Enable()
    {
        for (int i = 0; i < btns.Length; i++)
        {
            btns[i].interactable = true;
        }
        rect = menu2Cover.GetComponent<RectTransform>();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.zero;
    }
    public void onMenu2Hide()
    {
        rect = menuPanel.GetComponent<RectTransform>();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.zero;
    }
    public void onMenu2Disable()
    {
        for (int i = 0; i < btns.Length; i++)
        {
            btns[i].interactable = false;
        }
        rect = menu2Cover.GetComponent<RectTransform>();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = new Vector2(1f, 1f);
    }
    /*
    public void donateBtnClick()
    {
        Application.OpenURL("https://www.paypal.me/JParviainen");
    }
    */
	public void aboutBtnClick()
    {
        rect = aboutPanel.GetComponent<RectTransform>();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = new Vector2(1f, 1f);
    }
    public void backBtn2Click()
    {
        rect = aboutPanel.GetComponent<RectTransform>();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.zero;
        aboutScrollRect.normalizedPosition = new Vector2(0.5f, 1f);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && aboutPanel.GetComponent<RectTransform>().anchorMax == new Vector2(1f, 1f)) backBtn2Click();
        else if (Input.GetKeyDown(KeyCode.Escape) && (u.videoAdErrorPanelIsShowed))
        {
            u.SendMessageToAll("onRemoveAdsPanelHide");
            u.SendMessageToAll("onVideoNotLoadedYetPanelHide");
        }
        else if (Input.GetKeyDown(KeyCode.Escape)) onMenu2Hide();
    }

}
