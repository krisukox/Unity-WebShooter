using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rb2d;

    void OnCollisionEnter2D(Collision2D coll)
    {
        if(Time.timeSinceLevelLoad > 1)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void Start ()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.bodyType = RigidbodyType2D.Dynamic;
        rb2d.gravityScale = 0.1f;
        rb2d.velocity = new Vector2(0, 0);
    }
	
	void Update ()
    {
		if(LineScript.isCollision)
            rb2d.gravityScale = 1;
        else
            rb2d.gravityScale = 0.1f;
    }
}
