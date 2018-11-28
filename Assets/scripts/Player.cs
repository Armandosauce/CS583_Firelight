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

    private SceneChanger scenechanger;
    private AudioSource source;
    private float lumoHP;
    private bool _inv;
    private float timeLeft;
    private float _timestamp;
    private Enemy enemy;
    private Light lumo;
    private SpriteRenderer player;
    private Material def;
    private Animator anim;
    private static bool _gameover;

    // Use this for initialization
    void Start () {
        lumo = GameObject.FindGameObjectWithTag("Light").GetComponent<Light>();
        player = GetComponent<SpriteRenderer>();
        def = player.material;
        lumoHP = maxLumoHP;
        source = GetComponentInChildren<AudioSource>();
        scenechanger = gameObject.AddComponent<SceneChanger>() as SceneChanger;
        anim = GetComponent<Animator>();
        Time.timeScale = 1f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        healthUI.text = "HP: " + playerHP;
        updateLumo();

        //flicker on damage
        if (_inv)
        {
            player.enabled = !player.enabled;
        }
        else if (!player.enabled)
        {
            player.enabled = true;
        }

        // become vulnerable again after getting hit
        if (_timestamp <= Time.time)
        {
            _inv = false;
        }

        if (playerHP <= 0 || lumoHP <= 0)
        {
            GameOver();
        }        
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
        if(playerHP <= 0)
        {
            playerHP = 0;
        }
        healthUI.text = "HP: " + playerHP.ToString("0");

    }

    void GameOver()
    {
        if (!_gameover)
        {
            _gameover = true;
            Time.timeScale = 0.70f;
            anim.Play("player_death");
        }
    }

    public static bool isGameOver()
    {
        return _gameover;
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
        
    }

    public void playSound(AudioClip clip)
    {
        source.clip = clip;
        source.Play();
    }

    public void reloadScene()
    {
        scenechanger.reloadScene();
    }
}

