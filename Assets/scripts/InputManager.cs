using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    public Vector2 movement;
    public bool dashKey = false;
    public bool runKey = false;
    public bool attackKey = false;
    
	// Update is called once per frame
	void Update () {
        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        dashKey = Input.GetKeyDown("space");
        
        runKey = Input.GetKey(KeyCode.LeftShift);
        attackKey = Input.GetKeyDown(KeyCode.Backslash);

    }
}
