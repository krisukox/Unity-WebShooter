using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    private float cameraHeight;
    private Vector3 defaultRotation;

	void Start ()
    {
        defaultRotation = transform.eulerAngles;
        cameraHeight = transform.position.y;
        transform.position = new Vector3(transform.position.x, cameraHeight, -10);
    }
	
	void Update ()
    {
        transform.eulerAngles = defaultRotation;
        transform.position = new Vector3(transform.position.x, cameraHeight, -10);
    }
}
