using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class main_pic_behaviour : MonoBehaviour {
    private bool menu = false;
    private bool hideMenu = false;
	// Use this for initialization
	void Start () {
		
	}
	public void OnPauseGame()
    {
        menu = true;
        Color tempCol = this.GetComponent<Image>().color;
        tempCol = new Color(tempCol.r, tempCol.g, tempCol.b, 0f);
        this.GetComponent<Image>().color = tempCol;
    }
    public void OnMenuHide()
    {
        hideMenu = true;
        menu = false;

    }
	// Update is called once per frame
	void FixedUpdate () {
		if (menu)
        {
            Color tempCol = this.GetComponent<Image>().color;
            if (tempCol.a < 1f)
            {
                tempCol = new Color(tempCol.r, tempCol.g, tempCol.b, tempCol.a + 0.02f);
                this.GetComponent<Image>().color = tempCol;
            }
        }
        else if (hideMenu)
        {
            Color tempCol = this.GetComponent<Image>().color;
            if (tempCol.a > 0f)
            {
                tempCol = new Color(tempCol.r, tempCol.g, tempCol.b, tempCol.a - 0.02f);
                this.GetComponent<Image>().color = tempCol;
            }
        }
	}
}
