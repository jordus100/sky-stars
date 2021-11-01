using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miniball_behaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        //Debug.Log("COLLISION WITH BOTTOM WALL");
        if (collision.gameObject.name == "black_column (3)")
        {
            Rigidbody2D rb = this.GetComponent<Rigidbody2D>();
            if (rb.velocity.y < 2f)
            {
                float velY = rb.velocity.y;
                velY/=velY;
                velY*=2f;
                rb.velocity = new Vector2(rb.velocity.x, velY);
            }
        }

    }
    
    // Update is called once per frame
    void Update () {
		
	}
}
