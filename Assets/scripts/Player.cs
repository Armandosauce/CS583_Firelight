using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour {

    public float playerHP;
    public float maxLumoHP;
    public float lightDecRate = 1f;
    public float invTime;
    public Material material;
    public TextMesh healthUI;
    public TextMesh lumoUI;

    private float lumoHP;
    private bool _inv;
    private float timeLeft;
    private float _timestamp;
    private Enemy enemy;
    private Light lumo;
    private SpriteRenderer player;
    private Material def;
    private BoxCollider2D body;
    private BoxCollider2D weapon;

    // Use this for initialization
    void Start () {
        lumo = GameObject.FindGameObjectWithTag("Light").GetComponent<Light>();
        player = GetComponent<SpriteRenderer>();
        def = player.material;
        body = GameObject.FindGameObjectWithTag("PlayerHitBox").GetComponent<BoxCollider2D>();
        weapon = GameObject.FindGameObjectWithTag("Weapon").GetComponent<BoxCollider2D>();
        lumoHP = maxLumoHP;

	}
	
	// Update is called once per frame
	void FixedUpdate () {

        updateLumo();
        //flicker on damage
        if(_inv)
        {
            player.enabled = !player.enabled;
        }
        else if(!player.enabled)
        {
            player.enabled = true;
        }

        // become vulnerable again after getting hit
        if(_timestamp <= Time.time)
        {
            _inv = false;
        }

        // if hp goes to zero (or below), its game over
        if(playerHP <= 0)
        {
            GameOver();
        }
        healthUI.text = "HP: " + playerHP;
        
	}

    void updateLumo()
    {
        if(lumoHP > maxLumoHP)
        {
            lumoHP = maxLumoHP;
        }

        lumoUI.text = "LUMO: " + lumoHP.ToString("0");
        lumoHP = lumoHP - (Time.deltaTime * lightDecRate);
        lumo.spotAngle = lumoHP;

        if (lumoHP < 30)
        {
            player.material = material;
        }
        else if (player.material != material)
        {
            player.material = def;
        }

        if (lumo.spotAngle <= 1)
        {
            lumo.enabled = false;
        }
        else if (lumo.spotAngle > 1 && lumo.spotAngle <= 2)
        {
            lumo.enabled = true;
        }
    }

    public void damage(float amount)
    {
        playerHP -= amount;
        healthUI.text = "HP: " + playerHP.ToString("0");

    }

    void GameOver()
    {
        Time.timeScale = 0;
    }

    public void invincibility()
    {
        _inv = true;
        _timestamp = Time.time + invTime;

    }

    public bool isInv()
    {
        return _inv;
    }

    public void usePotion(string type, int amt)
    {
        if(type == "light")
        {
            lumoHP += amt;
        }

        if(type == "health")
        {
            playerHP += amt;
        }

        if(type == "speed")
        {

        }
    }
}

