using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    public Transform target;
    public float cam_speed = 0.05f;
    public float cam_zoom = 4f;
    Camera cam;
    
	// Use this for initialization
	void Start () {

        cam = GetComponent<Camera>();
  	}
	
	// Update is called once per frame
	void FixedUpdate () {

        cam.orthographicSize = (Screen.height / 100f) / cam_zoom;       

        if (target)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, cam_speed);
            transform.position += new Vector3(0, 0, -10);
        }



	}
}
