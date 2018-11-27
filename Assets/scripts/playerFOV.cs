using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerFOV : MonoBehaviour {

    public Transform target;
    public float light_speed = 1f;
    public float range = 10f;
    
	// Update is called once per frame
	void FixedUpdate () {
        
        
        if (target)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, light_speed);
            transform.position += new Vector3(0, 0, -10f);
        }
	}
}
