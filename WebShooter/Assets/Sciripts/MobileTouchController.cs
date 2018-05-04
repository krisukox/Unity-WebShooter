using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileTouchController : MonoBehaviour
{
    private static bool touchDown = false;
    private static bool touchUp = false;
    private static bool lastTouch = false;
	
	void Update ()
    {
        if (!lastTouch && Input.touchCount > 0)
        {
            lastTouch = true;
            touchDown = true;
        }
        else if (lastTouch && Input.touchCount == 0)
        {
            lastTouch = false;
            touchUp = true;
        }
        else if (Input.touchCount > 0)
        {
            touchDown = false;
            touchUp = false;
        }
        else if (Input.touchCount == 0)
        {
            touchDown = false;
            touchUp = false;
        }
    }

    public static bool IsTouchDown()
    {
        return touchDown;
    }

    public static bool IsTouchUp()
    {
        return touchUp;
    }
}
