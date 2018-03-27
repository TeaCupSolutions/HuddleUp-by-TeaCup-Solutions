using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfoController : MonoBehaviour {
    public TMP_Text text,text2;
    public Player player;
	// Use this for initialization
	void Start () {
        text.text = "Player " + player.playerNum;
        text.color = player.colour;
        text2.text = "Player " + player.playerNum;
        text2.color = player.colour;
    }
	
	// Update is called once per frame
	void Update () {
        Camera cam = Camera.current;

        if(cam)
        {
            transform.LookAt(transform.position + cam.transform.rotation * Vector3.forward,
            cam.transform.rotation * Vector3.up);
        }
        
    }
}
