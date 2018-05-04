using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScript : MonoBehaviour
{
    public int mapNumber = 0;

    private HingeJoint2D MyHinge = new HingeJoint2D();

    private float MaxXPosition;
    Rigidbody2D rb2d;
    Rigidbody2D rb2dLine;

    void OnCollisionEnter2D(Collision2D coll)
    {
        rb2dLine = coll.gameObject.GetComponent<Rigidbody2D>();
        MyHinge.connectedBody = rb2dLine;
        foreach (ContactPoint2D colliderPoint in coll.contacts)
        {
            Vector2 hitPoint = colliderPoint.point;
            hitPoint.x -= transform.position.x;
            hitPoint.x /= transform.localScale.x;
            hitPoint.y /= transform.localScale.y;
            MyHinge.anchor = hitPoint;
        }
    }

    void Start ()
    {
        MaxXPosition = (Camera.main.orthographicSize * 2 * 16) / 9f;
        transform.position = new Vector3 (mapNumber * MaxXPosition, 0,0);

        gameObject.AddComponent<HingeJoint2D>();
        MyHinge = GetComponent<HingeJoint2D>();

        MyHinge.autoConfigureConnectedAnchor = false;
        MyHinge.connectedAnchor = new Vector2(0, 0.05f);
    }
	
	void Update ()
    {
        if (Input.GetKeyUp("space") || MobileTouchController.IsTouchUp())
        {
            MyHinge.connectedBody = null;
        }
    }
}
