﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    
    private static GameManager _instance;
    

	// Use this for initialization
	public static GameManager Instance
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
