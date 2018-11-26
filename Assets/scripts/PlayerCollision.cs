using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour {

    private Enemy enemy;
    private Player player;

	// Use this for initialization
	void Start () {
        player = this.GetComponentInParent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "EnemyCol" && !player.isInv())
        {
            enemy = collision.gameObject.GetComponent<Enemy>();
            player.damage(enemy.damage);
            player.invincibility();
        }
    }
    
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "EnemyCol" && !player.isInv())
        {
            enemy = collision.gameObject.GetComponent<Enemy>();
            player.damage(enemy.damage);
            player.invincibility();
        }
    }

}
