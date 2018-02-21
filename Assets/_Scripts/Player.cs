using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Text UIText;
	public RectTransform HUD_Leadership, HUD_Communication, HUD_Creativity, HUD_BTC;

    //Leadership HUD Variables
    private float HUD_Leadership_Y; //stores the Y Position of the bar
    private float HUD_Leadership_MAX_X, HUD_Leadership_MIN_X;// holds the max and min X positions of the bar
    private int Leadership_Current = 0; //current value of the bar
    private int Leadership_Max; //Max value of the bar

	//Communication HUD variables
	private float HUD_Communication_Y; //stores the Y Position of the bar
	private float HUD_Communication_MAX_X, HUD_Communication_MIN_X;// holds the max and min X positions of the bar
	private int Communication_Current = 0; //current value of the bar
	private int Communication_Max; //Max value of the bar

	//Creativity HUD variables
	private float HUD_Creativity_Y; //stores the Y Position of the bar
	private float HUD_Creativity_MAX_X, HUD_Creativity_MIN_X;// holds the max and min X positions of the bar
	private int Creativity_Current = 0; //current value of the bar
	private int Creativity_Max; //Max value of the bar

	//Behaviour That Challenges HUD variables
	private float HUD_BTC_Y; //stores the Y Position of the bar
	private float HUD_BTC_MAX_X, HUD_BTC_MIN_X;// holds the max and min X positions of the bar
	private int BTC_Current = 0; //current value of the bar
	private int BTC_Max; //Max value of the bar
	public int playerNum;
	public string holding = "";


    void Start()
    {
        //initialize HUD values
		HUD_Leadership_Y = HUD_Leadership.localPosition.y;
		HUD_Leadership_MAX_X = HUD_Leadership.localPosition.x; // Max position is it's starting position
		HUD_Leadership_MIN_X = HUD_Leadership.localPosition.x - HUD_Leadership.rect.width; // Min position is our max position - bar width

		//Communication
		HUD_Communication_Y = HUD_Communication.localPosition.y;
		HUD_Communication_MAX_X = HUD_Communication.localPosition.x; // Max position is it's starting position
		HUD_Communication_MIN_X = HUD_Communication_MAX_X - HUD_Communication.rect.width; // Min position is our max position - bar width

		//Creativity
		HUD_Creativity_Y = HUD_Creativity.localPosition.y;
		HUD_Creativity_MAX_X = HUD_Creativity.localPosition.x; // Max position is it's starting position
		HUD_Creativity_MIN_X = HUD_Creativity.localPosition.x - HUD_Creativity.rect.width; // Min position is our max position - bar width

		//Behaviour That Challenges
		HUD_BTC_Y = HUD_BTC.localPosition.y;
		HUD_BTC_MAX_X = HUD_BTC.localPosition.x; // Max position is it's starting position
		HUD_BTC_MIN_X = HUD_BTC.localPosition.x - HUD_BTC.rect.width; // Min position is our max position - bar width


		Leadership_Max = 15;
		Communication_Max = 15;
		Creativity_Max = 15;
		BTC_Max = 15;


        //set bar to min position
        //HUD_Leadership.position = new Vector3(HUD_Leadership_MIN_X, HUD_Leadership_Y);

    }


    public Player(int player)
    {
        playerNum = player;
    }

    public void IncrementBasedOnID(int identifier)
    {
        if (identifier == 1) {
			this.Communication_Current += 1;
        }
        else if (identifier == 2)
        {
			this.Leadership_Current++;
        }
        else if (identifier == 3)
        {
			this.Creativity_Current += 1;
        }
        else if (identifier == 4)
        {
			this.BTC_Current += 1;
        }
    }

    public void Update()
    {
        //update Leadership bar
        UpdateHUD();
		Debug.Log("Player Speed: ");
    }

    private void UpdateHUD()
    {
        //if the player has reached max points we don't update the bar anymore
		if(this.Leadership_Max >= this.Leadership_Current){

			//Gets the X position the bar should be at given the parameters
			float barXPosition = MapBarPosition(Leadership_Current, 0, Leadership_Max, HUD_Leadership_MIN_X, HUD_Leadership_MAX_X);
			HUD_Leadership.localPosition = new Vector3(barXPosition, HUD_Leadership_Y);

		}

		//communication
		if (this.Communication_Max >= this.Communication_Current) {

			//get the x position of the bar
			float barXPosition = MapBarPosition(Communication_Current, 0, Communication_Max, HUD_Communication_MIN_X, HUD_Communication_MAX_X);
			HUD_Communication.localPosition = new Vector3 (barXPosition, HUD_Communication_Y);
		}


		//Creativity
		if (this.Creativity_Max >= this.Creativity_Current) {

			//get the x position of the bar
			float barXPosition = MapBarPosition(Creativity_Current, 0, Creativity_Max, HUD_Creativity_MIN_X, HUD_Creativity_MAX_X);
			HUD_Creativity.localPosition = new Vector3 (barXPosition, HUD_Creativity_Y);
		}


		//Behaviour That Challenges
		if (this.BTC_Max >= this.BTC_Current) {

			//get the x position of the bar
			float barXPosition = MapBarPosition(BTC_Current, 0, BTC_Max, HUD_BTC_MIN_X, HUD_BTC_MAX_X);
			HUD_BTC.localPosition = new Vector3 (barXPosition, HUD_BTC_Y);
		}


    }

    //Takes in Bar: Current value, int min (0), int max(10)
    private float MapBarPosition(int points, int minPoints, int maxPoints, float xMin, float xMax)
    {
        return (points - minPoints) * (xMax - xMin) / (maxPoints - minPoints) + xMin;
    }

}
