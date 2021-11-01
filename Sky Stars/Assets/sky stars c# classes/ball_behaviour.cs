using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball_behaviour : MonoBehaviour {
    //private Vector3[] ball_poss = new Vector3[2];
    //private Vector3 ball_vel;
    public GameObject ball;
    public GameObject miniball;
    public GameObject column;
    private bool playerReady = false;
    private bool firstUpdate = true;
    private bool paused = false;
    private Vector3 input_device_position_in_world_units;
    private float screen_width_in_world_units, screenHeightInUnits, screenWidthInUnits;
    // Use this for initialization
    void Awake()
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
        
        /*
        float ball_scale = (((screen_width_in_units*0.2f)/(0.53f*2)) * 2.5f * 1.23f); //0.53 is the ball's size in world units and 0.2 is because I wanted the ball to have width of 20% of the game area
        ball.transform.localScale = new Vector3(ball_scale, ball_scale, 1.0f);
        float mini_ball_scale = ball_scale * 0.4f;
        */
        miniball.transform.position = new Vector3(0.0f, 0.0f);
        //miniball.transform.localScale = new Vector3(mini_ball_scale, mini_ball_scale);
        //Debug.Log(miniball.name);
        ball.transform.position = new Vector3(0.0f, (screenHeightInUnits / 2.0f * -1.0f + (0.32f * ball.transform.localScale.y)));
        
        
	}
    private void FixedUpdate()
    {
            if (!paused)
            {

                
                //Debug.Log(ball_vel.magnitude);
                if (Input.touchSupported)
                {
                    if (Input.touches.Length != 0)
                    {
                        input_device_position_in_world_units = Camera.main.ScreenToWorldPoint(new Vector3(Input.touches[0].position.x, Input.touches[0].position.y));
                    }
                    else
                    {
                        input_device_position_in_world_units = ball.transform.position;
                    }
                }
                else
                {
                    input_device_position_in_world_units = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f));
                }
                
                Vector3 ball_velocity = new Vector3(input_device_position_in_world_units.x, input_device_position_in_world_units.y) - new Vector3(ball.transform.position.x, ball.transform.position.y);
                ball.GetComponent<Rigidbody2D>().velocity = ball_velocity * 7.5f;

                //ball.GetComponent<Rigidbody2D>().MovePosition(input_device_position_in_world_units);
            }   
    }
    // Update is called once per frame
    void Update () {
        //Debug.Log(ball.GetComponent<Rigidbody2D>().velocity.magnitude);
        /*
        miniball.GetComponent<Rigidbody2D>().velocity *= 0.9f;
        if (miniball.GetComponent<Rigidbody2D>().velocity.magnitude > 4.5f)
        {
            miniball.GetComponent<Rigidbody2D>().velocity = miniball.GetComponent<Rigidbody2D>().velocity.normalized * 4.5f;
        }
        */
        /*
        ball_poss[0] = ball_poss[1];
        ball_poss[1] = ball.transform.position;
        ball_vel = ball_poss[1] - ball_poss[0];
		*/
        //Debug.Log(miniball.GetComponent<Rigidbody2D>().velocity.magnitude);

    }
    public void onGamePause()
    {
        ball.GetComponent<Rigidbody2D>().simulated = false;
        miniball.GetComponent<Rigidbody2D>().simulated = false;
        ball.GetComponent<CircleCollider2D>().isTrigger = true;
        miniball.GetComponent<CircleCollider2D>().isTrigger = true;
        ball.GetComponent<CircleCollider2D>().enabled = false;
        miniball.GetComponent<CircleCollider2D>().enabled = false;
        paused = true;

    }
	/*
    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject == miniball)
        {
            
            Rigidbody2D rb = miniball.GetComponent<Rigidbody2D>();
            Rigidbody2D rb2 = ball.GetComponent<Rigidbody2D>();
            //Debug.Log(ball_vels[0]);
            if (rb2.velocity.magnitude > 0.5f)
            {
                //Debug.Log("Slowing down");
                rb.velocity *= 0.6f;
            }
            
            Debug.Log(ball_vel.x);
            Debug.Log(ball_vel.y);
            Debug.Log(ball_vel.z);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == miniball)
        {
            //Debug.Log(ball.GetComponent<Rigidbody2D>().velocity.magnitude);
            
        }
    }
    */
    public void onGameStart()
    {
        paused = false;
        miniball.GetComponent<Rigidbody2D>().simulated = true;
        ball.GetComponent<Rigidbody2D>().simulated = true;

    }
    public void onGameReset()
    {
        paused = true;
        miniball.transform.position = new Vector3(0.0f, 0.0f);
        ball.transform.position = new Vector3(0.0f, (screenHeightInUnits / 2.0f * -1.0f + (0.32f * ball.transform.localScale.y)));
        miniball.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        ball.GetComponent<CircleCollider2D>().isTrigger = false;
        miniball.GetComponent<CircleCollider2D>().isTrigger = false;
        ball.GetComponent<CircleCollider2D>().enabled = true;
        miniball.GetComponent<CircleCollider2D>().enabled = true;
    }
}
