using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Text UIText;
    public int playerNum = 0;
    public int leadership = 0;
    public int communication = 0;
    public int creativity = 0;
    public int destructiveness = 0;
	public string holding;

    public Player(int player)
    {
        playerNum = player;
    }

    public void IncrementBasedOnID(int identifier)
    {
        if (identifier == 1) {
            this.communication += 1;
        }
        else if (identifier == 2)
        {
            this.leadership += 1;
        }
        else if (identifier == 3)
        {
            this.creativity += 1;
        }
        else if (identifier == 4)
        {
            this.destructiveness += 1;
        }
    }

    public void Update()
    {
        UIText.text = this.toString();
    }

    public string toString()
    {
        string output = ("Player " + this.playerNum + "\n ");
        output += ("Leadership: " + this.leadership + "\n ");
        output += ("Comminication: " + this.communication + "\n ");
        output += ("Creativity: " + this.creativity + "\n ");
        output += ("BTC: " + this.destructiveness + "\n ");

        return output;
    }
}
