using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleButtons : MonoBehaviour {

    private Camera cam;
    public Transform target;
    private RectTransform button;

	// Use this for initialization
	void Start () {
        cam = GameObject.FindObjectOfType<Camera>();
        button = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 screenPos = cam.WorldToScreenPoint(target.position);
        screenPos.x += 10f;
        screenPos.y += -13f;
        button.position = screenPos;

        button.sizeDelta = new Vector2(385, 82);
	}
}
