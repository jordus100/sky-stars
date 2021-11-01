using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class game_manager : MonoBehaviour
{
    public GameObject usefulGO;
    public GameObject column;
    public GameObject column_2;
    public GameObject panel;
    public GameObject panel2;
    public GameObject column_3;
    public GameObject column_4;
    public GameObject ball;
    private GameObject new_star;
    private float screen_width_in_world_units, screenHeightInUnits, screenWidthInUnits;
    public GameObject star;
    private ball_behaviour ball_script;
    private float time_space;
    private bool next_star = true;
    private bool star_setup = false;
    private bool firstUpdate = true;
    private bool paused = true;
    private Vector3 mouse_world_pos;
    public GameObject miniball;
    public GameObject cross;
    public GameObject cross2;
    public GameObject cross3;
    public Text score;
    public GameObject mainPic;
    public GameObject menuPanel;
    private bool gameOn = false;
    private usefulClassForAll u;
    private void Awake()
    {
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
    /*
    void Awake()
    {
        float distance_from_middle = Camera.main.orthographicSize * 2 / 2.3f;
        //Debug.Log(distance_from_middle * 2f);
        if ((Screen.width / Screen.height) > (2.0f / 2.3f))
        {
            column.transform.position = new Vector3(((screenWidthInUnits / 2f - distance_from_middle) / 2f) + distance_from_middle, 0.0f, 0.0f);
            column_2.transform.position = new Vector3(((screenWidthInUnits / 2f * -1f + distance_from_middle) / 2f) - distance_from_middle, 0.0f, 0.0f);
            column.transform.localScale = new Vector3(screenWidthInUnits / 2f - distance_from_middle, screenHeightInUnits, 1.0f);
            column_2.transform.localScale = new Vector3(screenWidthInUnits / 2f * -1f + distance_from_middle, screenHeightInUnits, 1.0f);
            column_3.transform.position = new Vector3(0.0f, screenHeightInUnits / 2.0f + 0.5f);
            column_3.transform.localScale = new Vector3(screenWidthInUnits, 1.0f, 1.0f);
            column_4.transform.position = new Vector3(0.0f, (screenHeightInUnits / 2.0f * -1.0f - 0.5f));
            column_4.transform.localScale = new Vector3(screenWidthInUnits, 1.0f, 1.0f);
            screen_width_in_world_units = distance_from_middle * 2.0f;
            //ball_script.enabled = true;
            float x = distance_from_middle / (screenWidthInUnits / 2.0f);
            Debug.Log(screenHeightInUnits);
            Debug.Log(x);
            Debug.Log(screenWidthInUnits);
            //Debug.Log(Camera.main.pixelWidth);
            float right_band_fract = (distance_from_middle + screenWidthInUnits / 2f) / (screenWidthInUnits);
            float left_band_fract =(screenWidthInUnits / 2f - distance_from_middle) / screenWidthInUnits;
            score.GetComponent<RectTransform>().anchorMin = new Vector2(left_band_fract , 0.9f);
            score.GetComponent<RectTransform>().anchorMax = new Vector2(right_band_fract , 1f);
            
        }
        else
        {
            column.transform.localScale = new Vector3(1f, screenHeightInUnits);
            column.transform.position = new Vector3(screenWidthInUnits / 2f + column.transform.localScale.x / 2f, 0f);
            column_2.transform.localScale = new Vector3(1f, screenHeightInUnits);
            column_2.transform.position = new Vector3(screenWidthInUnits / 2f * -1f - column_2.transform.localScale.x / 2f, 0f);
            column_3.transform.localScale = new Vector3(screenWidthInUnits, 1f);
            column_3.transform.position = new Vector3(0f, screenHeightInUnits / 2f + column_3.transform.localScale.y / 2f);
            column_4.transform.localScale = new Vector3(screenWidthInUnits, 1f);
            column_4.transform.position = new Vector3(0f, screenHeightInUnits / 2f *-1f - column_4.transform.localScale.y / 2f);
            screen_width_in_world_units = screenWidthInUnits;
            ball_script.enabled = true;
            Debug.Log(screenWidthInUnits);
            score.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 0.9f);
            score.GetComponent<RectTransform>().anchorMax = new Vector2(1.0f, 1f);




        }
    
        

    }
    */
    void Update()
    {
        if (!paused && gameOn)
        {
            
            if (star_setup)
            {
                //Debug.Log(new_star.transform.localScale.x);
                new_star.transform.position = new Vector3(Random.Range(((screen_width_in_world_units / 2.0f * -1.0f) + (new_star.transform.localScale.x / 2.0f)), ((screen_width_in_world_units / 2.0f * 1.0f) - (new_star.transform.localScale.x / 2.0f))), (Camera.main.orthographicSize - (new_star.transform.localScale.y / 2.0f)));
                star_setup = false;
            }
            if (next_star)
            {
                time_space = Random.Range(0.4f, 4f);
                next_star = false;
            }
            if (time_space <= 0.0f)
            { }
            else
            {
                time_space -= Time.deltaTime;
                if (time_space <= 0.0f)
                {
                    NextStar();
                }
            }

        }
        if (paused && gameOn)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Input.touchSupported)
                {
                   mouse_world_pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.touches[0].position.x, Input.touches[0].position.y));

                }
                else
                {
                     mouse_world_pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));
                }
                Vector3 mouse_distance_from_ball = new Vector3(mouse_world_pos.x - ball.transform.position.x, mouse_world_pos.y- ball.transform.position.y);
                if (mouse_distance_from_ball.magnitude <= ball.transform.localScale.x * 0.32f && menuPanel.GetComponent<RectTransform>().anchorMax == new Vector2(0f, 0f))// 0.32f is the ball's  radius in scale 1
                {
                    paused = false;
                    u.SendMessageToAll("onGameStart");
                }
            }
        }

    }
    private void NextStar()
    {
        next_star = true;
        new_star = Instantiate(star);
        new_star.transform.localScale = star.transform.localScale;
        new_star.GetComponent<Rigidbody2D>().velocity = new Vector3(0f, Random.Range(-0.45f, -0.7f));
        star_setup = true;
        

    }
    public void onGameReset()
    {
        float distance_from_middle = Camera.main.orthographicSize * 2 / 2.3f;
        //Debug.Log(distance_from_middle * 2f);
        if (((float)Screen.width / Screen.height) > (2.0f / 2.3f))
        {
            Debug.Log("CASE 1");
            column.transform.position = new Vector3(((screenWidthInUnits / 2f - distance_from_middle) / 2f) + distance_from_middle, 0.0f, 0.0f);
            column_2.transform.position = new Vector3(((screenWidthInUnits / 2f * -1f + distance_from_middle) / 2f) - distance_from_middle, 0.0f, 0.0f);
            column.transform.localScale = new Vector3(screenWidthInUnits / 2f - distance_from_middle, screenHeightInUnits, 1.0f);
            column_2.transform.localScale = new Vector3(screenWidthInUnits / 2f * -1f + distance_from_middle, screenHeightInUnits, 1.0f);
            column_3.transform.position = new Vector3(0.0f, screenHeightInUnits / 2.0f + 0.5f);
            column_3.transform.localScale = new Vector3(screenWidthInUnits, 1.0f, 1.0f);
            column_4.transform.position = new Vector3(0.0f, (screenHeightInUnits / 2.0f * -1.0f - 0.5f));
            column_4.transform.localScale = new Vector3(screenWidthInUnits, 1.0f, 1.0f);
            //screen_width_in_world_units = distance_from_middle * 2.0f;
            //ball_script.enabled = true;
            float x = distance_from_middle / (screenWidthInUnits / 2.0f);
            Debug.Log(screenHeightInUnits);
            Debug.Log(x);
            Debug.Log(screenWidthInUnits);
            //Debug.Log(Camera.main.pixelWidth);
            float right_band_fract = (distance_from_middle + screenWidthInUnits / 2f) / (screenWidthInUnits);
            float left_band_fract = (screenWidthInUnits / 2f - distance_from_middle) / screenWidthInUnits;
            score.GetComponent<RectTransform>().anchorMin = new Vector2(left_band_fract, 0.9f);
            score.GetComponent<RectTransform>().anchorMax = new Vector2(right_band_fract, 1f);

        }
        else
        {
            Debug.Log("CASE 2");
            column.transform.localScale = new Vector3(1f, screenHeightInUnits);
            column.transform.position = new Vector3(screenWidthInUnits / 2f + column.transform.localScale.x / 2f, 0f);
            column_2.transform.localScale = new Vector3(1f, screenHeightInUnits);
            column_2.transform.position = new Vector3(screenWidthInUnits / 2f * -1f - column_2.transform.localScale.x / 2f, 0f);
            column_3.transform.localScale = new Vector3(screenWidthInUnits, 1f);
            column_3.transform.position = new Vector3(0f, screenHeightInUnits / 2f + column_3.transform.localScale.y / 2f);
            column_4.transform.localScale = new Vector3(screenWidthInUnits, 1f);
            column_4.transform.position = new Vector3(0f, screenHeightInUnits / 2f * -1f - column_4.transform.localScale.y / 2f);
            //screen_width_in_world_units = screenWidthInUnits;
            //ball_script.enabled = true;
            Debug.Log(screenWidthInUnits);
            score.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 0.9f);
            score.GetComponent<RectTransform>().anchorMax = new Vector2(1f, 1f);
        }
        paused = true;
        gameOn = true;
        time_space = 0f;
        star_setup = false;
        next_star = true;
    }
    public void onGamePause()
    {
        paused = true;
        gameOn = false;
    }
    public void onGameStart()
    {
        paused = false;
        gameOn = true;
    }
}
