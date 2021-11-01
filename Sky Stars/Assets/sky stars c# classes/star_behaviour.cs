using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class star_behaviour : MonoBehaviour {
    public GameObject ball;
    public GameObject down_band;
    public bool shouldMove = false;
    public Sprite yellow_star_sprite;
    public GameObject[] crosses = new GameObject[3];
    private int crosses_order_number = 0;
    public Sprite redcross;
    public GameObject score_counter;
    private bool endgame = false;
    private float screen_width_in_world_units, screenHeightInUnits, screenWidthInUnits;
    //private AudioSource pointSound;
    //public GameObject soundManager;
    // Use this for initialization
    private void Awake()
    {
        //pointSound = soundManager.GetComponent<AudioSource>();
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
        

        /*
        float ball_scale = (((screen_width_in_units * 0.2f) / (0.53f * 2)) * 2.5f);
        float miniBallScale = ball_scale * 0.4f;
        this.transform.localScale = new Vector3(miniBallScale * 0.5f, miniBallScale * 0.5f, 1.0f);
        */
        this.transform.position = new Vector3(0.0f, this.transform.localScale.y * 2.0f*1.0f+(screenHeightInUnits/2.0f));
        
	}
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "mini_ball")
        {
            if(gameObject.GetComponent<SpriteRenderer>().sprite != yellow_star_sprite)
            {
                //pointSound.Play();
                score_counter.SendMessage("OnScoreIncrease", SendMessageOptions.DontRequireReceiver);
            }
            gameObject.GetComponent<SpriteRenderer>().sprite = yellow_star_sprite;
            //Debug.Log("collision");
            


        }
    }
    // Update is called once per frame
    void Update () {
        
        
            if (this.transform.position.y < (down_band.transform.position.y + 0.5f - (this.transform.localScale.y  / 2.0f)))
            {
            if (gameObject.GetComponent<SpriteRenderer>().sprite != yellow_star_sprite && endgame == false)
            {
                //Debug.Log("przepuszczona_gwiazdeczka");
                score_counter.SendMessage("OnStarMiss", SendMessageOptions.DontRequireReceiver);
                Destroy(gameObject);

               
                

            }
            else
            {
                Destroy(gameObject);
            }
            }

        
	}
    public void onGamePause()
    {
        this.GetComponent<Rigidbody2D>().simulated = false;

    }
    public void onGameReset()
    {
        if (gameObject.name.Contains("Clone"))
        {
            Destroy(gameObject);
        }
        this.transform.position = new Vector3(0.0f, this.transform.localScale.y * 2.0f * 1.0f + (screenHeightInUnits / 2.0f));
    }
    
    
    public void onGameStart()
    {
        this.GetComponent<Rigidbody2D>().simulated = true;
    }
    
    
}

