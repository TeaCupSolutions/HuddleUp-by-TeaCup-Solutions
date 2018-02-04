using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
	//Declare Variables
	public float radius = 3f;
	public Transform interactionTransform;
	//public GameObject playersObject;
	public GameObject condictionObjectObject;
    public SubTask subtask;
	private string condictionObject;
	private float startTimeOfInteraction;
	private bool playerHasStartedInteraction = false;
	private float currentTimeOfGame;
	private float totalTimeForInteraction = 10;
	private float diffInCurrentTimeAndStartTime;
	private float completionPercentage;
	private GameObject[] players;

	void Start()
	{
		players = GameObject.FindGameObjectsWithTag("Player");
		condictionObject = condictionObjectObject.GetComponent<Pickupable>().name;
		//Debug.Log ("Condiction Object is: " + condictionObject);
	}
	
	void Update ()
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
					player.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>().
					getPickupActionState())                                                                  //If the distance is less than the radius and the player has pressed a button...  player.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>().getPickupActionState() returns true if the player has pressed the pick up button
				{
					//Debug.Log ("Hello");
					//Get the object the player is holding
					string objectBeingHeld = player.GetComponent<Player>().holding;

					if (objectBeingHeld == condictionObject)
					{
						//Debug.Log ("Check1");
						startTimeOfInteraction = Time.time * 1000;
						playerHasStartedInteraction = true;
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
			Debug.Log ("Check1");
			Destroy (gameObject);
            if (subtask != null) {
                subtask.IsCompleted = true;
            }
		}
	}
}