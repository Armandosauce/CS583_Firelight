using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    
    public GameObject target;
    public float fov = 5f; // field of view
    public float speed = 3f;
    public float hp;
    public float damage;
    public float invTime;
    public float deathTime;
    
    private Animator anim;
    private SpriteRenderer sprite;
    private BoxCollider2D col;
    private TextMesh hpText;
    private MeshRenderer text;
    private float distance;
    private bool _inv = false;
    private float _timestamp;
    private GameObject[] itemdrop;
    private Vector3 itempos;
    private int rand;
    private AudioSource source;

	// Use this for initialization
	void Start () {
        target = GameObject.FindGameObjectWithTag("Player");

        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        col = GetComponentInChildren<BoxCollider2D>();
        hpText = GetComponentInChildren<TextMesh>();
        text = GetComponentInChildren<MeshRenderer>();
        itemdrop = GameObject.FindGameObjectsWithTag("Consumable");
        rand = Random.Range(0, 4);
        source = GetComponentInChildren<AudioSource>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        if (hp <= 0)
        {
            die();
        }
        else
        {
            // invTime seconds after taking damage, become vulnerable
            if (_timestamp <= Time.time)
            {
                _inv = false;
            }

            distance = Vector2.Distance(transform.position, target.transform.position);

            hpText.text = "HP: " + hp.ToString("0");

            if (distance <= fov)
            {
                if ((target.transform.position.x - transform.position.x) < 0)
                {
                    sprite.flipX = true;
                    col.transform.rotation = Quaternion.Euler(0, -180, 0);
                }
                else
                {
                    sprite.flipX = false;
                    col.transform.rotation = Quaternion.Euler(0, -0, 0);

                }

                if (!_inv)
                {
                    chase();
                }
                else
                {
                    anim.SetBool("chasing", false);
                }
            }
            else
            {
                anim.SetBool("chasing", false);
                text.enabled = false;
            }
        } // if not DEAD, then update normally
    }

    void chase()
    {
        anim.SetBool("Hit", false);
        anim.SetBool("chasing", true);
        text.enabled = true;
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("chase"))
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
            
        }
    }

    public void takeDamage(float amount)
    {
        if (!_inv)
        {
            hp -= amount;
            invincibility();
            anim.SetBool("Hit", true);
        }
    }

    public void invincibility()
    {
        _inv = true;
        _timestamp = Time.time + invTime;
    }

    public void knockback(float force)
    {
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime * force);
        
    }

    void die()
    {
        if (!anim.GetBool("isDead")) {
            anim.SetBool("isDead", true);
            anim.SetBool("Hit", false);
            anim.SetBool("chasing", false);
            col.enabled = false;
            text.enabled = false;

            if (rand < itemdrop.Length)
            {
                itempos = new Vector3(transform.position.x + Random.Range(0, 1f), transform.position.y + Random.Range(0, 1f) + 0.5f, transform.position.z);
                GameObject item = Instantiate(itemdrop[rand], itempos, Quaternion.identity);
                item.transform.position = Vector2.MoveTowards(item.transform.position, target.transform.position, Time.deltaTime * -1);
            }

            StartCoroutine(Remove(deathTime));
        }
    }

    IEnumerator Remove(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }

    public void PlaySound(AudioClip clip)
    {
        source.clip = clip;
        source.Play();
    }
    
    
}
