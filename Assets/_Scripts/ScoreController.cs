using System;
using UnityEngine;

public class ScoreController : MonoBehaviour {
    public Color active;
    public Color notActive;
    public int playerIndex;
    private GameObject[] players;
    public Player playerStats;

    void Start() {
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    public void IncrementScore(int playerMSG, int scoreTypeMSG)
    {
        playerIndex = playerMSG;

        foreach (GameObject player in players)
        {
            playerStats = player.GetComponent<Player>();
            if (playerStats.playerNum == playerIndex)
            {
                playerStats.UIText.color = this.active;
                playerStats.IncrementBasedOnID(scoreTypeMSG);
            }
            else
            {
                playerStats.UIText.color = this.notActive;
            }
        }
    }
}



    /*
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

        //fix update to only do it if changed later
        foreach (GameObject player in players)
        {
            playerStats = player.GetComponent<Player>();
            if (playerStats.playerNum == playerIndex)
            {
                playerStats.UIText.color = this.active;
            }
            else
            {
                playerStats.UIText.color = this.notActive;
            }
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
        }
    }*/
