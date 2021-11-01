/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class background_clouds_behaviour : MonoBehaviour {
    private float screenHeightInUnits; 
    private float screenWidthInUnits;
    public float timeBetweenClouds;
    public float cloudSpeed;
    private float timeSpace;
    // Use this for initialization
    void Start()
    {
        timeSpace = timeBetweenClouds;
        screenHeightInUnits = Camera.main.orthographicSize * 2;
        screenWidthInUnits = screenHeightInUnits * Screen.width / Screen.height;
        if (gameObject.name == "cloud")
        {
            this.transform.position = new Vector3(0f, screenHeightInUnits / 2f * -1f - this.transform.localScale.y);

        }
        else
        {
            Vector3 pos = Vector3.zero;
            if (Mathf.RoundToInt(Random.Range(0f, 1f)) == 0)
            {
                pos.x = screenWidthInUnits / 2f + this.transform.localScale.x / 2f;
                this.GetComponent<Rigidbody2D>().velocity = new Vector3(-cloudSpeed, 0f);
            }
            else
            {
                pos.x = screenWidthInUnits / 2f * -1f - this.transform.localScale.x / 2f;
                this.GetComponent<Rigidbody2D>().velocity = new Vector3(cloudSpeed, 0f);
            }
            pos.y = Random.Range(screenHeightInUnits / 2f * -1f + this.transform.localScale.y / 2f, screenHeightInUnits / 2f - this.transform.localScale.y / 2f);
            this.transform.position = pos;

        }
    }
    
	
	// Update is called once per frame
	void Update () {
		if (gameObject.name == "cloud")
        {
            if (timeSpace <= 0f)
            {
                Instantiate(gameObject);
                timeSpace = timeBetweenClouds;
            }
            timeSpace -= Time.deltaTime;
        }
	}
}
*/
