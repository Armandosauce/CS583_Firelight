using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPotion : MonoBehaviour {

    public int amt;

    private Player player;
    private string type;


	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        type = "light";
        //this.gameObject.SetActive(false);
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerHitBox")
        {
            player.usePotion(type, amt);
            Destroy(this.gameObject);
        }   
    }

}
