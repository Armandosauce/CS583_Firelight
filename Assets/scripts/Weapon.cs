using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public float weaponDamage;
    
    //must be negative
    public float knockback;

    private Enemy enemy;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "EnemyCol")
        {
            enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.takeDamage(weaponDamage);
            

        }
    }

    /*
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "EnemyCol")
        {
            enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.takeDamage(weaponDamage);

        }
    }
    */
    
    
}
