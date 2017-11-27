using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour{
    public int playerNum = 0;
    public int leadership = 0;
    public int communication = 0;
    public int creativity = 0;
    public int destructiveness = 0;
    public Player(int player)
    {
        playerNum = player;
    }
}
