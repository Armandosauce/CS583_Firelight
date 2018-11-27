using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour {
    
    [SerializeField]
    private Sprite opened;
    private GameObject maplight;
    private GameObject playerLight;

    private SpriteRenderer render;
    private bool isOpen;
    private Vector3 itempos;
    private GameObject[] itemdrop;
    private int rand;
    private SceneChanger scenechanger;

    private void Start()
    {
        itemdrop = GameObject.FindGameObjectsWithTag("Consumable");
        rand = Random.Range(0, 2);
        render = GetComponent<SpriteRenderer>();
        scenechanger = gameObject.AddComponent<SceneChanger>() as SceneChanger;
        maplight = GameObject.FindGameObjectWithTag("MapLight");
        playerLight = GameObject.FindGameObjectWithTag("Light");
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && !isOpen)
        {
            if (this.gameObject.tag == "Goal")
            {
                scenechanger.loadSceneAfterDelay(3f, SceneChanger.currentSceneIndex() + 1);
                isOpen = true;
                render.sprite = opened;
                maplight.GetComponent<Light>().enabled = true;
                playerLight.GetComponent<Light>().enabled = false;
            }
            else
            {
                isOpen = true;
                render.sprite = opened;
                itempos = new Vector3(transform.position.x + Random.Range(0, 1f), transform.position.y + Random.Range(0, 1f) + 0.5f, transform.position.z);
                GameObject item = Instantiate(itemdrop[rand], itempos, Quaternion.identity);
                item.transform.position = Vector2.MoveTowards(item.transform.position, this.transform.position, Time.deltaTime * -3);
            }
        }
    }
    
}
