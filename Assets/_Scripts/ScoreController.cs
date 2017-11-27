using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {
    public Text scoreText;
    public int playerIndex;
    public GameObject[] players;
    public Player playerStats;
    // Use this for initialization
    void Start () {
        players = GameObject.FindGameObjectsWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            playerIndex = 1;
            Debug.Log("up key was pressed player 1 set");
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            playerIndex = 2;
            Debug.Log("down key was pressed player 2 set");
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            playerIndex = 3;
            Debug.Log("left key was pressed player 3 set");
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            playerIndex = 4;
            Debug.Log("right key was pressed player 4 set");
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            foreach (GameObject player in players) {
                playerStats = player.GetComponent<Player>();
                if (playerStats.playerNum == playerIndex)
                {
                    playerStats.communication += 1;
                    Debug.Log("player " + playerIndex + "Communication is " + playerStats.leadership);
                }
            }
            Debug.Log("w key was pressed");
        }
        
        else if (Input.GetKeyDown(KeyCode.D))
        {
            foreach (GameObject player in players) {
                playerStats = player.GetComponent<Player>();
                if (playerStats.playerNum == playerIndex)
                {
                    playerStats.destructiveness += 1;
                    Debug.Log("player " + playerIndex + "Destructiveness is " + playerStats.leadership);
                }
            }
            Debug.Log("d key was pressed");
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            foreach (GameObject player in players)
            {
                playerStats = player.GetComponent<Player>();
                if (playerStats.playerNum == playerIndex)
                {
                    playerStats.leadership += 1;
                    Debug.Log("player " + playerIndex + "Leadership is " + playerStats.leadership);
                }
            }
            Debug.Log("s key was pressed");
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            foreach (GameObject player in players)
            {
                playerStats = player.GetComponent<Player>();
                if (playerStats.playerNum == playerIndex)
                {
                    playerStats.creativity += 1;
                    Debug.Log("player " + playerIndex + "Creativity is " + playerStats.leadership);
                }
            }
            Debug.Log("a key was pressed");
        }
        scoreText.text = "";
        foreach (GameObject player in players)
        {
            playerStats = player.GetComponent<Player>();
            scoreText.text += ("Player " + playerStats.playerNum + "\n ");
            scoreText.text += ("Leadership: " + playerStats.leadership + "\n ");
            scoreText.text += ("Comminication: " + playerStats.communication + "\n ");
            scoreText.text += ("Creativity: " + playerStats.creativity + "\n ");
            scoreText.text += ("Destructiveness: " + playerStats.destructiveness + "\n ");
        }
        scoreText.text += ("Currenly Selected: " + playerIndex + "\n ");
    }
}
