using System;
using UnityEngine;

public class ScoreController : MonoBehaviour {
    public int playerIndex;
    private GameObject[] players;
    public Player playerStats;
    public ReplayController replayController;

    void Start() {
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    public void IncrementScore(int playerMSG, int scoreTypeMSG)
    {
        playerIndex = playerMSG;

        if(!StaticValuesNamespace.StaticValues.IsReplay)
        {
            replayController.AddScoreToReplay(playerIndex, scoreTypeMSG);
        }

        foreach (GameObject player in players)
        {
            playerStats = player.GetComponent<Player>();
            if (playerStats.playerNum == playerIndex)
            {
                playerStats.IncrementBasedOnID(scoreTypeMSG);

            }
        }
    }
}
