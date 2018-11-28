using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private InputManager input;
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer playerSprite;
    BoxCollider2D col;
    BoxCollider2D weapon;
    public Text text;

    public float dashSpeed;
    public float baseMovementSpeed;
    public float speedBoost;
    public float boostDelay;
    public float AbilityCooldown;

    private float baseDash;
    private float timeStamp;
    private float _dashOn;
    //private Sorting sort;

    // Use this for initialization
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerSprite = GetComponent<SpriteRenderer>();
        col = GameObject.FindGameObjectWithTag("PlayerHitBox").GetComponent<BoxCollider2D>();
        baseDash = 1f;
        input = gameObject.AddComponent<InputManager>() as InputManager;
        weapon = GetComponentInChildren<BoxCollider2D>();
        //sort = gameObject.AddComponent<Sorting>() as Sorting;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!Player.isGameOver())
        {
            checkLayer();

            if (baseDash != 1f)
            {
                baseDash = 1f;
            }

            input.movement = input.movement.normalized * Time.deltaTime * baseMovementSpeed;

            if (input.movement != Vector2.zero)
            {

                anim.SetBool("isWalking", true);
                anim.SetFloat("Input_X", input.movement.x);
                anim.SetFloat("Input_Y", input.movement.y);
                if (anim.GetFloat("Input_X") <= 0)
                {
                    playerSprite.flipX = true;
                    col.transform.rotation = Quaternion.Euler(0, -180, 0);
                    weapon.transform.rotation = Quaternion.Euler(0, -180, 0);
                }
                else
                {
                    playerSprite.flipX = false;
                    col.transform.rotation = Quaternion.Euler(0, 0, 0);
                    weapon.transform.rotation = Quaternion.Euler(0, 0, 0);
                }
            }
            else
            {

                anim.SetBool("isWalking", false);
            }

            CheckAttack();
            text.text = (timeStamp - Time.time).ToString("0.00");


            CheckDash();
            if (anim.GetCurrentAnimatorStateInfo(0).IsTag("Dash"))
            {
                baseDash = dashSpeed;
            }



            if (!anim.GetCurrentAnimatorStateInfo(0).IsTag("Action"))
            {
                if (input.runKey)
                {
                    rb.MovePosition(rb.position + input.movement * baseDash * speedBoost);
                }
                else
                {
                    rb.MovePosition(rb.position + input.movement * baseDash);
                }
            }



            if (timeStamp - Time.time <= 0)
            {
                text.enabled = false;
            }
        }
    }

    private void CheckAttack()
    {
        if (input.attackKey)
        {
            anim.SetBool("isAttacking", true);
            
        }
        else
        {
            anim.SetBool("isAttacking", false);
            
        }
        
    }

    private void CheckDash()
    {
        if (timeStamp <= Time.time)
        {
            if (input.dashKey && !anim.GetCurrentAnimatorStateInfo(0).IsTag("Action"))
            {
                timeStamp = Time.time + AbilityCooldown;
                anim.SetBool("isDashing", true);
                text.enabled = true;
                text.text = (timeStamp - Time.time).ToString("0.00");
            } else
            {
                text.enabled = false;
            }
        }
        else
        {
            text.text = (timeStamp - Time.time).ToString("0.00");
            anim.SetBool("isDashing", false);
        }
        
    }

    public void checkLayer()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Object");
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        List<GameObject> entities = new List<GameObject>();

        for (int i = 0; i < objects.Length; i++)
        {
            entities.Add(objects[i]);
        }

        for (int i = 0; i < enemies.Length; i++)
        {
            entities.Add(enemies[i]);
        }

        for (int i = 0; i < entities.Count; i++)
        {

            if (this.transform.position.y - entities[i].GetComponent<Transform>().position.y < 0.05)
            {
                if (entities[i].GetComponent<TilemapRenderer>())
                {
                    entities[i].GetComponent<TilemapRenderer>().sortingOrder = 3;
                }
                else
                {
                    entities[i].GetComponent<SpriteRenderer>().sortingOrder = 3;
                }
            }
            else
            {
                if (entities[i].GetComponent<TilemapRenderer>())
                {
                    entities[i].GetComponent<TilemapRenderer>().sortingOrder = 5;
                }
                else
                {
                    entities[i].GetComponent<SpriteRenderer>().sortingOrder = 5;
                }
            }


        }
    }

}







