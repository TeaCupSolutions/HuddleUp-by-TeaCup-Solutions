using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
	//Declare Variables
	public float radius = 1f;
	public Transform interactionTransform;
	//public GameObject playersObject;
	public GameObject condictionObjectObject;
    public SubTask subtask;
    public bool isOneShotInteraction = false;

    public GameObject sound;
    public AudioManager AM;

    private bool isFinished = false;
    private string condictionObject;
	private float startTimeOfInteraction;
	private bool playerHasStartedInteraction = false;
	private float currentTimeOfGame;
	public float totalTimeForInteraction = 100;
	private float diffInCurrentTimeAndStartTime;
	private float completionPercentage;
	private GameObject[] players;
    private bool[] IsShowingButton = new bool[] { false, false, false, false };
    private GameObject interactingPlayer;

	void Start()
	{
		players = GameObject.FindGameObjectsWithTag("Player");
        
        sound = GameObject.FindGameObjectWithTag("AudioManager");
        AM = sound.GetComponent<AudioManager>();

        if (condictionObjectObject)
        {
            condictionObject = condictionObjectObject.GetComponent<Pickupable>().name;
        }
		//Debug.Log ("Condiction Object is: " + condictionObject);
	}
	
	void Update ()
	{
        if (!isFinished)
        {
            if (playerHasStartedInteraction)
            {
                //Debug.Log ("Check2");
                //Get the percentage completion
                currentTimeOfGame = Time.time * 1000;
                //Debug.Log (currentTimeOfGame);
                diffInCurrentTimeAndStartTime = currentTimeOfGame - startTimeOfInteraction;
                diffInCurrentTimeAndStartTime = diffInCurrentTimeAndStartTime / 100;
                completionPercentage = totalTimeForInteraction * diffInCurrentTimeAndStartTime;
                //Debug.Log (completionPercentage);
                updateSlider(completionPercentage);
            }
            else
            {
                //Check if a player has interacted with the object
                foreach (GameObject player in players)                                                           //For each player in the game...
                {
                    float distance = Vector3.Distance(player.transform.position, interactionTransform.position); //Get the distance betweent the player and the object

                    if (distance <= radius &&
                        player.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>().getInteractionActionState())                                                                  //If the distance is less than the radius and the player has pressed a button...  player.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>().getPickupActionState() returns true if the player has pressed the pick up button
                    {
                        //Debug.Log ("Hello");
                        //Get the object the player is holding
                        string objectBeingHeld = player.GetComponent<Player>().holding;
                        interactingPlayer = player;

                        if (condictionObject != null)
                        {
                            if (objectBeingHeld == condictionObject)
                            {
                                //Debug.Log ("Check1");
                                if (this.tag == "chair")
                                    AM.Play("chairDrop");
                                else if (this.tag == "tDogFoodDropBowl")
                                    AM.Play("eatFood");
                                else if (this.tag == "cooking")
                                    AM.Play("Cooking");
                                else if (this.tag == "tPetDog")
                                    AM.Play("DogPlay");
                                else
                                    AM.Play("drop");



                                startTimeOfInteraction = Time.time * 1000;
                                playerHasStartedInteraction = true;

                                
                            }
                          /*  if (this.tag == "tDogFoodDropBowl")
                                AM.Play("eatFood");
                            else if (this.tag == "tPetDog")
                                AM.Play("DogPlay");
                            else if (this.tag == "chair")
                                AM.Play("chairDrop");
                            else
                                AM.Play("drop");*/
                        }
                        else
                        {
                            if (this.tag == "chair")
                                AM.Play("chairDrop");
                            else if (this.tag == "tDogFoodDropBowl")
                                AM.Play("eatFood");
                            else if (this.tag == "cooking")
                                AM.Play("Cooking");
                            else if (this.tag == "tPetDog")
                                AM.Play("DogPlay");
                            else
                                AM.Play("drop");
                            startTimeOfInteraction = Time.time * 1000;
                            playerHasStartedInteraction = true;
                        }
                    }
                    if (distance <= radius && player.GetComponent<Player>().holding == condictionObject)
                    {
                        bool runTask = true;
                        if (subtask.priorSubTasks.Count != 0)
                        {
                            foreach (SubTask s in subtask.priorSubTasks)
                            {
                                if (!s.isCompleted)
                                {
                                    runTask = false;
                                }
                            }
                        }

                        if (runTask)
                        {
                            player.GetComponent<Player>().InteractionButton.SetActive(true);
                            IsShowingButton[player.GetComponent<Player>().playerNum-1] = true;
                        }
                    }
                    else
                    {
                        if (IsShowingButton[player.GetComponent<Player>().playerNum-1])
                        {
                            player.GetComponent<Player>().InteractionButton.SetActive(false);
                            IsShowingButton[player.GetComponent<Player>().playerNum-1] = false;
                        }
                    }
                }
            }
        }
	}

	public void updateSlider(float percentage)
	{
		//Debug.Log ("Check3");
		Debug.Log ("Percentage: " + percentage);

		if (percentage >= 100)
		{
            if (IsShowingButton[interactingPlayer.GetComponent<Player>().playerNum-1])
            {
                interactingPlayer.GetComponent<Player>().InteractionButton.SetActive(false);
                IsShowingButton[interactingPlayer.GetComponent<Player>().playerNum-1] = false;
            }

            Debug.Log(name + "Check1");
            playerHasStartedInteraction = false;
            
            if (subtask != null) {
                subtask.CompletedIntercation(true,this, condictionObjectObject);
            }
            if (isOneShotInteraction)
            {
                isFinished = true;
                
            }
		}
	}
}