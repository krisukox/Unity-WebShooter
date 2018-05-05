using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineScript : MonoBehaviour
{
    private SpriteRenderer mySpriteRenderer;
    private float lineSpeed = 1.6f;
    private float newPoint;
    private Rigidbody2D rb2d;
    public static bool isCollision = false;

    void OnCollisionEnter2D(Collision2D coll)
    {
        rb2d.freezeRotation = false;
        rb2d.gravityScale = 1f;
        isCollision = true;
    }

	void Start () 
    {
        isCollision = false;
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.bodyType = RigidbodyType2D.Dynamic;
        rb2d.gravityScale = 0.1f;
        mySpriteRenderer.enabled = false;
    }
	
	void Update ()
    {
        Touch touch = new Touch();
        touch.phase = TouchPhase.Canceled;
        if(Input.touchCount > 0)
            touch = Input.GetTouch(0);
        if (Input.GetKeyDown("space") || touch.phase == TouchPhase.Began)
        {
            transform.eulerAngles = new Vector3(0, 0, -45);
            transform.localScale = new Vector3(1, 1, 1); 
            transform.localPosition = new Vector3(0.04f, 0.04f, 1);
            mySpriteRenderer.enabled = true;
        }
        else if (Input.GetKeyUp("space") || touch.phase == TouchPhase.Ended)
        {
            mySpriteRenderer.enabled = false;
            transform.eulerAngles = new Vector3(0, 0, -45);
            transform.localScale = new Vector3(1, 1, 1);
            transform.localPosition = new Vector3(0.04f, 0.04f, 1);
            rb2d.gravityScale = 0.1f;
            isCollision = false;
        }
        if ((Input.GetKey("space") || Input.touchCount > 0) && !isCollision)
        {
            rb2d.freezeRotation = true;
            transform.localScale += new Vector3(0, lineSpeed, 0);
            newPoint = (lineSpeed/10) / (Mathf.Sqrt(2)*2);
            transform.localPosition = transform.localPosition + new Vector3(newPoint, newPoint, 0);
        }
    }
}
