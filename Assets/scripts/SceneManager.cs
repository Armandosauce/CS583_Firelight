using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour {

    public Transform trans;
    public SpriteRenderer render;
    public Rigidbody2D rb;
    public Animator anim;
    public BoxCollider2D col;

    private static SceneManager _instance;

	// Use this for initialization
	public static SceneManager Instance
    {
        get { return _instance; }

    }

    private void Awake()
    {
        if((_instance != null) && (_instance != this))
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        
    }
}
