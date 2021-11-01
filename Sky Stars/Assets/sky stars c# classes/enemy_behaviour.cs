using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class enemy_behaviour : MonoBehaviour {
    public GameObject usefulGO;
    public GameObject star;
    private bool newEnemy = true;
    private float time_space;
    private GameObject latest_enemy;
    public GameObject enemy;
    private bool paused = false;
    private float gameWidth;
    public GameObject warning;
    private GameObject warningcpy;
    private int side;
    private float randomY;
    private bool showingWarn = false;
    public Sprite enemySprite;
    public Sprite miniballSprite;
    public GameObject miniball;
    public Sprite emptySprite;
    public Text score;
    private usefulClassForAll u;
    private float screen_width_in_world_units, screenHeightInUnits, screenWidthInUnits;
    public GameObject SoundManager;
    private AudioSource loseSound;

    private void Awake()
    {
        loseSound = SoundManager.GetComponent<AudioSource>();
        u = usefulGO.GetComponent<usefulClassForAll>();
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
    // Use this for initialization
    void Start () {
        
        /*
        float ball_scale = (((screenWidthInUnits * 0.2f) / (0.53f * 2)) * 2.5f);
        float miniBallScale = ball_scale * 0.4f;
        float starScale = miniBallScale * 0.5f;
        */
        if (gameObject.name == "enemy")
        {
            //this.transform.localScale = new Vector3(starScale * 1.3f, starScale*1.3f);
            this.transform.position = new Vector3(0.0f, ((screenHeightInUnits / 2.0f * -1.0f - (this.transform.localScale.y / 2.0f) - 2.0f)));
        }
       // warning.transform.localScale = new Vector3(star.transform.localScale.x * 1.5f, star.transform.localScale.y * 1.5f, 1.0f);
        
        warning.transform.position = new Vector3(0.0f, ((screenHeightInUnits / 2.0f * -1.0f - (warning.transform.localScale.y / 2.0f) - 2.0f)));
        
        gameWidth = screen_width_in_world_units;
        
    }


	
	// Update is called once per frame
	void Update () {
        if (!paused)
        {
            if (gameObject.name == "enemy")
            {
                if (newEnemy)
                {
                    time_space = UnityEngine.Random.Range(0.4f , 3.5f );
                    newEnemy = false;
                }
                if (time_space > 0.0f)
                {
                    time_space -= Time.deltaTime;
                }
                else
                {
                    nextEnemy();
                }
            }
            float screenWidthInUnits =screen_width_in_world_units;
            if ((this.transform.position.x < (screenWidthInUnits * -1.0f - this.transform.localScale.x)) || (this.transform.position.x > (screenWidthInUnits + transform.localScale.x))){
                Destroy(gameObject);
            }
        }
	}
    private void nextEnemy()
    {
        if (!showingWarn)
        {
            if ((int)Mathf.Round(UnityEngine.Random.value) == 0)// enemy pops out from left side of the screen
            {
                side = 0;

                warningcpy = Instantiate(warning);
                warningcpy.transform.localScale = this.transform.localScale*0.6f;
                randomY = UnityEngine.Random.Range((screenHeightInUnits / 2.0f * -1.0f + (warningcpy.transform.localScale.y / 2.0f)), (screenHeightInUnits / 2.0f * 1.0f - (warningcpy.transform.localScale.y / 2.0f)));
                warningcpy.transform.position = new Vector3((screenWidthInUnits / 2.0f * -1.0f) + warningcpy.transform.localScale.x / 2f, randomY);



                StartCoroutine(ShowWarning());




            }
            else //enemy pops out from the right side of the screen
            {
                side = 1;

                warningcpy = Instantiate(warning);
                warningcpy.transform.localScale = this.transform.localScale*0.6f;
                randomY = UnityEngine.Random.Range((screenHeightInUnits / 2.0f * -1.0f + (warningcpy.transform.localScale.y / 2.0f)), (screenHeightInUnits / 2.0f * 1.0f - (warningcpy.transform.localScale.y / 2.0f)));
                warningcpy.transform.position = new Vector3((screenWidthInUnits / 2.0f * 1.0f) - warningcpy.transform.localScale.x / 2f, randomY);

                StartCoroutine(ShowWarning());



            }

        }

        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "mini_ball")
        {
            u.LoadData();
            int Highscore = u.highscore;
            u.Save(Highscore, Int32.Parse(score.text));
            u.SendMessageToAll("onGamePause");
            //loseSound.Play();
            int numGameRun = PlayerPrefs.GetInt("numberGameRun");
            PlayerPrefs.SetInt("numberGameRun", numGameRun + 1);
            usefulClassForAll.afterGame = true;
            StartCoroutine(gameOverAnim());
        }
    }
    public void onGamePause()
    {
       
        this.GetComponent<Rigidbody2D>().simulated = false;
        paused = true;
    }
    public void onGameStart()
    {
        this.GetComponent<Rigidbody2D>().simulated = true; ;
        paused = false;
        Debug.Log("ENEMIES ACTIVATED");
    }
    public void onGameReset()
    {
        if (gameObject.name.Contains("Clone"))
        {
            Destroy(gameObject);
        }
        newEnemy = true;
        this.transform.position = new Vector3(0.0f, ((screenHeightInUnits / 2.0f * -1.0f - (this.transform.localScale.y / 2.0f) - 2.0f)));
        warning.transform.position = new Vector3(0.0f, ((screenHeightInUnits / 2.0f * -1.0f - (warning.transform.localScale.y / 2.0f) - 2.0f)));
    }

    private IEnumerator ShowWarning()
    {
        showingWarn = true;
        yield return new WaitForSeconds(1f);
        Destroy(warningcpy);
        latest_enemy = Instantiate(enemy);
        if (side == 0)
        {
            
            latest_enemy.transform.position = new Vector3((screenWidthInUnits / 2.0f * -1.0f), randomY);
            float randomSpeedMultiplier = UnityEngine.Random.Range(0.9f, 1.9f);
            latest_enemy.GetComponent<Rigidbody2D>().velocity = new Vector3(0.5f / 5.217391f * gameWidth * randomSpeedMultiplier, 0.0f);
            newEnemy = true;
        }
        else
        {
            
            latest_enemy.transform.position = new Vector3((screenWidthInUnits / 2.0f * 1.0f), randomY);
            float randomSpeedMultiplier = UnityEngine.Random.Range(0.9f, 1.9f);
            latest_enemy.GetComponent<Rigidbody2D>().velocity = new Vector3(0.5f / 5.217391f * gameWidth * -1f * randomSpeedMultiplier, 0f);
            newEnemy = true;
        }
        showingWarn = false;
        //Debug.Log(("End of warning"));
    }       
    private IEnumerator gameOverAnim()
    {
        yield return new WaitForSeconds(0.3f);
        gameObject.GetComponent<SpriteRenderer>().sprite = emptySprite;
        miniball.GetComponent<SpriteRenderer>().sprite = emptySprite;
        yield return new WaitForSeconds(0.3f);
        gameObject.GetComponent<SpriteRenderer>().sprite = enemySprite;
        miniball.GetComponent<SpriteRenderer>().sprite = miniballSprite;
        yield return new WaitForSeconds(0.3f);
        gameObject.GetComponent<SpriteRenderer>().sprite = emptySprite;
        miniball.GetComponent<SpriteRenderer>().sprite = emptySprite;
        yield return new WaitForSeconds(0.3f);
        gameObject.GetComponent<SpriteRenderer>().sprite = enemySprite;
        miniball.GetComponent<SpriteRenderer>().sprite = miniballSprite;
        yield return new WaitForSeconds(0.3f);
        gameObject.GetComponent<SpriteRenderer>().sprite = emptySprite;
        miniball.GetComponent<SpriteRenderer>().sprite = emptySprite;
        yield return new WaitForSeconds(0.3f);
        gameObject.GetComponent<SpriteRenderer>().sprite = enemySprite;
        miniball.GetComponent<SpriteRenderer>().sprite = miniballSprite;
        yield return new WaitForSeconds(0.3f);
        u.SendMessageToAll("onMenuShow");
    }
    
}